using System;
using System.Collections.Generic;
using System.Text;
using Imml.ComponentModel;

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Used to define geometric links between IMML documents or non-geometric links to a resource
    /// </summary>
    public class Anchor : PositionalElement
    {
        #region Properties
        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        public virtual string Uri { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public virtual AnchorType Type { get; set; } 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Anchor"/> class.
        /// </summary>
        public Anchor()
        {
            this.Type = AnchorType.NonGeometric;
        }
    }
}
