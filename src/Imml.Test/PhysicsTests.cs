using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Imml.Scene.Controls;
using Imml.Scene;

namespace Imml.Test
{
    public class PhysicsTests
    {
        [Fact]
        public void Primitive_Element_Is_Created_Without_Physics()
        {
            var primitive = new Primitive();
            Assert.Null(primitive.GetPhysics());
        }

        [Fact]
        public void Physics_Element_Can_Be_Retrieved_When_It_Is_Present()
        {
            var primitive = new Primitive();
            var physics = new Physics();
            physics.Enabled = true;
            physics.Movable = true;
            physics.Weight = 1234;

            primitive.Add(physics);

            Assert.NotNull(primitive.GetPhysics());
            Assert.Equal(physics.Weight, primitive.GetPhysics().Weight);
        }

        [Fact]
        public void Interaction_Settings_Can_Be_Retrieved_From_A_Physics_Element()
        {
            var primitive = new Primitive();
            var physics = new Physics();

            var interaction = new Interaction();
            interaction.Element = Guid.NewGuid().ToString();

            physics.Add(interaction);
            primitive.Add(physics);

            Assert.NotEmpty(primitive.GetPhysics().GetInteractions());
            Assert.Equal(1, primitive.GetPhysics().GetInteractions().Count);
            Assert.Equal(interaction.Element, primitive.GetPhysics().GetInteractions().First().Element);
        }
    }
}
