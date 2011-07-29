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

namespace Imml.Scene.Layout
{
    /// <summary>
    /// Overrides positional values for a given element with a layout instruction to dock to a particular region of the view with an optionally specified offset
    /// </summary>
    public class Dock : ImmlElement
    {
        private HorizontalAlignment _HorizontalAlignment;

        /// <summary>
        /// Gets or sets the horizontal alignment.
        /// </summary>
        /// <value>
        /// The horizontal alignment.
        /// </value>
        public HorizontalAlignment HorizontalAlignment
        {
            get { return _HorizontalAlignment; }
            set 
            {
                if (_HorizontalAlignment == value)
                    return;

                HorizontalAlignment oldValue = _HorizontalAlignment;
                _HorizontalAlignment = value;
                base.RaisePropertyChanged("HorizontalAlignment", oldValue, _HorizontalAlignment);
            }
        }

        private VerticalAlignment _VerticalAlignment;

        /// <summary>
        /// Gets or sets the vertical alignment.
        /// </summary>
        /// <value>
        /// The vertical alignment.
        /// </value>
        public VerticalAlignment VerticalAlignment
        {
            get { return _VerticalAlignment; }
            set
            {
                if (_VerticalAlignment == value)
                    return;

                VerticalAlignment oldValue = _VerticalAlignment;
                _VerticalAlignment = value;
                base.RaisePropertyChanged("VerticalAlignment", oldValue, _VerticalAlignment);
            }
        }

        private Vector3 _Offset;

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>
        /// The offset.
        /// </value>
        /// <remarks>The offset to apply during positioning where x is horizontal offset, y is vertical offset and z is depth offset</remarks>
        public Vector3 Offset
        {
            get { return _Offset; }
            set
            {
                if (_Offset == value)
                    return;

                Vector3 oldValue = _Offset;
                _Offset = value;
                base.RaisePropertyChanged("Offset", oldValue, _Offset);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dock"/> class.
        /// </summary>
        public Dock()
        {
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
        }
    }
}
