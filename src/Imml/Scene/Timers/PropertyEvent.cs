using System;
using System.Collections.Generic;
using System.Text;

namespace Imml.Scene.Timers
{
    /// <summary>
    /// Event that manipulates a property.
    /// </summary>
    public class PropertyEvent : TimelineEvent
    {
        /// <summary>
        /// The target property
        /// </summary>
        public virtual string Target { get; set; }

        /// <summary>
        /// The target property value
        /// </summary>
        public virtual string Value { get; set; }
    }
}
