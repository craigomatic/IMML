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

namespace Imml.Scene.Timers
{
    public class Timeline : ImmlElement
    {
        private bool _Enabled;

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

        private float _Speed;

        public virtual float Speed
        {
            get { return _Speed; }
            set
            {
                //prevent invalid speed values
                if (float.IsNaN(value) || float.IsInfinity(value) || value == 0)
                    value = 1f;

                if (_Speed == value)
                    return;
                
                float oldValue = _Speed;
                _Speed = value;

                base.RaisePropertyChanged("Speed", oldValue, _Speed);
            }
        }

        private bool _Loop;

        public virtual bool Loop
        {
            get { return _Loop; }
            set
            {
                if (_Loop == value)
                    return;

                _Loop = value;
                base.RaisePropertyChanged("Loop", !_Loop, _Loop);
            }
        }

        private bool _AutoTween;

        public virtual bool AutoTween
        {
            get { return _AutoTween; }
            set
            {
                if (_AutoTween == value)
                    return;

                _AutoTween = value;
                base.RaisePropertyChanged("AutoTween", !_AutoTween, _AutoTween);
            }
        }
    }
}
