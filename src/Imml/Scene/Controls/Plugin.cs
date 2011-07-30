using System;
using System.Collections.Generic;
using System.Text;

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Provides support for custom extensions.
    /// </summary>
    public class Plugin : ImmlElement
    {
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public virtual string Source { get; set; }

        private bool _Enabled;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Plugin"/> is enabled.
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

                _Enabled = value;

                base.RaisePropertyChanged("Enabled", !_Enabled, _Enabled);
            }
        }
    }
}
