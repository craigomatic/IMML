using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Imml.Scene.Controls;
using Imml.Scene;

namespace Imml.Test
{
    public class NetworkTests
    {
        [Fact]
        public void Primitive_Element_Is_Created_Without_Network()
        {
            var primitive = new Primitive();
            Assert.Null(primitive.GetNetwork());
        }

        [Fact]
        public void Network_Element_Can_Be_Retrieved_When_It_Is_Present()
        {
            var primitive = new Primitive();
            var network = new Network();
            network.Enabled = true;
            network.Owner = Guid.NewGuid().ToString();

            primitive.Add(network);

            Assert.NotNull(primitive.GetNetwork());
            Assert.Equal(network.Enabled, primitive.GetNetwork().Enabled);
            Assert.Equal(network.Owner, primitive.GetNetwork().Owner);
        }

        [Fact]
        public void Filter_Settings_Can_Be_Retrieved_From_A_Network_Element()
        {
            var primitive = new Primitive();
            var network = new Network();

            var filter = new Filter();
            filter.Target = Guid.NewGuid().ToString();

            network.Add(filter);
            primitive.Add(network);

            Assert.NotEmpty(primitive.GetNetwork().GetFilters());
            Assert.Equal(1, primitive.GetNetwork().GetFilters().Count);
            Assert.Equal(filter.Target, primitive.GetNetwork().GetFilters().First().Target);
        }
    }
}
