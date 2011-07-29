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
    /// Provides functionality for audio playback.
    /// </summary>
    public class Sound : PositionalElement
    {
        #region Properties

        private bool _Loop;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Sound"/> should loop.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it should loop; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Sound"/> is enabled.
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

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Sound"/> should stream.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it should stream; otherwise, <c>false</c>.
        /// </value>
        public bool Stream { get; set; }

        private float _Volume;

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        /// <remarks>A value between 0 and 1 that describes the amount of volume to use, where 1 is the full amount of available volume.</remarks>
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

        /// <summary>
        /// Gets or sets the pitch.
        /// </summary>
        /// <value>
        /// The pitch.
        /// </value>
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

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Sound"/> is spatial.
        /// </summary>
        /// <value>
        ///   <c>true</c> if spatial; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>When true, the sound is positional according to the rotation and position specified</remarks>
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

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Sound"/> class.
        /// </summary>
        public Sound()
        {
            this.Volume = 0.5f;
            this.Pitch = 1f;
            this.Spatial = true;
        }
    }
}
