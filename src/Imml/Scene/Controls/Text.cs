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
using Imml.Drawing;

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Provides functionality for spatial display of text.
    /// </summary>
    public class Text : PositionalElement
    {
        private TextAlignment _Alignment;

        /// <summary>
        /// Gets or sets the alignment.
        /// </summary>
        /// <value>
        /// The alignment.
        /// </value>
        public virtual TextAlignment Alignment
        {
            get { return _Alignment; }
            set
            {
                if (_Alignment == value)
                {
                    return;
                }

                var oldValue = _Alignment;
                _Alignment = value;

                base.RaisePropertyChanged("Alignment", oldValue, _Alignment);
            }
        }

        private string _Value;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public virtual string Value
        {
            get { return _Value; }
            set
            {
                if (_Value == value)
                    return;

                string oldValue = _Value;
                _Value = value;

                base.RaisePropertyChanged("Value", oldValue, _Value);
            }
        }

        private float _Opacity;

        /// <summary>
        /// Gets or sets the opacity.
        /// </summary>
        /// <value>
        /// The opacity.
        /// </value>
        /// <remarks>A value between 0 and 1 describing the amount of opacity</remarks>
        public virtual float Opacity
        {
            get { return _Opacity; }
            set
            {
                if (_Opacity == value)
                {
                    return;
                }
                var oldOpacity = _Opacity;
                _Opacity = value;
                base.RaisePropertyChanged("Opacity", oldOpacity, _Opacity);
            }
        }

        private Color3 _Colour;

        /// <summary>
        /// Gets or sets the colour.
        /// </summary>
        /// <value>
        /// The colour.
        /// </value>
        public virtual Color3 Colour
        {
            get { return _Colour; }
            set
            {
                if (_Colour == value)
                {
                    return;
                }
                var oldColour = _Colour;
                _Colour = value;
                base.RaisePropertyChanged("Colour", oldColour, _Colour);
            }
        }

        private float _Size;

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        /// <remarks>The size of the font in units</remarks>
        public virtual float Size
        {
            get { return _Size; }
            set
            {
                if (_Size == value)
                {
                    return;
                }
                var oldSize = _Size;
                _Size = value;
                base.RaisePropertyChanged("Size", oldSize, _Size);
            }
        }

        private bool _Bold;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Text"/> is bold.
        /// </summary>
        /// <value>
        ///   <c>true</c> if bold; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Bold
        {
            get { return _Bold; }
            set
            {
                if (_Bold == value)
                {
                    return;
                }

                _Bold = value;
                base.RaisePropertyChanged("Colour", !_Bold, _Bold);
            }
        }

        private bool _Underline;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Text"/> is underlined.
        /// </summary>
        /// <value>
        ///   <c>true</c> if underlined; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Underline
        {
            get { return _Underline; }
            set
            {
                if (_Underline == value)
                {
                    return;
                }

                _Underline = value;
                base.RaisePropertyChanged("Colour", !_Underline, _Underline);
            }
        }

        private bool _Italic;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Text"/> is italic.
        /// </summary>
        /// <value>
        ///   <c>true</c> if italic; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Italic
        {
            get { return _Italic; }
            set
            {
                if (_Italic == value)
                {
                    return;
                }

                _Italic = value;
                base.RaisePropertyChanged("Colour", !_Italic, _Italic);
            }
        }

        private bool _Billboard;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Text"/> should be billboarded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if billboarded; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Billboard
        {
            get { return _Billboard; }
            set
            {
                if (_Billboard == value)
                {
                    return;
                }

                _Billboard = value;
                base.RaisePropertyChanged("Colour", !_Billboard, _Billboard);
            }
        }

        private string _Font;

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        /// <value>
        /// The font.
        /// </value>
        /// <remarks>The name of the font to use. Valid values depend on the supported fonts on the client system.</remarks>
        public virtual string Font
        {
            get { return _Font; }
            set
            {
                if (_Font == value)
                    return;

                string oldValue = _Font;
                _Font = value;

                base.RaisePropertyChanged("Font", oldValue, _Font);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class.
        /// </summary>
        public Text()
        {
            this.Opacity = 1.0f;
            this.Size = 0.2f;
            this.Alignment = TextAlignment.Left;
        }
    }
}
