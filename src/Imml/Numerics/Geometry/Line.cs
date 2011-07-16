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
	/// Representation of a line.
	/// </summary>
	[DebuggerDisplay("Point1 = ({Point1}) Point2 = ({Point2})")]
	public struct Line : IEquatable<Line>, IFormattable
	{
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="line">Line to copy.</param>
		public Line(Line line)
		{
			this.Point1 = line.Point1;
			this.Point2 = line.Point2;
		}

		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="point1">A point on the line.</param>
		/// <param name="point2">A point on the line.</param>
		public Line(Vector3 point1, Vector3 point2)
		{
			this.Point1 = point1;
			this.Point2 = point2;
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
			if (value is Line)
			{
				return this.Equals((Line)value);
			}

			return false;
		}

		/// <summary>
		/// Determines whether the current line is equal to the specified line.
		/// </summary>
		/// <param name="value">A line.</param>
		/// <returns>True if the current line is equal to the <paramref name="value"/>, otherwise false.</returns>
		public bool Equals(Line value)
		{
			return
				this.Point1 == value.Point1 &&
				this.Point2 == value.Point2;
		}

		/// <summary>
		/// Returns a hash code for the current line.
		/// </summary>
		/// <returns>A hash code.</returns>
		/// <remarks>
		/// The hash code is not unique.
		/// If two lines are equal, their hash codes are guaranteed to be equal.
		/// If the lines are not equal, their hash codes are not guaranteed to be different.
		/// </remarks>
		public override int GetHashCode()
		{
			return HashCode.GetHashCode(
				this.Point1.GetHashCode(),
				this.Point2.GetHashCode());
		}

		/// <summary>
		/// Determines whether any of the fields of the specified line evaluates to an infinity.
		/// </summary>
		/// <param name="value">A line.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to an infinity, otherwise false.</returns>
		public static bool IsInfinity(Line value)
		{
			return
				Vector3.IsInfinity(value.Point1) ||
				Vector3.IsInfinity(value.Point2);
		}

		/// <summary>
		/// Determines whether any of the fields of the specified line evaluates to a value that is not a number.
		/// </summary>
		/// <param name="value">A line.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to a value that is not a number, otherwise false.</returns>
		public static bool IsNaN(Line value)
		{
			return
				Vector3.IsNaN(value.Point1) ||
				Vector3.IsNaN(value.Point2);
		}

		/// <summary>
		/// Determines whether any of the fields of the specified line evaluates to negative infinity.
		/// </summary>
		/// <param name="value">A line.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to negative infinity, otherwise false.</returns>
		public static bool IsNegativeInfinity(Line value)
		{
			return
				Vector3.IsNegativeInfinity(value.Point1) ||
				Vector3.IsNegativeInfinity(value.Point2);
		}

		/// <summary>
		/// Determines whether any of the fields of the specified line evaluates to positive infinity.
		/// </summary>
		/// <param name="value">A line.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to positive infinity, otherwise false.</returns>
		public static bool IsPositiveInfinity(Line value)
		{
			return
				Vector3.IsPositiveInfinity(value.Point1) ||
				Vector3.IsPositiveInfinity(value.Point2);
		}

		/// <summary>
		/// Returns the line parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <returns>The line parsed from the <paramref name="value"/>.</returns>
		public static Line Parse(string value)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, null);
		}

		/// <summary>
		/// Returns the line parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <returns>The line parsed from the <paramref name="value"/>.</returns>
		public static Line Parse(string value, NumberStyles numberStyle)
		{
			return Parse(value, numberStyle, null);
		}

		/// <summary>
		/// Returns the line parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <returns>The line parsed from the <paramref name="value"/>.</returns>
		public static Line Parse(string value, IFormatProvider formatProvider)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider);
		}

		/// <summary>
		/// Returns the line parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <returns>The line parsed from the <paramref name="value"/>.</returns>
		public static Line Parse(string value, NumberStyles numberStyle, IFormatProvider formatProvider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var numbers = Float.Parse(value, numberStyle, formatProvider);

			if (numbers.Length == ValueCount)
			{
				Line result;
				result.Point1.X = numbers[0];
				result.Point1.Y = numbers[1];
				result.Point1.Z = numbers[2];
				result.Point2.X = numbers[3];
				result.Point2.Y = numbers[4];
				result.Point2.Z = numbers[5];
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
			return Float.ToString(format, formatProvider, this.Point1.X, this.Point1.Y, this.Point1.Z, this.Point2.X, this.Point2.Y, this.Point2.Z);
		}

		/// <summary>
		/// Attempts to parse the line from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="result">The output variable for the line parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, out Line result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, null, out result);
		}

		/// <summary>
		/// Attempts to parse the line from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="result">The output variable for the line parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, out Line result)
		{
			return TryParse(value, numberStyle, null, out result);
		}

		/// <summary>
		/// Attempts to parse the line from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <param name="result">The output variable for the line parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, IFormatProvider formatProvider, out Line result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider, out result);
		}

		/// <summary>
		/// Attempts to parse the line from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <param name="result">The output variable for the line parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, IFormatProvider formatProvider, out Line result)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var numbers = Float.Parse(value, numberStyle, formatProvider);

			if (numbers.Length == ValueCount)
			{
				result.Point1.X = numbers[0];
				result.Point1.Y = numbers[1];
				result.Point1.Z = numbers[2];
				result.Point2.X = numbers[3];
				result.Point2.Y = numbers[4];
				result.Point2.Z = numbers[5];
				return true;
			}
			else
			{
				result = default(Line);
				return false;
			}
		}

		/// <summary>
		/// Returns the current line with the specified fields replaced with the specified values.
		/// </summary>
		/// <param name="point1">The new value for the <see cref="Point1"/> field. Use null to keep the old value.</param>
		/// <param name="point2">The new value for the <see cref="Point2"/> field. Use null to keep the old value.</param>
		/// <returns>The current line with the specified fields replaced with the specified values.</returns>
		public Line With(Vector3? point1 = null, Vector3? point2 = null)
		{
			return new Line
			{
				Point1 = point1 ?? this.Point1,
				Point2 = point2 ?? this.Point2,
			};
		}
		#endregion

		#region Operators
		/// <summary>
		/// Determines whether the lines are equal.
		/// </summary>
		/// <param name="value1">A line.</param>
		/// <param name="value2">A line.</param>
		/// <returns>True if the lines are equal, otherwise false.</returns>
		public static bool operator ==(Line value1, Line value2)
		{
			return
				value1.Point1 == value2.Point1 &&
				value1.Point2 == value2.Point2;
		}

		/// <summary>
		/// Determines whether the lines are not equal.
		/// </summary>
		/// <param name="value1">A line.</param>
		/// <param name="value2">A line.</param>
		/// <returns>True if the lines are not equal, otherwise false.</returns>
		public static bool operator !=(Line value1, Line value2)
		{
			return
				value1.Point1 != value2.Point1 ||
				value1.Point2 != value2.Point2;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets a line with the points set to a value that is not a number.
		/// </summary>
		/// <value>The line with the points set to a value that is not a number.</value>
		public static Line NaN
		{
			get { return nan; }
		}

		/// <summary>
		/// Gets a line with the points set to negative infinity.
		/// </summary>
		/// <value>The line with the points set to negative infinity.</value>
		public static Line NegativeInfinity
		{
			get { return negativeInfinity; }
		}

		/// <summary>
		/// Gets a line with the points set to positive infinity.
		/// </summary>
		/// <value>The line with the points set to positive infinity.</value>
		public static Line PositiveInfinity
		{
			get { return positiveInfinity; }
		}

		/// <summary>
		/// Gets a line with the points set to zero.
		/// </summary>
		/// <value>The line with the points set to zero.</value>
		public static Line Zero
		{
			get { return zero; }
		}
		#endregion

		#region Fields
		/// <summary>
		/// Point on the line.
		/// </summary>
		public Vector3 Point1;

		/// <summary>
		/// Point on the line.
		/// </summary>
		public Vector3 Point2;

		/// <summary>
		/// Number of values in the structure.
		/// </summary>
		private const int ValueCount = 6;

		/// <summary>
		/// A line with the points set to a value that is not a number.
		/// </summary>
		private static readonly Line nan = new Line(Vector3.NaN, Vector3.NaN);

		/// <summary>
		/// A line with the points set to negative infinity.
		/// </summary>
		private static readonly Line negativeInfinity = new Line(Vector3.NegativeInfinity, Vector3.NegativeInfinity);

		/// <summary>
		/// A line with the points set to positive infinity.
		/// </summary>
		private static readonly Line positiveInfinity = new Line(Vector3.PositiveInfinity, Vector3.PositiveInfinity);

		/// <summary>
		/// A line with the points set to zero.
		/// </summary>
		private static readonly Line zero = new Line(Vector3.Zero, Vector3.Zero);
		#endregion
	}
}
