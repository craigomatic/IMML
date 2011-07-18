using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

#if !SILVERLIGHT
using System.Xml.Linq;
#endif

using System.Xml.Schema;
using System.Xml;
using Imml.Scene.Container;
using System.Reflection;
using Imml.Scene.Controls;
using System.Xml.Linq;

namespace Imml.IO
{
    /// <summary>
    /// Provides support for 
    /// </summary>
    public class ImmlSerialiser : IImmlSerialiser
    {
        /// <summary>
        /// Gets the namespace.
        /// </summary>
        public string Namespace { get; private set; }

        public IList<MarkupException> Errors { get; private set; }

        private XmlReaderSettings _ReaderSettings;

#if !SILVERLIGHT        
        private XmlSchemaValidator _XmlSchemaValidator;
#endif

        public ImmlSerialiser()
        {
            this.Errors = new List<MarkupException>();

#if !SILVERLIGHT
            //load the schema from the embedded resource
            var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            var schemaStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("{0}.imml.xsd", assemblyName));
            var schema = XmlSchema.Read(schemaStream, _ReaderSettings_ValidationEventHandler);
            
            //setup the validator (for writing)
            var schemaSet = new XmlSchemaSet();
            schemaSet.Add(schema);

            _XmlSchemaValidator = new XmlSchemaValidator(schemaSet.NameTable, schemaSet, new XmlNamespaceManager(schemaSet.NameTable), XmlSchemaValidationFlags.None);
            _XmlSchemaValidator.Initialize();
            
            this.Namespace = schema.TargetNamespace;
#endif

            //setup the reader
            _ReaderSettings = new System.Xml.XmlReaderSettings();

#if !SILVERLIGHT
            _ReaderSettings.ValidationEventHandler += new ValidationEventHandler(_ReaderSettings_ValidationEventHandler);
            _ReaderSettings.Schemas.Add(schema);
            _ReaderSettings.ValidationType = ValidationType.Schema;
            _ReaderSettings.NameTable = schemaSet.NameTable;         
#endif
        }

#if !SILVERLIGHT
        void _ReaderSettings_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            this.Errors.Add(new MarkupException(e.Message, e.Exception.LineNumber, e.Exception.LinePosition));
        }
#endif

        public T Read<T>(string filePath) where T : IImmlElement
        {
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                return this.Read<T>(fs); 
            }
        }

        public T Read<T>(Stream stream) where T : IImmlElement
        {
            this.Errors.Clear();

            var reader = System.Xml.XmlReader.Create(stream, _ReaderSettings);
            reader.MoveToContent();

            XDocument xNode = null;

            try
            {
                xNode = XDocument.Load(reader, LoadOptions.SetBaseUri);
            }
            catch(XmlException e)
            {
                throw new MarkupException(e.Message, e.LineNumber, e.LinePosition);
            }

            //create an instance of T as this is what the IMML should be read into
            var immlContext = Activator.CreateInstance<T>();

            //if the first node matches the type of T, we are writing directly to it; if not, we are nesting within it (usually T will be an ImmlDocument in this case)            
            if (xNode.Root.Name.LocalName == _GetImmlName(immlContext))
            {
                _ReadNodes(xNode.Root, immlContext);
            }

            return immlContext;
        }

        public string Write<T>(T element) where T : IImmlElement
        {
            var xNodeParent = _WriteImml<T>(element);
            return xNodeParent.ToString(SaveOptions.None);
        }

        public void Write<T>(T element, Stream outputStream) where T : IImmlElement
        {
            var xNodeParent = _WriteImml(element);
            
            //TODO: Optimise this so that it progressively writes the XML to the stream instead of one big chunk at the end
            using (var xmlWriter = XmlWriter.Create(outputStream, new XmlWriterSettings { Indent = true }))
            {
                xNodeParent.WriteTo(xmlWriter);
            }
        }


        #region Writer methods

        private XNode _WriteImml<T>(T element) where T : IImmlElement
        {

#if SILVERLIGHT
            throw new NotImplementedException("Writing IMML is not supported under Silverlight");
#endif

#if !SILVERLIGHT

            //validate the IMML element to put the validator in the correct position
            var isImmlContext = element is Imml.ComponentModel.IImmlContext;

            if (!isImmlContext)
            {
                _XmlSchemaValidator.ValidateElement("IMML", this.Namespace, null);
                _XmlSchemaValidator.GetExpectedAttributes();
                _XmlSchemaValidator.GetUnspecifiedDefaultAttributes(new System.Collections.ArrayList());
                _XmlSchemaValidator.ValidateEndOfAttributes(null);
            }

            var documentXNode = _WriteElement(element);

            if (!isImmlContext)
            {
                _XmlSchemaValidator.ValidateEndElement(null);
            }

            return documentXNode;
#endif
        }

