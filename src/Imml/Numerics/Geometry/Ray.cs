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
	/// Representation of a ray.
	/// </summary>
	[DebuggerDisplay("Position = ({Position}) Direction = ({Direction})")]
	public struct Ray : IEquatable<Ray>, IFormattable
	{
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="ray">Ray to copy.</param>
		public Ray(Ray ray)
		{
			this.Position = ray.Position;
			this.Direction = ray.Direction;
		}

		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="position">Ray starting point.</param>
		/// <param name="direction">Ray direction vector.</param>
		public Ray(Vector3 position, Vector3 direction)
		{
			this.Position = position;
			this.Direction = direction;
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
			if (value is Ray)
			{
				return this.Equals((Ray)value);
			}

			return false;
		}

		/// <summary>
		/// Determines whether the current ray is equal to the specified ray.
		/// </summary>
		/// <param name="value">A ray.</param>
		/// <returns>True if the current ray is equal to the <paramref name="value"/>, otherwise false.</returns>
		public bool Equals(Ray value)
		{
			return
				this.Position == value.Position &&
				this.Direction == value.Direction;
		}

		/// <summary>
		/// Returns a hash code for the current ray.
		/// </summary>
		/// <returns>A hash code.</returns>
		/// <remarks>
		/// The hash code is not unique.
		/// If two rays are equal, their hash codes are guaranteed to be equal.
		/// If the rays are not equal, their hash codes are not guaranteed to be different.
		/// </remarks>
		public override int GetHashCode()
		{
			return HashCode.GetHashCode(
				this.Position.GetHashCode(),
				this.Direction.GetHashCode());
		}

		/// <summary>
		/// Determines whether any of the fields of the specified ray evaluates to an infinity.
		/// </summary>
		/// <param name="value">A ray.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to an infinity, otherwise false.</returns>
		public static bool IsInfinity(Ray value)
		{
			return
				Vector3.IsInfinity(value.Position) ||
				Vector3.IsInfinity(value.Direction);
		}

		/// <summary>
		/// Determines whether any of the fields of the specified ray evaluates to a value that is not a number.
		/// </summary>
		/// <param name="value">A ray.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to a value that is not a number, otherwise false.</returns>
		public static bool IsNaN(Ray value)
		{
			return
				Vector3.IsNaN(value.Position) ||
				Vector3.IsNaN(value.Direction);
		}

		/// <summary>
		/// Determines whether any of the fields of the specified ray evaluates to negative infinity.
		/// </summary>
		/// <param name="value">A ray.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to negative infinity, otherwise false.</returns>
		public static bool IsNegativeInfinity(Ray value)
		{
			return
				Vector3.IsNegativeInfinity(value.Position) ||
				Vector3.IsNegativeInfinity(value.Direction);
		}

		/// <summary>
		/// Determines whether any of the fields of the specified ray evaluates to positive infinity.
		/// </summary>
		/// <param name="value">A ray.</param>
		/// <returns>True if any of the fields of <paramref name="value"/> evaluates to positive infinity, otherwise false.</returns>
		public static bool IsPositiveInfinity(Ray value)
		{
			return
				Vector3.IsPositiveInfinity(value.Position) ||
				Vector3.IsPositiveInfinity(value.Direction);
		}

		/// <summary>
		/// Returns a ray with the direction vector pointing to the opposite direction.
		/// </summary>
		/// <param name="value">A ray.</param>
		/// <returns>A ray with the direction vector pointing to the opposite direction.</returns>
		public static Ray Negate(Ray value)
		{
			return new Ray
			{
				Position = value.Position,
				Direction = -value.Direction,
			};
		}

		/// <summary>
		/// Returns a ray with a direction vector with the magnitude of one, pointing to the same direction as the specified ray.
		/// </summary>
		/// <param name="value">A ray.</param>
		/// <returns>A ray with a direction vector with the magnitude of one, pointing to the same direction as the <paramref name="value"/>.</returns>
		public static Ray Normalize(Ray value)
		{
			return new Ray
			{
				Position = value.Position,
				Direction = Vector3.Normalize(value.Direction),
			};
		}

		/// <summary>
		/// Returns a ray with a direction vector with the magnitude of one, pointing to the same direction as the specified ray.
		/// </summary>
		/// <param name="position">Starting point.</param>
		/// <param name="direction">Direction vector.</param>
		/// <returns>A ray with a direction vector with the magnitude of one, pointing to the same direction as the specified ray.</returns>
		public static Ray Normalize(Vector3 position, Vector3 direction)
		{
			return new Ray
			{
				Position = position,
				Direction = Vector3.Normalize(direction),
			};
		}

		/// <summary>
		/// Returns the ray parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <returns>The ray parsed from the <paramref name="value"/>.</returns>
		public static Ray Parse(string value)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, null);
		}

		/// <summary>
		/// Returns the ray parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <returns>The ray parsed from the <paramref name="value"/>.</returns>
		public static Ray Parse(string value, NumberStyles numberStyle)
		{
			return Parse(value, numberStyle, null);
		}

		/// <summary>
		/// Returns the ray parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <returns>The ray parsed from the <paramref name="value"/>.</returns>
		public static Ray Parse(string value, IFormatProvider formatProvider)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider);
		}

		/// <summary>
		/// Returns the ray parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <returns>The ray parsed from the <paramref name="value"/>.</returns>
		public static Ray Parse(string value, NumberStyles numberStyle, IFormatProvider formatProvider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var numbers = Float.Parse(value, numberStyle, formatProvider);

			if (numbers.Length == ValueCount)
			{
				Ray result;
				result.Position.X = numbers[0];
				result.Position.Y = numbers[1];
				result.Position.Z = numbers[2];
				result.Direction.X = numbers[3];
				result.Direction.Y = numbers[4];
				result.Direction.Z = numbers[5];
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
			return Float.ToString(format, formatProvider, this.Position.X, this.Position.Y, this.Position.Z, this.Direction.X, this.Direction.Y, this.Direction.Z);
		}

		/// <summary>
		/// Attempts to parse the ray from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="result">The output variable for the ray parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, out Ray result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, null, out result);
		}

		/// <summary>
		/// Attempts to parse the ray from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="result">The output variable for the ray parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, out Ray result)
		{
			return TryParse(value, numberStyle, null, out result);
		}

		/// <summary>
		/// Attempts to parse the ray from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <param name="result">The output variable for the ray parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, IFormatProvider formatProvider, out Ray result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider, out result);
		}

		/// <summary>
		/// Attempts to parse the ray from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <param name="result">The output variable for the ray parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, IFormatProvider formatProvider, out Ray result)
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
				result.Direction.X = numbers[3];
				result.Direction.Y = numbers[4];
				result.Direction.Z = numbers[5];
				return true;
			}
			else
			{
				result = default(Ray);
				return false;
			}
		}

		/// <summary>
		/// Returns the current ray with the specified fields replaced with the specified values.
		/// </summary>
		/// <param name="position">The new value for the <see cref="Position"/> field. Use null to keep the old value.</param>
		/// <param name="direction">The new value for the <see cref="Direction"/> field. Use null to keep the old value.</param>
		/// <returns>The current ray with the specified fields replaced with the specified values.</returns>
		public Ray With(Vector3? position = null, Vector3? direction = null)
		{
			return new Ray
			{
				Position = position ?? this.Position,
				Direction = direction ?? this.Direction,
			};
		}
		#endregion

		#region Operators
		/// <summary>
		/// Determines whether the rays are equal.
		/// </summary>
		/// <param name="value1">A ray.</param>
		/// <param name="value2">A ray.</param>
		/// <returns>True if the rays are equal, otherwise false.</returns>
		public static bool operator ==(Ray value1, Ray value2)
		{
			return
				value1.Position == value2.Position &&
				value1.Direction == value2.Direction;
		}

		/// <summary>
		/// Determines whether the rays are not equal.
		/// </summary>
		/// <param name="value1">A ray.</param>
		/// <param name="value2">A ray.</param>
		/// <returns>True if the rays are not equal, otherwise false.</returns>
		public static bool operator !=(Ray value1, Ray value2)
		{
			return
				value1.Position != value2.Position ||
				value1.Direction != value2.Direction;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets a ray with the position and direction set to a value that is not a number.
		/// </summary>
		/// <value>The ray with the position and direction set to a value that is not a number.</value>
		public static Ray NaN
		{
			get { return nan; }
		}

		/// <summary>
		/// Gets a ray with the position and direction set to negative infinity.
		/// </summary>
		/// <value>The ray with the position and direction set to negative infinity.</value>
		public static Ray NegativeInfinity
		{
			get { return negativeInfinity; }
		}

		/// <summary>
		/// Gets a ray with the position and direction set to positive infinity.
		/// </summary>
		/// <value>The ray with the position and direction set to positive infinity.</value>
		public static Ray PositiveInfinity
		{
			get { return positiveInfinity; }
		}

		/// <summary>
		/// Gets a ray with the position and direction set to zero.
		/// </summary>
		/// <value>The ray with the position and direction set to zero.</value>
		public static Ray Zero
		{
			get { return zero; }
		}
		#endregion

		#region Fields
		/// <summary>
		/// Ray starting point.
		/// </summary>
		public Vector3 Position;

		/// <summary>
		/// Ray direction vector.
		/// </summary>
		public Vector3 Direction;

		/// <summary>
		/// Number of values in the structure.
		/// </summary>
		private const int ValueCount = 6;

		/// <summary>
		/// A ray with the position and direction set to a value that is not a number.
		/// </summary>
		private static readonly Ray nan = new Ray(Vector3.NaN, Vector3.NaN);

		/// <summary>
		/// A ray with the position and direction set to negative infinity.
		/// </summary>
		private static readonly Ray negativeInfinity = new Ray(Vector3.NegativeInfinity, Vector3.NegativeInfinity);

		/// <summary>
		/// A ray with the position and direction set to positive infinity.
		/// </summary>
		private static readonly Ray positiveInfinity = new Ray(Vector3.PositiveInfinity, Vector3.PositiveInfinity);

		/// <summary>
		/// A ray with the position and direction set to zero.
		/// </summary>
		private static readonly Ray zero = new Ray(Vector3.Zero, Vector3.Zero);
		#endregion
	}
}
