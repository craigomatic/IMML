using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.IO;
using Imml.IO;
using Imml.Scene.Container;
using Imml.Scene.Controls;
using System.Xml.Linq;
using System.Xml;
using Imml.Numerics;
using Imml.Drawing;
using System.Diagnostics;
using Imml.Scene;
using Moq;

namespace Imml.Test
{
    /// <summary>
    /// An ImmlSerialiser is capable of:
    /// - Reading from a stream into an object model
    /// - Reading from a file on disk into an object model
    /// - Writing from an object model into a string
    /// - Writing from an object model into a stream
    /// </summary>
    public class ImmlSerialiserTests
    {
        [Fact]
        public void Serialiser_Can_Read_From_A_Stream()
        {
            var immlString = "<IMML Author=\"craigomatic\" Camera=\"Camera\" xmlns=\"http://schemas.vastpark.com/2007/imml/\"><Camera Name=\"Camera\" Position=\"0,0,-5\" /><Primitive Type=\"Box\" Size=\"1,1,1\" /></IMML>";
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(immlString));

            var immlSerialiser = new ImmlSerialiser();
            var document = immlSerialiser.Read<ImmlDocument>(stream);

            Assert.NotNull(document);
            Assert.Equal(2, document.Elements.Count);
        }

        [Fact]
        public void Serialiser_Can_Read_Strings_From_Disk()
        {
            var tmpFile = System.IO.Path.GetTempFileName();
            var immlString = "<IMML Author=\"craigomatic\" Camera=\"Camera\" xmlns=\"http://schemas.vastpark.com/2007/imml/\"><Camera Name=\"Camera\" Position=\"0,0,-5\" /><Primitive Type=\"Box\" Size=\"1,1,1\" /></IMML>";

            try
            {
                System.IO.File.WriteAllText(tmpFile, immlString);

                var immlSerialiser = new ImmlSerialiser();
                var document = immlSerialiser.Read<ImmlDocument>(tmpFile);

                Assert.NotNull(document);
                Assert.Equal(2, document.Elements.Count);
            }
            finally
            {
                System.IO.File.Delete(tmpFile);
            }
        }

        [Fact(Skip="Inconclusive test, needs to be implemented correctly")]
        public void Serialiser_Creates_Object_Instances_For_All_Elements_Contained_Within_A_Document()
        {
            var bytes = EmbeddedResourceHelper.GetBytes("Imml.Test.Samples.Sample1.gz", System.Reflection.Assembly.GetExecutingAssembly());

            //unzip to get to the imml sample
            var decompressed = new MemoryStream(Ionic.Zlib.GZipStream.UncompressBuffer(bytes));
            var immlSerialiser = new ImmlSerialiser();
            var document = immlSerialiser.Read<ImmlDocument>(decompressed);

            Assert.NotEmpty(document.Elements);

            //TODO: improve the assertions to do as the method name suggests
        }

        [Fact]
        public void Reading_Invalid_Imml_Results_In_A_MarkupException()
        {
            var immlString = "<IMML Author=\"craigomatic\" Camera=\"Camera\" xmlns=\"http://schemas.vastpark.com/2007/imml/\"><THis is invalid IMML></IMML>";
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(immlString));

            var immlSerialiser = new ImmlSerialiser();

