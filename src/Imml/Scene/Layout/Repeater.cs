using System;
using System.Collections.Generic;
using System.Text;

namespace Imml.Scene.Layout
{
    /// <summary>
    /// Repeats the nested child elements the number of times specified by the Count attribute. All elements are generated with the same attributes.
    /// </summary>
    public class Repeater : ImmlElement
    {
        protected int _Count;

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        /// <remarks>Number of times to repeat the instances of the nested element</remarks>
        public virtual int Count
        {
            get { return _Count; }
            set
            {
                if (_Count == value)
                    return;

                _Count = value;
                base.RaisePropertyChanged("Count");
            }
        }
    }
}
