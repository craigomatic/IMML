using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Imml.ComponentModel;
using Xunit;
using Imml.Numerics;

namespace Imml.Test
{
    /// <summary>
    /// IMML is represented as a document hierarchy that is consistent with a traditionl scene graph 
    /// - Each element's position, rotation and scale are relative to their parent
    /// - Size is always represented in world units
    /// - The coordinate system follows the left-handed approach of x increasing to the right, y increasing upwards and z increasing into the screen
    /// </summary>
    public class SceneGraphTests
    {
        [Fact]
        public void WorldPosition_And_Position_Are_Identical_When_A_Positional_Element_Is_Not_Nested()
        {
            var element = new Mock<PositionalElement> { CallBase = true };
            element.Object.Position = new Numerics.Vector3(10, 11, 12);

            Assert.Equal(element.Object.Position, element.Object.WorldPosition);
        }

        [Fact]
        public void Nesting_A_Positional_Element_Inside_Another_Results_In_A_Change_To_Its_WorldPosition()
        {
            var element = new Mock<PositionalElement> { CallBase = true };
            element.Object.Position = new Vector3(10, 11, 12);

            var elementParent = new Mock<PositionalElement> { CallBase = true };
            elementParent.Object.Position = new Vector3(1, 2, 3);
            elementParent.Object.Add(element.Object);

            Assert.NotEqual(element.Object.Position, element.Object.WorldPosition);
            Assert.Equal(new Vector3(11, 13, 15), element.Object.WorldPosition);
        }

        [Fact]
        public void Nesting_A_Cubic_Element_Inside_Another_Results_In_A_Change_To_Its_WorldPosition()
        {
            var element = new Mock<CubicElement> { CallBase = true };
            element.Object.Position = new Vector3(10, 11, 12);

            var parent = new Mock<CubicElement> { CallBase = true };
            parent.Object.Position = new Vector3(1, 2, 3);
            parent.Object.Add(element.Object);

            Assert.NotEqual(element.Object.Position, element.Object.WorldPosition);
            Assert.Equal(new Vector3(11, 13, 15), element.Object.WorldPosition);
        }

        [Fact]
        public void Nesting_A_Cubic_Element_Inside_Another_Does_Not_Alter_Its_Size()
        {
            var element = new Mock<CubicElement> { CallBase = true };
            element.Object.Size = new Vector3(100, 200, 300);

            var parent = new Mock<CubicElement> { CallBase = true };
            parent.Object.Size = new Vector3(1000, 2000, 3000);
            parent.Object.Add(element.Object);

            Assert.Equal(new Vector3(100, 200, 300), element.Object.Size);
        }

        [Fact]
        public void Resizing_The_Parent_Element_Does_Not_Alter_The_Child()
        {
            var element = new Mock<CubicElement> { CallBase = true };
            element.Object.Size = new Vector3(100, 200, 300);

            var parent = new Mock<CubicElement> { CallBase = true };
            parent.Object.Size = new Vector3(1000, 2000, 3000);
            parent.Object.Add(element.Object);

            parent.Object.Size = new Vector3(2000, 1000, 10);

            Assert.Equal(new Vector3(100, 200, 300), element.Object.Size);
        }

        [Fact]
        public void Scaling_A_Hierarchy_Correctly_Scales_All_Elements_Involved()
        {
            var canvas = new Imml.Scene.Layout.Canvas();

            var topParent = new Mock<CubicElement> { CallBase = true };
            topParent.Object.Size = new Vector3(1, 2, 3);
            canvas.Add(topParent.Object);

            var firstLevelChild = new Mock<CubicElement> { CallBase = true };
            firstLevelChild.Object.Size = new Vector3(1, 2, 3);
            topParent.Object.Add(firstLevelChild.Object);
            var firstLevelCanvas = new Imml.Scene.Layout.Canvas();
            firstLevelChild.Object.Add(firstLevelCanvas);

            var secondLevelChild = new Mock<CubicElement> { CallBase = true };
            secondLevelChild.Object.Size = new Vector3(1, 2, 3);
            firstLevelCanvas.Add(secondLevelChild.Object);

            canvas.Scale = new Vector3(10, 20, 30);
            firstLevelCanvas.Scale = new Vector3(10, 10, 10);

            Assert.Equal(new Vector3(10, 20, 30), topParent.Object.WorldScale);
            Assert.Equal(new Vector3(10, 40, 90), topParent.Object.WorldSize);
            Assert.Equal(new Vector3(1, 2, 3), topParent.Object.Size);

            Assert.Equal(new Vector3(10, 20, 30), firstLevelChild.Object.WorldScale);
            Assert.Equal(new Vector3(10, 40, 90), firstLevelChild.Object.WorldSize);
            Assert.Equal(new Vector3(1, 2, 3), firstLevelChild.Object.Size);

            Assert.Equal(new Vector3(100, 200, 300), secondLevelChild.Object.WorldScale);
            Assert.Equal(new Vector3(100, 400, 900), secondLevelChild.Object.WorldSize);
            Assert.Equal(new Vector3(1, 2, 3), secondLevelChild.Object.Size);
        }

