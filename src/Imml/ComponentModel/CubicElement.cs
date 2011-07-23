using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imml.Numerics;
using Imml.Numerics.Geometry;

namespace Imml.ComponentModel
{
    /// <summary>
    /// Abstract class that represents an element with 3-dimensions
    /// </summary>
    public abstract class CubicElement : VisibleElement, ICubicElement
    {
        protected RenderMode _RenderMode;

        /// <summary>
        /// Gets or sets the render mode.
        /// </summary>
        /// <value>
        /// The render mode.
        /// </value>
        public virtual RenderMode RenderMode
        {
            get { return _RenderMode; }
            set
            {
                if (_RenderMode == value)
                    return;
                RenderMode oldValue = _RenderMode;

                _RenderMode = value;
                base.RaisePropertyChanged("RenderMode", oldValue, _RenderMode);
            }
        }

        protected bool _CastShadows;

        /// <summary>
        /// Gets or sets a value indicating whether shadows should be cast.
        /// </summary>
        /// <value>
        ///   <c>true</c> if shadows can be cast; otherwise, <c>false</c>.
        /// </value>
        public virtual bool CastShadows
        {
            get { return _CastShadows; }
            set
            {
                if (_CastShadows == value)
                    return;

                _CastShadows = value;
                base.RaisePropertyChanged("CastShadows", !_CastShadows, _CastShadows);
            }
        }

        public override Matrix4 Matrix
        {
            get
            {
                return Matrix4.Scale(this.Size.X / this.OriginalSize.X, this.Size.Y / this.OriginalSize.Y, this.Size.Z / this.OriginalSize.Z) * base.Matrix;
            }
        }

        protected Vector3 _Size;

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public virtual Vector3 Size
        {
            get { return _Size; }
            set
            {
                if (_Size.X == value.X && _Size.Y == value.Y && _Size.Z == value.Z)
                    return;

                Vector3 oldValue = _Size;
                _Size = value;
                base.RaisePropertyChanged("Size", oldValue, _Size);
            }
        }

        protected Vector3 _OriginalSize;

        public virtual Vector3 OriginalSize
        {
            get
            {
                if (_OriginalSize == Vector3.Zero)
                    return new Vector3(1);
                else
                    return _OriginalSize;
            }
        }

        public virtual Vector3 WorldSize
        {
            get
            {
                var worldScale = this.WorldScale;
                return new Vector3(worldScale.X * _Size.X, worldScale.Y * _Size.Y, worldScale.Z * _Size.Z);
            }
            set
            {
                var worldScale = this.WorldScale;
                this.Size = new Vector3(value.X / worldScale.X, value.Y / worldScale.Y, value.Z / worldScale.Z);
            }
        }

        public override BoundingBox BoundingBox
        {
            get
            {
                Vector3 vMin = new Vector3(this.Position.X - (this.Size.X / 2), this.Position.Y, this.Position.Z - (this.Size.Z / 2));
                Vector3 vMax = new Vector3(this.Position.X + (this.Size.X / 2), this.Position.Y + this.Size.Y, this.Position.Z + (this.Size.Z / 2));

                return BoundingBox.With(vMin, vMax);
            }
        }

        public override BoundingBox WorldBoundingBox
        {
            get
            {
                var worldPos = this.WorldPosition;
                var worldSize = this.WorldSize;
                Vector3 vMin = new Vector3(worldPos.X - (worldSize.X / 2), worldPos.Y, worldPos.Z - (worldSize.Z / 2));
                Vector3 vMax = new Vector3(worldPos.X + (worldSize.X / 2), worldPos.Y + this.Size.Y, worldPos.Z + (worldSize.Z / 2));

                return BoundingBox.With(vMin, vMax);
            }
        }
    }
}
