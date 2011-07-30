using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imml.Numerics;

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Container for referencing particle based effects resources for rendering.
    /// </summary>
    public class Emitter : ImmlElement
    {
        #region Properties

        protected float _Rate;

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>
        /// The rate.
        /// </value>
        /// <remarks>Modulator that alters the number of particles emitted per second.</remarks>
        public virtual float Rate
        {
            get { return _Rate; }
            set
            {
                if (_Rate != value)
                {
                    object oldValue = _Rate;
                    _Rate = value;
                    base.RaisePropertyChanged("Rate", oldValue, _Rate);
                }
            }
        }

        protected int _Count;

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        /// <remarks>Maximum number of particles that can exist at any one time</remarks>
        public virtual int Count
        {
            get { return _Count; }
            set
            {
                if (_Count != value)
                {
                    object oldValue = _Count;
                    _Count = value;
                    base.RaisePropertyChanged("Count", oldValue, _Count);
                }
            }
        }

        protected float _Cone;

        /// <summary>
        /// Gets or sets the cone.
        /// </summary>
        /// <value>
        /// The cone.
        /// </value>
        /// <remarks>The maximum angle in degrees that particles will vary from the direction of their force</remarks>
        public virtual float Cone
        {
            get { return _Cone; }
            set
            {
                if (_Cone != value)
                {
                    object oldValue = _Cone;
                    _Cone = value;
                    base.RaisePropertyChanged("Cone", oldValue, _Cone);
                }
            }
        }

        protected Vector3 _Force;

        /// <summary>
        /// Gets or sets the force.
        /// </summary>
        /// <value>
        /// The force.
        /// </value>
        public virtual Vector3 Force
        {
            get
            {
                return _Force;
            }
            set
            {
                if (_Force == value)
                    return;

                Vector3 oldValue = _Force;
                _Force = value;
                base.RaisePropertyChanged("Force", oldValue, _Force);
            }
        }

        protected float _LifeMin;

        /// <summary>
        /// Gets or sets the life min.
        /// </summary>
        /// <value>
        /// The life min.
        /// </value>
        /// <remarks>The minumum number of seconds a particle will live for.</remarks>
        public virtual float LifeMin
        {
            get { return _LifeMin; }
            set
            {
                if (_LifeMin != value)
                {
                    object oldValue = _LifeMin;
                    _LifeMin = value;
                    base.RaisePropertyChanged("LifeMin", oldValue, _LifeMin);
                }
            }
        }

        protected float _LifeMax;

        /// <summary>
        /// Gets or sets the life max.
        /// </summary>
        /// <value>
        /// The life max.
        /// </value>
        /// <remarks>The minumum number of seconds a particle will live for</remarks>
        public virtual float LifeMax
        {
            get { return _LifeMax; }
            set
            {
                if (_LifeMax != value)
                {
                    object oldValue = _LifeMax;
                    _LifeMax = value;
                    base.RaisePropertyChanged("LifeMax", oldValue, _LifeMax);
                }
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Emitter"/> class.
        /// </summary>
        public Emitter()
        {
            this.LifeMin = 1;
            this.LifeMax = 3;
            this.Force = new Vector3(0, 1, 0);
            this.Rate = 100;
            this.Count = 1000;
        }
    }
}
