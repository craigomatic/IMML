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
    }
}
