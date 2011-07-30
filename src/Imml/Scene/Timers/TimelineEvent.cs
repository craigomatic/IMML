using System;
using System.Collections.Generic;
using System.Text;

namespace Imml.Scene.Timers
{
    /// <summary>
    /// Abstract class representing an event with a time and a target element.
    /// </summary>
    public abstract class TimelineEvent : ImmlElement
    {
        /// <summary>
        /// Gets or sets the time the event will execute
        /// </summary>
        public virtual TimeSpan Time { get; set; }

        /// <summary>
        /// Gets or sets the target element
        /// </summary>
        public virtual string Element { get; set; }
    }
}
