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

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Container for Emitter elements.
    /// </summary>
    public class Effect : VisibleElement
    {
        #region Properties
        
        protected bool _Enabled;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Effect"/> is enabled.
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
                base.RaisePropertyChanged("Enabled", !_Enabled, _Enabled);
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Effect"/> class.
        /// </summary>
        public Effect()
        {
            this.Enabled = true;
        }
    }
}
