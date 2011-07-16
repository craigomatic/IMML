using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imml.Numerics;

namespace Imml.Scene.Controls
{
    public class Emitter : ImmlElement
    {
        public Emitter()
        {
            this.LifeMin = 1;
            this.LifeMax = 3;
            this.Force = new Vector3(0, 1, 0);
            this.Rate = 50;
            this.Count = 1000;
        }

        protected float _Rate;

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
    }
}
