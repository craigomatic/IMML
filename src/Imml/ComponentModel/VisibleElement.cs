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
using System.Diagnostics;
using Imml.Numerics;

namespace Imml.ComponentModel
{
    public abstract class VisibleElement : PositionalElement, IVisibleElement
    {        
        protected List<ImmlElement> _VisibleElements;

        public VisibleElement()
        {
            _VisibleElements = new List<ImmlElement>();
            _Visible = true;
        }

        public override void Add(ImmlElement element)
        {
            base.Add(element); 
            
            if (element is IVisibleElement)
            {
                _VisibleElements.Add(element);
            }
        }

        public override void Remove(ImmlElement element)
        {
            base.Remove(element);

            if (element is IVisibleElement)
            {
                _VisibleElements.Remove(element);
            }
        }

        public override void Clear()
        {
            base.Clear();

            _VisibleElements.Clear();
        }

        public override ImmlElement Parent
        {
            get
            {
                return base.Parent;
            }
            protected set
            {
                base.Parent = value;

                if (value is IVisibleElement)
                {
                    //TODO: propagate the visibility change
                }
            }
        }        

        /// <summary>
        /// Visibility setting for element collection
        /// </summary>
        protected bool _Visible;

        public virtual bool Visible
        {
            get
            {
                return _Visible;
            }
            set
            {
                if (_Visible == value)
                    return;

                _Visible = value;

                base.RaisePropertyChanged("Visible", !_Visible, _Visible);
            }
        }

        public virtual bool IsVisible
        {
            get
            {
                if (this.Parent != null && this.Parent is IVisibleElement)
                    return (this.Parent as IVisibleElement).IsVisible && _Visible;
                else
                    return this.Visible;
            }
        }       
    }
}
