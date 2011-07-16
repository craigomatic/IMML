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
using System.Text;

namespace Imml.Scene.Controls
{
    public class Preload : ImmlElement
    {
        private string _ProgressUpdate;

        public string ProgressUpdate
        {
            get { return _ProgressUpdate; }
            set 
            {
                if (_ProgressUpdate == value)
                    return;

                _ProgressUpdate = value;
                base.RaisePropertyChanged("ProgressUpdate");
            }
        }

        private string _DocumentLoaded;

        public string DocumentLoaded
        {
            get { return _DocumentLoaded; }
            set
            {
                if (_DocumentLoaded == value)
                    return;

                _DocumentLoaded = value;
                base.RaisePropertyChanged("DocumentLoaded");
            }
        }
    }
}