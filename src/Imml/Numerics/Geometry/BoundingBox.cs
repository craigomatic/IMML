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
	/// Representation of an axis-aligned bounding box.
	/// </summary>
	[DebuggerDisplay("Minimum = ({Minimum}) Maximum = ({Maximum})")]
	public struct BoundingBox
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="boundingBox">Bounding box to copy.</param>
		public BoundingBox(BoundingBox boundingBox)
		{
			this.Minimum = boundingBox.Minimum;
			this.Maximum = boundingBox.Maximum;
		}

		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="minimum">The minimum corner.</param>
		/// <param name="maximum">The maximum corner.</param>
		public BoundingBox(Vector3 minimum, Vector3 maximum)
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
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
			if (value is BoundingBox)
			{
				return this.Equals((BoundingBox)value);
			}

			return false;
		}

		/// <summary>
		/// Determines whether the current bounding box is equal to the specified bounding box.
		/// </summary>
		/// <param name="value">A bounding box.</param>
		/// <returns>True if the current bounding box is equal to the <paramref name="value"/>, otherwise false.</returns>
		public bool Equals(BoundingBox value)
		{
			return
				this.Minimum == value.Minimum &&
				this.Maximum == value.Maximum;
		}

		/// <summary>
		/// Returns a hash code for the current bounding box.
		/// </summary>
		/// <returns>A hash code.</returns>
		/// <remarks>
		/// The hash code is not unique.
		/// If two bounding boxes are equal, their hash codes are guaranteed to be equal.
		/// If the bounding boxes are not equal, their hash codes are not guaranteed to be different.
		/// </remarks>
		public override int GetHashCode()
		{
			return HashCode.GetHashCode(
				this.Minimum.GetHashCode(),
				this.Maximum.GetHashCode());
		}

		/// <summary>
		/// Determines whether any of the fields of the specified bounding box evaluates to an infinity.
		/// </summary>
		/// <param name="value">A bounding box.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to an infinity, otherwise false.</returns>
		public static bool IsInfinity(BoundingBox value)
		{
			return
				Vector3.IsInfinity(value.Minimum) ||
				Vector3.IsInfinity(value.Maximum);
		}

		/// <summary>
		/// Determines whether any of the fields of the specified bounding box evaluates to a value that is not a number.
		/// </summary>
		/// <param name="value">A bounding box.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to a value that is not a number, otherwise false.</returns>
		public static bool IsNaN(BoundingBox value)
		{
			return
				Vector3.IsNaN(value.Minimum) ||
				Vector3.IsNaN(value.Maximum);
		}

		/// <summary>
		/// Determines whether any of the fields of the specified bounding box evaluates to negative infinity.
		/// </summary>
		/// <param name="value">A bounding box.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to negative infinity, otherwise false.</returns>
		public static bool IsNegativeInfinity(BoundingBox value)
		{
			return
				Vector3.IsNegativeInfinity(value.Minimum) ||
				Vector3.IsNegativeInfinity(value.Maximum);
		}

		/// <summary>
		/// Determines whether any of the fields of the specified bounding box evaluates to positive infinity.
		/// </summary>
		/// <param name="value">A bounding box.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to positive infinity, otherwise false.</returns>
		public static bool IsPositiveInfinity(BoundingBox value)
		{
			return
				Vector3.IsPositiveInfinity(value.Minimum) ||
				Vector3.IsPositiveInfinity(value.Maximum);
		}

		/// <summary>
		/// Returns the smallest bounding box containing both of the specified bounding boxes.
		/// </summary>
		/// <param name="value1">A bounding box.</param>
		/// <param name="value2">A bounding box.</param>
		/// <returns>The smallest bounding box containing both of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static BoundingBox Merge(BoundingBox value1, BoundingBox value2)
		{
			return new BoundingBox
			{
				Minimum = Vector3.Min(value1.Minimum, value2.Minimum),
				Maximum = Vector3.Max(value1.Maximum, value2.Maximum),
			};
		}

		/// <summary>
		/// Returns the bounding box parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <returns>The bounding box parsed from the <paramref name="value"/>.</returns>
		public static BoundingBox Parse(string value)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, null);
		}

		/// <summary>
		/// Returns the bounding box parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <returns>The bounding box parsed from the <paramref name="value"/>.</returns>
		public static BoundingBox Parse(string value, NumberStyles numberStyle)
		{
			return Parse(value, numberStyle, null);
		}

		/// <summary>
		/// Returns the bounding box parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <returns>The bounding box parsed from the <paramref name="value"/>.</returns>
		public static BoundingBox Parse(string value, IFormatProvider formatProvider)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider);
		}

		/// <summary>
		/// Returns the bounding box parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <returns>The bounding box parsed from the <paramref name="value"/>.</returns>
		public static BoundingBox Parse(string value, NumberStyles numberStyle, IFormatProvider formatProvider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var numbers = Float.Parse(value, numberStyle, formatProvider);

			if (numbers.Length == ValueCount)
			{
				BoundingBox result;
				result.Minimum.X = numbers[0];
				result.Minimum.Y = numbers[1];
				result.Minimum.Z = numbers[2];
				result.Maximum.X = numbers[3];
				result.Maximum.Y = numbers[4];
				result.Maximum.Z = numbers[5];
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
			return Float.ToString(format, formatProvider, this.Minimum.X, this.Minimum.Y, this.Minimum.Z, this.Maximum.X, this.Maximum.Y, this.Maximum.Z);
		}

		/// <summary>
		/// Attempts to parse the bounding box from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="result">The output variable for the bounding box parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, out BoundingBox result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, null, out result);
		}

		/// <summary>
		/// Attempts to parse the bounding box from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="result">The output variable for the bounding box parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, out BoundingBox result)
		{
			return TryParse(value, numberStyle, null, out result);
		}

		/// <summary>
		/// Attempts to parse the bounding box from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <param name="result">The output variable for the bounding box parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, IFormatProvider formatProvider, out BoundingBox result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider, out result);
		}

		/// <summary>
		/// Attempts to parse the bounding box from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <param name="result">The output variable for the bounding box parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, IFormatProvider formatProvider, out BoundingBox result)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var numbers = Float.Parse(value, numberStyle, formatProvider);

			if (numbers.Length == ValueCount)
			{
				result.Minimum.X = numbers[0];
				result.Minimum.Y = numbers[1];
				result.Minimum.Z = numbers[2];
				result.Maximum.X = numbers[3];
				result.Maximum.Y = numbers[4];
				result.Maximum.Z = numbers[5];
				return true;
			}
			else
			{
				result = default(BoundingBox);
				return false;
			}
		}

		/// <summary>
		/// Returns the current bounding box with the specified fields replaced with the specified values.
		/// </summary>
		/// <param name="minimum">The new value for the <see cref="Minimum"/> field. Use null to keep the old value.</param>
		/// <param name="maximum">The new value for the <see cref="Maximum"/> field. Use null to keep the old value.</param>
		/// <returns>The current bounding box with the specified fields replaced with the specified values.</returns>
		public BoundingBox With(Vector3? minimum = null, Vector3? maximum = null)
		{
			return new BoundingBox
			{
				Minimum = minimum ?? this.Minimum,
				Maximum = maximum ?? this.Maximum,
			};
		}
		#endregion

		#region Operators
		/// <summary>
		/// Determines whether the bounding boxes are equal.
		/// </summary>
		/// <param name="value1">A bounding box.</param>
		/// <param name="value2">A bounding box.</param>
		/// <returns>True if the bounding boxes are equal, otherwise false.</returns>
		public static bool operator ==(BoundingBox value1, BoundingBox value2)
		{
			return
				value1.Minimum == value2.Minimum &&
				value1.Maximum == value2.Maximum;
		}

		/// <summary>
		/// Determines whether the bounding boxes are not equal.
		/// </summary>
		/// <param name="value1">A bounding box.</param>
		/// <param name="value2">A bounding box.</param>
		/// <returns>True if the bounding boxes are not equal, otherwise false.</returns>
		public static bool operator !=(BoundingBox value1, BoundingBox value2)
		{
			return
				value1.Minimum != value2.Minimum ||
				value1.Maximum != value2.Maximum;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets a bounding box with the corners set to a value that is not a number.
		/// </summary>
		/// <value>The bounding box with the corners set to a value that is not a number.</value>
		public static BoundingBox NaN
		{
			get { return nan; }
		}

		/// <summary>
		/// Gets a bounding box with the corners set to negative infinity.
		/// </summary>
		/// <value>The bounding box with the corners set to negative infinity.</value>
		public static BoundingBox NegativeInfinity
		{
			get { return negativeInfinity; }
		}

		/// <summary>
		/// Gets a bounding box with the corners set to positive infinity.
		/// </summary>
		/// <value>The bounding box with the corners set to positive infinity.</value>
		public static BoundingBox PositiveInfinity
		{
			get { return positiveInfinity; }
		}

		/// <summary>
		/// Gets a bounding box with the corners set to zero.
		/// </summary>
		/// <value>The bounding box with the corners set to zero.</value>
		public static BoundingBox Zero
		{
			get { return zero; }
		}
		#endregion

		#region Fields
		/// <summary>
		/// Minimum corner.
		/// </summary>
		public Vector3 Minimum;

		/// <summary>
		/// Maximum corner.
		/// </summary>
		public Vector3 Maximum;

		/// <summary>
		/// Number of values in the structure.
		/// </summary>
		private const int ValueCount = 6;

		/// <summary>
		/// A bounding box with the corners set to a value that is not a number.
		/// </summary>
		private static readonly BoundingBox nan = new BoundingBox(Vector3.NaN, Vector3.NaN);

		/// <summary>
		/// A bounding box with the corners set to negative infinity.
		/// </summary>
		private static readonly BoundingBox negativeInfinity = new BoundingBox(Vector3.NegativeInfinity, Vector3.NegativeInfinity);

		/// <summary>
		/// A bounding box with the corners set to positive infinity.
		/// </summary>
		private static readonly BoundingBox positiveInfinity = new BoundingBox(Vector3.PositiveInfinity, Vector3.PositiveInfinity);

		/// <summary>
		/// A bounding box with the corners set to zero.
		/// </summary>
		private static readonly BoundingBox zero = new BoundingBox(Vector3.Zero, Vector3.Zero);
		#endregion
	}
}
