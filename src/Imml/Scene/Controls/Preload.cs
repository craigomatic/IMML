using System;
using System.Collections.Generic;
using System.Text;

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Instructs the loader to load child elements of this node before continuing to load the remainder of the document.
    /// </summary>
    public class Preload : ImmlElement
    {
        private string _ProgressUpdate;

        /// <summary>
        /// Gets or sets the progress update.
        /// </summary>
        /// <value>
        /// The progress update.
        /// </value>
        /// <remarks>The name of the executable element to invoke on a progress update</remarks>
        public virtual string ProgressUpdate
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

        /// <summary>
        /// Gets or sets the document loaded.
        /// </summary>
        /// <value>
        /// The document loaded.
        /// </value>
        /// <remarks>The name of the executable element to invoke when the document has finished loading</remarks>
        public virtual string DocumentLoaded
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
