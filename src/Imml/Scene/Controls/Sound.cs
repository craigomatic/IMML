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
    public class Sound : PositionalElement
    {
        #region Properties

        private bool _Loop;

        public bool Loop
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

        public bool Stream { get; set; }

        private float _Volume;

        public float Volume
        {
            get { return _Volume; }
            set
            {
                if (_Volume == value)
                    return;
                
                float oldVolume = _Volume;
                
                _Volume = value;
                base.RaisePropertyChanged("Volume", oldVolume, _Volume);
            }
        }

        private float _Pitch;

        public float Pitch
        {
            get { return _Pitch; }
            set
            {
                if (_Pitch == value)
                    return;

                float oldPitch = _Pitch;

                _Pitch = value;
                base.RaisePropertyChanged("Pitch", oldPitch, _Pitch);
            }
        }

        private bool _Spatial;

        public bool Spatial
        {
            get { return _Spatial; }
            set
            {
                if (_Spatial == value)
                    return;

                _Spatial = value;
                base.RaisePropertyChanged("Spatial", !_Spatial, _Spatial);
            }
        }

        private string _Source;

        public string Source
        {
            get { return _Source; }
            set
            {
                if (_Source == value)
                    return;

                string oldValue = _Source;

                _Source = value;
                base.RaisePropertyChanged("Source", oldValue, _Source);
            }
        }

        #endregion

        public Sound()
        {
            this.Volume = 0.5f;
            this.Pitch = 1f;
            this.Spatial = true;
        }
    }
}
