using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imml.Scene
{
    public class Network : ImmlElement
    {
        public string Owner { get; set; }

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
                base.RaisePropertyChanged("Enabled", oldValue, _Enabled, "Network.Enabled");
            }
        }

        public Dictionary<string, Filter> Filters { get; set; }
    }
}
