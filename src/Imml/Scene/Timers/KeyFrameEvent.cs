//-----------------------------------------------------------------------
//VastPark is a lightweight extensible virtual world platform 
//and this file is a program released under the GPL.
//Copyright (C) 2009 VastPark
//This program is free software; you can redistribute it and/or
//modify it under the terms of the GNU General Public License
//as published by the Free Software Foundation; either version 2
//of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//GNU General Public License for more details.
//You should have received a copy of the GNU General Public License
//along with this program; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
//-----------------------------------------------------------------------
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
