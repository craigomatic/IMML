using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imml.Runtime
{
    public interface IRuntimeElement<T>
    {
        /// <summary>
        /// Gets the parent of this element
        /// </summary>
        ImmlElement Parent { get; }

        T Node { get; }

        /// <summary>
        /// Calculate any layout changes that need to be applied to the <see cref="T"/>
        /// </summary>
        /// <returns></returns>
        Task ApplyLayoutAsync();

        /// <summary>
        /// Load the resource for this element, ie: download model from remote location then load bytes for draw
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        T LoadAsync(T parentNode);

        /// <summary>
        /// Release any resources used by this element
        /// </summary>
        /// <returns></returns>
        Task DestroyAsync();
    }
}
