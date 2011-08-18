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
        public override Vector3 WorldScale
        {
            get
            {
                var baseScale = base.WorldScale;
                return new Vector3(baseScale.X * this.Scale.X, baseScale.Y * this.Scale.Y, baseScale.Z * this.Scale.Z);
            }
        } 
        #endregion
    }
}
