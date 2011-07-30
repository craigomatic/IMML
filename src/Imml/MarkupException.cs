using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imml
{
    /// <summary>
    /// Represents errors that are related to a markup validation failure.
    /// </summary>
    public class MarkupException : Exception
    {
        /// <summary>
        /// Gets the line number.
        /// </summary>
        public int LineNumber { get; private set; }

        /// <summary>
        /// Gets the line position.
        /// </summary>
        public int LinePosition { get; private set; }

        /// <summary>
        /// Gets the error text.
        /// </summary>
        public string ErrorText { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkupException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public MarkupException(string message)
            : base(message)
        {
            this.ErrorText = ErrorText;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkupException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="line">The line.</param>
        /// <param name="position">The position.</param>
        public MarkupException(string message, int line, int position)
            :base(message)
        {
            this.LineNumber = line;
            this.LinePosition = position;
        }
    }
}
