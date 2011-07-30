using System;
using System.Collections.Generic;
using System.Text;

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Provides support for rendering a web resource onto a material group.
    /// </summary>
    public class Web : ImmlElement
    {
        #region Properties
        private int _UpdateRate;

        /// <summary>
        /// The period of time in milliseconds to wait before redrawing the display
        /// </summary>
        public virtual int UpdateRate
        {
            get { return _UpdateRate; }
            set
            {
                if (_UpdateRate == value)
                    return;

                int oldValue = _UpdateRate;

                _UpdateRate = value;
                base.RaisePropertyChanged("UpdateRate", oldValue, _UpdateRate);
            }
        }

        private string _Source;

        /// <summary>
        /// The source uri for the web element to display
        /// </summary>
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


        private int _Width;

        /// <summary>
        /// The width in pixels of the web display
        /// </summary>
        public virtual int Width
        {
            get { return _Width; }
            set
            {
                if (_Width == value)
                    return;

                var oldValue = _Width;
                _Width = value;
                base.RaisePropertyChanged("Width", oldValue, _Width);
            }
        }


        private int _Height;

        /// <summary>
        /// The height in pixels of the web display
        /// </summary>
        public virtual int Height
        {
            get { return _Height; }
            set
            {
                if (_Height == value)
                    return;

                var oldValue = _Height;
                _Height = value;
                base.RaisePropertyChanged("Width", oldValue, _Height);
            }
        } 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Web"/> class.
        /// </summary>
        public Web()
        {
            this.UpdateRate = -1;
            this.Width = 1024;
            this.Height = 1024;
        }     
    }
}
