using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Imml.IO
{
    /// <summary>
    /// Provides support for reading and writting IMML to and from streams.
    /// </summary>
    public interface IImmlSerialiser
    {
        /// <summary>
        /// Gets or sets a value indicating whether to omit the XML declaration.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the XML declaration should be omitted; otherwise, <c>false</c>.
        /// </value>
        bool OmitXmlDeclaration { get; set; }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        IList<MarkupException> Errors { get; }

        /// <summary>
        /// Gets the IMML namespace.
        /// </summary>
        string Namespace { get; }

        /// <summary>
        /// Reads the specified file path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        T Read<T>(string filePath) where T : IImmlElement;

        /// <summary>
        /// Reads the specified stream.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        T Read<T>(Stream stream) where T : IImmlElement;

        /// <summary>
        /// Writes the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        string Write(IImmlElement element);

        /// <summary>
        /// Writes the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="attributeSortComparer">The attribute sort comparer.</param>
        /// <returns></returns>
        string Write(IImmlElement element, IComparer<string> attributeSortComparer);

        /// <summary>
        /// Writes the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="outputStream">The output stream.</param>
        void Write(IImmlElement element, Stream outputStream);

        /// <summary>
        /// Writes the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="outputStream">The output stream.</param>
        /// <param name="attributeSortComparer">The attribute sort comparer.</param>
        void Write(IImmlElement element, Stream outputStream, IComparer<string> attributeSortComparer);
    }
}
