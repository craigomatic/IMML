using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imml.Numerics;

namespace Imml.Scene
{
    public class Physics : ImmlElement
    {
        private bool _Enabled;

        public bool Enabled
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

        public bool Movable
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

        public float Weight
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

        public Vector3 Centre
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

        public BoundingType Bounding { get; set; }

        public bool IsCentreDefault { get; private set; }
    }
}
