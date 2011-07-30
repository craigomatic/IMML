using System;
using System.Collections.Generic;
using System.Text;
using Imml.Drawing;

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Represents a background resource for the document
    /// </summary>
    public class Background : ImmlElement
    {
        #region Properties
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public virtual string Source { get; set; }

        /// <summary>
        /// Gets or sets the colour.
        /// </summary>
        /// <value>
        /// The colour.
        /// </value>
        public virtual Color3 Colour { get; set; } 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Background"/> class.
        /// </summary>
        public Background()
        {
            this.Colour = new Color3("#000000");
        }
    }
}
