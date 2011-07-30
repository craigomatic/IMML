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
