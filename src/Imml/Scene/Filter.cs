using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imml.Scene
{
    public class Filter : ImmlElement
    {
        /// <summary>
        /// The target property
        /// </summary>
        public string Target { get; set; }

        public bool Enabled { get; set; }

        /// <summary>
        /// The upper limit of updates that should occur for this property per second. A value of -1 will prevent updates to the target ever being propagated
        /// </summary>
        public int UpdateRate { get; set; }

        /// <summary>
        /// The time the last update that passed through this filter was sent
        /// </summary>
        public DateTime LastSent { get; set; }
    }
}