        [Fact]
        public void Scaling_Text_Alters_Its_World_Size()
        {
            var canvas = new Imml.Scene.Layout.Canvas();

            var text = new Imml.Scene.Controls.Text();
            text.Size = 10;
            canvas.Add(text);

            canvas.Scale = new Vector3(10, 20, 30);

            Assert.Equal(text.WorldSize, 200);
        }

        [Fact]
        public void Scaling_An_Element_Alters_Its_World_Position()
        {
            var canvas = new Imml.Scene.Layout.Canvas();
            canvas.Scale = new Vector3(1, 2, 3);

            var element = new Mock<PositionalElement>() { CallBase = true };
            element.Object.Position = new Vector3(1,2,3);
            canvas.Add(element.Object);

            Assert.Equal(new Vector3(1,4,9), element.Object.WorldPosition);
        }

        [Fact]
        public void Scaling_An_Element_Does_Not_Alter_Its_Position_Or_Rotation()
        {
            var canvas = new Imml.Scene.Layout.Canvas();
            canvas.Scale = new Vector3(1, 2, 3);

            var element = new Mock<PositionalElement>() { CallBase = true };
            element.Object.Position = new Vector3(1, 2, 3);
            element.Object.Rotation = new Vector3(4, 5, 6);
            canvas.Add(element.Object);

            Assert.Equal(new Vector3(1, 2, 3), element.Object.Position);
            Assert.Equal(new Vector3(4, 5, 6), element.Object.Rotation);
        }

        [Fact]
        public void Setting_A_Positional_Pivot_Does_Not_Alter_Its_Position()
        {
            var element = new Mock<PositionalElement>() { CallBase = true };
            element.Object.Position = new Vector3(1, 2, 3);
            element.Object.Pivot = new Vector3(4, 5, 6);

            Assert.Equal(new Vector3(1, 2, 3), element.Object.Position);
        }

        [Fact]
        public void Rotating_A_Positional_With_Non_Zero_Pivot_Will_Alter_World_Position()
        {
            var element = new Mock<PositionalElement>() { CallBase = true };
            element.Object.Position = new Vector3(0,0,0);
            element.Object.Rotation = new Vector3((float)Math.PI, 0, 0);
            element.Object.Pivot = new Vector3(0,1,0);

            Assert.NotEqual(new Vector3(0, 0, 0), element.Object.WorldPosition);
        }

        [Fact]
        public void A_Tranformed_Positional_Matrix_Matches_Its_Transformations()
        {
            var element = new Mock<PositionalElement>() { CallBase = true };
            element.Object.Position = new Vector3(1, 2, 3);
            element.Object.Rotation = new Vector3((float)Math.PI, 0, 0);
            element.Object.Pivot = new Vector3(0, -3, 0);

            var matrix = element.Object.Matrix;

            Vector3 t, s;
            Quaternion rq;

            matrix.Decompose(out s, out rq, out t);

            var ypr = rq.ToPitchYawRoll(); 

            Assert.Equal(new Vector3(1, -4, 3), new Vector3((float)Math.Round(t.X), (float)Math.Round(t.Y), (float)Math.Round(t.Z)));
            Assert.Equal(new Vector3((int)Math.PI, 0, 0), new Vector3((float)Math.Round(ypr.X), (float)Math.Round(ypr.Y), (float)Math.Round(ypr.Z)));
        }
    }
}
