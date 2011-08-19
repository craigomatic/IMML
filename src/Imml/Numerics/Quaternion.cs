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
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Globalization;
	using System.Runtime.InteropServices;
	using System.Xml;
	using System.Xml.Serialization;

#if USE_DOUBLE_PRECISION
	using Number = System.Double;
#else
	using Number = System.Single;
#endif

	/// <summary>
	/// Representation of a quaternion with 4 values.
	/// </summary>
	[DebuggerDisplay("X = {X} Y = {Y} Z = {Z} W = {W} Magnitude = {Magnitude}")]
	public struct Quaternion : IComparable, IComparable<Quaternion>, IEnumerable<Number>, IEquatable<Quaternion>, IFormattable, IXmlSerializable
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="value">The value for all the components.</param>
		public Quaternion(Number value)
		{
			this.X = value;
			this.Y = value;
			this.Z = value;
			this.W = value;
		}

		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="values">A quaternion that provides the values for the X, Y, Z and W components.</param>
		public Quaternion(Quaternion values)
		{
			this.X = values.X;
			this.Y = values.Y;
			this.Z = values.Z;
			this.W = values.W;
		}

		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="x">The X component.</param>
		/// <param name="y">The Y component.</param>
		/// <param name="z">The Z component.</param>
		/// <param name="w">The W component.</param>
		public Quaternion(Number x, Number y, Number z, Number w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Returns the magnitude of the specified quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The magnitude of the <paramref name="value"/>.</returns>
		public static Number Abs(Quaternion value)
		{
			return value.Magnitude;
		}

		/// <summary>
		/// Returns the sum of the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <returns>The sum of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Quaternion Add(Quaternion value1, Quaternion value2)
		{
			return new Quaternion
			{
				X = value1.X + value2.X,
				Y = value1.Y + value2.Y,
				Z = value1.Z + value2.Z,
				W = value1.W + value2.W,
			};
		}

		/// <summary>
		/// Returns the linear blend of the specified quaternions using barycentric coordinates.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <param name="value3">A quaternion.</param>
		/// <param name="u">Barycentric coordinate u, expressing weight towards <paramref name="value2"/>.</param>
		/// <param name="v">Barycentric coordinate v, expressing weight towards <paramref name="value3"/>.</param>
		/// <returns>The linear blend of the <paramref name="value1"/>, <paramref name="value2"/> and the <paramref name="value3"/>, using barycentric coordinates <paramref name="u"/> and <paramref name="v"/>.</returns>
		public static Quaternion Barycentric(Quaternion value1, Quaternion value2, Quaternion value3, Number u, Number v)
		{
			return new Quaternion
			{
				X = value1.X + u * (value2.X - value1.X) + v * (value3.X - value1.X),
				Y = value1.Y + u * (value2.Y - value1.Y) + v * (value3.Y - value1.Y),
				Z = value1.Z + u * (value2.Z - value1.Z) + v * (value3.Z - value1.Z),
				W = value1.W + u * (value2.W - value1.W) + v * (value3.W - value1.W),
			};
		}

		/// <summary>
		/// Returns the linear blend of the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <param name="amount">Blend amount, in the range of [0, 1].</param>
		/// <returns>The linear blend of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Quaternion Blend(Quaternion value1, Quaternion value2, Number amount)
		{
			return new Quaternion
			{
				X = value1.X + amount * (value2.X - value1.X),
				Y = value1.Y + amount * (value2.Y - value1.Y),
				Z = value1.Z + amount * (value2.Z - value1.Z),
				W = value1.W + amount * (value2.W - value1.W),
			};
		}

		/// <summary>
		/// Returns the normalized linear blend of the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <param name="amount">Blend amount, in the range of [0, 1].</param>
		/// <returns>The normalized linear blend of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Quaternion BlendNormalized(Quaternion value1, Quaternion value2, Number amount)
		{
			return Normalize(
				value1.X + amount * (value2.X - value1.X),
				value1.Y + amount * (value2.Y - value1.Y),
				value1.Z + amount * (value2.Z - value1.Z),
				value1.W + amount * (value2.W - value1.W));
		}

		/// <summary>
		/// Returns the smallest integer value greater than or equal to each component of the specified quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The smallest integer value greater than or equal to each component of the <paramref name="value"/>.</returns>
		public static Quaternion Ceiling(Quaternion value)
		{
			return new Quaternion
			{
				X = (Number)Math.Ceiling(value.X),
				Y = (Number)Math.Ceiling(value.Y),
				Z = (Number)Math.Ceiling(value.Z),
				W = (Number)Math.Ceiling(value.W),
			};
		}

		/// <summary>
		/// Returns the components of the specified quaternion restricted to the specified interval.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <param name="minimum">Minimum values for the quaternion components.</param>
		/// <param name="maximum">Maximum values for the quaternion components.</param>
		/// <returns>The components of the <paramref name="value"/> restricted between the <paramref name="minimum"/> and the <paramref name="maximum"/>.</returns>
		public static Quaternion Clamp(Quaternion value, Quaternion minimum, Quaternion maximum)
		{
			return new Quaternion
			{
				X = Math.Max(minimum.X, Math.Min(value.X, maximum.X)),
				Y = Math.Max(minimum.Y, Math.Min(value.Y, maximum.Y)),
				Z = Math.Max(minimum.Z, Math.Min(value.Z, maximum.Z)),
				W = Math.Max(minimum.W, Math.Min(value.W, maximum.W)),
			};
		}

		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <param name="value">An object to compare with this object.</param>
		/// <returns>A 32-bit signed integer that indicates the relative order of the objects being compared.</returns>
		public int CompareTo(object value)
		{
			if (value is Quaternion)
			{
				return this.CompareTo((Quaternion)value);
			}

			throw new ArgumentException();
		}

		/// <summary>
		/// Compares the magnitude of the current quaternion with the magnitude of the specified quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>
		/// Zero if the magnitudes are equal.
		/// Less than zero if the magnitude of the current quaternion is less than the magnitude of the <paramref name="value"/>.
		/// Greater than zero if the magnitude of the current quaternion is greater than the magnitude of the <paramref name="value"/>.
		/// </returns>
		public int CompareTo(Quaternion value)
		{
			return this.MagnitudeSquared.CompareTo(value.MagnitudeSquared);
		}

		/// <summary>
		/// Returns the conjugate of the specified quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The conjugate of the <paramref name="value"/>.</returns>
		public static Quaternion Conjugate(Quaternion value)
		{
			return new Quaternion
			{
				X = -value.X,
				Y = -value.Y,
				Z = -value.Z,
				W = value.W,
			};
		}

		/// <summary>
		/// Returns the components of the specified quaternion divided by the specified scalar.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The components of the <paramref name="value1"/> divided by the <paramref name="value2"/>.</returns>
		public static Quaternion Divide(Quaternion value1, Number value2)
		{
			var s = 1 / value2;

			return new Quaternion
			{
				X = value1.X * s,
				Y = value1.Y * s,
				Z = value1.Z * s,
				W = value1.W * s,
			};
		}

		/// <summary>
		/// Returns the dot product of the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <returns>The dot product of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Number Dot(Quaternion value1, Quaternion value2)
		{
			return
				value1.X * value2.X +
				value1.Y * value2.Y +
				value1.Z * value2.Z +
				value1.W * value2.W;
		}

		/// <summary>
		/// Determines whether the current object is equal to the specified object.
		/// </summary>
		/// <param name="value">An object.</param>
		/// <returns>True if the current object is equal to the <paramref name="value"/>, otherwise false.</returns>
		public override bool Equals(object value)
		{
			if (value is Quaternion)
			{
				return this.Equals((Quaternion)value);
			}

			return false;
		}

		/// <summary>
		/// Determines whether the current quaternion is equal to the specified quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>True if the current quaternion is equal to the <paramref name="value"/>, otherwise false.</returns>
		public bool Equals(Quaternion value)
		{
			return
				this.X == value.X &&
				this.Y == value.Y &&
				this.Z == value.Z &&
				this.W == value.W;
		}

		/// <summary>
		/// Returns the largest integer value less than or equal to each component of the specified quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The largest integer value less than or equal to each component of the <paramref name="value"/>.</returns>
		public static Quaternion Floor(Quaternion value)
		{
			return new Quaternion
			{
				X = (Number)Math.Floor(value.X),
				Y = (Number)Math.Floor(value.Y),
				Z = (Number)Math.Floor(value.Z),
				W = (Number)Math.Floor(value.W),
			};
		}

		/// <summary>
		/// Returns the fractional part of each of the components of the specified quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The fractional part of each of the components of the <paramref name="value"/>.</returns>
		public static Quaternion Fraction(Quaternion value)
		{
			return new Quaternion
			{
				X = value.X - (Number)Math.Floor(value.X),
				Y = value.Y - (Number)Math.Floor(value.Y),
				Z = value.Z - (Number)Math.Floor(value.Z),
				W = value.W - (Number)Math.Floor(value.W),
			};
		}

		/// <summary>
		/// Returns a hash code for the current quaternion.
		/// </summary>
		/// <returns>A hash code.</returns>
		/// <remarks>
		/// The hash code is not unique.
		/// If two quaternions are equal, their hash codes are guaranteed to be equal.
		/// If the quaternions are not equal, their hash codes are not guaranteed to be different.
		/// </remarks>
		public override int GetHashCode()
		{
			return HashCode.GetHashCode(
				this.X.GetHashCode(),
				this.Y.GetHashCode(),
				this.Z.GetHashCode(),
				this.W.GetHashCode());
		}

		/// <summary>
		/// Returns an enumerator that iterates through the components.
		/// </summary>
		/// <returns>
		/// An enumerator object that can be used to iterate through the components.
		/// </returns>
		public IEnumerator<Number> GetEnumerator()
		{
			yield return this.X;
			yield return this.Y;
			yield return this.Z;
			yield return this.W;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the components.
		/// </summary>
		/// <returns>
		/// An enumerator object that can be used to iterate through the components.
		/// </returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>
		/// Determines whether any of the components of the specified quaternion evaluates to an infinity.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>True if any of the components of <paramref name="value"/> evaluates to an infinity, otherwise false.</returns>
		public static bool IsInfinity(Quaternion value)
		{
			return
				Number.IsInfinity(value.X) ||
				Number.IsInfinity(value.Y) ||
				Number.IsInfinity(value.Z) ||
				Number.IsInfinity(value.W);
		}

		/// <summary>
		/// Determines whether any of the components of the specified quaternion evaluates to a value that is not a number.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>True if any of the components of <paramref name="value"/> evaluates to a value that is not a number, otherwise false.</returns>
		public static bool IsNaN(Quaternion value)
		{
			return
				Number.IsNaN(value.X) ||
				Number.IsNaN(value.Y) ||
				Number.IsNaN(value.Z) ||
				Number.IsNaN(value.W);
		}

		/// <summary>
		/// Determines whether any of the components of the specified quaternion evaluates to negative infinity.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>True if any of the components of <paramref name="value"/> evaluates to negative infinity, otherwise false.</returns>
		public static bool IsNegativeInfinity(Quaternion value)
		{
			return
				Number.IsNegativeInfinity(value.X) ||
				Number.IsNegativeInfinity(value.Y) ||
				Number.IsNegativeInfinity(value.Z) ||
				Number.IsNegativeInfinity(value.W);
		}

		/// <summary>
		/// Determines whether any of the components of the specified quaternion evaluates to positive infinity.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>True if any of the components of <paramref name="value"/> evaluates to positive infinity, otherwise false.</returns>
		public static bool IsPositiveInfinity(Quaternion value)
		{
			return
				Number.IsPositiveInfinity(value.X) ||
				Number.IsPositiveInfinity(value.Y) ||
				Number.IsPositiveInfinity(value.Z) ||
				Number.IsPositiveInfinity(value.W);
		}

		/// <summary>
		/// Returns the larger value of each component of the spefied quaternion and the specified scalar.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The larger values of the components of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Quaternion Max(Quaternion value1, Number value2)
		{
			return new Quaternion
			{
				X = Math.Max(value1.X, value2),
				Y = Math.Max(value1.Y, value2),
				Z = Math.Max(value1.Z, value2),
				W = Math.Max(value1.W, value2),
			};
		}

		/// <summary>
		/// Returns the larger value of each component of the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <returns>The larger values of the components of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Quaternion Max(Quaternion value1, Quaternion value2)
		{
			return new Quaternion
			{
				X = Math.Max(value1.X, value2.X),
				Y = Math.Max(value1.Y, value2.Y),
				Z = Math.Max(value1.Z, value2.Z),
				W = Math.Max(value1.W, value2.W),
			};
		}

		/// <summary>
		/// Returns the smaller value of each component of the specified quaternion and the specified scalar.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The smaller values of the components of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Quaternion Min(Quaternion value1, Number value2)
		{
			return new Quaternion
			{
				X = Math.Min(value1.X, value2),
				Y = Math.Min(value1.Y, value2),
				Z = Math.Min(value1.Z, value2),
				W = Math.Min(value1.W, value2),
			};
		}

		/// <summary>
		/// Returns the smaller value of each component of the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <returns>The smaller values of the components of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Quaternion Min(Quaternion value1, Quaternion value2)
		{
			return new Quaternion
			{
				X = Math.Min(value1.X, value2.X),
				Y = Math.Min(value1.Y, value2.Y),
				Z = Math.Min(value1.Z, value2.Z),
				W = Math.Min(value1.W, value2.W),
			};
		}

		/// <summary>
		/// Returns the remainder of the components of the specified quaternion divided by the specified scalar.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The remainder of the components of the <paramref name="value1"/> divided by the <paramref name="value2"/>.</returns>
		public static Quaternion Modulo(Quaternion value1, Number value2)
		{
			return new Quaternion
			{
				X = value1.X % value2,
				Y = value1.Y % value2,
				Z = value1.Z % value2,
				W = value1.W % value2,
			};
		}

		/// <summary>
		/// Returns the components of the specified quaternion multiplied by the specified scalar.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The components of the <paramref name="value1"/> multiplied by the <paramref name="value2"/>.</returns>
		public static Quaternion Multiply(Quaternion value1, Number value2)
		{
			return new Quaternion
			{
				X = value1.X * value2,
				Y = value1.Y * value2,
				Z = value1.Z * value2,
				W = value1.W * value2,
			};
		}

		/// <summary>
		/// Returns the product of the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <returns>The product of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Quaternion Multiply(Quaternion value1, Quaternion value2)
		{
			return new Quaternion
			{
				X = value1.W * value2.X + value1.X * value2.W + value1.Y * value2.Z - value1.Z * value2.Y,
				Y = value1.W * value2.Y - value1.X * value2.Z + value1.Y * value2.W + value1.Z * value2.X,
				Z = value1.W * value2.Z + value1.X * value2.Y - value1.Y * value2.X + value1.Z * value2.W,
				W = value1.W * value2.W - value1.X * value2.X - value1.Y * value2.Y - value1.Z * value2.Z,
			};
		}

		/// <summary>
		/// Returns a quaternion with the same magnitude as the specified quaternion but pointing to the opposite direction.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>A quaternion with the same magnitude as the <paramref name="value"/> but pointing to the opposite direction.</returns>
		public static Quaternion Negate(Quaternion value)
		{
			return new Quaternion
			{
				X = -value.X,
				Y = -value.Y,
				Z = -value.Z,
				W = -value.W,
			};
		}

		/// <summary>
		/// Returns a quaternion with the magnitude of one, pointing to the same direction as the specified quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>A quaternion with the magnitude of one, pointing to the same direction as the <paramref name="value"/>.</returns>
		public static Quaternion Normalize(Quaternion value)
		{
			var scale = value.Magnitude;

			if (Float.Near(scale, 0))
			{
				return zero;
			}
			else
			{
				scale = 1 / scale;

				return new Quaternion
				{
					X = value.X * scale,
					Y = value.Y * scale,
					Z = value.Z * scale,
					W = value.W * scale,
				};
			}
		}

		/// <summary>
		/// Returns a quaternion with the magnitude of one, pointing to the same direction as the specified quaternion.
		/// </summary>
		/// <param name="x">The X component of the quaternion.</param>
		/// <param name="y">The Y component of the quaternion.</param>
		/// <param name="z">The Z component of the quaternion.</param>
		/// <param name="w">The W component of the quaternion.</param>
		/// <returns>A quaternion with the magnitude of one, pointing to the same direction as the specified quaternion.</returns>
		public static Quaternion Normalize(Number x, Number y, Number z, Number w)
		{
			var scale = (Number)Math.Sqrt(x * x + y * y + z * z + w * w);

			if (Float.Near(scale, 0))
			{
				return zero;
			}
			else
			{
				scale = 1 / scale;

				return new Quaternion
				{
					X = x * scale,
					Y = y * scale,
					Z = z * scale,
					W = w * scale,
				};
			}
		}

		/// <summary>
		/// Returns the components of the specified quaternion rounded to the nearest integer values.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The components of the <paramref name="value"/> rounded to the nearest integer values.</returns>
		public static Quaternion Round(Quaternion value)
		{
			return new Quaternion
			{
				X = (Number)Math.Round(value.X),
				Y = (Number)Math.Round(value.Y),
				Z = (Number)Math.Round(value.Z),
				W = (Number)Math.Round(value.W),
			};
		}

		/// <summary>
		/// Returns the quaternion parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <returns>The quaternion parsed from the <paramref name="value"/>.</returns>
		public static Quaternion Parse(string value)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, null);
		}

		/// <summary>
		/// Returns the quaternion parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <returns>The quaternion parsed from the <paramref name="value"/>.</returns>
		public static Quaternion Parse(string value, NumberStyles numberStyle)
		{
			return Parse(value, numberStyle, null);
		}

		/// <summary>
		/// Returns the quaternion parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <returns>The quaternion parsed from the <paramref name="value"/>.</returns>
		public static Quaternion Parse(string value, IFormatProvider formatProvider)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider);
		}

		/// <summary>
		/// Returns the quaternion parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <returns>The quaternion parsed from the <paramref name="value"/>.</returns>
		public static Quaternion Parse(string value, NumberStyles numberStyle, IFormatProvider formatProvider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var numbers = Float.Parse(value, numberStyle, formatProvider);

			if (numbers.Length == ValueCount)
			{
				return new Quaternion
				{
					X = numbers[0],
					Y = numbers[1],
					Z = numbers[2],
					W = numbers[3],
				};
			}
			else
			{
				throw new ValueCountMismatchException();
			}
		}

		/// <summary>
		/// Returns a quaternion with the magnitude of one, representing a rotation created from the specified rotation matrix.
		/// </summary>
		/// <param name="rotation">Rotation matrix.</param>
		/// <returns>A quaternion with the magnitude of one, representing a rotation created from the <paramref name="rotation"/>.</returns>
		public static Quaternion Rotate(Matrix4 rotation)
		{
			if (rotation.M11 + rotation.M22 + rotation.M33 > 0)
			{
				var t = 1 + rotation.M11 + rotation.M22 + rotation.M33;
				var s = 0.5f / (Number)Math.Sqrt(t);

				return new Quaternion
				{
					X = (rotation.M23 - rotation.M32) * s,
					Y = (rotation.M31 - rotation.M13) * s,
					Z = (rotation.M12 - rotation.M21) * s,
					W = s * t,
				};
			}
			else if (rotation.M11 > rotation.M22 && rotation.M11 > rotation.M33)
			{
				var t = 1 + rotation.M11 - rotation.M22 - rotation.M33;
				var s = 0.5f / (Number)Math.Sqrt(t);

				return new Quaternion
				{
					X = s * t,
					Y = (rotation.M12 + rotation.M21) * s,
					Z = (rotation.M31 + rotation.M13) * s,
					W = (rotation.M23 - rotation.M32) * s,
				};
			}
			else if (rotation.M22 > rotation.M33)
			{
				var t = 1 - rotation.M11 + rotation.M22 - rotation.M33;
				var s = 0.5f / (Number)Math.Sqrt(t);

				return new Quaternion
				{
					X = (rotation.M12 + rotation.M21) * s,
					Y = s * t,
					Z = (rotation.M23 + rotation.M32) * s,
					W = (rotation.M31 - rotation.M13) * s,
				};
			}
			else
			{
				var t = 1 - rotation.M11 - rotation.M22 + rotation.M33;
				var s = 0.5f / (Number)Math.Sqrt(t);

				return new Quaternion
				{
					X = (rotation.M31 + rotation.M13) * s,
					Y = (rotation.M23 + rotation.M32) * s,
					Z = s * t,
					W = (rotation.M12 - rotation.M21) * s,
				};
			}
		}

		/// <summary>
		/// Returns a quaternion with the magnitude of one, representing a rotation created from the specified angular velocity vector.
		/// </summary>
		/// <param name="angularVelocity">Angular velocity vector.</param>
		/// <returns>A quaternion with the magnitude of one, representing a rotation created from the <paramref name="angularVelocity"/>.</returns>
		public static Quaternion Rotate(Vector3 angularVelocity)
		{
			var scale = angularVelocity.Magnitude;

			if (Float.Near(scale, 0))
			{
				return Identity;
			}
			else
			{
				var angle = Angle.FromRadians(scale);
				var sin = Angle.Sin(angle * 0.5f) / scale;
				var cos = Angle.Cos(angle * 0.5f);

				return Normalize(
					sin * angularVelocity.X,
					sin * angularVelocity.Y,
					sin * angularVelocity.Z,
					cos);
			}
		}

		/// <summary>
		/// Returns a quaternion with the magnitude of one, representing a rotation around the specified axis.
		/// </summary>
		/// <param name="angle">Rotation angle.</param>
		/// <param name="axis">Rotation axis vector.</param>
		/// <returns>A quaternion with the magnitude of one, representing a rotation around <paramref name="axis"/>.</returns>
		public static Quaternion Rotate(Angle angle, Vector3 axis)
		{
			var cos = Angle.Cos(angle * 0.5f);
			var sin = Angle.Sin(angle * 0.5f);

			return new Quaternion
			{
				X = sin * axis.X,
				Y = sin * axis.Y,
				Z = sin * axis.Z,
				W = cos,
			};
		}

		/// <summary>
		/// Returns a quaternion with the magnitude of one, representing a rotation created from the specified angles.
		/// </summary>
		/// <param name="yaw">Yaw rotation angle.</param>
		/// <param name="pitch">Pitch rotation angle.</param>
		/// <param name="roll">Roll rotation angle.</param>
		/// <returns>A quaternion with the magnitude of one, representing the specified rotation.</returns>
		public static Quaternion Rotate(Angle yaw, Angle pitch, Angle roll)
		{
			var xsin = Angle.Sin(pitch * 0.5f);
			var xcos = Angle.Cos(pitch * 0.5f);
			var ysin = Angle.Sin(yaw * 0.5f);
			var ycos = Angle.Cos(yaw * 0.5f);
			var zsin = Angle.Sin(roll * 0.5f);
			var zcos = Angle.Cos(roll * 0.5f);

			return new Quaternion
			{
				X = (xsin * ycos * zcos) + (xcos * ysin * zsin),
				Y = (xcos * ysin * zcos) - (xsin * ycos * zsin),
				Z = (xcos * ycos * zsin) - (xsin * ysin * zcos),
				W = (xcos * ycos * zcos) + (xsin * ysin * zsin),
			};
		}

		/// <summary>
		/// Returns a quaternion with the magnitude of one, representing a rotation around the X axis.
		/// </summary>
		/// <param name="angle">Rotation angle.</param>
		/// <returns>A quaternion with the magnitude of one, representing a rotation around the X axis.</returns>
		public static Quaternion RotateX(Angle angle)
		{
			return new Quaternion
			{
				X = Angle.Sin(angle * 0.5f),
				Y = 0,
				Z = 0,
				W = Angle.Cos(angle * 0.5f),
			};
		}

		/// <summary>
		/// Returns a quaternion with the magnitude of one, representing a rotation around the Y axis.
		/// </summary>
		/// <param name="angle">Rotation angle.</param>
		/// <returns>A quaternion with the magnitude of one, representing a rotation around the Y axis.</returns>
		public static Quaternion RotateY(Angle angle)
		{
			return new Quaternion
			{
				X = 0,
				Y = Angle.Sin(angle * 0.5f),
				Z = 0,
				W = Angle.Cos(angle * 0.5f),
			};
		}

		/// <summary>
		/// Returns a quaternion with the magnitude of one, representing a rotation around the Z axis.
		/// </summary>
		/// <param name="angle">Rotation angle.</param>
		/// <returns>A quaternion with the magnitude of one, representing a rotation around the Z axis.</returns>
		public static Quaternion RotateZ(Angle angle)
		{
			return new Quaternion
			{
				X = 0,
				Y = 0,
				Z = Angle.Sin(angle * 0.5f),
				W = Angle.Cos(angle * 0.5f),
			};
		}

		/// <summary>
		/// Returns values indicating the sign of the components of the specified quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>Values indicating the sign of the components of the <paramref name="value"/>.</returns>
		public static Quaternion Sign(Quaternion value)
		{
			return new Quaternion
			{
				X = Math.Sign(value.X),
				Y = Math.Sign(value.Y),
				Z = Math.Sign(value.Z),
				W = Math.Sign(value.W),
			};
		}

		/// <summary>
		/// Returns the spherical linear blend between the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <param name="amount">Blend amount, in the range of [0, 1].</param>
		/// <returns>The spherical linear blend between the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Quaternion Slerp(Quaternion value1, Quaternion value2, Number amount)
		{
			var cos = Dot(value1, value2);

			if (Math.Abs(cos) >= 1)
			{
				return value1;
			}
			else
			{
				var t = Math.Acos(cos);
				var sin = Math.Sin(t);

				if (Math.Abs(sin) < 0.0001)
				{
					return new Quaternion
					{
						X = value1.X + (value2.X - value1.X) * amount,
						Y = value1.Y + (value2.Y - value1.Y) * amount,
						Z = value1.Z + (value2.Z - value1.Z) * amount,
						W = value1.W + (value2.W - value1.W) * amount,
					};
				}
				else
				{
					var amount1 = (Number)(Math.Sin((1 - amount) * t) / sin);
					var amount2 = (Number)(Math.Sin(amount * t) / sin);

					return new Quaternion
					{
						X = value1.X * amount1 + value2.X * amount2,
						Y = value1.Y * amount1 + value2.Y * amount2,
						Z = value1.Z * amount1 + value2.Z * amount2,
						W = value1.W * amount1 + value2.W * amount2,
					};
				}
			}
		}

		/// <summary>
		/// Returns the smooth step transition between the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <param name="amount">Step amount, in the range of [0, 1].</param>
		/// <returns>The smooth step transition from the <paramref name="value1"/> to the <paramref name="value2"/>.</returns>
		public static Quaternion SmoothStep(Quaternion value1, Quaternion value2, Number amount)
		{
			if (amount <= 0)
			{
				return value1;
			}

			if (amount >= 1)
			{
				return value2;
			}

			return Blend(value1, value2, (amount * amount) * (3 - 2 * amount));
		}

		/// <summary>
		/// Returns the step transition between the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <param name="amount">Step amount, in the range of [0, 1].</param>
		/// <returns>The <paramref name="value2"/> if the <paramref name="amount"/> is greater than 0, otherwise the <paramref name="value1"/> is returned.</returns>
		public static Quaternion Step(Quaternion value1, Quaternion value2, Number amount)
		{
			return (amount > 0) ? value2 : value1;
		}

		/// <summary>
		/// Returns the difference between the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <returns>The difference between the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Quaternion Subtract(Quaternion value1, Quaternion value2)
		{
			return new Quaternion
			{
				X = value1.X - value2.X,
				Y = value1.Y - value2.Y,
				Z = value1.Z - value2.Z,
				W = value1.W - value2.W,
			};
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
			return Float.ToString(format, formatProvider, this.X, this.Y, this.Z, this.W);
		}
        
		/// <summary>
		/// Attempts to parse the quaternion from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="result">The output variable for the quaternion parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, out Quaternion result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, null, out result);
		}

		/// <summary>
		/// Attempts to parse the quaternion from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="result">The output variable for the quaternion parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, out Quaternion result)
		{
			return TryParse(value, numberStyle, null, out result);
		}

		/// <summary>
		/// Attempts to parse the quaternion from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <param name="result">The output variable for the quaternion parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, IFormatProvider formatProvider, out Quaternion result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider, out result);
		}

		/// <summary>
		/// Attempts to parse the quaternion from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <param name="result">The output variable for the quaternion parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, IFormatProvider formatProvider, out Quaternion result)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var numbers = Float.Parse(value, numberStyle, formatProvider);

			if (numbers.Length == ValueCount)
			{
				result.X = numbers[0];
				result.Y = numbers[1];
				result.Z = numbers[2];
				result.W = numbers[3];
				return true;
			}
			else
			{
				result = default(Quaternion);
				return false;
			}
		}

		/// <summary>
		/// Gets the <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
		/// </summary>
		/// <returns>This method returns always null.</returns>
		System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>
		/// Generates an object from its XML representation.
		/// </summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized.</param>
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader.MoveToContent();

			if (!reader.IsEmptyElement)
			{
				var numbers = Float.ReadXml(reader);

				if (numbers.Length == ValueCount)
				{
					this.X = numbers[0];
					this.Y = numbers[1];
					this.Z = numbers[2];
					this.W = numbers[3];
				}

				reader.ReadEndElement();
			}
		}

		/// <summary>
		/// Converts an object into its XML representation.
		/// </summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			Float.WriteXml(writer, this.X, this.Y, this.Z, this.W);
		}
		#endregion

		#region Operators
		/// <summary>
		/// Determines whether the specified quaternions are equal.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <returns>True if the <paramref name="value1"/> is equal to the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator ==(Quaternion value1, Quaternion value2)
		{
			return
				value1.X == value2.X &&
				value1.Y == value2.Y &&
				value1.Z == value2.Z &&
				value1.W == value2.W;
		}

		/// <summary>
		/// Determines whether the specified quaternions are not equal.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <returns>True if the <paramref name="value1"/> is not equal to the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator !=(Quaternion value1, Quaternion value2)
		{
			return
				value1.X != value2.X ||
				value1.Y != value2.Y ||
				value1.Z != value2.Z ||
				value1.W != value2.W;
		}

		/// <summary>
		/// Compares the magnitudes of the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <returns>True if the magnitude of the <paramref name="value1"/> is less than the magnitude of the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator <(Quaternion value1, Quaternion value2)
		{
			return value1.MagnitudeSquared < value2.MagnitudeSquared;
		}

		/// <summary>
		/// Compares the magnitudes of the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <returns>True if the magnitude of the <paramref name="value1"/> is less than or equal to the magnitude of the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator <=(Quaternion value1, Quaternion value2)
		{
			return value1.MagnitudeSquared <= value2.MagnitudeSquared;
		}

		/// <summary>
		/// Compares the magnitudes of the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <returns>True if the magnitude of the <paramref name="value1"/> is greater than the magnitude of the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator >(Quaternion value1, Quaternion value2)
		{
			return value1.MagnitudeSquared > value2.MagnitudeSquared;
		}

		/// <summary>
		/// Compares the magnitudes of the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <returns>True if the magnitude of the <paramref name="value1"/> is greater than or equal to the magnitude of the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator >=(Quaternion value1, Quaternion value2)
		{
			return value1.MagnitudeSquared >= value2.MagnitudeSquared;
		}

		/// <summary>
		/// Returns a quaternion with the same magnitude as the specified quaternion but pointing to the opposite direction.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>A quaternion with the same magnitude as the <paramref name="value"/> but pointing to the opposite direction.</returns>
		public static Quaternion operator -(Quaternion value)
		{
			return new Quaternion
			{
				X = -value.X,
				Y = -value.Y,
				Z = -value.Z,
				W = -value.W,
			};
		}

		/// <summary>
		/// Returns the sum of the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <returns>Sum of the the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Quaternion operator +(Quaternion value1, Quaternion value2)
		{
			return new Quaternion
			{
				X = value1.X + value2.X,
				Y = value1.Y + value2.Y,
				Z = value1.Z + value2.Z,
				W = value1.W + value2.W,
			};
		}

		/// <summary>
		/// Returns the difference between the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <returns>Difference between the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Quaternion operator -(Quaternion value1, Quaternion value2)
		{
			return new Quaternion
			{
				X = value1.X - value2.X,
				Y = value1.Y - value2.Y,
				Z = value1.Z - value2.Z,
				W = value1.W - value2.W,
			};
		}

		/// <summary>
		/// Returns the components of the specified quaternion multiplied by the specified scalar.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The components of the <paramref name="value1"/> multiplied by the <paramref name="value2"/>.</returns>
		public static Quaternion operator *(Quaternion value1, Number value2)
		{
			return new Quaternion
			{
				X = value1.X * value2,
				Y = value1.Y * value2,
				Z = value1.Z * value2,
				W = value1.W * value2,
			};
		}

		/// <summary>
		/// Returns the components of the specified quaternion multiplied by the specified scalar.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The components of the <paramref name="value2"/> multiplied by the <paramref name="value1"/>.</returns>
		public static Quaternion operator *(Number value1, Quaternion value2)
		{
			return new Quaternion
			{
				X = value1 * value2.X,
				Y = value1 * value2.Y,
				Z = value1 * value2.Z,
				W = value1 * value2.W,
			};
		}

		/// <summary>
		/// Returns the product of the specified quaternions.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A quaternion.</param>
		/// <returns>The product of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Quaternion operator *(Quaternion value1, Quaternion value2)
		{
			return new Quaternion
			{
				X = value1.W * value2.X + value1.X * value2.W + value1.Y * value2.Z - value1.Z * value2.Y,
				Y = value1.W * value2.Y - value1.X * value2.Z + value1.Y * value2.W + value1.Z * value2.X,
				Z = value1.W * value2.Z + value1.X * value2.Y - value1.Y * value2.X + value1.Z * value2.W,
				W = value1.W * value2.W - value1.X * value2.X - value1.Y * value2.Y - value1.Z * value2.Z,
			};
		}

		/// <summary>
		/// Returns the components of the specified quaternion divided by the specified scalar.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The components of the <paramref name="value1"/> divided by the <paramref name="value2"/>.</returns>
		public static Quaternion operator /(Quaternion value1, Number value2)
		{
			var s = 1 / value2;

			return new Quaternion
			{
				X = value1.X * s,
				Y = value1.Y * s,
				Z = value1.Z * s,
				W = value1.W * s,
			};
		}

		/// <summary>
		/// Returns the remainder of the components of the specified quaternion divided by the specified scalar.
		/// </summary>
		/// <param name="value1">A quaternion.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>Remainder of the components of the <paramref name="value1"/> divided by the <paramref name="value2"/>.</returns>
		public static Quaternion operator %(Quaternion value1, Number value2)
		{
			return new Quaternion
			{
				X = value1.X % value2,
				Y = value1.Y % value2,
				Z = value1.Z % value2,
				W = value1.W % value2,
			};
		}
		#endregion

		#region Indexer
		/// <summary>
		/// Gets or sets the component at the specified index.
		/// </summary>
		/// <param name="index">Component index.</param>
		/// <returns>Value of the component at the <paramref name="index"/>.</returns>
		/// <exception cref="IndexOutOfRangeException">Thrown if the <paramref name="index"/> is less than zero or greater than or equal to the number of components in the quaternion.</exception>
		public Number this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return this.X;

					case 1:
						return this.Y;

					case 2:
						return this.Z;

					case 3:
						return this.W;

					default:
						throw new IndexOutOfRangeException();
				}
			}

			set
			{
				switch (index)
				{
					case 0:
						this.X = value;
						break;

					case 1:
						this.Y = value;
						break;

					case 2:
						this.Z = value;
						break;

					case 3:
						this.W = value;
						break;

					default:
						throw new IndexOutOfRangeException();
				}
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets a quaternion with the imaginary part set to zero and the real part set to one.
		/// </summary>
		/// <value>The quaternion with the imaginary part set to zero and the real part set to one.</value>
		public static Quaternion Identity
		{
			get { return Quaternion.identity; }
		}

		/// <summary>
		/// Gets the magnitude of the quaternion.
		/// </summary>
		/// <value>The magnitude of the quaternion.</value>
		public Number Magnitude
		{
			get { return (Number)Math.Sqrt(this.MagnitudeSquared); }
		}

		/// <summary>
		/// Gets the squared magnitude of the quaternion.
		/// </summary>
		/// <value>The squared magnitude of the quaternion.</value>
		public Number MagnitudeSquared
		{
			get { return Dot(this, this); }
		}

		/// <summary>
		/// Gets a quaternion with all the components set to their maximum values.
		/// </summary>
		/// <value>The quaternion with components set to their maximum values.</value>
		public static Quaternion MaxValue
		{
			get { return maxValue; }
		}

		/// <summary>
		/// Gets a quaternion with all the components set to their minimum values.
		/// </summary>
		/// <value>The quaternion with components set to their minimum values.</value>
		public static Quaternion MinValue
		{
			get { return minValue; }
		}

		/// <summary>
		/// Gets a quaternion with all the components set to a value that is not a number.
		/// </summary>
		/// <value>The quaternion with components set to a value that is not a number.</value>
		public static Quaternion NaN
		{
			get { return nan; }
		}

		/// <summary>
		/// Gets a quaternion with all the components set to negative infinity.
		/// </summary>
		/// <value>The quaternion with components set to negative infinity.</value>
		public static Quaternion NegativeInfinity
		{
			get { return negativeInfinity; }
		}

		/// <summary>
		/// Gets a quaternion with all the components set to one.
		/// </summary>
		/// <value>The quaternion with components set to one.</value>
		public static Quaternion One
		{
			get { return one; }
		}

		/// <summary>
		/// Gets a quaternion with all the components set to positive infinity.
		/// </summary>
		/// <value>The quaternion with components set to positive infinity.</value>
		public static Quaternion PositiveInfinity
		{
			get { return positiveInfinity; }
		}

		/// <summary>
		/// Gets a quaternion with the X component set to one.
		/// </summary>
		/// <value>The unit X quaternion.</value>
		public static Quaternion UnitX
		{
			get { return unitX; }
		}

		/// <summary>
		/// Gets a quaternion with the Y component set to one.
		/// </summary>
		/// <value>The unit Y quaternion.</value>
		public static Quaternion UnitY
		{
			get { return unitY; }
		}

		/// <summary>
		/// Gets a quaternion with the Z component set to one.
		/// </summary>
		/// <value>The unit Z quaternion.</value>
		public static Quaternion UnitZ
		{
			get { return unitZ; }
		}

		/// <summary>
		/// Gets a quaternion with the W component set to one.
		/// </summary>
		/// <value>The unit W quaternion.</value>
		public static Quaternion UnitW
		{
			get { return unitW; }
		}

		/// <summary>
		/// Gets a quaternion with all the components set to zero.
		/// </summary>
		/// <value>The zero quaternion.</value>
		public static Quaternion Zero
		{
			get { return zero; }
		}
		#endregion

		#region Fields
		/// <summary>
		/// X component.
		/// </summary>
		public Number X;

		/// <summary>
		/// Y component.
		/// </summary>
		public Number Y;

		/// <summary>
		/// Z component.
		/// </summary>
		public Number Z;

		/// <summary>
		/// W component.
		/// </summary>
		public Number W;

		/// <summary>
		/// The number of values in the quaternion.
		/// </summary>
		public const int ValueCount = 4;

		/// <summary>
		/// A quaternion with the imaginary part set to zero and real part set to one.
		/// </summary>
		private static readonly Quaternion identity = new Quaternion { W = 1 };

		/// <summary>
		/// A quaternion with all the components set to their maximum values.
		/// </summary>
		private static readonly Quaternion maxValue = new Quaternion(Number.MaxValue);

		/// <summary>
		/// A quaternion with all the components set to their minimum values.
		/// </summary>
		private static readonly Quaternion minValue = new Quaternion(Number.MinValue);

		/// <summary>
		/// A quaternion with all the components set to a value that is not a number.
		/// </summary>
		private static readonly Quaternion nan = new Quaternion(Number.NaN);

		/// <summary>
		/// A quaternion with all the components set to negative infinity.
		/// </summary>
		private static readonly Quaternion negativeInfinity = new Quaternion(Number.NegativeInfinity);

		/// <summary>
		/// A quaternion with all the components set to one.
		/// </summary>
		private static readonly Quaternion one = new Quaternion(1);

		/// <summary>
		/// A quaternion with all the components set to positive infinity.
		/// </summary>
		private static readonly Quaternion positiveInfinity = new Quaternion(Number.PositiveInfinity);

		/// <summary>
		/// A quaternion with the X component set to one.
		/// </summary>
		private static readonly Quaternion unitX = new Quaternion { X = 1 };

		/// <summary>
		/// A quaternion with the Y component set to one.
		/// </summary>
		private static readonly Quaternion unitY = new Quaternion { Y = 1 };

		/// <summary>
		/// A quaternion with the Z component set to one.
		/// </summary>
		private static readonly Quaternion unitZ = new Quaternion { Z = 1 };

		/// <summary>
		/// A quaternion with the W component set to one.
		/// </summary>
		private static readonly Quaternion unitW = new Quaternion { W = 1 };

		/// <summary>
		/// A quaternion with all the components set to zero.
		/// </summary>
		private static readonly Quaternion zero = new Quaternion(0);
		#endregion

        /// <summary>
        /// Returns this quaternion rotation as Pitch, Yaw and Roll
        /// </summary>
        /// <returns></returns>
        public Vector3 ToPitchYawRoll()
        {
            const float Epsilon = 0.0009765625f;
            const float Threshold = 0.5f - Epsilon;

            float yaw;
            float pitch;
            float roll;

            float XY = this.X * this.Y;
            float ZW = this.Z * this.W;

            float TEST = XY + ZW;

            if (TEST < -Threshold || TEST > Threshold)
            {

                int sign = System.Math.Sign(TEST);

                yaw = (float)(sign * 2 * (float)System.Math.Atan2(this.X, this.W));

                pitch = (float)(sign * (System.Math.PI / 2));

                roll = 0;

            }
            else
            {

                float XX = this.X * this.X;
                float XZ = this.X * this.Z;
                float XW = this.X * this.W;

                float YY = this.Y * this.Y;
                float TW = this.Y * this.W;
                float YZ = this.Y * this.Z;

                float KK = this.Z * this.Z;

                yaw = (float)System.Math.Atan2(2 * TW - 2 * XZ, 1 - 2 * YY - 2 * KK);

                pitch = (float)System.Math.Atan2(2 * XW - 2 * YZ, 1 - 2 * XX - 2 * KK);

                roll = (float)System.Math.Asin(2 * TEST);
            }

            return new Vector3(pitch, yaw, roll);
        }
	}
}
