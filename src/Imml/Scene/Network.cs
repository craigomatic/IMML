using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imml.Scene
{
    /// <summary>
    /// Provides support for enabling state change propagation.
    /// </summary>
    public class Network : ImmlElement
    {
        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        /// <remarks>An identifier that specifies the owner of a given element</remarks>
        public string Owner { get; set; }

        private bool _Enabled;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Network"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
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
    }
}
