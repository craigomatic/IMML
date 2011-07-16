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

namespace Imml.Numerics.Geometry
{
	using System.Diagnostics;
	using System.Globalization;
	using System.Runtime.InteropServices;

#if USE_DOUBLE_PRECISION
	using Number = System.Double;
#else
	using Number = System.Single;
#endif

	/// <summary>
	/// Representation of a line segment.
	/// </summary>
	[DebuggerDisplay("End1 = ({End1}) End2 = ({End2})")]
	public struct LineSegment : IEquatable<LineSegment>, IFormattable
	{
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="lineSegment">Line segment to copy.</param>
		public LineSegment(LineSegment lineSegment)
		{
			this.End1 = lineSegment.End1;
			this.End2 = lineSegment.End2;
		}

		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="end1">Line segment end point.</param>
		/// <param name="end2">Line segment end point.</param>
		public LineSegment(Vector3 end1, Vector3 end2)
		{
			this.End1 = end1;
			this.End2 = end2;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Determines whether the current object is equal to the specified object.
		/// </summary>
		/// <param name="value">An object.</param>
		/// <returns>True if the current object is equal to the <paramref name="value"/>, otherwise false.</returns>
		public override bool Equals(object value)
		{
			if (value is LineSegment)
			{
				return this.Equals((LineSegment)value);
			}

			return false;
		}

		/// <summary>
		/// Determines whether the current line segment is equal to the specified line segment.
		/// </summary>
		/// <param name="value">A line segment.</param>
		/// <returns>True if the current line segment is equal to the <paramref name="value"/>, otherwise false.</returns>
		public bool Equals(LineSegment value)
		{
			return
				this.End1 == value.End1 &&
				this.End2 == value.End2;
		}

		/// <summary>
		/// Returns a hash code for the current line segment.
		/// </summary>
		/// <returns>A hash code.</returns>
		/// <remarks>
		/// The hash code is not unique.
		/// If two line segments are equal, their hash codes are guaranteed to be equal.
		/// If the line segments are not equal, their hash codes are not guaranteed to be different.
		/// </remarks>
		public override int GetHashCode()
		{
			return HashCode.GetHashCode(
				this.End1.GetHashCode(),
				this.End2.GetHashCode());
		}

		/// <summary>
		/// Determines whether any of the fields of the specified line segment evaluates to an infinity.
		/// </summary>
		/// <param name="value">A line segment.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to an infinity, otherwise false.</returns>
		public static bool IsInfinity(LineSegment value)
		{
			return
				Vector3.IsInfinity(value.End1) ||
				Vector3.IsInfinity(value.End2);
		}

		/// <summary>
		/// Determines whether any of the fields of the specified line segment evaluates to a value that is not a number.
		/// </summary>
		/// <param name="value">A line segment.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to a value that is not a number, otherwise false.</returns>
		public static bool IsNaN(LineSegment value)
		{
			return
				Vector3.IsNaN(value.End1) ||
				Vector3.IsNaN(value.End2);
		}

		/// <summary>
		/// Determines whether any of the fields of the specified line segment evaluates to negative infinity.
		/// </summary>
		/// <param name="value">A line segment.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to negative infinity, otherwise false.</returns>
		public static bool IsNegativeInfinity(LineSegment value)
		{
			return
				Vector3.IsNegativeInfinity(value.End1) ||
				Vector3.IsNegativeInfinity(value.End2);
		}

		/// <summary>
		/// Determines whether any of the fields of the specified line segment evaluates to positive infinity.
		/// </summary>
		/// <param name="value">A line segment.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to positive infinity, otherwise false.</returns>
		public static bool IsPositiveInfinity(LineSegment value)
		{
			return
				Vector3.IsPositiveInfinity(value.End1) ||
				Vector3.IsPositiveInfinity(value.End2);
		}

		/// <summary>
		/// Returns the line segment parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <returns>The line segment parsed from the <paramref name="value"/>.</returns>
		public static LineSegment Parse(string value)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, null);
		}

		/// <summary>
		/// Returns the line segment parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <returns>The line segment parsed from the <paramref name="value"/>.</returns>
		public static LineSegment Parse(string value, NumberStyles numberStyle)
		{
			return Parse(value, numberStyle, null);
		}

