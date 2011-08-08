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
	/// Representation of a bounding sphere.
	/// </summary>
	[DebuggerDisplay("Position = ({Position}) Radius = {Radius}")]
	public struct BoundingSphere
	{
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="boundingSphere">Bounding sphere to copy.</param>
		public BoundingSphere(BoundingSphere boundingSphere)
		{
			this.Position = boundingSphere.Position;
			this.Radius = boundingSphere.Radius;
		}

		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="position">The center point.</param>
		/// <param name="radius">The bounding radius.</param>
		public BoundingSphere(Vector3 position, Number radius)
		{
			this.Position = position;
			this.Radius = radius;
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
			if (value is BoundingSphere)
			{
				return this.Equals((BoundingSphere)value);
			}

			return false;
		}

		/// <summary>
		/// Determines whether the current bounding sphere is equal to the specified bounding sphere.
		/// </summary>
		/// <param name="value">A bounding sphere.</param>
		/// <returns>True if the current bounding sphere is equal to the <paramref name="value"/>, otherwise false.</returns>
		public bool Equals(BoundingSphere value)
		{
			return
				this.Position == value.Position &&
				this.Radius == value.Radius;
		}

		/// <summary>
		/// Returns a hash code for the current bounding sphere.
		/// </summary>
		/// <returns>A hash code.</returns>
		/// <remarks>
		/// The hash code is not unique.
		/// If two bounding spheres are equal, their hash codes are guaranteed to be equal.
		/// If the bounding spheres are not equal, their hash codes are not guaranteed to be different.
		/// </remarks>
		public override int GetHashCode()
		{
			return HashCode.GetHashCode(
				this.Position.GetHashCode(),
				this.Radius.GetHashCode());
		}

		/// <summary>
		/// Determines whether any of the fields of the specified bounding sphere evaluates to an infinity.
		/// </summary>
		/// <param name="value">A bounding sphere.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to an infinity, otherwise false.</returns>
		public static bool IsInfinity(BoundingSphere value)
		{
			return
				Vector3.IsInfinity(value.Position) ||
				Number.IsInfinity(value.Radius);
		}

		/// <summary>
		/// Determines whether any of the fields of the specified bounding sphere evaluates to a value that is not a number.
		/// </summary>
		/// <param name="value">A bounding sphere.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to a value that is not a number, otherwise false.</returns>
		public static bool IsNaN(BoundingSphere value)
		{
			return
				Vector3.IsNaN(value.Position) ||
				Number.IsNaN(value.Radius);
		}

		/// <summary>
		/// Determines whether any of the fields of the specified bounding sphere evaluates to negative infinity.
		/// </summary>
		/// <param name="value">A bounding sphere.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to negative infinity, otherwise false.</returns>
		public static bool IsNegativeInfinity(BoundingSphere value)
		{
			return
				Vector3.IsNegativeInfinity(value.Position) ||
				Number.IsNegativeInfinity(value.Radius);
		}

		/// <summary>
		/// Determines whether any of the fields of the specified bounding sphere evaluates to positive infinity.
		/// </summary>
		/// <param name="value">A bounding sphere.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to positive infinity, otherwise false.</returns>
		public static bool IsPositiveInfinity(BoundingSphere value)
		{
			return
				Vector3.IsPositiveInfinity(value.Position) ||
				Number.IsPositiveInfinity(value.Radius);
		}

		/// <summary>
		/// Returns the smallest bounding sphere containing both of the specified bounding spheres.
		/// </summary>
		/// <param name="value1">A bouding sphere.</param>
		/// <param name="value2">A bouding sphere.</param>
		/// <returns>The smallest bounding sphere containing both of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static BoundingSphere Merge(BoundingSphere value1, BoundingSphere value2)
		{
			var separation = value2.Position - value1.Position;
			var distance = separation.Magnitude;

			if (value1.Radius - value2.Radius >= distance)
			{
				return value1;
			}
			else if (value2.Radius - value1.Radius >= distance)
			{
				return value2;
			}
			else
			{
				var radius = (distance + value1.Radius + value2.Radius) * 0.5f;
				var d = (radius - value1.Radius) / distance;

				return new BoundingSphere
				{
					Position = value1.Position + separation * d,
					Radius = radius,
				};
			}
		}

		/// <summary>
		/// Returns the bounding sphere parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <returns>The bounding sphere parsed from the <paramref name="value"/>.</returns>
		public static BoundingSphere Parse(string value)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, null);
		}

		/// <summary>
		/// Returns the bounding sphere parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <returns>The bounding sphere parsed from the <paramref name="value"/>.</returns>
		public static BoundingSphere Parse(string value, NumberStyles numberStyle)
		{
			return Parse(value, numberStyle, null);
		}

		/// <summary>
		/// Returns the bounding sphere parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <returns>The bounding sphere parsed from the <paramref name="value"/>.</returns>
		public static BoundingSphere Parse(string value, IFormatProvider formatProvider)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider);
		}

		/// <summary>
		/// Returns the bounding sphere parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <returns>The bounding sphere parsed from the <paramref name="value"/>.</returns>
		public static BoundingSphere Parse(string value, NumberStyles numberStyle, IFormatProvider formatProvider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var numbers = Float.Parse(value, numberStyle, formatProvider);

			if (numbers.Length == ValueCount)
			{
				BoundingSphere result;
				result.Position.X = numbers[0];
				result.Position.Y = numbers[1];
				result.Position.Z = numbers[2];
				result.Radius = numbers[3];
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
			return Float.ToString(format, formatProvider, this.Position.X, this.Position.Y, this.Position.Z, this.Radius);
		}

		/// <summary>
		/// Attempts to parse the bounding sphere from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="result">The output variable for the bounding sphere parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, out BoundingSphere result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, null, out result);
		}

		/// <summary>
		/// Attempts to parse the bounding sphere from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="result">The output variable for the bounding sphere parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, out BoundingSphere result)
		{
			return TryParse(value, numberStyle, null, out result);
		}

		/// <summary>
		/// Attempts to parse the bounding sphere from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <param name="result">The output variable for the bounding sphere parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, IFormatProvider formatProvider, out BoundingSphere result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider, out result);
		}

		/// <summary>
		/// Attempts to parse the bounding sphere from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <param name="result">The output variable for the bounding sphere parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, IFormatProvider formatProvider, out BoundingSphere result)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var numbers = Float.Parse(value, numberStyle, formatProvider);

			if (numbers.Length == ValueCount)
			{
				result.Position.X = numbers[0];
				result.Position.Y = numbers[1];
				result.Position.Z = numbers[2];
				result.Radius = numbers[3];
				return true;
			}
			else
			{
				result = default(BoundingSphere);
				return false;
			}
		}
		#endregion

		#region Operators
		/// <summary>
		/// Determines whether the bounding spheres are equal.
		/// </summary>
		/// <param name="value1">A bounding sphere.</param>
		/// <param name="value2">A bounding sphere.</param>
		/// <returns>True if the bounding spheres are equal, otherwise false.</returns>
		public static bool operator ==(BoundingSphere value1, BoundingSphere value2)
		{
			return
				value1.Position == value2.Position &&
				value1.Radius == value2.Radius;
		}

		/// <summary>
		/// Determines whether the bounding spheres are not equal.
		/// </summary>
		/// <param name="value1">A bounding sphere.</param>
		/// <param name="value2">A bounding sphere.</param>
		/// <returns>True if the bounding spheres are not equal, otherwise false.</returns>
		public static bool operator !=(BoundingSphere value1, BoundingSphere value2)
		{
			return
				value1.Position != value2.Position ||
				value1.Radius != value2.Radius;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets a bounding sphere with the fields set to a value that is not a number.
		/// </summary>
		/// <value>The bounding sphere with the fields set to a value that is not a number.</value>
		public static BoundingSphere NaN
		{
			get { return nan; }
		}

		/// <summary>
		/// Gets a bounding sphere with the fields set to negative infinity.
		/// </summary>
		/// <value>The bounding sphere with the fields set to negative infinity.</value>
		public static BoundingSphere NegativeInfinity
		{
			get { return negativeInfinity; }
		}

		/// <summary>
		/// Gets a bounding sphere with the fields set to positive infinity.
		/// </summary>
		/// <value>The bounding sphere with the fields set to positive infinity.</value>
		public static BoundingSphere PositiveInfinity
		{
			get { return positiveInfinity; }
		}

		/// <summary>
		/// Gets a bounding sphere with the fields set to zero.
		/// </summary>
		/// <value>The bounding sphere with the fields set to zero.</value>
		public static BoundingSphere Zero
		{
			get { return zero; }
		}
		#endregion

		#region Fields
		/// <summary>
		/// Center point.
		/// </summary>
		public Vector3 Position;

		/// <summary>
		/// Bounding radius.
		/// </summary>
		public Number Radius;

		/// <summary>
		/// Number of values in the structure.
		/// </summary>
		private const int ValueCount = 4;

		/// <summary>
		/// A bounding sphere with the fields set to a value that is not a number.
		/// </summary>
		private static readonly BoundingSphere nan = new BoundingSphere(Vector3.NaN, Number.NaN);

		/// <summary>
		/// A bounding sphere with the fields set to negative infinity.
		/// </summary>
		private static readonly BoundingSphere negativeInfinity = new BoundingSphere(Vector3.NegativeInfinity, Number.NegativeInfinity);

		/// <summary>
		/// A bounding sphere with the fields set to positive infinity.
		/// </summary>
		private static readonly BoundingSphere positiveInfinity = new BoundingSphere(Vector3.PositiveInfinity, Number.PositiveInfinity);

		/// <summary>
		/// A bounding sphere with the fields set to zero.
		/// </summary>
		private static readonly BoundingSphere zero = new BoundingSphere(Vector3.Zero, 0);
		#endregion
	}
}
