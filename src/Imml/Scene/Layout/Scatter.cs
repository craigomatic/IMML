using System;
using System.Collections.Generic;
using System.Text;

namespace Imml.Scene.Layout
{
    /// <summary>
    /// Arranges child elements onto a target element in a pseudo-random manner.
    /// </summary>
    public class Scatter : ImmlElement
    {
        #region Properties
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        /// <remarks>The target element to scatter upon.</remarks>
        public virtual string Target { get; set; }

        /// <summary>
        /// Gets or sets the seed.
        /// </summary>
        /// <value>
        /// The seed.
        /// </value>
        /// <remarks>The constant to use when generating the scatter positions. Enables persistent scatter on all document hosts. If not specified, consistent layout is not guaranteed.</remarks>
        public virtual int Seed { get; set; } 
        #endregion

        public Scatter()
        {
            this.Seed = new Random().Next();
        }
    }
}
