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
    public class Text : PositionalElement
    {
        private TextAlignment _Alignment;

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

        public Text()
        {
            this.Opacity = 1.0f;
            this.Size = 0.2f;
            this.Alignment = TextAlignment.Left;
        }
    }
}
