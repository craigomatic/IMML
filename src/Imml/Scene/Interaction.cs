using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imml.Scene
{
    /// <summary>
    /// Defines physics material interaction properties between physics enabled elements.
    /// </summary>
    public class Interaction : ImmlElement
    {
        #region Properties
        /// <summary>
        /// Name of the element to apply this interaction with
        /// </summary>
        public virtual string Element { get; set; }

        private float _StaticFriction;

        /// <summary>
        /// Gets or sets the static friction.
        /// </summary>
        /// <value>
        /// The static friction.
        /// </value>
        public virtual float StaticFriction
        {
            get { return _StaticFriction; }
            set
            {
                if (_StaticFriction == value)
                {
                    return;
                }

                var oldValue = _StaticFriction;
                _StaticFriction = value;
                base.RaisePropertyChanged("StaticFriction", oldValue, value);
            }
        }

        private float _DynamicFriction;

        /// <summary>
        /// Gets or sets the dynamic friction.
        /// </summary>
        /// <value>
        /// The dynamic friction.
        /// </value>
        public virtual float DynamicFriction
        {
            get { return _DynamicFriction; }
            set
            {
                if (_DynamicFriction == value)
                {
                    return;
                }

                var oldValue = _DynamicFriction;
                _DynamicFriction = value;
                base.RaisePropertyChanged("DynamicFriction", oldValue, value);
            }
        }

        private float _Elasticity;

        /// <summary>
        /// Gets or sets the elasticity.
        /// </summary>
        /// <value>
        /// The elasticity.
        /// </value>
        public virtual float Elasticity
        {
            get { return _Elasticity; }
            set
            {
                if (_Elasticity == value)
                {
                    return;
                }

                var oldValue = _Elasticity;
                _Elasticity = value;
                base.RaisePropertyChanged("Elasticity", oldValue, value);
            }
        }

        private float _Softness;

        /// <summary>
        /// Gets or sets the softness.
        /// </summary>
        /// <value>
        /// The softness.
        /// </value>
        public virtual float Softness
        {
            get { return _Softness; }
            set
            {
                if (_Softness == value)
                {
                    return;
                }

                var oldValue = _Softness;
                _Softness = value;
                base.RaisePropertyChanged("Softness", oldValue, value);
            }
        } 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Interaction"/> class.
        /// </summary>
        public Interaction()
        {
            this.StaticFriction = 0.9f;
            this.DynamicFriction = 0.5f;
            this.Elasticity = 0.4f;
            this.Softness = 0.7f;
        }
    }
}
