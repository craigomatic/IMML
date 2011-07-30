using System;
using System.Collections.Generic;
using System.Text;
using Imml.ComponentModel;

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Provides basic rendering of five main types of primitve: Box, Cone, Cylinder, Plane and Sphere.
    /// </summary>
    public class Primitive : CubicElement, IPhysicsHostElement, IMaterialHostElement
    {
        #region Properties

        private PrimitiveType _Type;

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public virtual PrimitiveType Type
        {
            get { return _Type; }
            set
            {
                if (_Type == value)
                    return;
                
                object oldValue = _Type;
                _Type = value;
                base.RaisePropertyChanged("Type", oldValue, _Type);
            }
        }

        private PrimitiveComplexity _Complexity;

        /// <summary>
        /// Gets or sets the complexity.
        /// </summary>
        /// <value>
        /// The complexity.
        /// </value>
        public virtual PrimitiveComplexity Complexity
        {
            get { return _Complexity; }
            set
            {
                if (_Complexity == value)
                    return;
                
                object oldValue = _Complexity;
                _Complexity = value;
                base.RaisePropertyChanged("Complexity", oldValue, _Complexity);
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Primitive"/> class.
        /// </summary>
        public Primitive()
        {
            this.Complexity = PrimitiveComplexity.Medium;
        }
    }
}
