using Imml.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Provides support for script based logic.
    /// </summary>
    public class Script : ImmlElement, ISourcedElement
    {
        /// <summary>
        /// Gets or sets the code value for the script
        /// </summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// Gets or sets the optional source for the script
        /// </summary>
        public virtual string Source { get; set; }

        /// <summary>
        /// Gets or sets the language the script is written in
        /// </summary>
        public virtual string Language { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Script"/> class.
        /// </summary>
        public Script()
        {
            this.Language = "Lua";
        }
    }
}
