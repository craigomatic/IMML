﻿//-----------------------------------------------------------------------
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
using System.Linq;
using System.Text;
using Imml.Numerics;
using Imml.Drawing;
using Imml.ComponentModel;

namespace Imml.Scene.Container
{
    public class ImmlDocument : ImmlElement, IImmlContext
    {
        #region Properties 

        private Uri _HostUri;

        /// <summary>
        /// HostUri associated with this document
        /// </summary>
        public Uri HostUri
        {
            get { return _HostUri; }
            set
            {
                if (_HostUri == value)
                    return;

                _HostUri = value;
                base.RaisePropertyChanged("HostUri");
            }
        }


        private string _Camera;

        /// <summary>
        /// The default camera for this document to use
        /// </summary>
        public string Camera
        {
            get { return _Camera; }
            set
            {
                if (_Camera == value)
                    return;

                string oldValue = _Camera;
                _Camera = value;

                base.RaisePropertyChanged("Camera", oldValue, _Camera);
            }
        }        

        private string _Author;
        public string Author
        {
            get { return _Author; }
            set
            {
                if (_Author == value)
                    return;

                _Author = value;
                base.RaisePropertyChanged("Author");
            }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description == value)
                    return;

                _Description = value;
                base.RaisePropertyChanged("Description");
            }
        }


        public IList<string> Tags { get; set; }

        private string _Background;

        /// <summary>
        /// The name of the selected background
        /// </summary>
        public string Background
        {
            get { return _Background; }
            set
            {
                if (_Background == value)
                    return;

                string oldValue = _Background;
                _Background = value;
                base.RaisePropertyChanged("Background", oldValue, _Background);
            }
        }

        private float _PhysicsSpeed;

        public float PhysicsSpeed
        {
            get { return _PhysicsSpeed; }
            set
            {
                if (_PhysicsSpeed == value)
                    return;

                _PhysicsSpeed = value;
                base.RaisePropertyChanged("PhysicsSpeed");
            }
        }

        private float _SoundSpeed;

        public float SoundSpeed
        {
            get { return _SoundSpeed; }
            set
            {
                if (_SoundSpeed == value)
                    return;

                _SoundSpeed = value;
                base.RaisePropertyChanged("SoundSpeed");
            }
        }

        private float _AnimationSpeed;

        public float AnimationSpeed
        {
            get { return _AnimationSpeed; }
            set
            {
                if (_AnimationSpeed == value)
                    return;

                _AnimationSpeed = value;
                base.RaisePropertyChanged("AnimationSpeed");
            }
        }

        private Vector3 _Gravity;

        public Vector3 Gravity
        {
            get { return _Gravity; }
            set
            {
                if (_Gravity == value)
                    return;

                _Gravity = value;
                base.RaisePropertyChanged("Gravity");
            }
        }

        private Color3 _GlobalIllumination;

        public Color3 GlobalIllumination
        {
            get { return _GlobalIllumination; }
            set
            {
                if (value == null)
                    value = new Color3(0.4f, 0.4f, 0.4f);

                _GlobalIllumination = value;
                base.RaisePropertyChanged("GlobalIllumination");
            }
        }

        #endregion

        public ImmlDocument()
        {
            this.Behaviours = new List<string>();
            this.Tags = new List<string>();
        }
    }
}