using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Imml.Numerics.Geometry;
using Imml.Numerics;

namespace Imml.ComponentModel
{
    public abstract class PositionalElement : ImmlElement, IPositionalElement, INetworkHostElement
    {
        protected IPositionalElement _PositionalParent;

        public override ImmlElement Parent
        {
            get { return base.Parent; }
            protected set
            {
                base.Parent = value;

                if (value is IPositionalElement)
                    _PositionalParent = value as IPositionalElement;
                else
                    _PositionalParent = null;
            }
        }

        public virtual Matrix4 WorldMatrix
        {
            get
            {
                if (_PositionalParent != null)
                    return this.Matrix * _PositionalParent.WorldMatrix;

                return this.Matrix;
            }
        }

        public virtual Matrix4 Matrix
        {
            get
            {
                return Matrix4.Multiply(Matrix4.Rotate(Angle.FromRadians(_Rotation.Y), Angle.FromRadians(_Rotation.X), Angle.FromRadians(_Rotation.Z)), Matrix4.Translate(_Position));
            }
        }

        public virtual Vector3 WorldPosition
        {
            get
            {
                var worldScale = this.WorldScale;

                if (_PositionalParent != null)
                    return new Vector3(this.Position.X * worldScale.X, this.Position.Y * worldScale.Y, this.Position.Z * worldScale.Z) + _PositionalParent.WorldPosition;

                return new Vector3(this.Position.X * worldScale.X, this.Position.Y * worldScale.Y, this.Position.Z * worldScale.Y);
            }
            set
            {
                var worldScale = this.WorldScale;

                if (_PositionalParent != null)
                {
                    var parentWorldPosition = _PositionalParent.WorldPosition;
                    this.Position = new Vector3((value.X - parentWorldPosition.X) / worldScale.X, (value.Y - parentWorldPosition.Y) / worldScale.Y, (value.Z - parentWorldPosition.Z) / worldScale.Z);
                }
                else
                {
                    this.Position = value - worldScale;
                }
            }
        }

        public virtual Vector3 WorldRotation
        {
            get
            {
                if (_PositionalParent != null)
                    return _PositionalParent.WorldRotation + _Rotation;

                return _Rotation;
            }
            set
            {
                if (_PositionalParent != null)
                    this.Rotation = value - _PositionalParent.WorldRotation;
                else
                    this.Rotation = value;
            }
        }

        public virtual Vector3 WorldScale
        {
            get
            {
                if (_PositionalParent != null)
                    return _PositionalParent.WorldScale;
                else
                    return Vector3.One;
            }
        }

        protected Vector3 _Rotation;

        /// <summary>
        /// Get/Set rotation for the element in parent relative coordinates
        /// </summary>
        public virtual Vector3 Rotation
        {
            get
            {
                return _Rotation;
            }
            set
            {
                if (_Rotation == value)
                    return;

                Vector3 oldValue = _Rotation;
                _Rotation = value;

                base.RaisePropertyChanged("Rotation", oldValue, _Rotation);
            }
        }

        protected Vector3 _Position;

        /// <summary>
        /// Get/set position for the element in parent relative coordinates
        /// </summary>
        public virtual Vector3 Position
        {
            get
            {
                return _Position;
            }
            set
            {
                if (_Position == value)
                    return;

                Vector3 oldValue = _Position;
                _Position = value;
                base.RaisePropertyChanged("Position", oldValue, _Position);
            }
        }

        public virtual BoundingBox BoundingBox
        {
            get
            {
                return new BoundingBox(Vector3.Zero, this.Position);
            }
        }

        public virtual BoundingBox WorldBoundingBox
        {
            get
            {
                return new BoundingBox(Vector3.Zero, this.WorldPosition);
            }
        }

        /// <summary>
        /// The child elements that are positional
        /// </summary>
        protected List<IPositionalElement> _PositionalElements;

        public PositionalElement()
        {
            _Position = new Vector3();
            _Rotation = new Vector3();
            _PositionalElements = new List<IPositionalElement>();
        }

        public override void Add(ImmlElement element)
        {
            base.Add(element);

            if (element is IPositionalElement)
            {
                _PositionalElements.Add(element as IPositionalElement);
            }
        }

        public override void Remove(ImmlElement element)
        {
            base.Remove(element);

            if (element is IPositionalElement)
            {
                _PositionalElements.Remove(element as IPositionalElement);
            }
        }

        public override void Clear()
        {
            base.Clear();

            _PositionalElements.Clear();
        }     
    }
}