		/// <summary>
		/// Returns the line segment parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <returns>The line segment parsed from the <paramref name="value"/>.</returns>
		public static LineSegment Parse(string value, IFormatProvider formatProvider)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider);
		}

		/// <summary>
		/// Returns the line segment parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <returns>The line segment parsed from the <paramref name="value"/>.</returns>
		public static LineSegment Parse(string value, NumberStyles numberStyle, IFormatProvider formatProvider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var numbers = Float.Parse(value, numberStyle, formatProvider);

			if (numbers.Length == ValueCount)
			{
				LineSegment result;
				result.End1.X = numbers[0];
				result.End1.Y = numbers[1];
				result.End1.Z = numbers[2];
				result.End2.X = numbers[3];
				result.End2.Y = numbers[4];
				result.End2.Z = numbers[5];
				return result;
			}
			else
			{
				throw new ValueCountMismatchException();
			}
		}

		/// <summary>
		/// Returns a string representation of the current object.
		/// </summary>
		/// <returns>A string representation of the current object.</returns>
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		/// <summary>
		/// Returns a string representation of the current object.
		/// </summary>
		/// <param name="format">Format string.</param>
		/// <returns>A string representation of the current object.</returns>
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		/// <summary>
		/// Returns a string representation of the current object.
		/// </summary>
		/// <param name="formatProvider">Format provider.</param>
		/// <returns>A string representation of the current object.</returns>
		public string ToString(IFormatProvider formatProvider)
		{
			return this.ToString(null, formatProvider);
		}

		/// <summary>
		/// Returns a string representation of the current object.
		/// </summary>
		/// <param name="format">Format string.</param>
		/// <param name="formatProvider">Format provider.</param>
		/// <returns>A string representation of the current object.</returns>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return Float.ToString(format, formatProvider, this.End1.X, this.End1.Y, this.End1.Z, this.End2.X, this.End2.Y, this.End2.Z);
		}

		/// <summary>
		/// Attempts to parse the line segment from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="result">The output variable for the line segment parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, out LineSegment result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, null, out result);
		}

		/// <summary>
		/// Attempts to parse the line segment from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="result">The output variable for the line segment parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, out LineSegment result)
		{
			return TryParse(value, numberStyle, null, out result);
		}

		/// <summary>
		/// Attempts to parse the line segment from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <param name="result">The output variable for the line segment parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, IFormatProvider formatProvider, out LineSegment result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider, out result);
		}

		/// <summary>
		/// Attempts to parse the line segment from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <param name="result">The output variable for the line segment parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, IFormatProvider formatProvider, out LineSegment result)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var numbers = Float.Parse(value, numberStyle, formatProvider);

			if (numbers.Length == ValueCount)
			{
				result.End1.X = numbers[0];
				result.End1.Y = numbers[1];
				result.End1.Z = numbers[2];
				result.End2.X = numbers[3];
				result.End2.Y = numbers[4];
				result.End2.Z = numbers[5];
				return true;
			}
			else
			{
				result = default(LineSegment);
				return false;
			}
		}

		/// <summary>
		/// Returns the current line segment with the specified fields replaced with the specified values.
		/// </summary>
		/// <param name="end1">The new value for the <see cref="End1"/> field. Use null to keep the old value.</param>
		/// <param name="end2">The new value for the <see cref="End2"/> field. Use null to keep the old value.</param>
		/// <returns>The current line segment with the specified fields replaced with the specified values.</returns>
		public LineSegment With(Vector3? end1 = null, Vector3? end2 = null)
		{
			return new LineSegment
			{
				End1 = end1 ?? this.End1,
				End2 = end2 ?? this.End2,
			};
		}
		#endregion

		#region Operators
		/// <summary>
		/// Determines whether the line segments are equal.
		/// </summary>
		/// <param name="value1">A line segment.</param>
		/// <param name="value2">A line segment.</param>
		/// <returns>True if the line segments are equal, otherwise false.</returns>
		public static bool operator ==(LineSegment value1, LineSegment value2)
		{
			return
				value1.End1 == value2.End1 &&
				value1.End2 == value2.End2;
		}

		/// <summary>
		/// Determines whether the line segments are not equal.
		/// </summary>
		/// <param name="value1">A line segment.</param>
		/// <param name="value2">A line segment.</param>
		/// <returns>True if the line segments are not equal, otherwise false.</returns>
		public static bool operator !=(LineSegment value1, LineSegment value2)
		{
			return
				value1.End1 != value2.End1 ||
				value1.End2 != value2.End2;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets a line segment with the end points set to a value that is not a number.
		/// </summary>
		/// <value>The line segment with the end points set to a value that is not a number.</value>
		public static LineSegment NaN
		{
			get { return nan; }
		}

		/// <summary>
		/// Gets a line segment with the end points set to negative infinity.
		/// </summary>
		/// <value>The line segment with the end points set to negative infinity.</value>
		public static LineSegment NegativeInfinity
		{
			get { return negativeInfinity; }
		}

		/// <summary>
		/// Gets a line segment with the end points set to positive infinity.
		/// </summary>
		/// <value>The line segment with the end points set to positive infinity.</value>
		public static LineSegment PositiveInfinity
		{
			get { return positiveInfinity; }
		}

		/// <summary>
		/// Gets a line segment with the end points set to zero.
		/// </summary>
		/// <value>The line segment with the end points set to zero.</value>
		public static LineSegment Zero
		{
			get { return zero; }
		}
		#endregion

		#region Fields
		/// <summary>
		/// Line segment end point.
		/// </summary>
		public Vector3 End1;

		/// <summary>
		/// Line segment end point.
		/// </summary>
		public Vector3 End2;

		/// <summary>
		/// Number of values in the structure.
		/// </summary>
		private const int ValueCount = 6;

		/// <summary>
		/// A line segment with the end points set to a value that is not a number.
		/// </summary>
		private static readonly LineSegment nan = new LineSegment(Vector3.NaN, Vector3.NaN);

		/// <summary>
		/// A line segment with the end points set to negative infinity.
		/// </summary>
		private static readonly LineSegment negativeInfinity = new LineSegment(Vector3.NegativeInfinity, Vector3.NegativeInfinity);

		/// <summary>
		/// A line segment with the end points set to positive infinity.
		/// </summary>
		private static readonly LineSegment positiveInfinity = new LineSegment(Vector3.PositiveInfinity, Vector3.PositiveInfinity);

		/// <summary>
		/// A line segment with the end points set to zero.
		/// </summary>
		private static readonly LineSegment zero = new LineSegment(Vector3.Zero, Vector3.Zero);
		#endregion
	}
}
