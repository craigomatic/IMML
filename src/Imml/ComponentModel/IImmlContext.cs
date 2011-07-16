using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imml.ComponentModel
{
    /// <summary>
    /// Represents a self-contained IMML context
    /// </summary>
    public interface IImmlContext : IImmlElement
    {
        /// <summary>
        /// Gets the author.
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        IList<string> Tags { get; }
    }
}
