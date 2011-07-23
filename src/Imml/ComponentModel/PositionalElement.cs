//-----------------------------------------------------------------------
//VastPark is a lightweight extensible virtual world platform 
//and this file is a program released under the GPL.
//Copyright (C) 2009 VastPark
//This program is free software; you can redistribute it and/or
//modify it under the terms of the GNU General Public License
//as published by the Free Software Foundation; either version 2
//of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//GNU General Public License for more details.
//You should have received a copy of the GNU General Public License
//along with this program; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Imml.Numerics.Geometry;
using Imml.Numerics;

namespace Imml.ComponentModel
{
    public abstract class PositionalElement : ImmlElement, IPositionalElement
    {
        /// <summary>
        /// Gets or sets the point for the element to pivot on when performing transformations.
        /// </summary>
        /// <value>
        /// The pivot.
        /// </value>
        public virtual Vector3 Pivot { get; set; }

        protected IPositionalElement _PositionalParent;

        protected ICubicElement _CubicParent;

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

                if (value is ICubicElement)
                    _CubicParent = value as ICubicElement;
                else
                    _CubicParent = null;
            }
        }

        protected Matrix4 _Matrix;

        public virtual Matrix4 RelativeMatrix
        {
            get
            {
                return Matrix4.Multiply(Matrix4.Rotate(Angle.FromRadians(_Rotation.Y), Angle.FromRadians(_Rotation.X), Angle.FromRadians(_Rotation.Z)), Matrix4.Translate(_Position));
            }
        }

        public virtual Matrix4 WorldMatrix
        {
            get
            {
                if (_CubicParent != null)
                    return this.RelativeMatrix * _PositionalParent.WorldMatrix * Matrix4.Scale(_CubicParent.WorldScale.X, _CubicParent.WorldScale.Y, _CubicParent.WorldScale.Z);

                if (_PositionalParent != null)
                    return this.RelativeMatrix * _PositionalParent.WorldMatrix;

                return this.RelativeMatrix;
            }
        }

        public virtual Matrix4 Matrix
        {
            get
            {
                return this.WorldMatrix;
            }
        }


        public virtual Vector3 WorldPosition
        {
            get
            {
                return this.WorldMatrix.Translation;
            }
            set
            {
                if (_CubicParent != null)
                    this.Position = (Matrix4.Translate(value) * Matrix4.Scale(_CubicParent.WorldScale.X, _CubicParent.WorldScale.Y, _CubicParent.WorldScale.Z)).Translation;
                else if (_PositionalParent != null)
                    this.Position = (Matrix4.Translate(value) * Matrix4.Invert(_PositionalParent.WorldMatrix)).Translation;
                else
                    this.Position = value;
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
                //calculate the local position to set based on the world position requested
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
                Vector3 s, t;
                Quaternion q;                
                this.WorldMatrix.Decompose(out s, out q, out t);
                return s;
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
                return BoundingBox.With(null, this.Position);
            }
        }

        public virtual BoundingBox WorldBoundingBox
        {
            get
            {
                return BoundingBox.With(null, this.Matrix.Translation);
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

        public virtual bool IsTransforming { get; set; }

        public virtual void ApplyTransform()
        {
            _Matrix = Matrix4.Multiply(Matrix4.Rotate(Angle.FromRadians(_Rotation.Y), Angle.FromRadians(_Rotation.X), Angle.FromRadians(_Rotation.Z)), Matrix4.Translate(_Position));
        }
    }
}
