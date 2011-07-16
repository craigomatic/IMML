using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Imml.Scene.Controls;
using Moq;
using Imml.ComponentModel;

namespace Imml.Test
{
    public class VisibleElementTests
    {
        [Fact]        
        public void Visible_Elements_Default_To_A_Visibility_Of_True()
        {
            var visibleElement = new Mock<VisibleElement> { CallBase = true };

            Assert.True(visibleElement.Object.Visible);
            Assert.True(visibleElement.Object.IsVisible);
        }

        [Fact]
        public void Visibility_Of_A_Child_Element_Is_False_When_The_Parent_Element_It_Is_Nested_Within_Is_False()
        {
            var childElement = new Mock<VisibleElement>() { CallBase = true };
            childElement.Object.Visible = true;

            var parentElement = new Mock<VisibleElement>() { CallBase = true };
            parentElement.Object.Visible = false;

            parentElement.Object.Add(childElement.Object);

            Assert.False(childElement.Object.IsVisible);
        }

        [Fact]
        public void Removing_An_Element_From_Its_Parent_Returns_IsVisible_As_True_When_Its_Visible_Property_Is_Set_To_True()
        {
            var childElement = new Mock<VisibleElement>() { CallBase = true };
            childElement.Object.Visible = true;

            var parentElement = new Mock<VisibleElement>() { CallBase = true };
            parentElement.Object.Visible = false;

            parentElement.Object.Add(childElement.Object);

            //remove
            parentElement.Object.Remove(childElement.Object);

            Assert.True(childElement.Object.IsVisible);
        }
    }
}
