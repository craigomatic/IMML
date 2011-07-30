using System;
using System.Collections.Generic;
using System.Text;
using Imml.ComponentModel;
using Imml.Numerics;

namespace Imml.Scene.Layout
{
    /// <summary>
    /// Allows elements to be stacked together according to the specified spacing.
    /// </summary>
    public class Stack : VisibleElement
    {
        protected Vector3 _Spacing;

        /// <summary>
        /// Gets or sets the spacing.
        /// </summary>
        /// <value>
        /// The spacing.
        /// </value>
        /// <remarks>The amount of spacing to apply between each child element of the stack</remarks>
        public virtual Vector3 Spacing
        {
            get { return _Spacing; }
            set
            {
                if (_Spacing == value)
                    return;

                _Spacing = value;
                base.RaisePropertyChanged("Spacing");
            }
        }
    }
}
