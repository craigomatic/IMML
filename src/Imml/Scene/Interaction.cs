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
using System.Linq;
using System.Text;

namespace Imml.Scene
{
    /// <summary>
    /// Defines physics material interaction properties between physics enabled elements.
    /// </summary>
    public class Interaction : ImmlElement
    {
        /// <summary>
        /// Name of the element to apply this interaction with
        /// </summary>
        public string Element { get; set; }

        private float _StaticFriction;

        /// <summary>
        /// Gets or sets the static friction.
        /// </summary>
        /// <value>
        /// The static friction.
        /// </value>
        public float StaticFriction
        {
            get { return _StaticFriction; }
            set
            {
                if (_StaticFriction == value)
                {
                    return;
                }

                var oldValue = _StaticFriction;
                _StaticFriction = value;
                base.RaisePropertyChanged("StaticFriction", oldValue, value);
            }
        }

        private float _DynamicFriction;

        /// <summary>
        /// Gets or sets the dynamic friction.
        /// </summary>
        /// <value>
        /// The dynamic friction.
        /// </value>
        public float DynamicFriction
        {
            get { return _DynamicFriction; }
            set
            {
                if (_DynamicFriction == value)
                {
                    return;
                }

                var oldValue = _DynamicFriction;
                _DynamicFriction = value;
                base.RaisePropertyChanged("DynamicFriction", oldValue, value);
            }
        }

        private float _Elasticity;

        /// <summary>
        /// Gets or sets the elasticity.
        /// </summary>
        /// <value>
        /// The elasticity.
        /// </value>
        public float Elasticity
        {
            get { return _Elasticity; }
            set
            {
                if (_Elasticity == value)
                {
                    return;
                }

                var oldValue = _Elasticity;
                _Elasticity = value;
                base.RaisePropertyChanged("Elasticity", oldValue, value);
            }
        }

        private float _Softness;

        /// <summary>
        /// Gets or sets the softness.
        /// </summary>
        /// <value>
        /// The softness.
        /// </value>
        public float Softness
        {
            get { return _Softness; }
            set
            {
                if (_Softness == value)
                {
                    return;
                }

                var oldValue = _Softness;
                _Softness = value;
                base.RaisePropertyChanged("Softness", oldValue, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Interaction"/> class.
        /// </summary>
        public Interaction()
        {
            this.StaticFriction = 0.9f;
            this.DynamicFriction = 0.5f;
            this.Elasticity = 0.4f;
            this.Softness = 0.1f;
        }
    }
}
