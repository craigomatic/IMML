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

namespace Imml.Scene.Layout
{
    /// <summary>
    /// Repeats the nested child elements the number of times specified by the Count attribute. All elements are generated with the same attributes.
    /// </summary>
    public class Repeater : ImmlElement
    {
        protected int _Count;

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        /// <remarks>Number of times to repeat the instances of the nested element</remarks>
        public virtual int Count
        {
            get { return _Count; }
            set
            {
                if (_Count == value)
                    return;

                _Count = value;
                base.RaisePropertyChanged("Count");
            }
        }
    }
}
