using System;
using System.Collections.Generic;
using System.Text;
using Imml.ComponentModel;

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Container for Emitter elements.
    /// </summary>
    public class Effect : VisibleElement
    {
        #region Properties
        
        protected bool _Enabled;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Effect"/> is enabled.
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
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Effect"/> class.
        /// </summary>
        public Effect()
        {
            this.Enabled = true;
        }
    }
}
