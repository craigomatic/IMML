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
        /// <param name="outputStream">The output stream.</param>
        void Write(IImmlElement element, Stream outputStream);
    }
}
