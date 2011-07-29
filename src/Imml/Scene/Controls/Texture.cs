using System;
using System.Collections.Generic;
using System.Text;

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Defines a custom texture and the mapping to use when applying it to a material group.
    /// </summary>
    public class Texture : ImmlElement
    {
        protected string _Source;

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
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

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
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

        /// <summary>
        /// Gets or sets the tile U.
        /// </summary>
        /// <value>
        /// The tile U.
        /// </value>
        /// <remarks>The number of segments in the U direction to use when tiling</remarks>
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

        /// <summary>
        /// Gets or sets the tile V.
        /// </summary>
        /// <value>
        /// The tile V.
        /// </value>
        /// <remarks>The number of segments in the V direction to use when tiling</remarks>
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

        /// <summary>
        /// Gets or sets the offset U.
        /// </summary>
        /// <value>
        /// The offset U.
        /// </value>
        /// <remarks>The offset of the U coordinate in the UV texture coordinate system</remarks>
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

        /// <summary>
        /// Gets or sets the offset V.
        /// </summary>
        /// <value>
        /// The offset V.
        /// </value>
        /// <remarks>The offset of the V coordinate in the UV texture coordinate system</remarks>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Texture"/> class.
        /// </summary>
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
