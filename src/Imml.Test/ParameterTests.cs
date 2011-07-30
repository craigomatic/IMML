using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Imml.Scene.Controls;
using Imml.Scene;

namespace Imml.Test
{
    public class ParameterTests
    {
        [Fact]
        public void Plugin_Element_Is_Created_Without_Parameters()
        {
            var plugin = new Plugin();
            Assert.Empty(plugin.GetParameters());
        }

        [Fact]
        public void Parameter_Elements_Can_Be_Retrieved_When_Present()
        {
            var element = new Plugin();
            var parameter = new Parameter();
            parameter.Key = Guid.NewGuid().ToString();
            parameter.Value = Guid.NewGuid().ToString();

            element.Add(parameter);

            Assert.Equal(1, element.GetParameters().Count);
            Assert.Equal(parameter.Key, element.GetParameters().First().Key);
            Assert.Equal(parameter.Value, element.GetParameters().First().Value);
        }
    }
}
