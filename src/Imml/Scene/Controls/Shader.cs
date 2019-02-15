using System;
using System.Collections.Generic;
using System.Text;
using Imml.ComponentModel;

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Provides support for shader based manipulation of elements.
    /// </summary>
    public class Shader : ImmlElement, IParameterHost, ISourcedElement
    {
        private bool _Enabled;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Shader"/> is enabled.
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
                base.RaisePropertyChanged("Enabled", !_Enabled, _Enabled, "Shader.Enabled");
            }
        }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public virtual string Source { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shader"/> class.
        /// </summary>
        public Shader()
        {
            this.Enabled = true;
        }
    }
}
