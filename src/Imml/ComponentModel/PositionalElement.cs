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

        protected Vector3 _Pivot;

        /// <summary>
        /// Get/set the pivot point for the element in local coordinates
        /// </summary>
        public virtual Vector3 Pivot
        {
            get
            {
                return _Pivot;
            }
            set
            {
                if (_Pivot == value)
                    return;

                Vector3 oldValue = _Pivot;
                _Pivot = value;
                base.RaisePropertyChanged("Pivot", oldValue, _Pivot);
            }
        }

        public virtual Vector3 WorldScale
        {
            get
            {
                if (_PositionalParent != null)
                    return _PositionalParent.WorldScale;
                
                return Vector3.One;
            }
        }

        public virtual Matrix4 Matrix
        {
            get
            {
                var worldPos = this.WorldPosition;
                var worldRot = this.WorldRotation;

                return Matrix4.Rotate(Angle.FromRadians(worldRot.Y), Angle.FromRadians(worldRot.X), Angle.FromRadians(worldRot.Z)) * Matrix4.Translate(worldPos);
            }
        }

        public virtual Vector3 WorldPosition
        {
            get
            {
                var worldScale = this.WorldScale;
                var pivotTranslation = this.Pivot.ComponentWiseMultiply(worldScale);
                var scaledPosition = this.Position.ComponentWiseMultiply(worldScale);

                var transMat = Matrix4.Translate(-pivotTranslation);
                transMat *= Matrix4.Rotate(Angle.FromRadians(_Rotation.Y), Angle.FromRadians(_Rotation.X), Angle.FromRadians(_Rotation.Z));

                if (_PositionalParent != null)
                    transMat *= Matrix4.Translate(pivotTranslation + scaledPosition + _PositionalParent.WorldPosition);
                else
                    transMat *= Matrix4.Translate(pivotTranslation + scaledPosition);

                return transMat.Translation;
            }
            set
            {
                var worldScale = this.WorldScale;

                if (_PositionalParent != null)
                {
                    var parentWorldPosition = _PositionalParent.WorldPosition;
                    this.Position = (value - parentWorldPosition).ComponentWiseDivide(worldScale);
                }
                else
                {
                    this.Position = value.ComponentWiseDivide(worldScale);
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
                
                this.Rotation = value;
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
