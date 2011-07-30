using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imml.Scene
{
    /// <summary>
    /// Provides hooks between events and the logic to execute when an event is invoked.
    /// </summary>
    public class Trigger : ImmlElement
    {
        /// <summary>
        /// Type of event the trigger is associated with
        /// </summary>
        public virtual EventType Event { get; set; }

        /// <summary>
        /// Name of target triggerable element
        /// </summary>
        public virtual string Target { get; set; }

        /// <summary>
        /// When true, events that cause the trigger to invoke should be executed
        /// </summary>
        public virtual bool Enabled { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Trigger"/> class.
        /// </summary>
        public Trigger()
        {
            this.Enabled = true;
        }
    }
}
