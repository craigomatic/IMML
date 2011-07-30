using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imml.ComponentModel;

namespace Imml.Scene.Container
{
    /// <summary>
    /// A light-weight document container that allows for a combination of logic and visual presentation.
    /// </summary>
    /// <remarks>Similar to the IMML element, but is assumed to never be the main document context of a scene, rather a child context of an IMML document element.</remarks>
    public class ImmlWidget : ImmlElement, IImmlContext
    {
        /// <summary>
        /// Gets the author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        public IList<string> Tags { get; set; }

        public ImmlWidget()
        {
            this.Tags = new List<string>();
        }
    }
}
