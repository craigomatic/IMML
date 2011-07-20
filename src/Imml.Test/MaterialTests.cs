using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Imml.Scene.Controls;
using Imml.Scene;

namespace Imml.Test
{
    public class MaterialTests
    {
        [Fact]
        public void Primitive_Element_Is_Created_Without_A_Default_MaterialGroup()
        {
            var primitive = new Primitive();
            Assert.Null(primitive.GetMaterialGroup(-1));
        }

        [Fact]
        public void MaterialGroup_Elements_Can_Be_Retrieved_By_Id()
        {
            var primitive = new Primitive();

            var mGroupIdOne = new MaterialGroup();
            mGroupIdOne.Id = 1;

            var mGroupIdTwo = new MaterialGroup();
            mGroupIdTwo.Id = 2;

            var mGroupIdDefault = new MaterialGroup();
            mGroupIdDefault.Id = -1;

            primitive.Add(mGroupIdDefault);
            primitive.Add(mGroupIdTwo);
            primitive.Add(mGroupIdOne);

            Assert.NotNull(primitive.GetMaterialGroup(-1));
            Assert.NotNull(primitive.GetMaterialGroup(1));
            Assert.NotNull(primitive.GetMaterialGroup(2));
        }

        [Fact]
        public void Material_Elements_Can_Be_Retrieved_From_A_MaterialGroup()
        {
            var primitive = new Primitive();

            var mGroupIdOne = new MaterialGroup();
            mGroupIdOne.Id = 1;

            mGroupIdOne.Add(Material.Default);
            primitive.Add(mGroupIdOne);

            Assert.NotNull(primitive.GetMaterialGroup(1));
            Assert.Equal(Material.Default.ToString(), primitive.GetMaterialGroup(1).GetMaterial().ToString());
        }

        [Fact]
        public void Texture_Elements_Can_Be_Retrieved_From_A_MaterialGroup()
        {
            var primitive = new Primitive();

            var mGroupIdOne = new MaterialGroup();
            mGroupIdOne.Id = 1;

            var texture = new Texture { Source = Guid.NewGuid().ToString() };
            mGroupIdOne.Add(texture);
            primitive.Add(mGroupIdOne);

            Assert.NotNull(primitive.GetMaterialGroup(1));
            Assert.Equal(texture.Source, primitive.GetMaterialGroup(1).GetTexture().Source);
        }

        [Fact]
        public void Video_Elements_Can_Be_Retrieved_From_A_MaterialGroup()
        {
            var primitive = new Primitive();

            var mGroupIdOne = new MaterialGroup();
            mGroupIdOne.Id = 1;

            var video = new Video { Source = Guid.NewGuid().ToString() };
            mGroupIdOne.Add(video);
            primitive.Add(mGroupIdOne);

            Assert.NotNull(primitive.GetMaterialGroup(1));
            Assert.Equal(video.Source, primitive.GetMaterialGroup(1).GetVideo().Source);
        }

        [Fact]
        public void Web_Elements_Can_Be_Retrieved_From_A_MaterialGroup()
        {
            var primitive = new Primitive();

            var mGroupIdOne = new MaterialGroup();
            mGroupIdOne.Id = 1;

            var web = new Web { Source = Guid.NewGuid().ToString() };
            mGroupIdOne.Add(web);
            primitive.Add(mGroupIdOne);

            Assert.NotNull(primitive.GetMaterialGroup(1));
            Assert.Equal(web.Source, primitive.GetMaterialGroup(1).GetWeb().Source);
        }
    }
}
