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
    public class PositionalElementTests
    {
        [Fact]
        public void Default_Position_Is_Zero_Vector()
        {
            var element = new Mock<PositionalElement>();
            Assert.Equal(new Vector3(), element.Object.Position);
        }

        [Fact]
        public void Default_Rotation_Is_Zero_Vector()
        {
            var element = new Mock<PositionalElement>();
            Assert.Equal(new Vector3(), element.Object.Rotation);
        }        
    }
}
