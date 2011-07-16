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
        public void Resizing_The_Parent_Element_Propagates_To_The_Child()
        {
            var element = new Mock<CubicElement> { CallBase = true };
            element.Object.Size = new Vector3(100, 200, 300);

            var parent = new Mock<CubicElement> { CallBase = true };
            parent.Object.Size = new Vector3(1000, 2000, 3000);
            parent.Object.Add(element.Object);

            parent.Object.Size = new Vector3(2000, 1000, 10);

            Assert.NotEqual(new Vector3(100, 200, 300), element.Object.Size);
        }
    }
}
