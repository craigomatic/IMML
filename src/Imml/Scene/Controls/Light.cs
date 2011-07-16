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
using Imml.Drawing;
using Imml.ComponentModel;

namespace Imml.Scene.Controls
{
    public class Light : PositionalElement
    {
        #region Properties

        protected bool _Enabled;

        public virtual bool Enabled
        {
            get { return _Enabled; }
            set
            {
                if (_Enabled != value)
                {
                    _Enabled = value;
                    base.RaisePropertyChanged("Enabled", !_Enabled, _Enabled);
                }
            }
        }

        protected bool _CastShadows;

        public virtual bool CastShadows
        {
            get { return _CastShadows; }
            set
            {
                if (_CastShadows == value)
                    return;

                _CastShadows = value;
                base.RaisePropertyChanged("CastShadows", !_CastShadows, _CastShadows);
            }
        }

        protected float _InnerCone;

        /// <summary>
        /// Aka Phi
        /// </summary>
        public virtual float InnerCone
        {
            get { return _InnerCone; }
            set
            {
                if (_InnerCone != value)
                {
                    object oldValue = _InnerCone;
                    _InnerCone = value;
                    base.RaisePropertyChanged("InnerCone", oldValue, _InnerCone);
                }
            }
        }

        protected float _OuterCone;

        /// <summary>
        /// Aka Theta
        /// </summary>
        public virtual float OuterCone
        {
            get { return _OuterCone; }
            set
            {
                if (_OuterCone != value)
                {
                    object oldValue = _OuterCone;
                    _OuterCone = value;
                    base.RaisePropertyChanged("OuterCone", oldValue, _OuterCone);
                }
            }
        }

        protected float _ConstantAttenuation;

        public virtual float ConstantAttenuation
        {
            get { return _ConstantAttenuation; }
            set
            {
                if (_ConstantAttenuation != value)
                {
                    object oldValue = _ConstantAttenuation;
                    _ConstantAttenuation = value;
                    base.RaisePropertyChanged("ConstantAttenuation", oldValue, _ConstantAttenuation);
                }
            }
        }

        protected float _LinearAttenuation;

        public virtual float LinearAttenuation
        {
            get { return _LinearAttenuation; }
            set
            {
                if (_LinearAttenuation != value)
                {
                    object oldValue = _LinearAttenuation;
                    _LinearAttenuation = value;
                    base.RaisePropertyChanged("LinearAttenuation", oldValue, _LinearAttenuation);
                }
            }
        }

        protected float _QuadraticAttenuation;

        public virtual float QuadraticAttenuation
        {
            get { return _QuadraticAttenuation; }
            set
            {
                if (_QuadraticAttenuation != value)
                {
                    object oldValue = _QuadraticAttenuation;
                    _QuadraticAttenuation = value;
                    base.RaisePropertyChanged("QuadraticAttenuation", oldValue, _QuadraticAttenuation);
                }
            }
        }

        protected float _Range;

        public virtual float Range
        {
            get { return _Range; }
            set
            {
                if (_Range != value)
                {
                    object oldValue = _Range;
                    _Range = value;
                    base.RaisePropertyChanged("Range", oldValue, _Range);
                }
            }
        }

        protected float _Falloff;

        public virtual float Falloff
        {
            get { return _Falloff; }
            set
            {
                if (_Falloff != value)
                {
                    object oldValue = _Falloff;
                    _Falloff = value;
                    base.RaisePropertyChanged("Falloff", oldValue, _Falloff);
                }
            }
        }

        protected LightType _Type;

        public virtual LightType Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    object oldType = _Type;
                    _Type = value;
                    base.RaisePropertyChanged("Type", oldType, _Type);
                }
            }
        }

        protected Color3 _Diffuse;

        public virtual Color3 Diffuse
        {
            get { return _Diffuse; }
            set
            {
                if (value == null)
                    value = new Color3("#FFFFFF");

                if (_Diffuse == value)
                    return;

                object oldDiffuse = _Diffuse;
                _Diffuse = value;
                
                base.RaisePropertyChanged("Diffuse", oldDiffuse, _Diffuse);
            }
        }

        protected Color3 _Specular;

        public virtual Color3 Specular
        {
            get { return _Specular; }
            set
            {
                if (value == null)
                    value = new Color3("#FFFFFF");

                if (_Specular == value)
                    return;

                object oldSpecular = _Specular;
                _Specular = value;

                base.RaisePropertyChanged("Specular", oldSpecular, _Specular);
            }
        }

        #endregion

        public Light()
        {
            _CastShadows = true;
            _Enabled = true;
            _Range = 20;
            _Type = LightType.Point;
            _Diffuse = new Color3(0.4f, 0.4f, 0.4f);
            _Specular = new Color3(0.2f, 0.2f, 0.2f);
            _InnerCone = 10;
            _OuterCone = 100;
        }
    }
}
