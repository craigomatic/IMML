using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imml.Scene
{
    /// <summary>
    /// A change relating to a particle within an Effect.
    /// </summary>
    public class ParticleChange : ImmlElement
    {
        #region Properties
        protected ParticleChangeType _Type;

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public virtual ParticleChangeType Type
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
                base.RaisePropertyChanged("Type", oldValue, _Type, "Type");
            }
        }

        protected float _Minimum;

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        /// <remarks>The minumum initial bounds for the particle change</remarks>
        public virtual float Minimum
        {
            get { return _Minimum; }
            set
            {
                if (_Minimum != value)
                {
                    object oldValue = _Minimum;
                    _Minimum = value;
                    base.RaisePropertyChanged("Minimum", oldValue, _Minimum, "Minimum");
                }
            }
        }

        protected float _Maximum;

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        /// <remarks>The maximum initial bounds for the particle change</remarks>
        public virtual float Maximum
        {
            get { return _Maximum; }
            set
            {
                if (_Maximum != value)
                {
                    object oldValue = _Maximum;
                    _Maximum = value;
                    base.RaisePropertyChanged("Maximum", oldValue, _Maximum, "Maximum");
                }
            }
        }

        protected float _EndRate;

        /// <summary>
        /// Gets or sets the end rate.
        /// </summary>
        /// <value>
        /// The end rate.
        /// </value>
        /// <remarks>Fraction of the initial change value when particle is dying</remarks>
        public virtual float EndRate
        {
            get { return _EndRate; }
            set
            {
                if (_EndRate != value)
                {
                    object oldValue = _EndRate;
                    _EndRate = value;
                    base.RaisePropertyChanged("EndRate", oldValue, _EndRate, "EndRate");
                }
            }
        } 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleChange"/> class.
        /// </summary>
        public ParticleChange()
        {
            this.EndRate = 1;
        }
    }
}
