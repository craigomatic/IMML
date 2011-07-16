// Copyright (c) 2011 Vesa Tuomiaro

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

namespace Imml.Numerics
{
	using System.Globalization;
	using System.Runtime.InteropServices;
	using System.Text;
	using System.Xml;

#if USE_DOUBLE_PRECISION
	using Integer = System.Int64;
	using Number = System.Double;
#else
	using Integer = System.Int32;
	using Number = System.Single;
#endif

	/// <summary>
	/// Provides methods for working with floating point numbers.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	internal struct Float
	{
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="number">Floating point number.</param>
		public Float(Number number) : this()
		{
			this.number = number;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Determines whether the specified numbers are equal or nearly equal.
		/// </summary>
		/// <param name="value1">Number to compare.</param>
		/// <param name="value2">Number to compare.</param>
		/// <returns>True if the numbers are equal or nearly equal, otherwise false.</returns>
		public static bool Near(Number value1, Number value2)
		{
			var n1 = new Float(value1).bits;

			if (n1 < 0)
			{
				n1 = Integer.MinValue - n1;
			}

			var n2 = new Float(value2).bits;
			
			if (n2 < 0)
			{
				n2 = Integer.MinValue - n2;
			}
			
			return Math.Abs(n1 - n2) < 10;
		}

		/// <summary>
		/// Parses an array of floating point numbers from a string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each floating point number.</param>
		/// <param name="formatProvider">The format provider for each floating point number.</param>
		/// <returns>An array of floating point numbers.</returns>
		public static Number[] Parse(string value, NumberStyles numberStyle, IFormatProvider formatProvider)
		{
			var strings = value.Split(whitespace, StringSplitOptions.RemoveEmptyEntries);
			var numbers = new Number[strings.Length];

			for (var i = 0; i < strings.Length; i++)
			{
				numbers[i] = Integer.Parse(value, numberStyle, formatProvider);
			}

			return numbers;
		}

		/// <summary>
		/// Attempts to parse an array of floating point numbers from a string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each floating point number.</param>
		/// <param name="formatProvider">The format provider for each floating point number.</param>
		/// <returns>An array of floating point numbers.</returns>
		public static Number[] TryParse(string value, NumberStyles numberStyle, IFormatProvider formatProvider)
		{
			var strings = value.Split(whitespace, StringSplitOptions.RemoveEmptyEntries);
			var numbers = new Number[strings.Length];

			for (var i = 0; i < strings.Length; i++)
			{
				if (!Number.TryParse(value, numberStyle, formatProvider, out numbers[i]))
				{
					return empty;
				}
			}

			return numbers;
		}

		/// <summary>
		/// Formats an array of floating point numbers as a string.
		/// </summary>
		/// <param name="format">Format string.</param>
		/// <param name="formatProvider">Format provider.</param>
		/// <param name="numbers">An array of floating point numbers.</param>
		/// <returns>The numbers formatted with the <paramref name="format"/> and the <paramref name="formatProvider"/>.</returns>
		public static string ToString(string format, IFormatProvider formatProvider, params Number[] numbers)
		{
			var s = new StringBuilder();

			for (var i = 0; i < numbers.Length; i++)
			{
				if (i > 0)
				{
					s.Append(',');
				}

				s.Append(numbers[i].ToString(format, formatProvider));
			}

			return s.ToString();
		}

		/// <summary>
		/// Reads an array of floating point numbers from xml string.
		/// </summary>
		/// <param name="reader">Xml reader.</param>
		/// <returns>An array of floating point numbers.</returns>
		public static Number[] ReadXml(XmlReader reader)
		{
			var strings = reader.ReadContentAsString().Split(whitespace, StringSplitOptions.RemoveEmptyEntries);
			var numbers = new Number[strings.Length];

			for (var i = 0; i < strings.Length; i++)
			{
#if USE_DOUBLE_PRECISION
				numbers[i] = XmlConvert.ToDouble(strings[i]);
#else
				numbers[i] = XmlConvert.ToSingle(strings[i]);
#endif
			}

			return numbers;
		}

		/// <summary>
		/// Writes an array of floating point numbers to xml string.
		/// </summary>
		/// <param name="writer">Xml writer.</param>
		/// <param name="numbers">An array of floating point numbers.</param>
		public static void WriteXml(XmlWriter writer, params Number[] numbers)
		{
			for (var i = 1; i < numbers.Length; i++)
			{
				if (i > 0)
				{
					writer.WriteString(" ");
				}

				writer.WriteString(XmlConvert.ToString(numbers[i]));
			}
		}
		#endregion

		#region Fields
		/// <summary>
		/// Whitespace characters.
		/// </summary>
		private static readonly char[] whitespace = { ' ', '\t', '\r', '\n' };

		/// <summary>
		/// Empty floating point number array.
		/// </summary>
		private static readonly Number[] empty = { };

		/// <summary>
		/// Floating point number.
		/// </summary>
		[FieldOffset(0)]
		private readonly Number number;

		/// <summary>
		/// Bit representation of the floating point number.
		/// </summary>
		[FieldOffset(0)]
		private readonly Integer bits;
		#endregion
	}
}
