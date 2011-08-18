using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Imml.ComponentModel;
using Moq;
using Imml.Numerics;

namespace Imml.Test
{
    public class CubicElementTests
    {
        [Fact]
        public void Default_Position_Is_Zero_Vector()
        {
            var element = new Mock<CubicElement>();
            Assert.Equal(new Vector3(), element.Object.Position);
        }

        [Fact]
        public void Default_Rotation_Is_Zero_Vector()
        {
            var element = new Mock<CubicElement>();
            Assert.Equal(new Vector3(), element.Object.Rotation);
        }

        [Fact]
        public void Default_Size_Is_Zero_Vector()
        {
            var element = new Mock<CubicElement>();
            Assert.Equal(new Vector3(), element.Object.Size);
        }

        [Fact]
        public void Default_RenderMode_Is_Fill()
        {
            var element = new Mock<CubicElement>();
            Assert.Equal(RenderMode.Fill, element.Object.RenderMode);
        }

        [Fact]
        public void Default_CastShadows_Is_False()
        {
            var element = new Mock<CubicElement>();
            Assert.Equal(false, element.Object.CastShadows);
        }
    }
}
