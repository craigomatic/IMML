using System;
using System.Collections.Generic;
using System.Text;

namespace Imml.Scene.Controls
{
    public class Texture : ImmlElement
    {
        protected string _Source;

        public virtual string Source
        {
            get { return _Source; }
            set
            {
                if (_Source == value)
                    return;

                string oldValue = _Source;
                _Source = value;
                base.RaisePropertyChanged("Source", oldValue, _Source, "Texture.Source");
            }
        }


        protected TextureType _Type;       

        public virtual TextureType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if (_Type == value)
                {
                    return;
                }

                var oldValue = _Type;
                _Type = value;
                base.RaisePropertyChanged("Type", oldValue, _Type, "Texture.Type");
            }
        }

        protected float _TileU;        

        public virtual float TileU
        {
            get
            {
                return _TileU;
            }
            set
            {
                if (_TileU == value)
                {
                    return;
                }

                var oldValue = _TileU;
                _TileU = value;
                base.RaisePropertyChanged("TileU", oldValue, _TileU, "Texture.TileU");
            }
        }

        protected float _TileV;        

        public virtual float TileV
        {
            get
            {
                return _TileV;
            }
            set
            {
                if (_TileV == value)
                {
                    return;
                }

                var oldValue = _TileV;
                _TileV = value;
                base.RaisePropertyChanged("TileV", oldValue, _TileV, "Texture.TileV");
            }
        }

        protected float _OffsetU;        

        public virtual float OffsetU
        {
            get
            {
                return _OffsetU;
            }
            set
            {
                if (_OffsetU == value)
                {
                    return;
                }

                var oldValue = _OffsetU;
                _OffsetU = value;
                base.RaisePropertyChanged("OffsetU", oldValue, _OffsetU, "Texture.OffsetU");
            }
        }

        protected float _OffsetV;

        public virtual float OffsetV
        {
            get
            {
                return _OffsetV;
            }
            set
            {
                if (_OffsetV == value)
                {
                    return;
                }

                var oldValue = _OffsetV;
                _OffsetV = value;
                base.RaisePropertyChanged("OffsetV", oldValue, OffsetV, "Texture.OffsetV");
            }
        }


        public Texture()
        {
            this.TileU = 1.0f;
            this.TileV = 1.0f;
            this.OffsetU = 0.0f;
            this.OffsetV = 0.0f;
            this.Source = string.Empty;
            this.Type = TextureType.Diffuse;
        }
    }
}
