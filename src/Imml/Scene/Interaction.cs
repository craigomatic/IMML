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

namespace Imml.Scene
{
    public class Interaction : ImmlElement
    {
        /// <summary>
        /// Name of the element to apply this interaction with
        /// </summary>
        public string Element { get; set; }

        public float StaticFriction { get; set; }
        public float DynamicFriction { get; set; }
        public float Elasticity { get; set; }
        public float Softness { get; set; }

        public Interaction()
        {
            this.StaticFriction = 0.9f;
            this.DynamicFriction = 0.5f;
            this.Elasticity = 0.4f;
            this.Softness = 0.1f;
        }
    }
}
