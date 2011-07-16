using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imml
{
    /// <summary>
    /// Maintains a registry of elements to guarantee unique names.
    /// </summary>
    public interface IImmlElementRegistry
    {
        /// <summary>
        /// Gets the elements.
        /// </summary>
        IDictionary<int, ImmlElement> Elements { get; }

        /// <summary>
        /// Adds the specified element into the registry, modifying it's name if it is not valid.
        /// </summary>
        /// <param name="element">The element.</param>
        void Add(ImmlElement element);

        /// <summary>
        /// Determines whether the registry contains an element with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if the specified name contains name; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsName(string name);

        /// <summary>
        /// Determines whether the registry contains an element with the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        ///   <c>true</c> if the specified id contains id; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsID(int id);

        /// <summary>
        /// Removes the specified element from the registry, freeing up the name used by it.
        /// </summary>
        /// <param name="element">The element.</param>
        void Remove(ImmlElement element);

        /// <summary>
        /// Tries the name of the get element by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        bool TryGetElementByName<T>(string name, out T element) where T : ImmlElement;

        /// <summary>
        /// Tries the get element by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The id.</param>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        bool TryGetElementById<T>(int id, out T element) where T : ImmlElement;        
    }
}
