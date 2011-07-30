using System;
using System.Collections.Generic;
using System.Text;
using Imml.Numerics;

namespace Imml.Scene.Timers
{
    /// <summary>
    /// Represents a keyframe on a timeline
    /// </summary>
    public class KeyframeEvent : TimelineEvent
    {
        /// <summary>
        /// When true, the destination transform is relative to where it is currently
        /// </summary>
        public virtual bool Relative { get; set; }

        /// <summary>
        /// The value to apply for this keyframe event
        /// </summary>
        public virtual Vector3 Value { get; set; }

        /// <summary>
        /// The type of key framing occuring as the result of this event
        /// </summary>
        public virtual KeyFrameType Target { get; set; }

        ///// <summary>
        ///// TODO: Research the validity/benefit of this
        ///// The type of interpolation to use when moving through this keyframe
        ///// </summary>
        //public InterpolationType Interpolation { get; set; }
    }
}
