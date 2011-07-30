using System;
using System.Collections.Generic;
using System.Text;
using Imml.Numerics;
using Imml.ComponentModel;

namespace Imml.Scene.Layout
{
    /// <summary>
    /// Overrides positional values for a given element with a layout instruction to dock to a particular region of the view with an optionally specified offset
    /// </summary>
    public class Dock : ImmlElement, INetworkHostElement
    {
        #region Properties
        private HorizontalAlignment _HorizontalAlignment;

        /// <summary>
        /// Gets or sets the horizontal alignment.
        /// </summary>
        /// <value>
        /// The horizontal alignment.
        /// </value>
        public virtual HorizontalAlignment HorizontalAlignment
        {
            get { return _HorizontalAlignment; }
            set
            {
                if (_HorizontalAlignment == value)
                    return;

                HorizontalAlignment oldValue = _HorizontalAlignment;
                _HorizontalAlignment = value;
                base.RaisePropertyChanged("HorizontalAlignment", oldValue, _HorizontalAlignment);
            }
        }

        private VerticalAlignment _VerticalAlignment;

        /// <summary>
        /// Gets or sets the vertical alignment.
        /// </summary>
        /// <value>
        /// The vertical alignment.
        /// </value>
        public virtual VerticalAlignment VerticalAlignment
        {
            get { return _VerticalAlignment; }
            set
            {
                if (_VerticalAlignment == value)
                    return;

                VerticalAlignment oldValue = _VerticalAlignment;
                _VerticalAlignment = value;
                base.RaisePropertyChanged("VerticalAlignment", oldValue, _VerticalAlignment);
            }
        }

        private Vector3 _Offset;

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>
        /// The offset.
        /// </value>
        /// <remarks>The offset to apply during positioning where x is horizontal offset, y is vertical offset and z is depth offset</remarks>
        public virtual Vector3 Offset
        {
            get { return _Offset; }
            set
            {
                if (_Offset == value)
                    return;

                Vector3 oldValue = _Offset;
                _Offset = value;
                base.RaisePropertyChanged("Offset", oldValue, _Offset);
            }
        } 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Dock"/> class.
        /// </summary>
        public Dock()
        {
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
        }
    }
}
