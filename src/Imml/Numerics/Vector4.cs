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
	/// Representation of a vector with 4 values.
	/// </summary>
	[DebuggerDisplay("X = {X} Y = {Y} Z = {Z} W = {W} Magnitude = {Magnitude}")]
	public struct Vector4 : IComparable, IComparable<Vector4>, IEnumerable<Number>, IEquatable<Vector4>, IFormattable, IXmlSerializable
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="value">The value for all the components.</param>
		public Vector4(Number value)
		{
			this.X = value;
			this.Y = value;
			this.Z = value;
			this.W = value;
		}

		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="values">A vector that provides the values for the X and Y components.</param>
		public Vector4(Vector2 values)
		{
			this.X = values.X;
			this.Y = values.Y;
			this.Z = 0;
			this.W = 0;
		}

		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="values">A vector that provides the values for the X, Y and Z components.</param>
		public Vector4(Vector3 values)
		{
			this.X = values.X;
			this.Y = values.Y;
			this.Z = values.Z;
			this.W = 0;
		}

		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="values">A vector that provides the values for the X, Y, Z and W components.</param>
		public Vector4(Vector4 values)
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
		public Vector4(Number x, Number y, Number z, Number w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Returns the magnitude of the specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The magnitude of the <paramref name="value"/>.</returns>
		public static Number Abs(Vector4 value)
		{
			return value.Magnitude;
		}

		/// <summary>
		/// Returns the sum of the specified vectors.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <returns>The sum of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Vector4 Add(Vector4 value1, Vector4 value2)
		{
			return new Vector4
			{
				X = value1.X + value2.X,
				Y = value1.Y + value2.Y,
				Z = value1.Z + value2.Z,
				W = value1.W + value2.W,
			};
		}

		/// <summary>
		/// Returns the linear blend of the specified vectors using barycentric coordinates.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <param name="value3">A vector.</param>
		/// <param name="u">Barycentric coordinate u, expressing weight towards <paramref name="value2"/>.</param>
		/// <param name="v">Barycentric coordinate v, expressing weight towards <paramref name="value3"/>.</param>
		/// <returns>The linear blend of the <paramref name="value1"/>, <paramref name="value2"/> and the <paramref name="value3"/>, using barycentric coordinates <paramref name="u"/> and <paramref name="v"/>.</returns>
		public static Vector4 Barycentric(Vector4 value1, Vector4 value2, Vector4 value3, Number u, Number v)
		{
			return new Vector4
			{
				X = value1.X + u * (value2.X - value1.X) + v * (value3.X - value1.X),
				Y = value1.Y + u * (value2.Y - value1.Y) + v * (value3.Y - value1.Y),
				Z = value1.Z + u * (value2.Z - value1.Z) + v * (value3.Z - value1.Z),
				W = value1.W + u * (value2.W - value1.W) + v * (value3.W - value1.W),
			};
		}

		/// <summary>
		/// Returns the linear blend of the specified vectors.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <param name="amount">Blend amount, in the range of [0, 1].</param>
		/// <returns>The linear blend of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Vector4 Blend(Vector4 value1, Vector4 value2, Number amount)
		{
			return new Vector4
			{
				X = value1.X + amount * (value2.X - value1.X),
				Y = value1.Y + amount * (value2.Y - value1.Y),
				Z = value1.Z + amount * (value2.Z - value1.Z),
				W = value1.W + amount * (value2.W - value1.W),
			};
		}

		/// <summary>
		/// Returns the normalized linear blend of the specified vectors.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <param name="amount">Blend amount, in the range of [0, 1].</param>
		/// <returns>The normalized linear blend of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Vector4 BlendNormalized(Vector4 value1, Vector4 value2, Number amount)
		{
			return Normalize(
				value1.X + amount * (value2.X - value1.X),
				value1.Y + amount * (value2.Y - value1.Y),
				value1.Z + amount * (value2.Z - value1.Z),
				value1.W + amount * (value2.W - value1.W));
		}

		/// <summary>
		/// Returns a point on a spline between the specified points using the Catmull-Rom spline interpolation method.
		/// </summary>
		/// <param name="point1">A point.</param>
		/// <param name="point2">A point.</param>
		/// <param name="point3">A point.</param>
		/// <param name="point4">A point.</param>
		/// <param name="amount">Interpolation amount, in range [0, 1].</param>
		/// <returns>A point on the spline between <paramref name="point1"/>, <paramref name="point2"/>, <paramref name="point3"/> and the <paramref name="point4"/>.</returns>
		public static Vector4 CatmullRom(Vector4 point1, Vector4 point2, Vector4 point3, Vector4 point4, Number amount)
		{
			var amount2 = amount * amount;
			var amount3 = amount * amount2;

			return new Vector4
			{
				X = 0.5f * (2 * point2.X + amount * (point3.X - point1.X) + amount2 * (2 * point1.X - 5 * point2.X + 4 * point3.X - point4.X) + amount3 * (point4.X - 3 * point3.X + 3 * point2.X - point1.X)),
				Y = 0.5f * (2 * point2.Y + amount * (point3.Y - point1.Y) + amount2 * (2 * point1.Y - 5 * point2.Y + 4 * point3.Y - point4.Y) + amount3 * (point4.Y - 3 * point3.Y + 3 * point2.Y - point1.Y)),
				Z = 0.5f * (2 * point2.Z + amount * (point3.Z - point1.Z) + amount2 * (2 * point1.Z - 5 * point2.Z + 4 * point3.Z - point4.Z) + amount3 * (point4.Z - 3 * point3.Z + 3 * point2.Z - point1.Z)),
				W = 0.5f * (2 * point2.W + amount * (point3.W - point1.W) + amount2 * (2 * point1.W - 5 * point2.W + 4 * point3.W - point4.W) + amount3 * (point4.W - 3 * point3.W + 3 * point2.W - point1.W)),
			};
		}

		/// <summary>
		/// Returns the smallest integer value greater than or equal to each component of the specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The smallest integer value greater than or equal to each component of the <paramref name="value"/>.</returns>
		public static Vector4 Ceiling(Vector4 value)
		{
			return new Vector4
			{
				X = (Number)Math.Ceiling(value.X),
				Y = (Number)Math.Ceiling(value.Y),
				Z = (Number)Math.Ceiling(value.Z),
				W = (Number)Math.Ceiling(value.W),
			};
		}

		/// <summary>
		/// Returns the components of the specified vector restricted to the specified interval.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="minimum">Minimum values for the vector components.</param>
		/// <param name="maximum">Maximum values for the vector components.</param>
		/// <returns>The components of the <paramref name="value"/> restricted between the <paramref name="minimum"/> and the <paramref name="maximum"/>.</returns>
		public static Vector4 Clamp(Vector4 value, Vector4 minimum, Vector4 maximum)
		{
			return new Vector4
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
			if (value is Vector4)
			{
				return this.CompareTo((Vector4)value);
			}

			throw new ArgumentException();
		}

		/// <summary>
		/// Compares the magnitude of the current vector with the magnitude of the specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>
		/// Zero if the magnitudes are equal.
		/// Less than zero if the magnitude of the current vector is less than the magnitude of the <paramref name="value"/>.
		/// Greater than zero if the magnitude of the current vector is greater than the magnitude of the <paramref name="value"/>.
		/// </returns>
		public int CompareTo(Vector4 value)
		{
			return this.MagnitudeSquared.CompareTo(value.MagnitudeSquared);
		}

		/// <summary>
		/// Returns the distance between the specified points.
		/// </summary>
		/// <param name="point1">A point.</param>
		/// <param name="point2">A point.</param>
		/// <returns>The distance between the <paramref name="point1"/> and the <paramref name="point2"/>.</returns>
		public static Number Distance(Vector4 point1, Vector4 point2)
		{
			return (point1 - point2).Magnitude;
		}

		/// <summary>
		/// Returns the square of the distance between the specified points.
		/// </summary>
		/// <param name="point1">A point.</param>
		/// <param name="point2">A point.</param>
		/// <returns>The square of the distance between the <paramref name="point1"/> and the <paramref name="point2"/>.</returns>
		public static Number DistanceSquared(Vector4 point1, Vector4 point2)
		{
			return (point1 - point2).MagnitudeSquared;
		}

		/// <summary>
		/// Returns the components of the specified vector divided by the specified scalar.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The components of the <paramref name="value1"/> divided by the <paramref name="value2"/>.</returns>
		public static Vector4 Divide(Vector4 value1, Number value2)
		{
			var s = 1 / value2;

			return new Vector4
			{
				X = value1.X * s,
				Y = value1.Y * s,
				Z = value1.Z * s,
				W = value1.W * s,
			};
		}

		/// <summary>
		/// Returns the dot product of the current vector and the specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The dot product of the current vector and the <paramref name="value"/>.</returns>
		public Number Dot(Vector4 value)
		{
			return
				this.X * value.X +
				this.Y * value.Y +
				this.Z * value.Z +
				this.W * value.W;
		}

		/// <summary>
		/// Returns the dot product of the specified vectors.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <returns>The dot product of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Number Dot(Vector4 value1, Vector4 value2)
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
			if (value is Vector4)
			{
				return this.Equals((Vector4)value);
			}

			return false;
		}

		/// <summary>
		/// Determines whether the current vector is equal to the specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>True if the current vector is equal to the <paramref name="value"/>, otherwise false.</returns>
		public bool Equals(Vector4 value)
		{
			return
				this.X == value.X &&
				this.Y == value.Y &&
				this.Z == value.Z &&
				this.W == value.W;
		}

		/// <summary>
		/// Returns the largest integer value less than or equal to each component of the specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The largest integer value less than or equal to each component of the <paramref name="value"/>.</returns>
		public static Vector4 Floor(Vector4 value)
		{
			return new Vector4
			{
				X = (Number)Math.Floor(value.X),
				Y = (Number)Math.Floor(value.Y),
				Z = (Number)Math.Floor(value.Z),
				W = (Number)Math.Floor(value.W),
			};
		}

		/// <summary>
		/// Returns the fractional part of each of the components of the specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The fractional part of each of the components of the <paramref name="value"/>.</returns>
		public static Vector4 Fraction(Vector4 value)
		{
			return new Vector4
			{
				X = value.X - (Number)Math.Floor(value.X),
				Y = value.Y - (Number)Math.Floor(value.Y),
				Z = value.Z - (Number)Math.Floor(value.Z),
				W = value.W - (Number)Math.Floor(value.W),
			};
		}

		/// <summary>
		/// Returns a hash code for the current vector.
		/// </summary>
		/// <returns>A hash code.</returns>
		/// <remarks>
		/// The hash code is not unique.
		/// If two vectors are equal, their hash codes are guaranteed to be equal.
		/// If the vectors are not equal, their hash codes are not guaranteed to be different.
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
		/// Returns a point on a spline between the specified points using the Hermite spline interpolation method.
		/// </summary>
		/// <param name="point1">A point.</param>
		/// <param name="tangent1">A tangent vector.</param>
		/// <param name="point2">A point.</param>
		/// <param name="tangent2">A tangent vector.</param>
		/// <param name="amount">Interpolation amount, in range [0, 1].</param>
		/// <returns>A point on a spline between the <paramref name="point1"/> and the <paramref name="point2"/>.</returns>
		public static Vector4 Hermite(Vector4 point1, Vector4 tangent1, Vector4 point2, Vector4 tangent2, Number amount)
		{
			var amount2 = amount * amount;
			var amount3 = amount * amount2;
			var h1 = (2 * amount3) - (3 * amount2) + 1;
			var h2 = (3 * amount2) - (2 * amount3);
			var h3 = amount3 - (2 * amount2) + amount;
			var h4 = amount3 - amount2;

			return new Vector4
			{
				X = point1.X * h1 + point2.X * h2 + tangent1.X * h3 + tangent2.X * h4,
				Y = point1.Y * h1 + point2.Y * h2 + tangent1.Y * h3 + tangent2.Y * h4,
				Z = point1.Z * h1 + point2.Z * h2 + tangent1.Z * h3 + tangent2.Z * h4,
				W = point1.W * h1 + point2.W * h2 + tangent1.W * h3 + tangent2.W * h4,
			};
		}

		/// <summary>
		/// Determines whether any of the components of the specified vector evaluates to an infinity.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>True if any of the components of <paramref name="value"/> evaluates to an infinity, otherwise false.</returns>
		public static bool IsInfinity(Vector4 value)
		{
			return
				Number.IsInfinity(value.X) ||
				Number.IsInfinity(value.Y) ||
				Number.IsInfinity(value.Z) ||
				Number.IsInfinity(value.W);
		}

		/// <summary>
		/// Determines whether any of the components of the specified vector evaluates to a value that is not a number.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>True if any of the components of <paramref name="value"/> evaluates to a value that is not a number, otherwise false.</returns>
		public static bool IsNaN(Vector4 value)
		{
			return
				Number.IsNaN(value.X) ||
				Number.IsNaN(value.Y) ||
				Number.IsNaN(value.Z) ||
				Number.IsNaN(value.W);
		}

		/// <summary>
		/// Determines whether any of the components of the specified vector evaluates to negative infinity.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>True if any of the components of <paramref name="value"/> evaluates to negative infinity, otherwise false.</returns>
		public static bool IsNegativeInfinity(Vector4 value)
		{
			return
				Number.IsNegativeInfinity(value.X) ||
				Number.IsNegativeInfinity(value.Y) ||
				Number.IsNegativeInfinity(value.Z) ||
				Number.IsNegativeInfinity(value.W);
		}

		/// <summary>
		/// Determines whether any of the components of the specified vector evaluates to positive infinity.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>True if any of the components of <paramref name="value"/> evaluates to positive infinity, otherwise false.</returns>
		public static bool IsPositiveInfinity(Vector4 value)
		{
			return
				Number.IsPositiveInfinity(value.X) ||
				Number.IsPositiveInfinity(value.Y) ||
				Number.IsPositiveInfinity(value.Z) ||
				Number.IsPositiveInfinity(value.W);
		}

		/// <summary>
		/// Returns the larger value of each component of the spefied vector and the specified scalar.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The larger values of the components of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Vector4 Max(Vector4 value1, Number value2)
		{
			return new Vector4
			{
				X = Math.Max(value1.X, value2),
				Y = Math.Max(value1.Y, value2),
				Z = Math.Max(value1.Z, value2),
				W = Math.Max(value1.W, value2),
			};
		}

		/// <summary>
		/// Returns the larger value of each component of the specified vectors.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <returns>The larger values of the components of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Vector4 Max(Vector4 value1, Vector4 value2)
		{
			return new Vector4
			{
				X = Math.Max(value1.X, value2.X),
				Y = Math.Max(value1.Y, value2.Y),
				Z = Math.Max(value1.Z, value2.Z),
				W = Math.Max(value1.W, value2.W),
			};
		}

		/// <summary>
		/// Returns the smaller value of each component of the specified vector and the specified scalar.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The smaller values of the components of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Vector4 Min(Vector4 value1, Number value2)
		{
			return new Vector4
			{
				X = Math.Min(value1.X, value2),
				Y = Math.Min(value1.Y, value2),
				Z = Math.Min(value1.Z, value2),
				W = Math.Min(value1.W, value2),
			};
		}

		/// <summary>
		/// Returns the smaller value of each component of the specified vectors.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <returns>The smaller values of the components of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Vector4 Min(Vector4 value1, Vector4 value2)
		{
			return new Vector4
			{
				X = Math.Min(value1.X, value2.X),
				Y = Math.Min(value1.Y, value2.Y),
				Z = Math.Min(value1.Z, value2.Z),
				W = Math.Min(value1.W, value2.W),
			};
		}

		/// <summary>
		/// Returns the remainder of the components of the specified vector divided by the specified scalar.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The remainder of the components of the <paramref name="value1"/> divided by the <paramref name="value2"/>.</returns>
		public static Vector4 Modulo(Vector4 value1, Number value2)
		{
			return new Vector4
			{
				X = value1.X % value2,
				Y = value1.Y % value2,
				Z = value1.Z % value2,
				W = value1.W % value2,
			};
		}

		/// <summary>
		/// Returns the components of the specified vector multiplied by the specified scalar.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The components of the <paramref name="value1"/> multiplied by the <paramref name="value2"/>.</returns>
		public static Vector4 Multiply(Vector4 value1, Number value2)
		{
			return new Vector4
			{
				X = value1.X * value2,
				Y = value1.Y * value2,
				Z = value1.Z * value2,
				W = value1.W * value2,
			};
		}

		/// <summary>
		/// Returns a vector with the same magnitude as the specified vector but pointing to the opposite direction.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>A vector with the same magnitude as the <paramref name="value"/> but pointing to the opposite direction.</returns>
		public static Vector4 Negate(Vector4 value)
		{
			return new Vector4
			{
				X = -value.X,
				Y = -value.Y,
				Z = -value.Z,
				W = -value.W,
			};
		}

		/// <summary>
		/// Returns a vector with the magnitude of one, pointing to the same direction as the specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>A vector with the magnitude of one, pointing to the same direction as the <paramref name="value"/>.</returns>
		public static Vector4 Normalize(Vector4 value)
		{
			var scale = value.Magnitude;

			if (Float.Near(scale, 0))
			{
				return zero;
			}
			else
			{
				scale = 1 / scale;

				return new Vector4
				{
					X = value.X * scale,
					Y = value.Y * scale,
					Z = value.Z * scale,
					W = value.W * scale,
				};
			}
		}

		/// <summary>
		/// Returns a vector with the magnitude of one, pointing to the same direction as the specified vector.
		/// </summary>
		/// <param name="x">The X component of the vector.</param>
		/// <param name="y">The Y component of the vector.</param>
		/// <param name="z">The Z component of the vector.</param>
		/// <param name="w">The W component of the vector.</param>
		/// <returns>A vector with the magnitude of one, pointing to the same direction as the specified vector.</returns>
		public static Vector4 Normalize(Number x, Number y, Number z, Number w)
		{
			var scale = (Number)Math.Sqrt(x * x + y * y + z * z + w * w);

			if (Float.Near(scale, 0))
			{
				return zero;
			}
			else
			{
				scale = 1 / scale;

				return new Vector4
				{
					X = x * scale,
					Y = y * scale,
					Z = z * scale,
					W = w * scale,
				};
			}
		}

		/// <summary>
		/// Returns the vector parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <returns>The vector parsed from the <paramref name="value"/>.</returns>
		public static Vector4 Parse(string value)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, null);
		}

		/// <summary>
		/// Returns the vector parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <returns>The vector parsed from the <paramref name="value"/>.</returns>
		public static Vector4 Parse(string value, NumberStyles numberStyle)
		{
			return Parse(value, numberStyle, null);
		}

		/// <summary>
		/// Returns the vector parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <returns>The vector parsed from the <paramref name="value"/>.</returns>
		public static Vector4 Parse(string value, IFormatProvider formatProvider)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider);
		}

		/// <summary>
		/// Returns the vector parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <returns>The vector parsed from the <paramref name="value"/>.</returns>
		public static Vector4 Parse(string value, NumberStyles numberStyle, IFormatProvider formatProvider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var numbers = Float.Parse(value, numberStyle, formatProvider);

			if (numbers.Length == ValueCount)
			{
				return new Vector4
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
		/// Returns the specified vector projected on the specified line.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="pointOnLine1">A point on the line.</param>
		/// <param name="pointOnLine2">A point on the line.</param>
		/// <returns>The <paramref name="value"/> projected on the line defined by the <paramref name="pointOnLine1"/> and the <paramref name="pointOnLine2"/>.</returns>
		public static Vector4 ProjectOnLine(Vector4 value, Vector4 pointOnLine1, Vector4 pointOnLine2)
		{
			var t = pointOnLine2 - pointOnLine1;
			var u = Dot(value - pointOnLine1, t) / t.MagnitudeSquared;

			return pointOnLine1 + u * t;
		}

		/// <summary>
		/// Returns the specified vector projected on the specified line segment.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="lineSegmentEnd1">Line segment end.</param>
		/// <param name="lineSegmentEnd2">Line segment end.</param>
		/// <returns>The <paramref name="value"/> projected on the line segment between the <paramref name="lineSegmentEnd1"/> and the <paramref name="lineSegmentEnd2"/>.</returns>
		public static Vector4 ProjectOnLineSegment(Vector4 value, Vector4 lineSegmentEnd1, Vector4 lineSegmentEnd2)
		{
			var t = lineSegmentEnd2 - lineSegmentEnd1;
			var u = Dot(value - lineSegmentEnd1, t) / t.MagnitudeSquared;

			if (u < 0)
			{
				u = 0;
			}

			if (u > 1)
			{
				u = 1;
			}

			return lineSegmentEnd1 + u * t;
		}

		/// <summary>
		/// Returns the specified vector projected on the specified plane.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="planePosition">Plane position.</param>
		/// <param name="planeNormal">Plane normal vector.</param>
		/// <returns>The <paramref name="value"/> projected on the plane defined by the <paramref name="planePosition"/> and the <paramref name="planeNormal"/>.</returns>
		public static Vector4 ProjectOnPlane(Vector4 value, Vector4 planePosition, Vector4 planeNormal)
		{
			return value - planeNormal * (Dot(value, planeNormal) - Dot(planePosition, planeNormal));
		}

		/// <summary>
		/// Returns the specified vector projected on the specified ray.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="rayPosition">Ray position.</param>
		/// <param name="rayDirection">Ray direction vector.</param>
		/// <returns>The <paramref name="value"/> projected on the ray defined by the <paramref name="rayPosition"/> and the <paramref name="rayDirection"/>.</returns>
		public static Vector4 ProjectOnRay(Vector4 value, Vector4 rayPosition, Vector4 rayDirection)
		{
			var u = Dot(value - rayPosition, rayDirection) / rayDirection.MagnitudeSquared;

			if (u < 0)
			{
				u = 0;
			}

			return rayPosition + u * rayDirection;
		}

		/// <summary>
		/// Returns the reflection of the specified vector when reflected off a plane.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="planeNormal">Plane normal.</param>
		/// <returns>The reflection of <paramref name="value"/> when reflected off a plane with the normal <paramref name="planeNormal"/>.</returns>
		public static Vector4 Reflect(Vector4 value, Vector4 planeNormal)
		{
			return value - 2 * Dot(value, planeNormal) * planeNormal;
		}

		/// <summary>
		/// Returns the refraction of the specified vector when refracted off a plane.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="planeNormal">Plane normal.</param>
		/// <param name="refractiveIndex1">Refractive index of the source substance.</param>
		/// <param name="refractiveIndex2">Refractive index of the target substance.</param>
		/// <returns>The refraction of <paramref name="value"/> when refracted off a plane with the normal <paramref name="planeNormal"/> where the refractive index of the source substance is the <paramref name="refractiveIndex1"/> and the refractive index of the target substance is the <paramref name="refractiveIndex2"/>.</returns>
		public static Vector4 Refract(Vector4 value, Vector4 planeNormal, Number refractiveIndex1, Number refractiveIndex2)
		{
			var dot = Dot(planeNormal, value);
			var eta = refractiveIndex1 / refractiveIndex2;
			var k = 1 - eta * eta * (1 - dot * dot);

			if (k < 0)
			{
				return Zero;
			}
			else
			{
				return (eta * value) - (eta * dot + (Number)Math.Sqrt(k)) * planeNormal;
			}
		}

		/// <summary>
		/// Returns the components of the specified vector rounded to the nearest integer values.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The components of the <paramref name="value"/> rounded to the nearest integer values.</returns>
		public static Vector4 Round(Vector4 value)
		{
			return new Vector4
			{
				X = (Number)Math.Round(value.X),
				Y = (Number)Math.Round(value.Y),
				Z = (Number)Math.Round(value.Z),
				W = (Number)Math.Round(value.W),
			};
		}

		/// <summary>
		/// Returns values indicating the sign of the components of the specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>Values indicating the sign of the components of the <paramref name="value"/>.</returns>
		public static Vector4 Sign(Vector4 value)
		{
			return new Vector4
			{
				X = Math.Sign(value.X),
				Y = Math.Sign(value.Y),
				Z = Math.Sign(value.Z),
				W = Math.Sign(value.W),
			};
		}

		/// <summary>
		/// Returns the spherical linear blend between the specified vectors.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <param name="amount">Blend amount, in the range of [0, 1].</param>
		/// <returns>The spherical linear blend between the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Vector4 Slerp(Vector4 value1, Vector4 value2, Number amount)
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
					return new Vector4
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

					return new Vector4
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
		/// Returns the smooth step transition between the specified vectors.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <param name="amount">Step amount, in the range of [0, 1].</param>
		/// <returns>The smooth step transition from the <paramref name="value1"/> to the <paramref name="value2"/>.</returns>
		public static Vector4 SmoothStep(Vector4 value1, Vector4 value2, Number amount)
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
		/// Returns the step transition between the specified vectors.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <param name="amount">Step amount, in the range of [0, 1].</param>
		/// <returns>The <paramref name="value2"/> if the <paramref name="amount"/> is greater than 0, otherwise the <paramref name="value1"/> is returned.</returns>
		public static Vector4 Step(Vector4 value1, Vector4 value2, Number amount)
		{
			return (amount > 0) ? value2 : value1;
		}

		/// <summary>
		/// Returns the difference between the specified vectors.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <returns>The difference between the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Vector4 Subtract(Vector4 value1, Vector4 value2)
		{
			return new Vector4
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
		/// Attempts to parse the vector from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="result">The output variable for the vector parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, out Vector4 result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, null, out result);
		}

		/// <summary>
		/// Attempts to parse the vector from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="result">The output variable for the vector parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, out Vector4 result)
		{
			return TryParse(value, numberStyle, null, out result);
		}

		/// <summary>
		/// Attempts to parse the vector from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <param name="result">The output variable for the vector parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, IFormatProvider formatProvider, out Vector4 result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider, out result);
		}

		/// <summary>
		/// Attempts to parse the vector from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <param name="result">The output variable for the vector parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, IFormatProvider formatProvider, out Vector4 result)
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
				result = default(Vector4);
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
		/// Determines whether the specified vectors are equal.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <returns>True if the <paramref name="value1"/> is equal to the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator ==(Vector4 value1, Vector4 value2)
		{
			return
				value1.X == value2.X &&
				value1.Y == value2.Y &&
				value1.Z == value2.Z &&
				value1.W == value2.W;
		}

		/// <summary>
		/// Determines whether the specified vectors are not equal.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <returns>True if the <paramref name="value1"/> is not equal to the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator !=(Vector4 value1, Vector4 value2)
		{
			return
				value1.X != value2.X ||
				value1.Y != value2.Y ||
				value1.Z != value2.Z ||
				value1.W != value2.W;
		}

		/// <summary>
		/// Compares the magnitudes of the specified vectors.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <returns>True if the magnitude of the <paramref name="value1"/> is less than the magnitude of the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator <(Vector4 value1, Vector4 value2)
		{
			return value1.MagnitudeSquared < value2.MagnitudeSquared;
		}

		/// <summary>
		/// Compares the magnitudes of the specified vectors.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <returns>True if the magnitude of the <paramref name="value1"/> is less than or equal to the magnitude of the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator <=(Vector4 value1, Vector4 value2)
		{
			return value1.MagnitudeSquared <= value2.MagnitudeSquared;
		}

		/// <summary>
		/// Compares the magnitudes of the specified vectors.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <returns>True if the magnitude of the <paramref name="value1"/> is greater than the magnitude of the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator >(Vector4 value1, Vector4 value2)
		{
			return value1.MagnitudeSquared > value2.MagnitudeSquared;
		}

		/// <summary>
		/// Compares the magnitudes of the specified vectors.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <returns>True if the magnitude of the <paramref name="value1"/> is greater than or equal to the magnitude of the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator >=(Vector4 value1, Vector4 value2)
		{
			return value1.MagnitudeSquared >= value2.MagnitudeSquared;
		}

		/// <summary>
		/// Returns a vector with the same magnitude as the specified vector but pointing to the opposite direction.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>A vector with the same magnitude as the <paramref name="value"/> but pointing to the opposite direction.</returns>
		public static Vector4 operator -(Vector4 value)
		{
			return new Vector4
			{
				X = -value.X,
				Y = -value.Y,
				Z = -value.Z,
				W = -value.W,
			};
		}

		/// <summary>
		/// Returns the sum of the specified vectors.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <returns>Sum of the the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Vector4 operator +(Vector4 value1, Vector4 value2)
		{
			return new Vector4
			{
				X = value1.X + value2.X,
				Y = value1.Y + value2.Y,
				Z = value1.Z + value2.Z,
				W = value1.W + value2.W,
			};
		}

		/// <summary>
		/// Returns the difference between the specified vectors.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A vector.</param>
		/// <returns>Difference between the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Vector4 operator -(Vector4 value1, Vector4 value2)
		{
			return new Vector4
			{
				X = value1.X - value2.X,
				Y = value1.Y - value2.Y,
				Z = value1.Z - value2.Z,
				W = value1.W - value2.W,
			};
		}

		/// <summary>
		/// Returns the components of the specified vector multiplied by the specified scalar.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The components of the <paramref name="value1"/> multiplied by the <paramref name="value2"/>.</returns>
		public static Vector4 operator *(Vector4 value1, Number value2)
		{
			return new Vector4
			{
				X = value1.X * value2,
				Y = value1.Y * value2,
				Z = value1.Z * value2,
				W = value1.W * value2,
			};
		}

		/// <summary>
		/// Returns the components of the specified vector multiplied by the specified scalar.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The components of the <paramref name="value2"/> multiplied by the <paramref name="value1"/>.</returns>
		public static Vector4 operator *(Number value1, Vector4 value2)
		{
			return new Vector4
			{
				X = value1 * value2.X,
				Y = value1 * value2.Y,
				Z = value1 * value2.Z,
				W = value1 * value2.W,
			};
		}

		/// <summary>
		/// Returns the components of the specified vector divided by the specified scalar.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The components of the <paramref name="value1"/> divided by the <paramref name="value2"/>.</returns>
		public static Vector4 operator /(Vector4 value1, Number value2)
		{
			var s = 1 / value2;

			return new Vector4
			{
				X = value1.X * s,
				Y = value1.Y * s,
				Z = value1.Z * s,
				W = value1.W * s,
			};
		}

		/// <summary>
		/// Returns the remainder of the components of the specified vector divided by the specified scalar.
		/// </summary>
		/// <param name="value1">A vector.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>Remainder of the components of the <paramref name="value1"/> divided by the <paramref name="value2"/>.</returns>
		public static Vector4 operator %(Vector4 value1, Number value2)
		{
			return new Vector4
			{
				X = value1.X % value2,
				Y = value1.Y % value2,
				Z = value1.Z % value2,
				W = value1.W % value2,
			};
		}
		#endregion

		#region Conversions
		/// <summary>
		/// Performs an implicit conversion from <see cref="Vector2"/> to <see cref="Vector4"/>.
		/// </summary>
		/// <param name="value">The vector to convert.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator Vector4(Vector2 value)
		{
			return new Vector4(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="Vector3"/> to <see cref="Vector4"/>.
		/// </summary>
		/// <param name="value">The vector to convert.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator Vector4(Vector3 value)
		{
			return new Vector4(value);
		}
		#endregion

		#region Indexer
		/// <summary>
		/// Gets or sets the component at the specified index.
		/// </summary>
		/// <param name="index">Component index.</param>
		/// <returns>Value of the component at the <paramref name="index"/>.</returns>
		/// <exception cref="IndexOutOfRangeException">Thrown if the <paramref name="index"/> is less than zero or greater than or equal to the number of components in the vector.</exception>
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
		/// Gets a vector with magnitude of one, pointing backward.
		/// </summary>
		/// <value>The backward vector.</value>
		public static Vector4 Backward
		{
			get { return backward; }
		}

		/// <summary>
		/// Gets a vector with magnitude of one, pointing down.
		/// </summary>
		/// <value>The down vector.</value>
		public static Vector4 Down
		{
			get { return down; }
		}

		/// <summary>
		/// Gets a vector with magnitude of one, pointing forward.
		/// </summary>
		/// <value>The forward vector.</value>
		public static Vector4 Forward
		{
			get { return forward; }
		}

		/// <summary>
		/// Gets a vector with magnitude of one, pointing left.
		/// </summary>
		/// <value>The left vector.</value>
		public static Vector4 Left
		{
			get { return left; }
		}

		/// <summary>
		/// Gets the magnitude of the vector.
		/// </summary>
		/// <value>The magnitude of the vector.</value>
		public Number Magnitude
		{
			get { return (Number)Math.Sqrt(this.MagnitudeSquared); }
		}

		/// <summary>
		/// Gets the squared magnitude of the vector.
		/// </summary>
		/// <value>The squared magnitude of the vector.</value>
		public Number MagnitudeSquared
		{
			get { return Dot(this, this); }
		}

		/// <summary>
		/// Gets a vector with all the components set to their maximum values.
		/// </summary>
		/// <value>The vector with components set to their maximum values.</value>
		public static Vector4 MaxValue
		{
			get { return maxValue; }
		}

		/// <summary>
		/// Gets a vector with all the components set to their minimum values.
		/// </summary>
		/// <value>The vector with components set to their minimum values.</value>
		public static Vector4 MinValue
		{
			get { return minValue; }
		}

		/// <summary>
		/// Gets a vector with all the components set to a value that is not a number.
		/// </summary>
		/// <value>The vector with components set to a value that is not a number.</value>
		public static Vector4 NaN
		{
			get { return nan; }
		}

		/// <summary>
		/// Gets a vector with all the components set to negative infinity.
		/// </summary>
		/// <value>The vector with components set to negative infinity.</value>
		public static Vector4 NegativeInfinity
		{
			get { return negativeInfinity; }
		}

		/// <summary>
		/// Gets a vector with all the components set to one.
		/// </summary>
		/// <value>The vector with components set to one.</value>
		public static Vector4 One
		{
			get { return one; }
		}

		/// <summary>
		/// Gets a vector with all the components set to positive infinity.
		/// </summary>
		/// <value>The vector with components set to positive infinity.</value>
		public static Vector4 PositiveInfinity
		{
			get { return positiveInfinity; }
		}

		/// <summary>
		/// Gets a vector with magnitude of one, pointing right.
		/// </summary>
		/// <value>The right vector.</value>
		public static Vector4 Right
		{
			get { return right; }
		}

		/// <summary>
		/// Gets a vector with the X component set to one.
		/// </summary>
		/// <value>The unit X vector.</value>
		public static Vector4 UnitX
		{
			get { return unitX; }
		}

		/// <summary>
		/// Gets a vector with the Y component set to one.
		/// </summary>
		/// <value>The unit Y vector.</value>
		public static Vector4 UnitY
		{
			get { return unitY; }
		}

		/// <summary>
		/// Gets a vector with the Z component set to one.
		/// </summary>
		/// <value>The unit Z vector.</value>
		public static Vector4 UnitZ
		{
			get { return unitZ; }
		}

		/// <summary>
		/// Gets a vector with the W component set to one.
		/// </summary>
		/// <value>The unit W vector.</value>
		public static Vector4 UnitW
		{
			get { return unitW; }
		}

		/// <summary>
		/// Gets a vector with magnitude of one, pointing up.
		/// </summary>
		/// <value>The up vector.</value>
		public static Vector4 Up
		{
			get { return up; }
		}

		/// <summary>
		/// Gets a vector with all the components set to zero.
		/// </summary>
		/// <value>The zero vector.</value>
		public static Vector4 Zero
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
		/// The number of values in the vector.
		/// </summary>
		public const int ValueCount = 4;

		/// <summary>
		/// A vector with magnitude of one, pointing backward.
		/// </summary>
		private static readonly Vector4 backward = new Vector4 { Z = 1 };

		/// <summary>
		/// A vector with magnitude of one, pointing down.
		/// </summary>
		private static readonly Vector4 down = new Vector4 { Y = -1 };

		/// <summary>
		/// A vector with magnitude of one, pointing forward.
		/// </summary>
		private static readonly Vector4 forward = new Vector4 { Z = -1 };

		/// <summary>
		/// A vector with magnitude of one, pointing left.
		/// </summary>
		private static readonly Vector4 left = new Vector4 { X = -1 };

		/// <summary>
		/// A vector with all the components set to their maximum values.
		/// </summary>
		private static readonly Vector4 maxValue = new Vector4(Number.MaxValue);

		/// <summary>
		/// A vector with all the components set to their minimum values.
		/// </summary>
		private static readonly Vector4 minValue = new Vector4(Number.MinValue);

		/// <summary>
		/// A vector with all the components set to a value that is not a number.
		/// </summary>
		private static readonly Vector4 nan = new Vector4(Number.NaN);

		/// <summary>
		/// A vector with all the components set to negative infinity.
		/// </summary>
		private static readonly Vector4 negativeInfinity = new Vector4(Number.NegativeInfinity);

		/// <summary>
		/// A vector with all the components set to one.
		/// </summary>
		private static readonly Vector4 one = new Vector4(1);

		/// <summary>
		/// A vector with all the components set to positive infinity.
		/// </summary>
		private static readonly Vector4 positiveInfinity = new Vector4(Number.PositiveInfinity);

		/// <summary>
		/// A vector with magnitude of one, pointing right.
		/// </summary>
		private static readonly Vector4 right = new Vector4 { X = 1 };

		/// <summary>
		/// A vector with the X component set to one.
		/// </summary>
		private static readonly Vector4 unitX = new Vector4 { X = 1 };

		/// <summary>
		/// A vector with the Y component set to one.
		/// </summary>
		private static readonly Vector4 unitY = new Vector4 { Y = 1 };

		/// <summary>
		/// A vector with the Z component set to one.
		/// </summary>
		private static readonly Vector4 unitZ = new Vector4 { Z = 1 };

		/// <summary>
		/// A vector with the W component set to one.
		/// </summary>
		private static readonly Vector4 unitW = new Vector4 { W = 1 };

		/// <summary>
		/// A vector with magnitude of one, pointing up.
		/// </summary>
		private static readonly Vector4 up = new Vector4 { Y = 1 };

		/// <summary>
		/// A vector with all the components set to zero.
		/// </summary>
		private static readonly Vector4 zero = new Vector4(0);
		#endregion
	}
}
