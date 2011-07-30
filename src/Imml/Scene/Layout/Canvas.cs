using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imml.ComponentModel;
using Imml.Numerics;

namespace Imml.Scene.Layout
{
    /// <summary>
    /// Allows one or more elements to be scaled uniformly.
    /// </summary>
    public class Canvas : VisibleElement
    {
        #region Properties
        private ICubicElement _ScalableParent;

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public override ImmlElement Parent
        {
            get
            {
                return base.Parent;
            }
            protected set
            {
                base.Parent = value;

                var parent = this.Parent;

                while (parent != null)
                {
                    if (parent is ICubicElement)
                    {
                        _ScalableParent = parent as ICubicElement;
                        break;
                    }

                    parent = parent.Parent;
                }
            }
        }

        private Vector3 _Scale;

        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>
        /// The scale.
        /// </value>
        public virtual Vector3 Scale
        {
            get
            {
                return _Scale;
            }
            set
            {
                if (value == _Scale)
                    return;

                var oldValue = _Scale;
                _Scale = value;
                base.RaisePropertyChanged("Scale", oldValue, _Scale);
            }
        }

        /// <summary>
        /// Gets or sets the world scale.
        /// </summary>
        /// <value>
        /// The world scale.
        /// </value>
        public new Vector3 WorldScale
        {
            get
            {
                if (_ScalableParent != null)
                {
                    var parentWorldScale = _ScalableParent.WorldScale;
                    return new Vector3(parentWorldScale.X * _Scale.X, parentWorldScale.Y * _Scale.Y, parentWorldScale.Z * _Scale.Z);
                }
                else
                {
                    return _Scale;
                }
            }
            set
            {
                if (_ScalableParent != null)
                {
                    var parentWorldScale = _ScalableParent.WorldScale;
                    this.Scale = new Vector3(value.X / parentWorldScale.X, value.Y / parentWorldScale.Y, value.Z / parentWorldScale.Z);
                }
            }
        } 
        #endregion
    }
}
