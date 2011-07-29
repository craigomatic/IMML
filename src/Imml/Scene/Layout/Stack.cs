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
using Imml.ComponentModel;
using Imml.Numerics;

namespace Imml.Scene.Layout
{
    /// <summary>
    /// Allows elements to be stacked together according to the specified spacing.
    /// </summary>
    public class Stack : VisibleElement
    {
        protected Vector3 _Spacing;

        /// <summary>
        /// Gets or sets the spacing.
        /// </summary>
        /// <value>
        /// The spacing.
        /// </value>
        /// <remarks>The amount of spacing to apply between each child element of the stack</remarks>
        public Vector3 Spacing
        {
            get { return _Spacing; }
            set
            {
                if (_Spacing == value)
                    return;

                _Spacing = value;
                base.RaisePropertyChanged("Spacing");
            }
        }
    }
}
