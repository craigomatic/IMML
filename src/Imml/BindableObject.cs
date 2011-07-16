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
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace Imml
{
    //CP:allow for old value and new value in the eventargs
    public class PropertyNotificationEventArgs : PropertyChangedEventArgs
    {
        /// <summary>
        /// The owner of the property
        /// </summary>
        public object Owner { get; set; }

        public object OldValue { get; set; }
        public object NewValue { get; set; }
        public string PropertyHierarchy { get; set; }

        public PropertyNotificationEventArgs(string propertyName)
            : this(propertyName, null, null)
        { }

        public PropertyNotificationEventArgs(string propertyName, object oldValue, object newValue)
            : base(propertyName)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
    }

    /// <summary>  
    /// Implements the INotifyPropertyChanged interface and 
    /// exposes a RaisePropertyChanged method for derived 
    /// classes to raise the PropertyChange event.  The event 
    /// arguments created by this class are cached to prevent 
    /// managed heap fragmentation.
    ///
    /// Written by Josh Smith
    /// URL http://joshsmithonwpf.wordpress.com/2007/08/29/a-base-class-which-implements-inotifypropertychanged/     
    /// </summary>
    public abstract class BindableObject : INotifyPropertyChanged
    {
        #region Data

        private static readonly Dictionary<string, PropertyNotificationEventArgs> eventArgCache;
        private const string ERROR_MSG = "{0} is not a public property of {1}";

        #endregion // Data

        #region Constructors

        static BindableObject()
        {
            eventArgCache = new Dictionary<string, PropertyNotificationEventArgs>();
        }

        protected BindableObject()
        {
        }

        #endregion // Constructors

        #region Public Members

        /// <summary>
        /// Raised when a public property of this object is set.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Returns an instance of PropertyChangedEventArgs for 
        /// the specified property name.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property to create event args for.
        /// </param>		
        public static PropertyNotificationEventArgs GetPropertyChangedEventArgs(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException(
                    "propertyName cannot be null or empty.");

            PropertyNotificationEventArgs args;

            // Get the event args from the cache, creating them
            // and adding to the cache if necessary.
            lock (typeof(BindableObject))
            {
                bool isCached = eventArgCache.ContainsKey(propertyName);
                if (!isCached)
                {
                    eventArgCache.Add(
                        propertyName,
                        new PropertyNotificationEventArgs(propertyName));
                }

                args = eventArgCache[propertyName];
            }

            return args;
        }

        #endregion // Public Members

        #region Protected Members

        /// <summary>
        /// Derived classes can override this method to
        /// execute logic after a property is set. The 
        /// base implementation does nothing.
        /// </summary>
        /// <param name="propertyName">
        /// The property which was changed.
        /// </param>
        protected virtual void AfterPropertyChanged(string propertyName)
        {
        }

        /// <summary>
        /// Attempts to raise the PropertyChanged event, and 
        /// invokes the virtual AfterPropertyChanged method, 
        /// regardless of whether the event was raised or not.
        /// </summary>
        /// <param name="propertyName">
        /// The property which was changed.
        /// </param>
        protected void RaisePropertyChanged(string propertyName, object oldValue, object newValue)
        {
            _RaisePropertyChanged(propertyName, propertyName, oldValue, newValue, true);
        }

        protected void RaisePropertyChanged(string propertyName, object oldValue, object newValue, string propertyHierarchy)
        {
            _RaisePropertyChanged(propertyName, propertyHierarchy, oldValue, newValue, true);
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            _RaisePropertyChanged(propertyName, propertyName, null, null, false);
        }

        private void _RaisePropertyChanged(string propertyName, string propertyHierarchy, object oldValue, object newValue, bool attachValues)
        {
            this.VerifyProperty(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                // Get the cached event args.
                PropertyNotificationEventArgs args =
                    GetPropertyChangedEventArgs(propertyName);

                args.Owner = this;

                if (attachValues)
                {
                    //CP: attach the old and new values
                    args.NewValue = newValue;
                    args.OldValue = oldValue;
                }

                args.PropertyHierarchy = propertyHierarchy;

                // Raise the PropertyChanged event.
                handler(this, args);
            }

            this.AfterPropertyChanged(propertyName);
        }

        #endregion // Protected Members

        #region Private Helpers

        [Conditional("DEBUG")]
        private void VerifyProperty(string propertyName)
        {
            Type type = this.GetType();

            // Look for a public property with the specified name.
            PropertyInfo propInfo = type.GetProperty(propertyName);

            if (propInfo == null)
            {
                // The property could not be found,
                // so alert the developer of the problem.

                string msg = string.Format(
                    ERROR_MSG,
                    propertyName,
                    type.FullName);

                Debug.Assert(false, msg);
            }
        }

        #endregion // Private Helpers
    }
}
