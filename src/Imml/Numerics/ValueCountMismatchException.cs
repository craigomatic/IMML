﻿// Copyright (c) 2011 Vesa Tuomiaro

// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
using System;
using System.Runtime.Serialization;

namespace Imml.Numerics
{
	/// <summary>
	/// The exception that is thrown when the input string contains wrong number of values.
	/// </summary>
	public class ValueCountMismatchException : FormatException
	{
		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public ValueCountMismatchException() : this("Invalid number of values in the input string.") { }

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		/// <param name="message">Exception message.</param>
		public ValueCountMismatchException(string message) : base(message) { }

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		/// <param name="message">Exception message.</param>
		/// <param name="inner">Inner exception.</param>
		public ValueCountMismatchException(string message, Exception inner) : base(message, inner) { }
	}
}
