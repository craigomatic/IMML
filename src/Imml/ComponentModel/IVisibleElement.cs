using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imml.ComponentModel
{
    /// <summary>
    /// Represents elements that have a visual appearance.
    /// </summary>
    public interface IVisibleElement : IPositionalElement
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IVisibleElement"/> is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if visible; otherwise, <c>false</c>.
        /// </value>
        bool Visible { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is visible based its position in the scene hierarchy.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        bool IsVisible { get; }
    }
}
