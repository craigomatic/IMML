//-----------------------------------------------------------------------
//VastPark is a lightweight extensible virtual world platform 
//and this file is a program released under the GPL.
//Copyright (C) 2009 VastPark
//This program is free software; you can redistribute it and/or
//modify it under the terms of the GNU General Public License
//as published by the Free Software Foundation; either version 2
//of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//GNU General Public License for more details.
//You should have received a copy of the GNU General Public License
//along with this program; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
//-----------------------------------------------------------------------
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
