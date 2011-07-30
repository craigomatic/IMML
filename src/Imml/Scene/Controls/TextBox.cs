using System;
using System.Collections.Generic;
using System.Text;
using Imml.ComponentModel;

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Provides functionality for text input.
    /// </summary>
    public class TextBox : Text
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TextBox"/> is a password textbox.
        /// </summary>
        /// <value>
        ///   <c>true</c> if password style; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Password { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of characters.
        /// </summary>
        /// <value>
        /// The max characters.
        /// </value>
        public virtual int MaxLength { get; set; }
    }
}
