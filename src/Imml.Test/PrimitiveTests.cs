using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imml.Scene.Controls;
using Xunit;
using Imml.Scene;

namespace Imml.Test
{
    /// <summary>
    /// Primitive's have a default position of 0,0,0
    /// Default rotation of 0,0,0
    /// Default size of 0,0,0
    /// Default to representing a box type
    /// Default to Medium complexity
    /// Can contain other elements which have at least a position and rotation
    /// 
    /// 
    /// </summary>
    public class PrimitiveTests
    {
        [Fact]
        public void Default_Type_Is_Box()
        {
            var primitive = new Primitive();
            Assert.Equal(PrimitiveType.Box, primitive.Type);
        }

        [Fact]
        public void Default_Complexity_Is_Medium()
        {
            var primitive = new Primitive();
            Assert.Equal(PrimitiveComplexity.Medium, primitive.Complexity);
        }
    }
}
