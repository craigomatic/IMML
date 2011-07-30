using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imml.Numerics;

namespace Imml.Scene
{
    /// <summary>
    /// Settings that define the physics behaviour of an element
    /// </summary>
    public class Physics : ImmlElement
    {
        #region Properties
        private bool _Enabled;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Physics"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Enabled
        {
            get { return _Enabled; }
            set
            {
                if (_Enabled == value)
                    return;

                bool oldValue = _Enabled;

                _Enabled = value;
                base.RaisePropertyChanged("Enabled", oldValue, _Enabled, "Physics.Enabled");
            }
        }

        private bool _Movable;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Physics"/> is movable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if movable; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Movable
        {
            get { return _Movable; }
            set
            {
                if (_Movable == value)
                    return;
                bool oldValue = _Movable;

                _Movable = value;
                base.RaisePropertyChanged("Movable", oldValue, _Movable, "Physics.Movable");
            }
        }

        private float _Weight;

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public virtual float Weight
        {
            get { return _Weight; }
            set
            {
                if (_Weight == value)
                    return;

                float oldValue = _Weight;

                _Weight = value;
                base.RaisePropertyChanged("Weight", oldValue, _Weight, "Physics.Weight");
            }
        }

        private Vector3 _Centre;

        /// <summary>
        /// Gets or sets the centre.
        /// </summary>
        /// <value>
        /// The centre.
        /// </value>
        public virtual Vector3 Centre
        {
            get { return _Centre; }
            set
            {
                if (_Centre == value)
                    return;

                Vector3 oldValue = _Centre;

                _Centre = value;
                this.IsCentreDefault = false;
                base.RaisePropertyChanged("Centre", oldValue, _Centre, "Physics.Centre");
            }
        }

        /// <summary>
        /// Gets or sets the bounding.
        /// </summary>
        /// <value>
        /// The bounding.
        /// </value>
        public virtual BoundingType Bounding { get; set; }

        public bool IsCentreDefault { get; private set; } 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Physics"/> class.
        /// </summary>
        public Physics()
        {
            this.Movable = true;
            this.Bounding = BoundingType.ConvexHull;
        }
    }
}
