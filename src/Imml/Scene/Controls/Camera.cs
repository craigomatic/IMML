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
using Imml.Numerics;

namespace Imml.Scene.Controls
{
    public class Camera : PositionalElement
    {
        #region Properties

        protected bool _Enabled;

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

        protected float _FOV;

        public virtual float FOV
        {
            get { return _FOV; }
            set
            {
                if (_FOV == value)
                    return;

                float oldValue = _FOV;
                _FOV = value;
                base.RaisePropertyChanged("FOV", oldValue, _FOV);
            }
        }

        protected string _ChaseTarget;

        public virtual string ChaseTarget
        {
            get { return _ChaseTarget; }
            set
            {
                if (_ChaseTarget == value)
                    return;

                string oldValue = _ChaseTarget;
                _ChaseTarget = value;
                base.RaisePropertyChanged("ChaseTarget", oldValue, _ChaseTarget);
            }
        }

        protected Vector3 _ChasePositionOffset;

        public virtual Vector3 ChasePositionOffset
        {
            get { return _ChasePositionOffset; }
            set
            {
                if (_ChasePositionOffset == value)
                    return;

                Vector3 oldValue = _ChasePositionOffset;
                _ChasePositionOffset = value;
                base.RaisePropertyChanged("ChasePositionOffset", oldValue, _ChasePositionOffset);
            }
        }

        protected Vector3 _ChaseLookAtOffset;

        public virtual Vector3 ChaseLookAtOffset
        {
            get { return _ChaseLookAtOffset; }
            set
            {
                if (_ChaseLookAtOffset == value)
                    return;

                Vector3 oldValue = _ChaseLookAtOffset;
                _ChaseLookAtOffset = value;
                base.RaisePropertyChanged("ChaseLookAtOffset", oldValue, _ChaseLookAtOffset);
            }
        }

        protected float _ChaseDamping;

        public virtual float ChaseDamping
        {
            get { return _ChaseDamping; }
            set
            {
                if (_ChaseDamping == value)
                    return;

                float oldValue = _ChaseDamping;
                _ChaseDamping = value;
                base.RaisePropertyChanged("ChaseDamping", oldValue, _ChaseDamping);
            }
        }

        protected float _ChaseStiffness;

        public virtual float ChaseStiffness
        {
            get { return _ChaseStiffness; }
            set
            {
                if (_ChaseStiffness == value)
                    return;

                float oldValue = _ChaseStiffness;
                _ChaseStiffness = value;
                base.RaisePropertyChanged("ChaseStiffness", oldValue, _ChaseStiffness);
            }
        }

        protected float _ChaseMass;

        public virtual float ChaseMass
        {
            get { return _ChaseMass; }
            set
            {
                if (_ChaseMass == value)
                    return;

                float oldValue = _ChaseMass;
                _ChaseMass = value;
                base.RaisePropertyChanged("ChaseMass", oldValue, _ChaseMass);
            }
        }

        protected ProjectionType _Projection;

        public virtual ProjectionType Projection
        {
            get { return _Projection; }
            set
            {
                if (_Projection == value)
                    return;

                ProjectionType oldValue = _Projection;
                _Projection = value;
                base.RaisePropertyChanged("Projection", oldValue, _Projection);
            }
        }

        protected float _NearPlane;

        public virtual float NearPlane
        {
            get
            {
                return _NearPlane;
            }

            set
            {
                if (_NearPlane == value)
                    return;

                float oldValue = _NearPlane;
                _NearPlane = value;
                base.RaisePropertyChanged("NearPlane", oldValue, _NearPlane);
            }
        }

        protected float _FarPlane;

        public virtual float FarPlane
        {
            get
            {
                return _FarPlane;
            }

            set
            {
                if (_FarPlane == value)
                    return;

                float oldValue = _FarPlane;
                _FarPlane = value;
                base.RaisePropertyChanged("FarPlane", oldValue, _FarPlane);
            }
        }

        #endregion

        public Camera()
        {
            this.FOV = 60;
            this.Projection = ProjectionType.Perspective;
            this.NearPlane = 0.01f;
            this.FarPlane = 1000f;
        }
    }
}
