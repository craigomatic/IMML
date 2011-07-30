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
    /// Provides support for embedding video resources into a material group.
    /// </summary>
    public class Video : ImmlElement
    {
        #region Properties
        private string _Source;

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public virtual string Source
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

        protected TimeSpan _Seek;

        /// <summary>
        /// Gets or sets the seek.
        /// </summary>
        /// <value>
        /// The seek.
        /// </value>
        public virtual TimeSpan Seek
        {
            get { return _Seek; }
            set
            {
                if (_Seek == value)
                    return;

                TimeSpan oldValue = _Seek;
                _Seek = value;
                this.SeekRequested = true;
                base.RaisePropertyChanged("Seek", oldValue, _Seek);
                this.SeekRequested = false;
            }
        }

        /// <summary>
        /// Returns true if a seek has been requested
        /// </summary>
        public bool SeekRequested { get; private set; }

        private bool _Enabled;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Video"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>When enabled the video should begin playback from the current seek position.</remarks>
        public virtual bool Enabled
        {
            get { return _Enabled; }
            set
            {
                if (_Enabled == value)
                    return;

                _Enabled = value;
                base.RaisePropertyChanged("Enabled", !_Enabled, _Enabled, "Video.Enabled");
            }
        }

        private bool _Stream;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Video"/> is stream.
        /// </summary>
        /// <value>
        ///   <c>true</c> if stream; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Stream
        {
            get { return _Stream; }
            set
            {
                if (_Stream == value)
                    return;

                _Stream = value;
                base.RaisePropertyChanged("Stream", !_Stream, _Stream);
            }
        }

        private float _Volume;

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        public virtual float Volume
        {
            get { return _Volume; }
            set
            {
                if (_Volume == value)
                    return;

                value = _ValidateVolume(value);

                _Volume = value;
                base.RaisePropertyChanged("Volume");
            }
        }

        private float _ValidateVolume(float value)
        {
            if (value < 0)
                return 0;

            if (value > 1)
                return 1;

            return value;
        }

        private bool _Loop;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Video"/> should loop.
        /// </summary>
        /// <value>
        ///   <c>true</c> if looped; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Loop
        {
            get { return _Loop; }
            set
            {
                if (_Loop == value)
                    return;

                _Loop = value;
                base.RaisePropertyChanged("Loop", !_Loop, _Loop, "Video.Loop");
            }
        } 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Video"/> class.
        /// </summary>
        public Video()
        {
            this.Volume = 0.5f;
            this.Enabled = true;
            this.Seek = TimeSpan.Zero;
        }
    }
}