#if !SILVERLIGHT
        private XElement _WriteElement(IImmlElement element)
        {            
            var elementType = _GetImmlName(element);
            var xNode = new XElement(XName.Get(elementType, this.Namespace));

            _XmlSchemaValidator.ValidateElement(elementType, this.Namespace, null);
            _WriteAttributes(element, xNode);
            
            //test if this element has children that need to be written also
            if(element.HasChildren)
            {
                foreach (var childElement in element.Elements)
	            {
                    var xChildNode = _WriteElement(childElement);
                    xNode.Add(xChildNode);
	            }
            }

            _XmlSchemaValidator.ValidateEndElement(null);

            return xNode;
        }

        private void _WriteAttributes(IImmlElement immlElement, XElement xElement)
        {            
            var validAttributes = _XmlSchemaValidator.GetExpectedAttributes();
            var type = immlElement.GetType();

            foreach (var attribute in validAttributes)
            {
                try
                {
                    var pInfo = type.GetProperty(attribute.Name);
                    var value = pInfo.GetValue(immlElement, null);
                    var valueString = TypeConvert.Parse(value);

                    var xAttribute = new XAttribute(attribute.Name, valueString);
                    xElement.Add(xAttribute);

                    _XmlSchemaValidator.ValidateAttribute(attribute.Name, string.Empty, valueString, null);
                }
                catch (XmlSchemaValidationException e)
                {
                    this.Errors.Add(new MarkupException(e.Message, e.LineNumber, e.LinePosition));

                    //schema valid property is probably missing
                    System.Diagnostics.Debug.Fail(string.Format("Schema contains an attribute '{0}' that is not represented in the object model for '{1}' elements.", attribute.Name, xElement.Name));
                }
            }

            _XmlSchemaValidator.ValidateEndOfAttributes(null);
        }
#endif
        #endregion

        #region Reader methods

        private void _ReadNodes(XNode node, IImmlElement parentElement)
        {
            //clear the parent of all child nodes
            parentElement.Clear();

            //process the attributes for this node
            _ReadAttributes(node, parentElement);

            if (!(node is XElement))
            {
                return;
            }

            foreach (var child in (node as XElement).Nodes())
            {
                //process the new potential parent element
                switch (child.NodeType)
                {
                    case XmlNodeType.Element:
                        {
                            //create document object and populate its properties
                            var xElement = (child as XElement);
                            var childElement = ElementFactory.Create(xElement.Name.LocalName);

                            if (childElement == null)
                                throw new MarkupException("Element processing failure at element: " + (child as XElement).Name.LocalName); //should never get here or it means there are schema issues

                            if (xElement.HasAttributes)
                            {
                                _ReadAttributes(child, childElement);
                            }

                            //look for children before attaching to parent element, this has the effect of doing something like the following:                            
                            //materialGroup.Add(material)
                            //then materialGroup.Add(shader)
                            //then model.add(materialGroup)
                            //then document.add(model)

                            if (xElement.HasElements)
                            {
                                _ReadNodes(child, childElement);
                            }

                            parentElement.Add(childElement);

                            break;
                        }
                    case XmlNodeType.Text:
                        {
                            XText textNode = child as XText;

                            if (parentElement is Script)
                            {
                                var script = (parentElement as Script);
                                script.Value = textNode.Value.Replace("\n", "\r\n").TrimStart('\n').TrimEnd('\n');

                                //replace the xml reserved characters for < and >
                                script.Value = script.Value.Replace("&lt;", "<");
                                script.Value = script.Value.Replace("&gt;", "<");
                            }
                            else if (parentElement is Text)
                            {
                                (parentElement as Text).Value = textNode.Value.Replace("\n", "\r\n").TrimStart('\n').TrimEnd('\n');
                            }

                            break;
                        }
                }
            }
        }
        
        private void _ReadAttributes(XNode node, IImmlElement element)
        {
            if (!(node is XElement))
            {
                return;
            }

            foreach (var attribute in (node as XElement).Attributes())
            {
                if (!attribute.Name.LocalName.Equals("xmlns"))
                {
                    try
                    {
                        var attributeName = attribute.Name.LocalName;
                        var pInfo = element.GetType().GetProperty(attributeName);

                        if (pInfo == null)
                        {
                            this.Errors.Add(new MarkupException("Attribute with name " + attributeName + " is invalid in this context."));
                            continue;
                        }

                        pInfo.SetValue(element, TypeConvert.Parse(attribute.Value, pInfo.PropertyType), null);
                    }
                    catch (TypeLoadException)
                    {

                    }
                    catch (Exception)
                    {

                    }
                }
            }

        }

        #endregion

        private string _GetImmlName(IImmlElement element)
        {
            if (element is ImmlDocument)
            {
                return "IMML";
            }
            else if (element is ImmlWidget)
            {
                return "Widget";
            }

            return element.GetType().Name;
        }
    }
}