            try
            {
                var document = immlSerialiser.Read<ImmlDocument>(stream);
                Assert.True(false); //fail
            }
            catch (MarkupException)
            {
                Assert.True(true); //pass
            }
        }

        [Fact]
        public void Serialiser_Can_Write_An_Object_To_A_String()
        {
            var primitive = new Primitive();
            primitive.Name = Guid.NewGuid().ToString();

            var immlSerialiser = new ImmlSerialiser();
            var imml = immlSerialiser.Write(primitive);

            Assert.True(imml.Contains(string.Format("Name=\"{0}\"", primitive.Name)));
        }

        [Fact]
        public void Serialiser_Can_Write_An_Object_To_A_Stream()
        {
            var primitive = new Primitive();
            primitive.Name = Guid.NewGuid().ToString();

            var outputStream = new MemoryStream();
            var immlSerialiser = new ImmlSerialiser();
            immlSerialiser.Write(primitive, outputStream);

            Assert.True(outputStream.Length > 0);

            var immlString = Encoding.ASCII.GetString(outputStream.ToArray());
            Assert.True(immlString.Contains(string.Format("Name=\"{0}\"", primitive.Name)));
        }

        [Fact]
        public void Serialiser_Can_Write_An_ImmlDocument_With_Attributes_To_A_String()
        {
            var immlDocument = new ImmlDocument();
            immlDocument.Author = Guid.NewGuid().ToString();
            immlDocument.AnimationSpeed = (float)new Random().NextDouble();
            immlDocument.SoundSpeed = (float)new Random().NextDouble();
            immlDocument.PhysicsSpeed = (float)new Random().NextDouble();
            immlDocument.Background = Guid.NewGuid().ToString();
            immlDocument.Behaviours.Add(Guid.NewGuid().ToString());
            immlDocument.Behaviours.Add(Guid.NewGuid().ToString());
            immlDocument.Camera = Guid.NewGuid().ToString();
            immlDocument.Context = Guid.NewGuid().ToString();
            immlDocument.Description = Guid.NewGuid().ToString();
            immlDocument.GlobalIllumination = Color3.Red;
            immlDocument.Gravity = Vector3.UnitZ;
            immlDocument.HostUri = new Uri("http://www.vastpark.com");
            immlDocument.Name = Guid.NewGuid().ToString();
            immlDocument.Tags.Add("tag1");
            immlDocument.Tags.Add("tag2");

            var immlSerialiser = new ImmlSerialiser();
            var immlString = immlSerialiser.Write(immlDocument);

            Assert.True(immlString.Contains(string.Format("{0}=\"{1}\"", "Author", immlDocument.Author)));
            Assert.True(immlString.Contains(string.Format("{0}=\"{1}\"", "AnimationSpeed", immlDocument.AnimationSpeed)));
            Assert.True(immlString.Contains(string.Format("{0}=\"{1}\"", "SoundSpeed", immlDocument.SoundSpeed)));
            Assert.True(immlString.Contains(string.Format("{0}=\"{1}\"", "PhysicsSpeed", immlDocument.PhysicsSpeed)));
            Assert.True(immlString.Contains(string.Format("{0}=\"{1}\"", "Background", immlDocument.Background)));
            Assert.True(immlString.Contains(string.Format("{0}=\"{1}\"", "Camera", immlDocument.Camera)));
            Assert.True(immlString.Contains(string.Format("{0}=\"{1}\"", "Description", immlDocument.Description)));
            Assert.True(immlString.Contains(string.Format("{0}=\"{1}\"", "GlobalIllumination", immlDocument.GlobalIllumination)));
            Assert.True(immlString.Contains(string.Format("{0}=\"{1}\"", "HostUri", immlDocument.HostUri)));
            Assert.True(immlString.Contains(string.Format("{0}=\"{1}\"", "Name", immlDocument.Name)));
            Assert.True(immlString.Contains(string.Format("{0}=\"{1}\"", "Behaviours", TypeConvert.Parse(immlDocument.Behaviours))));
            Assert.True(immlString.Contains(string.Format("{0}=\"{1}\"", "Tags", TypeConvert.Parse(immlDocument.Tags))));
        }

        [Fact]
        public void Serialiser_Can_Write_A_Simple_Nested_Hierarchy_To_A_String()
        {
            var immlDocument = new ImmlDocument();
            immlDocument.Author = Guid.NewGuid().ToString();

            var model = new Model();
            model.Name = Guid.NewGuid().ToString();
            model.Source = string.Format("http://{0}", Guid.NewGuid().ToString());

            var primitive = new Primitive();
            primitive.Name = Guid.NewGuid().ToString();

            var camera = new Camera();
            camera.Name = Guid.NewGuid().ToString();

            primitive.Add(camera);
            model.Add(primitive);
            immlDocument.Add(model);

            var immlSerialiser = new ImmlSerialiser();
            var immlString = immlSerialiser.Write(immlDocument);

            var textReader = new StringReader(immlString);
            var xDoc = XDocument.Load(textReader);

            //verify the model is in the correct place
            var xElementModel = xDoc.Descendants(XName.Get("Model", immlSerialiser.Namespace)).Where(e => e.Attribute(XName.Get("Name")).Value == model.Name).FirstOrDefault();
            Assert.NotNull(xElementModel);
            Assert.Equal(immlDocument.Name, xElementModel.Parent.Attribute(XName.Get("Name")).Value);

            //verify the primitive is in the correct place
            var xElementPrimitive = xDoc.Descendants(XName.Get("Primitive", immlSerialiser.Namespace)).Where(e => e.Attribute(XName.Get("Name")).Value == primitive.Name).FirstOrDefault();
            Assert.NotNull(xElementPrimitive);
            Assert.Equal(model.Name, xElementPrimitive.Parent.Attribute(XName.Get("Name")).Value);

            //verify the camera is in the correct place
            var xElementCamera = xDoc.Descendants(XName.Get("Camera", immlSerialiser.Namespace)).Where(e => e.Attribute(XName.Get("Name")).Value == camera.Name).FirstOrDefault();
            Assert.NotNull(xElementCamera);
            Assert.Equal(primitive.Name, xElementCamera.Parent.Attribute(XName.Get("Name")).Value);
        }

        [Fact]
        public void Serialiser_Can_Write_A_File_With_A_Flat_Hierarchy_Of_Ten_Thousand_Elements_In_Under_Five_Seconds()
        {
            var tmpFile = Path.GetTempFileName();

            try
            {
                //10k elements produces ~2mb of IMML
                var elementsToGenerate = 10000;
                var elementTypeArray = new string[] { "Primitive", "Model", "Camera" };
                var random = new Random();
                var immlContext = new ImmlDocument();

                for (int i = 0; i < elementsToGenerate; i++)
                {
                    var index = random.Next(0, elementTypeArray.Length);
                    var typeToCreate = elementTypeArray[index];

                    var element = ElementFactory.Create(typeToCreate);
                    immlContext.Add(element);
                }

                using (var fs = new FileStream(tmpFile, FileMode.Create))
                {
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();

                    new ImmlSerialiser().Write(immlContext, fs);
                    stopWatch.Stop();
                    
                    Console.WriteLine(fs.Length);
                    Assert.NotInRange(stopWatch.Elapsed.TotalSeconds, 5, int.MaxValue);
                }
            }
            finally
            {
                System.IO.File.Delete(tmpFile);
            }
        }
    
        [Fact]
        public void Serialiser_Writes_Material_Values_Correctly_To_String()
        {
            var model = new Model();
            
            var materialGroup = new MaterialGroup();
            materialGroup.Id = -1;

            var material = new Material(Color3.Red, Color3.Green, Color3.Blue, Color3.Black, 10, 1);
            materialGroup.Add(material);

            model.Add(materialGroup);

            var immlSerialiser = new ImmlSerialiser();
            var immlString = immlSerialiser.Write(model);

            var textReader = new StringReader(immlString);
            var xDoc = XDocument.Load(textReader);

            var xElementMaterial = xDoc.Descendants(XName.Get("Material", immlSerialiser.Namespace)).FirstOrDefault();
            Assert.NotNull(xElementMaterial);

            Assert.Equal(material.Ambient.ToString(), xElementMaterial.Attribute(XName.Get("Ambient")).Value);
            Assert.Equal(material.Diffuse.ToString(), xElementMaterial.Attribute(XName.Get("Diffuse")).Value);
            Assert.Equal(material.Emissive.ToString(), xElementMaterial.Attribute(XName.Get("Emissive")).Value);
            Assert.Equal(material.Specular.ToString(), xElementMaterial.Attribute(XName.Get("Specular")).Value);
            Assert.Equal(material.Power.ToString(), xElementMaterial.Attribute(XName.Get("Power")).Value);
            Assert.Equal(material.Opacity.ToString(), xElementMaterial.Attribute(XName.Get("Opacity")).Value);
        }

        [Fact]
        public void Serialiser_Writes_Vector3_Values_Correctly_To_String()
        {
            var model = new Model();
            model.Position = new Vector3(float.MinValue, float.Epsilon, float.MaxValue);
            model.Rotation = new Vector3(1, 2, 3);

            var immlSerialiser = new ImmlSerialiser();
            var immlString = immlSerialiser.Write(model);

            var textReader = new StringReader(immlString);
            var xDoc = XDocument.Load(textReader);

            var xElementModel = xDoc.Descendants(XName.Get("Model", immlSerialiser.Namespace)).FirstOrDefault();
            Assert.NotNull(xElementModel);

            Assert.Equal(model.Position.ToString(), xElementModel.Attribute(XName.Get("Position")).Value);
            Assert.Equal(model.Rotation.ToString(), xElementModel.Attribute(XName.Get("Rotation")).Value);
        }
    
        [Fact]
        public void Serialiser_Reports_An_Error_When_A_Minor_Validation_Problem_Is_Encountered()
        {
            //the camera position attribute is invalid
            var immlString = "<IMML Author=\"craigomatic\" Camera=\"Camera\" xmlns=\"http://schemas.vastpark.com/2007/imml/\"><Camera Name=\"Camera\" Position=\"5\" /><Primitive Type=\"Box\" Size=\"1,1,1\" /></IMML>"; ;

            var immlSerialiser = new ImmlSerialiser();
            immlSerialiser.Read<ImmlDocument>(new MemoryStream(Encoding.UTF8.GetBytes(immlString)));

            Assert.NotEmpty(immlSerialiser.Errors);
            Assert.Equal(1, immlSerialiser.Errors.Count);
        }

        [Fact]
        public void Serialiser_Does_Not_Report_Errors_When_The_IMML_Is_Valid()
        {
            var immlString = "<IMML Author=\"craigomatic\" Camera=\"Camera\" xmlns=\"http://schemas.vastpark.com/2007/imml/\"><Camera Name=\"Camera\" Position=\"5,5,5\" /><Primitive Type=\"Box\" Size=\"1,1,1\" /></IMML>"; ;

            var immlSerialiser = new ImmlSerialiser();
            immlSerialiser.Read<ImmlDocument>(new MemoryStream(Encoding.UTF8.GetBytes(immlString)));

            Assert.Empty(immlSerialiser.Errors);
        }

        [Fact]
        public void Serialiser_Correctly_Writes_Script_Elements()
        {
            var script = new Script();
            script.Value = "function main(obj, args)\r\nend";

            var immlSerialiser = new ImmlSerialiser();
            var immlString = immlSerialiser.Write(script);

            var textReader = new StringReader(immlString);
            var xDoc = XDocument.Load(textReader);

            var xElementScript = xDoc.Descendants(XName.Get("Script", immlSerialiser.Namespace)).FirstOrDefault();
            Assert.NotNull(xElementScript);

            Assert.Contains("function main(obj, args)", xElementScript.Value);
        }

        [Fact]
        public void Serialiser_Correctly_Reads_IMML_Containing_Script_Elements()
        {
            var immlString = "<Script Language=\"Lua\" Name=\"Script\" Source=\"\" xmlns=\"http://schemas.vastpark.com/2007/imml/\">function main(obj, args)\r\nend</Script>";
            var immlSerialiser = new ImmlSerialiser();
            var script = immlSerialiser.Read<Script>(new MemoryStream(Encoding.UTF8.GetBytes(immlString)));

            Assert.Contains("function main(obj, args)", script.Value);
        }

        [Fact]
        public void Serialiser_Does_Not_Write_Optional_Attributes_That_Do_Not_Have_A_Value()
        {
            var immlDocument = new ImmlDocument();
            var immlSerialiser = new ImmlSerialiser();
            var immlString = immlSerialiser.Write(immlDocument);

            //test for some optional values
            Assert.DoesNotContain("Author", immlString);
            Assert.DoesNotContain("Description", immlString);
        }

        [Fact]
        public void Serialiser_Does_Not_Write_Attributes_That_Are_Set_To_Their_Default_Values()
        {
            var immlDocument = new ImmlDocument();
            var immlSerialiser = new ImmlSerialiser();
            var immlString = immlSerialiser.Write(immlDocument);

            //test for some optional values
            Assert.DoesNotContain("Gravity=\"0,-9.8,0\"", immlString);
            Assert.DoesNotContain("GlobalIllumination=\"#4c4c4c\"", immlString);
            Assert.DoesNotContain("AnimationSpeed=\"1\"", immlString);
            Assert.DoesNotContain("PhysicsSpeed=\"1\"", immlString);
            Assert.DoesNotContain("SoundSpeed=\"1\"", immlString);
        }

        [Fact]
        public void Using_A_Sort_Comparer_Alters_The_Order_Of_The_Attrbutes_Written_By_The_Serialiser()
        {
            var immlSerialiser = new ImmlSerialiser();
            var primitive = new Primitive();
            primitive.Behaviours.Add(Guid.NewGuid().ToString());
            primitive.CastShadows = true;
            primitive.Complexity = PrimitiveComplexity.VeryHigh;
            primitive.Name = Guid.NewGuid().ToString();
            primitive.Position = new Vector3(1, 2, 3);
            primitive.Rotation = new Vector3(4, 5, 6);
            primitive.Size = new Vector3(7, 8, 9);

            var attribSortComparer = new Mock<IComparer<string>>();
            attribSortComparer.Setup(c => c.Compare(It.IsAny<string>(), It.IsAny<string>())).Returns(new Func<string, string, int>(_NameFirstAttributeComparer));

            var immlString = immlSerialiser.Write(primitive, attribSortComparer.Object);

            var textReader = new StringReader(immlString);
            var xDoc = XDocument.Load(textReader);

            var xElementPrimitive = xDoc.Descendants(XName.Get("Primitive", immlSerialiser.Namespace)).FirstOrDefault();
            Assert.NotNull(xElementPrimitive);

            Assert.Equal("Name", xElementPrimitive.Attributes().First().Name.LocalName);
        }

        private int _NameFirstAttributeComparer(string x, string y)
        {
            if (x == y)
            {
                return 0;
            }

            if (x == "Name")
            {
                return -1;
            }

            if (y == "Name")
            {
                return 1;
            }

            return x.CompareTo(y);
        }


    }
}
