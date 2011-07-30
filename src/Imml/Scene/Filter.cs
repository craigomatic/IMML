using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imml.Scene
{
    /// <summary>
    /// Provides support for filtering state changes of an attribute on a given element.
    /// </summary>
    public class Filter : ImmlElement
    {
        /// <summary>
        /// The target property
        /// </summary>
        public virtual string Target { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Filter"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Enabled { get; set; }

        /// <summary>
        /// The upper limit of updates that should occur for this property per second. A value of -1 will prevent updates to the target ever being propagated
        /// </summary>
        public virtual int UpdateRate { get; set; }

        /// <summary>
        /// The time the last update that passed through this filter was sent
        /// </summary>
        public DateTime LastSent { get; set; }
    }
}
