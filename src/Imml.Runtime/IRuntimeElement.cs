using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imml.Runtime
{
    public interface IRuntimeElement<T> : IDisposable
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
        void ApplyLayout();

        /// <summary>
        /// Acquire any resources this element is dependent on for load
        /// </summary>
        /// <returns></returns>
        Task AcquireResourcesAsync();

        /// <summary>
        /// Load the resource for this element, all resources required should have been already retrieved by <see cref="AcquireResourcesAsync"/>
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        T Load(T parentNode);
    }
}
