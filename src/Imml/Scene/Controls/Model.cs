using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Imml.ComponentModel;

namespace Imml.Scene.Controls
{
    /// <summary>
    /// Provides functionality for displaying mesh data.
    /// </summary>
    public class Model : CubicElement, IPhysicsHostElement, IMaterialHostElement, ISourcedElement
    {
        #region Properties

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public virtual string Source { get; set; }

        #endregion        
    }
}
