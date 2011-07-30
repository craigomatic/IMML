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

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Provides support for shader based manipulation of elements.
    /// </summary>
    public class Shader : ImmlElement
    {
        private bool _Enabled;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Shader"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Enabled
        {
            get { return _Enabled; }
            set
            {
                if (_Enabled == value)
                    return;

                _Enabled = value;
                base.RaisePropertyChanged("Enabled", !_Enabled, _Enabled, "Shader.Enabled");
            }
        }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public virtual string Source { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shader"/> class.
        /// </summary>
        public Shader()
        {
            this.Enabled = true;
        }
    }
}
