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
	using Imml.Numerics.Geometry;
	using System.Runtime.InteropServices;
	using System.Xml;
	using System.Xml.Serialization;

#if USE_DOUBLE_PRECISION
	using Number = System.Double;
#else
	using Number = System.Single;
#endif

	/// <summary>
	/// Representation of a 4 by 4 matrix with 16 values.
	/// </summary>
	[DebuggerDisplay("Trace = {Trace} Determinant = {Determinant}")]
	public struct Matrix4 : IComparable, IComparable<Matrix4>, IEnumerable<Number>, IEquatable<Matrix4>, IFormattable, IXmlSerializable
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="value">The value for all the elements.</param>
		public Matrix4(Number value)
		{
			this.M11 = value;
			this.M21 = value;
			this.M31 = value;
			this.M41 = value;
			this.M12 = value;
			this.M22 = value;
			this.M32 = value;
			this.M42 = value;
			this.M13 = value;
			this.M23 = value;
			this.M33 = value;
			this.M43 = value;
			this.M14 = value;
			this.M24 = value;
			this.M34 = value;
			this.M44 = value;
		}

		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="values">A matrix that provides values for all the elements.</param>
		public Matrix4(Matrix4 values)
		{
			this.M11 = values.M11;
			this.M21 = values.M21;
			this.M31 = values.M31;
			this.M41 = values.M41;
			this.M12 = values.M12;
			this.M22 = values.M22;
			this.M32 = values.M32;
			this.M42 = values.M42;
			this.M13 = values.M13;
			this.M23 = values.M23;
			this.M33 = values.M33;
			this.M43 = values.M43;
			this.M14 = values.M14;
			this.M24 = values.M24;
			this.M34 = values.M34;
			this.M44 = values.M44;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Returns the sum of the specified matrices.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <returns>The sum of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Matrix4 Add(Matrix4 value1, Matrix4 value2)
		{
			return new Matrix4
			{
				M11 = value1.M11 + value2.M11,
				M21 = value1.M21 + value2.M21,
				M31 = value1.M31 + value2.M31,
				M41 = value1.M41 + value2.M41,
				M12 = value1.M12 + value2.M12,
				M22 = value1.M22 + value2.M22,
				M32 = value1.M32 + value2.M32,
				M42 = value1.M42 + value2.M42,
				M13 = value1.M13 + value2.M13,
				M23 = value1.M23 + value2.M23,
				M33 = value1.M33 + value2.M33,
				M43 = value1.M43 + value2.M43,
				M14 = value1.M14 + value2.M14,
				M24 = value1.M24 + value2.M24,
				M34 = value1.M34 + value2.M34,
				M44 = value1.M44 + value2.M44,
			};
		}

		/// <summary>
		/// Returns the linear blend of the specified matrices using barycentric coordinates.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <param name="value3">A matrix.</param>
		/// <param name="u">Barycentric coordinate u, expressing weight towards <paramref name="value2"/>.</param>
		/// <param name="v">Barycentric coordinate v, expressing weight towards <paramref name="value3"/>.</param>
		/// <returns>The linear blend of the <paramref name="value1"/>, <paramref name="value2"/> and the <paramref name="value3"/>, using barycentric coordinates <paramref name="u"/> and <paramref name="v"/>.</returns>
		public static Matrix4 Barycentric(Matrix4 value1, Matrix4 value2, Matrix4 value3, Number u, Number v)
		{
			return new Matrix4
			{
				M11 = value1.M11 + u * (value2.M11 - value1.M11) + v * (value3.M11 - value1.M11),
				M21 = value1.M21 + u * (value2.M21 - value1.M21) + v * (value3.M21 - value1.M21),
				M31 = value1.M31 + u * (value2.M31 - value1.M31) + v * (value3.M31 - value1.M31),
				M41 = value1.M41 + u * (value2.M41 - value1.M41) + v * (value3.M41 - value1.M41),
				M12 = value1.M12 + u * (value2.M12 - value1.M12) + v * (value3.M12 - value1.M12),
				M22 = value1.M22 + u * (value2.M22 - value1.M22) + v * (value3.M22 - value1.M22),
				M32 = value1.M32 + u * (value2.M32 - value1.M32) + v * (value3.M32 - value1.M32),
				M42 = value1.M42 + u * (value2.M42 - value1.M42) + v * (value3.M42 - value1.M42),
				M13 = value1.M13 + u * (value2.M13 - value1.M13) + v * (value3.M13 - value1.M13),
				M23 = value1.M23 + u * (value2.M23 - value1.M23) + v * (value3.M23 - value1.M23),
				M33 = value1.M33 + u * (value2.M33 - value1.M33) + v * (value3.M33 - value1.M33),
				M43 = value1.M43 + u * (value2.M43 - value1.M43) + v * (value3.M43 - value1.M43),
				M14 = value1.M14 + u * (value2.M14 - value1.M14) + v * (value3.M14 - value1.M14),
				M24 = value1.M24 + u * (value2.M24 - value1.M24) + v * (value3.M24 - value1.M24),
				M34 = value1.M34 + u * (value2.M34 - value1.M34) + v * (value3.M34 - value1.M34),
				M44 = value1.M44 + u * (value2.M44 - value1.M44) + v * (value3.M44 - value1.M44),
			};
		}

		/// <summary>
		/// Returns the billboard transformation matrix created from the specified object position, camera position and the camera up vector.
		/// </summary>
		/// <param name="objectPosition">Object position.</param>
		/// <param name="cameraPosition">Camera position.</param>
		/// <param name="cameraUpVector">Camera up vector.</param>
		/// <returns>The billboard transformation matrix created from the <paramref name="objectPosition"/>, <paramref name="cameraPosition"/> and the <paramref name="cameraUpVector"/>.</returns>
		public static Matrix4 Billboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 cameraUpVector)
		{
			var z = Vector3.Normalize(cameraPosition - objectPosition);
			var x = Vector3.Normalize(Vector3.Cross(cameraUpVector, z));
			var y = Vector3.Normalize(Vector3.Cross(z, x));

			return new Matrix4
			{
				M11 = x.X,
				M21 = x.Y,
				M31 = x.Z,
				M12 = y.X,
				M22 = y.Y,
				M32 = y.Z,
				M13 = z.X,
				M23 = z.Y,
				M33 = z.Z,
				M14 = objectPosition.X,
				M24 = objectPosition.Y,
				M34 = objectPosition.Z,
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the linear blend of the specified matrices.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <param name="amount">Blend amount, in the range of [0, 1].</param>
		/// <returns>The linear blend of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Matrix4 Blend(Matrix4 value1, Matrix4 value2, Number amount)
		{
			return new Matrix4
			{
				M11 = value1.M11 + amount * (value2.M11 - value1.M11),
				M21 = value1.M21 + amount * (value2.M21 - value1.M21),
				M31 = value1.M31 + amount * (value2.M31 - value1.M31),
				M41 = value1.M41 + amount * (value2.M41 - value1.M41),
				M12 = value1.M12 + amount * (value2.M12 - value1.M12),
				M22 = value1.M22 + amount * (value2.M22 - value1.M22),
				M32 = value1.M32 + amount * (value2.M32 - value1.M32),
				M42 = value1.M42 + amount * (value2.M42 - value1.M42),
				M13 = value1.M13 + amount * (value2.M13 - value1.M13),
				M23 = value1.M23 + amount * (value2.M23 - value1.M23),
				M33 = value1.M33 + amount * (value2.M33 - value1.M33),
				M43 = value1.M43 + amount * (value2.M43 - value1.M43),
				M14 = value1.M14 + amount * (value2.M14 - value1.M14),
				M24 = value1.M24 + amount * (value2.M24 - value1.M24),
				M34 = value1.M34 + amount * (value2.M34 - value1.M34),
				M44 = value1.M44 + amount * (value2.M44 - value1.M44),
			};
		}

		/// <summary>
		/// Returns the smallest integer value greater than or equal to each element of the specified matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The smallest integer value greater than or equal to each element of the <paramref name="value"/>.</returns>
		public static Matrix4 Ceiling(Matrix4 value)
		{
			return new Matrix4
			{
				M11 = (Number)Math.Ceiling(value.M11),
				M21 = (Number)Math.Ceiling(value.M21),
				M31 = (Number)Math.Ceiling(value.M31),
				M41 = (Number)Math.Ceiling(value.M41),
				M12 = (Number)Math.Ceiling(value.M12),
				M22 = (Number)Math.Ceiling(value.M22),
				M32 = (Number)Math.Ceiling(value.M32),
				M42 = (Number)Math.Ceiling(value.M42),
				M13 = (Number)Math.Ceiling(value.M13),
				M23 = (Number)Math.Ceiling(value.M23),
				M33 = (Number)Math.Ceiling(value.M33),
				M43 = (Number)Math.Ceiling(value.M43),
				M14 = (Number)Math.Ceiling(value.M14),
				M24 = (Number)Math.Ceiling(value.M24),
				M34 = (Number)Math.Ceiling(value.M34),
				M44 = (Number)Math.Ceiling(value.M44),
			};
		}

		/// <summary>
		/// Returns the elements of the specified matrix restricted to the specified interval.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="minimum">Minimum values for the matrix elements.</param>
		/// <param name="maximum">Maximum values for the matrix elements.</param>
		/// <returns>The elements of the <paramref name="value"/> restricted between the <paramref name="minimum"/> and the <paramref name="maximum"/>.</returns>
		public static Matrix4 Clamp(Matrix4 value, Matrix4 minimum, Matrix4 maximum)
		{
			return new Matrix4
			{
				M11 = Math.Max(minimum.M11, Math.Min(value.M11, maximum.M11)),
				M21 = Math.Max(minimum.M21, Math.Min(value.M21, maximum.M21)),
				M31 = Math.Max(minimum.M31, Math.Min(value.M31, maximum.M31)),
				M41 = Math.Max(minimum.M41, Math.Min(value.M41, maximum.M41)),
				M12 = Math.Max(minimum.M12, Math.Min(value.M12, maximum.M12)),
				M22 = Math.Max(minimum.M22, Math.Min(value.M22, maximum.M22)),
				M32 = Math.Max(minimum.M32, Math.Min(value.M32, maximum.M32)),
				M42 = Math.Max(minimum.M42, Math.Min(value.M42, maximum.M42)),
				M13 = Math.Max(minimum.M13, Math.Min(value.M13, maximum.M13)),
				M23 = Math.Max(minimum.M23, Math.Min(value.M23, maximum.M23)),
				M33 = Math.Max(minimum.M33, Math.Min(value.M33, maximum.M33)),
				M43 = Math.Max(minimum.M43, Math.Min(value.M43, maximum.M43)),
				M14 = Math.Max(minimum.M14, Math.Min(value.M14, maximum.M14)),
				M24 = Math.Max(minimum.M24, Math.Min(value.M24, maximum.M24)),
				M34 = Math.Max(minimum.M34, Math.Min(value.M34, maximum.M34)),
				M44 = Math.Max(minimum.M44, Math.Min(value.M44, maximum.M44)),
			};
		}

		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <param name="value">An object to compare with this object.</param>
		/// <returns>A 32-bit signed integer that indicates the relative order of the objects being compared.</returns>
		public int CompareTo(object value)
		{
			if (value is Matrix4)
			{
				return this.CompareTo((Matrix4)value);
			}

			throw new ArgumentException();
		}

		/// <summary>
		/// Compares the determinant of the current matrix with the determinant of the specified matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>
		/// Zero if the determinants are equal.
		/// Less than zero if the determinant of the current matrix is less than the determinant of the <paramref name="value"/>.
		/// Greater than zero if the determinant of the current matrix is greater than the determinant of the <paramref name="value"/>.
		/// </returns>
		public int CompareTo(Matrix4 value)
		{
			return this.Determinant.CompareTo(value.Determinant);
		}

		/// <summary>
		/// Returns the constrained billboard transformation matrix created from the specified object position, camera position and the billboard axis vector.
		/// </summary>
		/// <param name="objectPosition">Object position.</param>
		/// <param name="cameraPosition">Camera position.</param>
		/// <param name="billboardAxis">Billboard axis vector.</param>
		/// <returns>The constrained billboard transformation matrix created from the <paramref name="objectPosition"/>, <paramref name="cameraPosition"/> and the <paramref name="billboardAxis"/>.</returns>
		public static Matrix4 ConstrainedBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 billboardAxis)
		{
			var x = Vector3.Normalize(Vector3.Cross(billboardAxis, cameraPosition - objectPosition));
			var y = Vector3.Normalize(billboardAxis);
			var z = Vector3.Normalize(Vector3.Cross(x, billboardAxis));

			return new Matrix4
			{
				M11 = x.X,
				M21 = x.Y,
				M31 = x.Z,
				M12 = y.X,
				M22 = y.Y,
				M32 = y.Z,
				M13 = z.X,
				M23 = z.Y,
				M33 = z.Z,
				M14 = objectPosition.X,
				M24 = objectPosition.Y,
				M34 = objectPosition.Z,
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the elements of the specified matrix divided by the specified scalar.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The elements of the <paramref name="value1"/> divided by the <paramref name="value2"/>.</returns>
		public static Matrix4 Divide(Matrix4 value1, Number value2)
		{
			return new Matrix4
			{
				M11 = value1.M11 / value2,
				M21 = value1.M21 / value2,
				M31 = value1.M31 / value2,
				M41 = value1.M41 / value2,
				M12 = value1.M12 / value2,
				M22 = value1.M22 / value2,
				M32 = value1.M32 / value2,
				M42 = value1.M42 / value2,
				M13 = value1.M13 / value2,
				M23 = value1.M23 / value2,
				M33 = value1.M33 / value2,
				M43 = value1.M43 / value2,
				M14 = value1.M14 / value2,
				M24 = value1.M24 / value2,
				M34 = value1.M34 / value2,
				M44 = value1.M44 / value2,
			};
		}

		/// <summary>
		/// Determines whether the current object is equal to the specified object.
		/// </summary>
		/// <param name="value">An object.</param>
		/// <returns>True if the current object is equal to the <paramref name="value"/>, otherwise false.</returns>
		public override bool Equals(object value)
		{
			if (value is Matrix4)
			{
				return this.Equals((Matrix4)value);
			}

			return false;
		}

		/// <summary>
		/// Determines whether the current matrix is equal to the specified matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>True if the current matrix is equal to the <paramref name="value"/>, otherwise false.</returns>
		public bool Equals(Matrix4 value)
		{
			return
				this.M11 == value.M11 &&
				this.M21 == value.M21 &&
				this.M31 == value.M31 &&
				this.M41 == value.M41 &&
				this.M12 == value.M12 &&
				this.M22 == value.M22 &&
				this.M32 == value.M32 &&
				this.M42 == value.M42 &&
				this.M13 == value.M13 &&
				this.M23 == value.M23 &&
				this.M33 == value.M33 &&
				this.M43 == value.M43 &&
				this.M14 == value.M14 &&
				this.M24 == value.M24 &&
				this.M34 == value.M34 &&
				this.M44 == value.M44;
		}

		/// <summary>
		/// Returns the largest integer value less than or equal to each element of the specified matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The largest integer value less than or equal to each element of the <paramref name="value"/>.</returns>
		public static Matrix4 Floor(Matrix4 value)
		{
			return new Matrix4
			{
				M11 = (Number)Math.Floor(value.M11),
				M21 = (Number)Math.Floor(value.M21),
				M31 = (Number)Math.Floor(value.M31),
				M41 = (Number)Math.Floor(value.M41),
				M12 = (Number)Math.Floor(value.M12),
				M22 = (Number)Math.Floor(value.M22),
				M32 = (Number)Math.Floor(value.M32),
				M42 = (Number)Math.Floor(value.M42),
				M13 = (Number)Math.Floor(value.M13),
				M23 = (Number)Math.Floor(value.M23),
				M33 = (Number)Math.Floor(value.M33),
				M43 = (Number)Math.Floor(value.M43),
				M14 = (Number)Math.Floor(value.M14),
				M24 = (Number)Math.Floor(value.M24),
				M34 = (Number)Math.Floor(value.M34),
				M44 = (Number)Math.Floor(value.M44),
			};
		}

		/// <summary>
		/// Returns the fractional part of each of the elements of the specified matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The fractional part of each of the elements of the <paramref name="value"/>.</returns>
		public static Matrix4 Fraction(Matrix4 value)
		{
			return new Matrix4
			{
				M11 = value.M11 - (Number)Math.Floor(value.M11),
				M21 = value.M21 - (Number)Math.Floor(value.M21),
				M31 = value.M31 - (Number)Math.Floor(value.M31),
				M41 = value.M41 - (Number)Math.Floor(value.M41),
				M12 = value.M12 - (Number)Math.Floor(value.M12),
				M22 = value.M22 - (Number)Math.Floor(value.M22),
				M32 = value.M32 - (Number)Math.Floor(value.M32),
				M42 = value.M42 - (Number)Math.Floor(value.M42),
				M13 = value.M13 - (Number)Math.Floor(value.M13),
				M23 = value.M23 - (Number)Math.Floor(value.M23),
				M33 = value.M33 - (Number)Math.Floor(value.M33),
				M43 = value.M43 - (Number)Math.Floor(value.M43),
				M14 = value.M14 - (Number)Math.Floor(value.M14),
				M24 = value.M24 - (Number)Math.Floor(value.M24),
				M34 = value.M34 - (Number)Math.Floor(value.M34),
				M44 = value.M44 - (Number)Math.Floor(value.M44),
			};
		}

		/// <summary>
		/// Returns a hash code for the current matrix.
		/// </summary>
		/// <returns>A hash code.</returns>
		/// <remarks>
		/// The hash code is not unique.
		/// If two matrices are equal, their hash codes are guaranteed to be equal.
		/// If the matrices are not equal, their hash codes are not guaranteed to be different.
		/// </remarks>
		public override int GetHashCode()
		{
			return HashCode.GetHashCode(
				HashCode.GetHashCode(this.M11.GetHashCode(), this.M21.GetHashCode(), this.M31.GetHashCode(), this.M41.GetHashCode()),
				HashCode.GetHashCode(this.M12.GetHashCode(), this.M22.GetHashCode(), this.M32.GetHashCode(), this.M42.GetHashCode()),
				HashCode.GetHashCode(this.M13.GetHashCode(), this.M23.GetHashCode(), this.M33.GetHashCode(), this.M43.GetHashCode()),
				HashCode.GetHashCode(this.M14.GetHashCode(), this.M24.GetHashCode(), this.M34.GetHashCode(), this.M44.GetHashCode()));
		}

		/// <summary>
		/// Returns an enumerator that iterates through the elements.
		/// </summary>
		/// <returns>
		/// An enumerator object that can be used to iterate through the elements.
		/// </returns>
		public IEnumerator<Number> GetEnumerator()
		{
			yield return this.M11;
			yield return this.M21;
			yield return this.M31;
			yield return this.M41;
			yield return this.M12;
			yield return this.M22;
			yield return this.M32;
			yield return this.M42;
			yield return this.M13;
			yield return this.M23;
			yield return this.M33;
			yield return this.M43;
			yield return this.M14;
			yield return this.M24;
			yield return this.M34;
			yield return this.M44;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the elements.
		/// </summary>
		/// <returns>
		/// An enumerator object that can be used to iterate through the elements.
		/// </returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>
		/// Returns the inverse of the specified matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The inverse of the <paramref name="value"/>.</returns>
		public static Matrix4 Invert(Matrix4 value)
		{
			Matrix4 result;

			var d01 = value.M31 * value.M42 - value.M41 * value.M32;
			var d02 = value.M31 * value.M43 - value.M41 * value.M33;
			var d12 = value.M32 * value.M43 - value.M42 * value.M33;
			var d13 = value.M32 * value.M44 - value.M42 * value.M34;
			var d23 = value.M33 * value.M44 - value.M43 * value.M34;
			var d30 = value.M34 * value.M41 - value.M44 * value.M31;

			result.M11 = value.M22 * d23 - value.M23 * d13 + value.M24 * d12;
			result.M21 = -(value.M21 * d23 + value.M23 * d30 + value.M24 * d02);
			result.M31 = value.M21 * d13 + value.M22 * d30 + value.M24 * d01;
			result.M41 = -(value.M21 * d12 - value.M22 * d02 + value.M23 * d01);

			var s = 1 / (value.M11 * result.M11 + value.M12 * result.M21 + value.M13 * result.M31 + value.M14 * result.M41);

			result.M11 *= s;
			result.M21 *= s;
			result.M31 *= s;
			result.M41 *= s;

			result.M12 = -(value.M12 * d23 - value.M13 * d13 + value.M14 * d12) * s;
			result.M22 = (value.M11 * d23 + value.M13 * d30 + value.M14 * d02) * s;
			result.M32 = -(value.M11 * d13 + value.M12 * d30 + value.M14 * d01) * s;
			result.M42 = (value.M11 * d12 - value.M12 * d02 + value.M13 * d01) * s;

			d01 = value.M11 * value.M22 - value.M21 * value.M12;
			d02 = value.M11 * value.M23 - value.M21 * value.M13;
			d12 = value.M12 * value.M23 - value.M22 * value.M13;
			d13 = value.M12 * value.M24 - value.M22 * value.M14;
			d23 = value.M13 * value.M24 - value.M23 * value.M14;
			d30 = value.M14 * value.M21 - value.M24 * value.M11;

			result.M13 = (value.M42 * d23 - value.M43 * d13 + value.M44 * d12) * s;
			result.M23 = -(value.M41 * d23 + value.M43 * d30 + value.M44 * d02) * s;
			result.M33 = (value.M41 * d13 + value.M42 * d30 + value.M44 * d01) * s;
			result.M43 = -(value.M41 * d12 - value.M42 * d02 + value.M43 * d01) * s;
			result.M14 = -(value.M32 * d23 - value.M33 * d13 + value.M34 * d12) * s;
			result.M24 = (value.M31 * d23 + value.M33 * d30 + value.M34 * d02) * s;
			result.M34 = -(value.M31 * d13 + value.M32 * d30 + value.M34 * d01) * s;
			result.M44 = (value.M31 * d12 - value.M32 * d02 + value.M33 * d01) * s;

			return result;
		}

		/// <summary>
		/// Returns the rigid-body inverse of the specified matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The rigid-body inverse of the <paramref name="value"/>.</returns>
		public static Matrix4 InvertRigidBody(Matrix4 value)
		{
			return new Matrix4
			{
				M11 = value.M11,
				M21 = value.M12,
				M31 = value.M13,
				M41 = 0,
				M12 = value.M21,
				M22 = value.M22,
				M32 = value.M23,
				M42 = 0,
				M13 = value.M31,
				M23 = value.M32,
				M33 = value.M33,
				M43 = 0,
				M14 = -(value.M11 * value.M14 + value.M21 * value.M24 + value.M31 * value.M34),
				M24 = -(value.M12 * value.M14 + value.M22 * value.M24 + value.M32 * value.M34),
				M34 = -(value.M13 * value.M14 + value.M23 * value.M24 + value.M33 * value.M34),
				M44 = 1,
			};
		}

		/// <summary>
		/// Determines whether any of the elements of the specified matrix evaluates to an infinity.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>True if any of the elements of <paramref name="value"/> evaluates to an infinity, otherwise false.</returns>
		public static bool IsInfinity(Matrix4 value)
		{
			return
				Number.IsInfinity(value.M11) ||
				Number.IsInfinity(value.M21) ||
				Number.IsInfinity(value.M31) ||
				Number.IsInfinity(value.M41) ||
				Number.IsInfinity(value.M12) ||
				Number.IsInfinity(value.M22) ||
				Number.IsInfinity(value.M32) ||
				Number.IsInfinity(value.M42) ||
				Number.IsInfinity(value.M13) ||
				Number.IsInfinity(value.M23) ||
				Number.IsInfinity(value.M33) ||
				Number.IsInfinity(value.M43) ||
				Number.IsInfinity(value.M14) ||
				Number.IsInfinity(value.M24) ||
				Number.IsInfinity(value.M34) ||
				Number.IsInfinity(value.M44);
		}

		/// <summary>
		/// Determines whether any of the elements of the specified matrix evaluates to a value that is not a number.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>True if any of the elements of <paramref name="value"/> evaluates to a value that is not a number, otherwise false.</returns>
		public static bool IsNaN(Matrix4 value)
		{
			return
				Number.IsNaN(value.M11) ||
				Number.IsNaN(value.M21) ||
				Number.IsNaN(value.M31) ||
				Number.IsNaN(value.M41) ||
				Number.IsNaN(value.M12) ||
				Number.IsNaN(value.M22) ||
				Number.IsNaN(value.M32) ||
				Number.IsNaN(value.M42) ||
				Number.IsNaN(value.M13) ||
				Number.IsNaN(value.M23) ||
				Number.IsNaN(value.M33) ||
				Number.IsNaN(value.M43) ||
				Number.IsNaN(value.M14) ||
				Number.IsNaN(value.M24) ||
				Number.IsNaN(value.M34) ||
				Number.IsNaN(value.M44);
		}

		/// <summary>
		/// Determines whether any of the elements of the specified matrix evaluates to negative infinity.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>True if any of the elements of <paramref name="value"/> evaluates to negative infinity, otherwise false.</returns>
		public static bool IsNegativeInfinity(Matrix4 value)
		{
			return
				Number.IsNegativeInfinity(value.M11) ||
				Number.IsNegativeInfinity(value.M21) ||
				Number.IsNegativeInfinity(value.M31) ||
				Number.IsNegativeInfinity(value.M41) ||
				Number.IsNegativeInfinity(value.M12) ||
				Number.IsNegativeInfinity(value.M22) ||
				Number.IsNegativeInfinity(value.M32) ||
				Number.IsNegativeInfinity(value.M42) ||
				Number.IsNegativeInfinity(value.M13) ||
				Number.IsNegativeInfinity(value.M23) ||
				Number.IsNegativeInfinity(value.M33) ||
				Number.IsNegativeInfinity(value.M43) ||
				Number.IsNegativeInfinity(value.M14) ||
				Number.IsNegativeInfinity(value.M24) ||
				Number.IsNegativeInfinity(value.M34) ||
				Number.IsNegativeInfinity(value.M44);
		}

		/// <summary>
		/// Determines whether any of the elements of the specified matrix evaluates to positive infinity.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>True if any of the elements of <paramref name="value"/> evaluates to positive infinity, otherwise false.</returns>
		public static bool IsPositiveInfinity(Matrix4 value)
		{
			return
				Number.IsPositiveInfinity(value.M11) ||
				Number.IsPositiveInfinity(value.M21) ||
				Number.IsPositiveInfinity(value.M31) ||
				Number.IsPositiveInfinity(value.M41) ||
				Number.IsPositiveInfinity(value.M12) ||
				Number.IsPositiveInfinity(value.M22) ||
				Number.IsPositiveInfinity(value.M32) ||
				Number.IsPositiveInfinity(value.M42) ||
				Number.IsPositiveInfinity(value.M13) ||
				Number.IsPositiveInfinity(value.M23) ||
				Number.IsPositiveInfinity(value.M33) ||
				Number.IsPositiveInfinity(value.M43) ||
				Number.IsPositiveInfinity(value.M14) ||
				Number.IsPositiveInfinity(value.M24) ||
				Number.IsPositiveInfinity(value.M34) ||
				Number.IsPositiveInfinity(value.M44);
		}

		/// <summary>
		/// Returns the look-at transformation matrix created from the specified object position, camera position and the camera up vector.
		/// </summary>
		/// <param name="objectPosition">Object position.</param>
		/// <param name="cameraPosition">Camera position.</param>
		/// <param name="cameraUpVector">Camera up vector.</param>
		/// <returns>The look-at transformation matrix created from the <paramref name="objectPosition"/>, <paramref name="cameraPosition"/> and the <paramref name="cameraUpVector"/>.</returns>
		public static Matrix4 LookAt(Vector3 objectPosition, Vector3 cameraPosition, Vector3 cameraUpVector)
		{
			var z = Vector3.Normalize(cameraPosition - objectPosition);
			var x = Vector3.Normalize(Vector3.Cross(cameraUpVector, z));
			var y = Vector3.Normalize(Vector3.Cross(z, x));

			return new Matrix4
			{
				M11 = x.X,
				M21 = y.X,
				M31 = z.X,
				M12 = x.Y,
				M22 = y.Y,
				M32 = z.Y,
				M13 = x.Z,
				M23 = y.Z,
				M33 = z.Z,
				M14 = -x.X * cameraPosition.X - x.Y * cameraPosition.Y - x.Z * cameraPosition.Z,
				M24 = -y.X * cameraPosition.X - y.Y * cameraPosition.Y - y.Z * cameraPosition.Z,
				M34 = -z.X * cameraPosition.X - z.Y * cameraPosition.Y - z.Z * cameraPosition.Z,
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the larger value of each element of the spefied matrix and the specified scalar.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The larger values of the elements of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Matrix4 Max(Matrix4 value1, Number value2)
		{
			return new Matrix4
			{
				M11 = Math.Max(value1.M11, value2),
				M21 = Math.Max(value1.M21, value2),
				M31 = Math.Max(value1.M31, value2),
				M41 = Math.Max(value1.M41, value2),
				M12 = Math.Max(value1.M12, value2),
				M22 = Math.Max(value1.M22, value2),
				M32 = Math.Max(value1.M32, value2),
				M42 = Math.Max(value1.M42, value2),
				M13 = Math.Max(value1.M13, value2),
				M23 = Math.Max(value1.M23, value2),
				M33 = Math.Max(value1.M33, value2),
				M43 = Math.Max(value1.M43, value2),
				M14 = Math.Max(value1.M14, value2),
				M24 = Math.Max(value1.M24, value2),
				M34 = Math.Max(value1.M34, value2),
				M44 = Math.Max(value1.M44, value2),
			};
		}

		/// <summary>
		/// Returns the larger value of each element of the specified matrices.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <returns>The larger values of the elements of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Matrix4 Max(Matrix4 value1, Matrix4 value2)
		{
			return new Matrix4
			{
				M11 = Math.Max(value1.M11, value2.M11),
				M21 = Math.Max(value1.M21, value2.M21),
				M31 = Math.Max(value1.M31, value2.M31),
				M41 = Math.Max(value1.M41, value2.M41),
				M12 = Math.Max(value1.M12, value2.M12),
				M22 = Math.Max(value1.M22, value2.M22),
				M32 = Math.Max(value1.M32, value2.M32),
				M42 = Math.Max(value1.M42, value2.M42),
				M13 = Math.Max(value1.M13, value2.M13),
				M23 = Math.Max(value1.M23, value2.M23),
				M33 = Math.Max(value1.M33, value2.M33),
				M43 = Math.Max(value1.M43, value2.M43),
				M14 = Math.Max(value1.M14, value2.M14),
				M24 = Math.Max(value1.M24, value2.M24),
				M34 = Math.Max(value1.M34, value2.M34),
				M44 = Math.Max(value1.M44, value2.M44),
			};
		}

		/// <summary>
		/// Returns the smaller value of each element of the specified matrix and the specified scalar.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The smaller values of the elements of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Matrix4 Min(Matrix4 value1, Number value2)
		{
			return new Matrix4
			{
				M11 = Math.Min(value1.M11, value2),
				M21 = Math.Min(value1.M21, value2),
				M31 = Math.Min(value1.M31, value2),
				M41 = Math.Min(value1.M41, value2),
				M12 = Math.Min(value1.M12, value2),
				M22 = Math.Min(value1.M22, value2),
				M32 = Math.Min(value1.M32, value2),
				M42 = Math.Min(value1.M42, value2),
				M13 = Math.Min(value1.M13, value2),
				M23 = Math.Min(value1.M23, value2),
				M33 = Math.Min(value1.M33, value2),
				M43 = Math.Min(value1.M43, value2),
				M14 = Math.Min(value1.M14, value2),
				M24 = Math.Min(value1.M24, value2),
				M34 = Math.Min(value1.M34, value2),
				M44 = Math.Min(value1.M44, value2),
			};
		}

		/// <summary>
		/// Returns the smaller value of each element of the specified matrices.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <returns>The smaller values of the elements of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Matrix4 Min(Matrix4 value1, Matrix4 value2)
		{
			return new Matrix4
			{
				M11 = Math.Min(value1.M11, value2.M11),
				M21 = Math.Min(value1.M21, value2.M21),
				M31 = Math.Min(value1.M31, value2.M31),
				M41 = Math.Min(value1.M41, value2.M41),
				M12 = Math.Min(value1.M12, value2.M12),
				M22 = Math.Min(value1.M22, value2.M22),
				M32 = Math.Min(value1.M32, value2.M32),
				M42 = Math.Min(value1.M42, value2.M42),
				M13 = Math.Min(value1.M13, value2.M13),
				M23 = Math.Min(value1.M23, value2.M23),
				M33 = Math.Min(value1.M33, value2.M33),
				M43 = Math.Min(value1.M43, value2.M43),
				M14 = Math.Min(value1.M14, value2.M14),
				M24 = Math.Min(value1.M24, value2.M24),
				M34 = Math.Min(value1.M34, value2.M34),
				M44 = Math.Min(value1.M44, value2.M44),
			};
		}

		/// <summary>
		/// Returns the model transformation matrix created from the specified object position and the object rotation quaternion.
		/// </summary>
		/// <param name="objectPosition">Object position.</param>
		/// <param name="objectRotation">Object rotation quaternion.</param>
		/// <returns>The model transformation matrix created from the <paramref name="objectPosition"/> and the <paramref name="objectRotation"/>.</returns>
		public static Matrix4 Model(Vector3 objectPosition, Quaternion objectRotation)
		{
			var ii = objectRotation.X * objectRotation.X;
			var ij = objectRotation.X * objectRotation.Y;
			var ik = objectRotation.X * objectRotation.Z;
			var iw = objectRotation.X * objectRotation.W;
			var jj = objectRotation.Y * objectRotation.Y;
			var jk = objectRotation.Y * objectRotation.Z;
			var jw = objectRotation.Y * objectRotation.W;
			var kk = objectRotation.Z * objectRotation.Z;
			var kw = objectRotation.Z * objectRotation.W;

			return new Matrix4
			{
				M11 = 1 - 2 * (jj + kk),
				M21 = 2 * (ij + kw),
				M31 = 2 * (ik - jw),
				M12 = 2 * (ij - kw),
				M22 = 1 - 2 * (ii + kk),
				M32 = 2 * (jk + iw),
				M13 = 2 * (ik + jw),
				M23 = 2 * (jk - iw),
				M33 = 1 - 2 * (ii + jj),
				M14 = objectPosition.X,
				M24 = objectPosition.Y,
				M34 = objectPosition.Z,
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the model transformation matrix created from the specified object position, object up vector and the object forward vector.
		/// </summary>
		/// <param name="objectPosition">Object position.</param>
		/// <param name="objectUpVector">Object up vector.</param>
		/// <param name="objectForwardVector">Object forward vector.</param>
		/// <returns>The model transformation matrix created from the <paramref name="objectPosition"/>, <paramref name="objectUpVector"/> and the <paramref name="objectForwardVector"/>.</returns>
		public static Matrix4 Model(Vector3 objectPosition, Vector3 objectUpVector, Vector3 objectForwardVector)
		{
			var z = Vector3.Normalize(-objectForwardVector);
			var x = Vector3.Normalize(Vector3.Cross(objectUpVector, z));
			var y = Vector3.Normalize(Vector3.Cross(z, x));

			return new Matrix4
			{
				M11 = x.X,
				M21 = x.Y,
				M31 = x.Z,
				M12 = y.X,
				M22 = y.Y,
				M32 = y.Z,
				M13 = z.X,
				M23 = z.Y,
				M33 = z.Z,
				M14 = objectPosition.X,
				M24 = objectPosition.Y,
				M34 = objectPosition.Z,
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the remainder of the elements of the specified matrix divided by the specified scalar.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The remainder of the elements of the <paramref name="value1"/> divided by the <paramref name="value2"/>.</returns>
		public static Matrix4 Modulo(Matrix4 value1, Number value2)
		{
			return new Matrix4
			{
				M11 = value1.M11 % value2,
				M21 = value1.M21 % value2,
				M31 = value1.M31 % value2,
				M41 = value1.M41 % value2,
				M12 = value1.M12 % value2,
				M22 = value1.M22 % value2,
				M32 = value1.M32 % value2,
				M42 = value1.M42 % value2,
				M13 = value1.M13 % value2,
				M23 = value1.M23 % value2,
				M33 = value1.M33 % value2,
				M43 = value1.M43 % value2,
				M14 = value1.M14 % value2,
				M24 = value1.M24 % value2,
				M34 = value1.M34 % value2,
				M44 = value1.M44 % value2,
			};
		}

		/// <summary>
		/// Returns the elements of the specified matrix multiplied by the specified scalar.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The elements of the <paramref name="value1"/> multiplied by the <paramref name="value2"/>.</returns>
		public static Matrix4 Multiply(Matrix4 value1, Number value2)
		{
			return new Matrix4
			{
				M11 = value1.M11 * value2,
				M21 = value1.M21 * value2,
				M31 = value1.M31 * value2,
				M41 = value1.M41 * value2,
				M12 = value1.M12 * value2,
				M22 = value1.M22 * value2,
				M32 = value1.M32 * value2,
				M42 = value1.M42 * value2,
				M13 = value1.M13 * value2,
				M23 = value1.M23 * value2,
				M33 = value1.M33 * value2,
				M43 = value1.M43 * value2,
				M14 = value1.M14 * value2,
				M24 = value1.M24 * value2,
				M34 = value1.M34 * value2,
				M44 = value1.M44 * value2,
			};
		}

		/// <summary>
		/// Returns the product of the specified matrix and the specified vector.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A vector.</param>
		/// <returns>The product of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Vector4 Multiply(Matrix4 value1, Vector4 value2)
		{
			return new Vector4
			{
				X = value1.M11 * value2.X + value1.M12 * value2.Y + value1.M13 * value2.Z + value1.M14 * value2.W,
				Y = value1.M21 * value2.X + value1.M22 * value2.Y + value1.M23 * value2.Z + value1.M24 * value2.W,
				Z = value1.M31 * value2.X + value1.M32 * value2.Y + value1.M33 * value2.Z + value1.M34 * value2.W,
				W = value1.M41 * value2.X + value1.M42 * value2.Y + value1.M43 * value2.Z + value1.M44 * value2.W,
			};
		}

		/// <summary>
		/// Returns the product of the specified matrices.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <returns>The product of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Matrix4 Multiply(Matrix4 value1, Matrix4 value2)
		{
			return new Matrix4
			{
				M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41,
				M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41,
				M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41,
				M41 = value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41,
				M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42,
				M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42,
				M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42,
				M42 = value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42,
				M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43,
				M23 = value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43,
				M33 = value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43,
				M43 = value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43,
				M14 = value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44,
				M24 = value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44,
				M34 = value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44,
				M44 = value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44,
			};
		}

		/// <summary>
		/// Returns the negation of the specified matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The negation of the <paramref name="value"/>.</returns>
		public static Matrix4 Negate(Matrix4 value)
		{
			return new Matrix4
			{
				M11 = -value.M11,
				M21 = -value.M21,
				M31 = -value.M31,
				M41 = -value.M41,
				M12 = -value.M12,
				M22 = -value.M22,
				M32 = -value.M32,
				M42 = -value.M42,
				M13 = -value.M13,
				M23 = -value.M23,
				M33 = -value.M33,
				M43 = -value.M43,
				M14 = -value.M14,
				M24 = -value.M24,
				M34 = -value.M34,
				M44 = -value.M44,
			};
		}

		/// <summary>
		/// Returns the orthographic projection matrix created from the specified view frustum parameters.
		/// </summary>
		/// <param name="left">X coordinate of the left vertical clipping plane.</param>
		/// <param name="right">X coordinate of the right vertical clipping plane.</param>
		/// <param name="bottom">Y coordinate of the bottom horizontal clipping plane.</param>
		/// <param name="top">Y coordinate of the top horizontal clipping plane.</param>
		/// <param name="near">The distance to the near depth clipping plane.</param>
		/// <param name="far">The distance to the far depth clipping plane.</param>
		/// <returns>The orthographic projection matrix created from the view frustum parameters.</returns>
		public static Matrix4 Orthographic(Number left, Number right, Number bottom, Number top, Number near, Number far)
		{
			return new Matrix4
			{
				M11 = 2 / (right - left),
				M22 = 2 / (top - bottom),
				M33 = -2 / (far - near),
				M14 = -(right + left) / (right - left),
				M24 = -(top + bottom) / (top - bottom),
				M34 = -(far + near) / (far - near),
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the matrix parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <returns>The matrix parsed from the <paramref name="value"/>.</returns>
		public static Matrix4 Parse(string value)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, null);
		}

		/// <summary>
		/// Returns the matrix parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each element.</param>
		/// <returns>The matrix parsed from the <paramref name="value"/>.</returns>
		public static Matrix4 Parse(string value, NumberStyles numberStyle)
		{
			return Parse(value, numberStyle, null);
		}

		/// <summary>
		/// Returns the matrix parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each element.</param>
		/// <returns>The matrix parsed from the <paramref name="value"/>.</returns>
		public static Matrix4 Parse(string value, IFormatProvider formatProvider)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider);
		}

		/// <summary>
		/// Returns the matrix parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each element.</param>
		/// <param name="formatProvider">The format provider for each element.</param>
		/// <returns>The matrix parsed from the <paramref name="value"/>.</returns>
		public static Matrix4 Parse(string value, NumberStyles numberStyle, IFormatProvider formatProvider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var numbers = Float.Parse(value, numberStyle, formatProvider);

			if (numbers.Length == ValueCount)
			{
				return new Matrix4
				{
					M11 = numbers[0],
					M21 = numbers[1],
					M31 = numbers[2],
					M41 = numbers[3],
					M12 = numbers[4],
					M22 = numbers[5],
					M32 = numbers[6],
					M42 = numbers[7],
					M13 = numbers[8],
					M23 = numbers[9],
					M33 = numbers[10],
					M43 = numbers[11],
					M14 = numbers[12],
					M24 = numbers[13],
					M34 = numbers[14],
					M44 = numbers[15],
				};
			}
			else
			{
				throw new ValueCountMismatchException();
			}
		}

		/// <summary>
		/// Returns the perspective projection matrix created from the specified view frustum parameters.
		/// </summary>
		/// <param name="verticalFieldOfView">Vertical field of view angle.</param>
		/// <param name="viewportWidth">Width of the viewport.</param>
		/// <param name="viewportHeight">Height of the viewport.</param>
		/// <param name="near">The distance to the near depth clipping plane.</param>
		/// <param name="far">The distance to the far depth clipping plane.</param>
		/// <returns>The perspective projection matrix created from the view frustum parameters.</returns>
		public static Matrix4 Perspective(Angle verticalFieldOfView, Number viewportWidth, Number viewportHeight, Number near, Number far)
		{
			var aspectRatio = viewportWidth / viewportHeight;

			var ymax = near * Angle.Tan(verticalFieldOfView * 0.5f);
			var ymin = -ymax;

			var xmin = ymin * aspectRatio;
			var xmax = ymax * aspectRatio;

			return Perspective(xmin, xmax, ymin, ymax, near, far);
		}

		/// <summary>
		/// Returns the perspective projection matrix created from the specified view frustum parameters.
		/// </summary>
		/// <param name="left">X coordinate of the left vertical clipping plane.</param>
		/// <param name="right">X coordinate of the right vertical clipping plane.</param>
		/// <param name="bottom">Y coordinate of the bottom horizontal clipping plane.</param>
		/// <param name="top">Y coordinate of the top horizontal clipping plane.</param>
		/// <param name="near">The distance to the near depth clipping plane.</param>
		/// <param name="far">The distance to the far depth clipping plane.</param>
		/// <returns>The perspective projection matrix created from the view frustum parameters.</returns>
		public static Matrix4 Perspective(Number left, Number right, Number bottom, Number top, Number near, Number far)
		{
			var A = (right + left) / (right - left);
			var B = (top + bottom) / (top - bottom);
			var C = -(far + near) / (far - near);
			var D = -(2 * far * near) / (far - near);

			return new Matrix4
			{
				M11 = (2 * near) / (right - left),
				M22 = (2 * near) / (top - bottom),
				M13 = A,
				M23 = B,
				M33 = C,
				M43 = -1,
				M34 = D,
			};
		}

		/// <summary>
		/// Returns the picking projection matrix created from the specified picking region coordinates.
		/// </summary>
		/// <param name="x">X coordinate of the picking region.</param>
		/// <param name="y">Y coordinate of the picking region.</param>
		/// <param name="width">The width of the picking region.</param>
		/// <param name="height">The height of the picking region.</param>
		/// <param name="viewportWidth">Width of the viewport.</param>
		/// <param name="viewportHeight">Height of the viewport.</param>
		/// <returns>The picking projection matrix created from the picking region coordinates.</returns>
		public static Matrix4 Pick(Number x, Number y, Number width, Number height, Number viewportWidth, Number viewportHeight)
		{
			return new Matrix4
			{
				M11 = viewportWidth / width,
				M22 = viewportHeight / height,
				M33 = 1,
				M14 = (viewportWidth - 2 * x) / width,
				M24 = (viewportHeight - 2 * y) / height,
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the specified vector projected to the viewport.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="projection">Projection matrix.</param>
		/// <param name="viewportWidth">Width of the viewport.</param>
		/// <param name="viewportHeight">Height of the viewport.</param>
		/// <returns>The <paramref name="value"/> projected to the viewport.</returns>
		public static Vector3 ProjectToViewport(Vector3 value, Matrix4 projection, Number viewportWidth, Number viewportHeight)
		{
			var x = value.X * projection.M11 + value.Y * projection.M12 + value.Z * projection.M13 + projection.M14;
			var y = value.X * projection.M21 + value.Y * projection.M22 + value.Z * projection.M23 + projection.M24;
			var z = value.X * projection.M31 + value.Y * projection.M32 + value.Z * projection.M33 + projection.M34;
			var w = value.X * projection.M41 + value.Y * projection.M42 + value.Z * projection.M43 + projection.M44;

			return new Vector3
			{
				X = (x / w + 1) * 0.5f * viewportWidth,
				Y = (y / w + 1) * 0.5f * viewportHeight,
				Z = (z / w + 1) * 0.5f,
			};
		}

		/// <summary>
		/// Returns the specified vector projected to the viewport.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="projection">Projection matrix.</param>
		/// <param name="viewportWidth">Width of the viewport.</param>
		/// <param name="viewportHeight">Height of the viewport.</param>
		/// <returns>The <paramref name="value"/> projected to the viewport.</returns>
		public static Vector4 ProjectToViewport(Vector4 value, Matrix4 projection, Number viewportWidth, Number viewportHeight)
		{
			var x = value.X * projection.M11 + value.Y * projection.M12 + value.Z * projection.M13 + value.W * projection.M14;
			var y = value.X * projection.M21 + value.Y * projection.M22 + value.Z * projection.M23 + value.W * projection.M24;
			var z = value.X * projection.M31 + value.Y * projection.M32 + value.Z * projection.M33 + value.W * projection.M34;
			var w = value.X * projection.M41 + value.Y * projection.M42 + value.Z * projection.M43 + value.W * projection.M44;

			return new Vector4
			{
				X = (x + 1) * 0.5f * viewportWidth,
				Y = (y + 1) * 0.5f * viewportHeight,
				Z = (z + 1) * 0.5f,
				W = w,
			};
		}

		/// <summary>
		/// Returns the rotation matrix from created the specified rotation quaternion.
		/// </summary>
		/// <param name="rotation">Rotation quaternion.</param>
		/// <returns>The rotation matrix created from the <paramref name="rotation"/>.</returns>
		public static Matrix4 Rotate(Quaternion rotation)
		{
			var ii = rotation.X * rotation.X;
			var ij = rotation.X * rotation.Y;
			var ik = rotation.X * rotation.Z;
			var iw = rotation.X * rotation.W;
			var jj = rotation.Y * rotation.Y;
			var jk = rotation.Y * rotation.Z;
			var jw = rotation.Y * rotation.W;
			var kk = rotation.Z * rotation.Z;
			var kw = rotation.Z * rotation.W;

			return new Matrix4
			{
				M11 = 1 - 2 * (jj + kk),
				M21 = 2 * (ij + kw),
				M31 = 2 * (ik - jw),
				M12 = 2 * (ij - kw),
				M22 = 1 - 2 * (ii + kk),
				M32 = 2 * (jk + iw),
				M13 = 2 * (ik + jw),
				M23 = 2 * (jk - iw),
				M33 = 1 - 2 * (ii + jj),
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the rotation matrix created from the specified angular velocity vector.
		/// </summary>
		/// <param name="angularVelocity">Angular velocity vector.</param>
		/// <returns>The rotation matrix created from the <paramref name="angularVelocity"/>.</returns>
		public static Matrix4 Rotate(Vector3 angularVelocity)
		{
			var scale = angularVelocity.Magnitude;

			if (Float.Near(scale, 0))
			{
				return identity;
			}
			else
			{
				return Rotate(Angle.FromRadians(scale), angularVelocity / scale);
			}
		}

		/// <summary>
		/// Returns the rotation matrix created from the specified rotation angle around the specified rotation axis.
		/// </summary>
		/// <param name="angle">Rotation angle.</param>
		/// <param name="axis">Rotation axis vector.</param>
		/// <returns>The rotation matrix created from the <paramref name="angle"/> around the <paramref name="axis"/>.</returns>
		public static Matrix4 Rotate(Angle angle, Vector3 axis)
		{
			var cosA = Angle.Cos(angle);
			var cosX = (1 - cosA) * axis.X;
			var cosY = (1 - cosA) * axis.Y;
			var cosZ = (1 - cosA) * axis.Z;

			var sinA = Angle.Sin(angle);
			var sinX = sinA * axis.X;
			var sinY = sinA * axis.Y;
			var sinZ = sinA * axis.Z;

			return new Matrix4
			{
				M11 = cosX * axis.X + cosA,
				M21 = cosY * axis.X + sinZ,
				M31 = cosZ * axis.X - sinY,
				M12 = cosX * axis.Y - sinZ,
				M22 = cosY * axis.Y + cosA,
				M32 = cosZ * axis.Y + sinX,
				M13 = cosX * axis.Z + sinY,
				M23 = cosY * axis.Z - sinX,
				M33 = cosZ * axis.Z + cosA,
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the rotation matrix created from the specified angles.
		/// </summary>
		/// <param name="yaw">Yaw rotation angle.</param>
		/// <param name="pitch">Pitch rotation angle.</param>
		/// <param name="roll">Roll rotation angle.</param>
		/// <returns>The rotation matrix created from the specified angles.</returns>
		public static Matrix4 Rotate(Angle yaw, Angle pitch, Angle roll)
		{
			var sinR = Angle.Sin(roll);
			var sinP = Angle.Sin(pitch);
			var sinH = Angle.Sin(yaw);
			var cosR = Angle.Cos(roll);
			var cosP = Angle.Cos(pitch);
			var cosH = Angle.Cos(yaw);

			return new Matrix4
			{
				M11 = cosR * cosH - sinR * sinP * sinH,
				M21 = sinR * cosH + cosR * sinP * sinH,
				M31 = -cosP * sinH,
				M12 = -sinR * cosP,
				M22 = cosR * cosP,
				M32 = sinP,
				M13 = cosR * sinH + sinR * sinP * cosH,
				M23 = sinR * sinH - cosR * sinP * cosH,
				M33 = cosP * cosH,
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the rotation matrix created from the specified angle around the X axis.
		/// </summary>
		/// <param name="angle">An angle.</param>
		/// <returns>The rotation matrix created from the <paramref name="angle"/>.</returns>
		public static Matrix4 RotateX(Angle angle)
		{
			var sin = Angle.Sin(angle);
			var cos = Angle.Cos(angle);

			return new Matrix4
			{
				M11 = 1,
				M22 = cos,
				M32 = sin,
				M23 = -sin,
				M33 = cos,
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the rotation matrix created from the specified angle around the Y axis.
		/// </summary>
		/// <param name="angle">An angle.</param>
		/// <returns>The rotation matrix created from the <paramref name="angle"/>.</returns>
		public static Matrix4 RotateY(Angle angle)
		{
			var sin = Angle.Sin(angle);
			var cos = Angle.Cos(angle);

			return new Matrix4
			{
				M11 = cos,
				M31 = -sin,
				M22 = 1,
				M13 = sin,
				M33 = cos,
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the rotation matrix created from the specified angle around the Z axis.
		/// </summary>
		/// <param name="angle">An angle.</param>
		/// <returns>The rotation matrix created from the <paramref name="angle"/>.</returns>
		public static Matrix4 RotateZ(Angle angle)
		{
			var sin = Angle.Sin(angle);
			var cos = Angle.Cos(angle);

			return new Matrix4
			{
				M11 = cos,
				M21 = sin,
				M12 = -sin,
				M22 = cos,
				M33 = 1,
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the elements of the specified matrix rounded to the nearest integer values.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The elements of the <paramref name="value"/> rounded to the nearest integer values.</returns>
		public static Matrix4 Round(Matrix4 value)
		{
			return new Matrix4
			{
				M11 = (Number)Math.Round(value.M11),
				M21 = (Number)Math.Round(value.M21),
				M31 = (Number)Math.Round(value.M31),
				M41 = (Number)Math.Round(value.M41),
				M12 = (Number)Math.Round(value.M12),
				M22 = (Number)Math.Round(value.M22),
				M32 = (Number)Math.Round(value.M32),
				M42 = (Number)Math.Round(value.M42),
				M13 = (Number)Math.Round(value.M13),
				M23 = (Number)Math.Round(value.M23),
				M33 = (Number)Math.Round(value.M33),
				M43 = (Number)Math.Round(value.M43),
				M14 = (Number)Math.Round(value.M14),
				M24 = (Number)Math.Round(value.M24),
				M34 = (Number)Math.Round(value.M34),
				M44 = (Number)Math.Round(value.M44),
			};
		}

		/// <summary>
		/// Returns the scaling transformation matrix created from the specified scaling factor.
		/// </summary>
		/// <param name="scaling">A scaling factor.</param>
		/// <returns>The scaling transformation matrix created from the <paramref name="scaling"/>.</returns>
		public static Matrix4 Scale(Number scaling)
		{
			return new Matrix4
			{
				M11 = scaling,
				M22 = scaling,
				M33 = scaling,
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the scaling transformation matrix created from the specified scaling factors.
		/// </summary>
		/// <param name="x">A scaling factor along the X axis.</param>
		/// <param name="y">A scaling factor along the Y axis.</param>
		/// <param name="z">A scaling factor along the Z axis.</param>
		/// <returns>The scaling transformation matrix created from the <paramref name="x"/>, <paramref name="y"/> and the <paramref name="z"/>.</returns>
		public static Matrix4 Scale(Number x, Number y, Number z)
		{
			return new Matrix4
			{
				M11 = x,
				M22 = y,
				M33 = z,
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns values indicating the sign of the elements of the specified matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>Values indicating the sign of the elements of the <paramref name="value"/>.</returns>
		public static Matrix4 Sign(Matrix4 value)
		{
			return new Matrix4
			{
				M11 = Math.Sign(value.M11),
				M21 = Math.Sign(value.M21),
				M31 = Math.Sign(value.M31),
				M41 = Math.Sign(value.M41),
				M12 = Math.Sign(value.M12),
				M22 = Math.Sign(value.M22),
				M32 = Math.Sign(value.M32),
				M42 = Math.Sign(value.M42),
				M13 = Math.Sign(value.M13),
				M23 = Math.Sign(value.M23),
				M33 = Math.Sign(value.M33),
				M43 = Math.Sign(value.M43),
				M14 = Math.Sign(value.M14),
				M24 = Math.Sign(value.M24),
				M34 = Math.Sign(value.M34),
				M44 = Math.Sign(value.M44),
			};
		}

		/// <summary>
		/// Returns the smooth step transition between the specified matrices.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <param name="amount">Step amount, in the range of [0, 1].</param>
		/// <returns>The smooth step transition from the <paramref name="value1"/> to the <paramref name="value2"/>.</returns>
		public static Matrix4 SmoothStep(Matrix4 value1, Matrix4 value2, Number amount)
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
		/// Returns the step transition between the specified matrices.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <param name="amount">Step amount, in the range of [0, 1].</param>
		/// <returns>The <paramref name="value2"/> if the <paramref name="amount"/> is greater than 0, otherwise the <paramref name="value1"/> is returned.</returns>
		public static Matrix4 Step(Matrix4 value1, Matrix4 value2, Number amount)
		{
			return (amount > 0) ? value2 : value1;
		}

		/// <summary>
		/// Returns the difference between the specified matrices.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <returns>The difference between the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Matrix4 Subtract(Matrix4 value1, Matrix4 value2)
		{
			return new Matrix4
			{
				M11 = value1.M11 - value2.M11,
				M21 = value1.M21 - value2.M21,
				M31 = value1.M31 - value2.M31,
				M41 = value1.M41 - value2.M41,
				M12 = value1.M12 - value2.M12,
				M22 = value1.M22 - value2.M22,
				M32 = value1.M32 - value2.M32,
				M42 = value1.M42 - value2.M42,
				M13 = value1.M13 - value2.M13,
				M23 = value1.M23 - value2.M23,
				M33 = value1.M33 - value2.M33,
				M43 = value1.M43 - value2.M43,
				M14 = value1.M14 - value2.M14,
				M24 = value1.M24 - value2.M24,
				M34 = value1.M34 - value2.M34,
				M44 = value1.M44 - value2.M44,
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
			return Float.ToString(format, formatProvider, this.M11, this.M21, this.M31, this.M41, this.M12, this.M22, this.M32, this.M42, this.M13, this.M23, this.M33, this.M43, this.M14, this.M24, this.M34, this.M44);
		}

		/// <summary>
		/// Returns the translation transformation matrix created from the specified translation vector.
		/// </summary>
		/// <param name="translation">Translation vector.</param>
		/// <returns>The translation transformation matrix created from the <paramref name="translation"/>.</returns>
		public static Matrix4 Translate(Vector3 translation)
		{
			return new Matrix4
			{
				M11 = 1,
				M22 = 1,
				M33 = 1,
				M14 = translation.X,
				M24 = translation.Y,
				M34 = translation.Z,
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the translation transformation matrix created from the specified translation amounts.
		/// </summary>
		/// <param name="x">A translation amount along the X axis.</param>
		/// <param name="y">A translation amount along the Y axis.</param>
		/// <param name="z">A translation amount along the Z axis.</param>
		/// <returns>The translation transformation matrix created from the <paramref name="x"/>, <paramref name="y"/> and the <paramref name="z"/>.</returns>
		public static Matrix4 Translate(Number x, Number y, Number z)
		{
			return new Matrix4
			{
				M11 = 1,
				M22 = 1,
				M33 = 1,
				M14 = x,
				M24 = y,
				M34 = z,
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the transpose of the specified matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The transpose of the <paramref name="value"/>.</returns>
		public static Matrix4 Transpose(Matrix4 value)
		{
			return new Matrix4
			{
				M11 = value.M11,
				M21 = value.M12,
				M31 = value.M13,
				M41 = value.M14,
				M12 = value.M21,
				M22 = value.M22,
				M32 = value.M23,
				M42 = value.M24,
				M13 = value.M31,
				M23 = value.M32,
				M33 = value.M33,
				M43 = value.M34,
				M14 = value.M41,
				M24 = value.M42,
				M34 = value.M43,
				M44 = value.M44,
			};
		}

		/// <summary>
		/// Attempts to parse the matrix from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="result">The output variable for the matrix parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, out Matrix4 result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, null, out result);
		}

		/// <summary>
		/// Attempts to parse the matrix from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each element.</param>
		/// <param name="result">The output variable for the matrix parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, out Matrix4 result)
		{
			return TryParse(value, numberStyle, null, out result);
		}

		/// <summary>
		/// Attempts to parse the matrix from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each element.</param>
		/// <param name="result">The output variable for the matrix parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, IFormatProvider formatProvider, out Matrix4 result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider, out result);
		}

		/// <summary>
		/// Attempts to parse the matrix from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each element.</param>
		/// <param name="formatProvider">The format provider for each element.</param>
		/// <param name="result">The output variable for the matrix parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, IFormatProvider formatProvider, out Matrix4 result)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var numbers = Float.Parse(value, numberStyle, formatProvider);

			if (numbers.Length == ValueCount)
			{
				result.M11 = numbers[0];
				result.M21 = numbers[1];
				result.M31 = numbers[2];
				result.M41 = numbers[3];
				result.M12 = numbers[4];
				result.M22 = numbers[5];
				result.M32 = numbers[6];
				result.M42 = numbers[7];
				result.M13 = numbers[8];
				result.M23 = numbers[9];
				result.M33 = numbers[10];
				result.M43 = numbers[11];
				result.M14 = numbers[12];
				result.M24 = numbers[13];
				result.M34 = numbers[14];
				result.M44 = numbers[15];
				return true;
			}
			else
			{
				result = default(Matrix4);
				return false;
			}
		}

		/// <summary>
		/// Returns the specified vector unprojected from the viewport.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="projection">Projection matrix.</param>
		/// <param name="viewportWidth">Width of the viewport.</param>
		/// <param name="viewportHeight">Height of the viewport.</param>
		/// <returns>The <paramref name="value"/> unprojected from the viewport.</returns>
		public static Vector3 UnprojectFromViewport(Vector3 value, Matrix4 projection, Number viewportWidth, Number viewportHeight)
		{
			var p = Invert(projection);
			var x = (value.X / viewportWidth) * 2 - 1;
			var y = (value.Y / viewportHeight) * 2 - 1;
			var z = value.Z * 2 - 1;
			var w = 1 / (x * p.M41 + y * p.M42 + z * p.M43 + p.M44);

			return new Vector3
			{
				X = (x * p.M11 + y * p.M12 + z * p.M13 + p.M14) * w,
				Y = (x * p.M21 + y * p.M22 + z * p.M23 + p.M24) * w,
				Z = (x * p.M31 + y * p.M32 + z * p.M33 + p.M34) * w,
			};
		}

		/// <summary>
		/// Returns the specified vector unprojected from the viewport.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="projection">Projection matrix.</param>
		/// <param name="viewportWidth">Width of the viewport.</param>
		/// <param name="viewportHeight">Height of the viewport.</param>
		/// <returns>The <paramref name="value"/> unprojected from the viewport.</returns>
		public static Vector4 UnprojectFromViewport(Vector4 value, Matrix4 projection, Number viewportWidth, Number viewportHeight)
		{
			var p = Invert(projection);
			var x = (value.X / viewportWidth) * 2 - 1;
			var y = (value.Y / viewportHeight) * 2 - 1;
			var z = value.Z * 2 - 1;
			var w = value.W;

			return new Vector4
			{
				X = x * p.M11 + y * p.M12 + z * p.M13 + w * p.M14,
				Y = x * p.M21 + y * p.M22 + z * p.M23 + w * p.M24,
				Z = x * p.M31 + y * p.M32 + z * p.M33 + w * p.M34,
				W = x * p.M41 + y * p.M42 + z * p.M43 + w * p.M44,
			};
		}

		/// <summary>
		/// Returns the view transformation matrix created from the specified camera position and the camera rotation quaternion.
		/// </summary>
		/// <param name="cameraPosition">Camera position.</param>
		/// <param name="cameraRotation">Camera rotation quaternion.</param>
		/// <returns>The view transformation matrix created from the <paramref name="cameraPosition"/> and the <paramref name="cameraRotation"/>.</returns>
		public static Matrix4 View(Vector3 cameraPosition, Quaternion cameraRotation)
		{
			var ii = cameraRotation.X * cameraRotation.X;
			var ij = cameraRotation.X * cameraRotation.Y;
			var ik = cameraRotation.X * cameraRotation.Z;
			var iw = cameraRotation.X * cameraRotation.W;
			var jj = cameraRotation.Y * cameraRotation.Y;
			var jk = cameraRotation.Y * cameraRotation.Z;
			var jw = cameraRotation.Y * cameraRotation.W;
			var kk = cameraRotation.Z * cameraRotation.Z;
			var kw = cameraRotation.Z * cameraRotation.W;

			var x = new Vector3
			{
				X = 1 - 2 * (jj + kk),
				Y = 2 * (ij + kw),
				Z = 2 * (ik - jw),
			};

			var y = new Vector3
			{
				X = 2 * (ij - kw),
				Y = 1 - 2 * (ii + kk),
				Z = 2 * (jk + iw),
			};

			var z = new Vector3
			{
				X = 2 * (ik + jw),
				Y = 2 * (jk - iw),
				Z = 1 - 2 * (ii + jj),
			};

			return new Matrix4
			{
				M11 = x.X,
				M21 = y.X,
				M31 = z.X,
				M12 = x.Y,
				M22 = y.Y,
				M32 = z.Y,
				M13 = x.Z,
				M23 = y.Z,
				M33 = z.Z,
				M14 = -x.X * cameraPosition.X - x.Y * cameraPosition.Y - x.Z * cameraPosition.Z,
				M24 = -y.X * cameraPosition.X - y.Y * cameraPosition.Y - y.Z * cameraPosition.Z,
				M34 = -z.X * cameraPosition.X - z.Y * cameraPosition.Y - z.Z * cameraPosition.Z,
				M44 = 1,
			};
		}

		/// <summary>
		/// Returns the view transformation matrix created from the specified camera position, camera up vector and the camera forward vector.
		/// </summary>
		/// <param name="cameraPosition">Camera position.</param>
		/// <param name="cameraUpVector">Camera up vector.</param>
		/// <param name="cameraForwardVector">Camera forward vector.</param>
		/// <returns>The view transformation matrix created from the <paramref name="cameraPosition"/>, <paramref name="cameraUpVector"/> and the <paramref name="cameraForwardVector"/>.</returns>
		public static Matrix4 View(Vector3 cameraPosition, Vector3 cameraUpVector, Vector3 cameraForwardVector)
		{
			var z = Vector3.Normalize(-cameraForwardVector);
			var x = Vector3.Normalize(Vector3.Cross(cameraUpVector, z));
			var y = Vector3.Normalize(Vector3.Cross(x, x));

			return new Matrix4
			{
				M11 = x.X,
				M21 = y.X,
				M31 = z.X,
				M12 = x.Y,
				M22 = y.Y,
				M32 = z.Y,
				M13 = x.Z,
				M23 = y.Z,
				M33 = z.Z,
				M14 = -x.X * cameraPosition.X - x.Y * cameraPosition.Y - x.Z * cameraPosition.Z,
				M24 = -y.X * cameraPosition.X - y.Y * cameraPosition.Y - y.Z * cameraPosition.Z,
				M34 = -z.X * cameraPosition.X - z.Y * cameraPosition.Y - z.Z * cameraPosition.Z,
				M44 = 1,
			};
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
					this.M11 = numbers[0];
					this.M21 = numbers[1];
					this.M31 = numbers[2];
					this.M41 = numbers[3];
					this.M12 = numbers[4];
					this.M22 = numbers[5];
					this.M32 = numbers[6];
					this.M42 = numbers[7];
					this.M13 = numbers[8];
					this.M23 = numbers[9];
					this.M33 = numbers[10];
					this.M43 = numbers[11];
					this.M14 = numbers[12];
					this.M24 = numbers[13];
					this.M34 = numbers[14];
					this.M44 = numbers[15];
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
			Float.WriteXml(writer, this.M11, this.M21, this.M31, this.M41, this.M12, this.M22, this.M32, this.M42, this.M13, this.M23, this.M33, this.M43, this.M14, this.M24, this.M34, this.M44);
		}
		#endregion

		#region Operators
		/// <summary>
		/// Determines whether the specified matrices are equal.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <returns>True if the <paramref name="value1"/> is equal to the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator ==(Matrix4 value1, Matrix4 value2)
		{
			return
				value1.M11 == value2.M11 &&
				value1.M21 == value2.M21 &&
				value1.M31 == value2.M31 &&
				value1.M41 == value2.M41 &&
				value1.M12 == value2.M12 &&
				value1.M22 == value2.M22 &&
				value1.M32 == value2.M32 &&
				value1.M42 == value2.M42 &&
				value1.M13 == value2.M13 &&
				value1.M23 == value2.M23 &&
				value1.M33 == value2.M33 &&
				value1.M43 == value2.M43 &&
				value1.M14 == value2.M14 &&
				value1.M24 == value2.M24 &&
				value1.M34 == value2.M34 &&
				value1.M44 == value2.M44;
		}

		/// <summary>
		/// Determines whether the specified matrices are not equal.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <returns>True if the <paramref name="value1"/> is not equal to the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator !=(Matrix4 value1, Matrix4 value2)
		{
			return
				value1.M11 != value2.M11 ||
				value1.M21 != value2.M21 ||
				value1.M31 != value2.M31 ||
				value1.M41 != value2.M41 ||
				value1.M12 != value2.M12 ||
				value1.M22 != value2.M22 ||
				value1.M32 != value2.M32 ||
				value1.M42 != value2.M42 ||
				value1.M13 != value2.M13 ||
				value1.M23 != value2.M23 ||
				value1.M33 != value2.M33 ||
				value1.M43 != value2.M43 ||
				value1.M14 != value2.M14 ||
				value1.M24 != value2.M24 ||
				value1.M34 != value2.M34 ||
				value1.M44 != value2.M44;
		}

		/// <summary>
		/// Compares the determinants of the specified matrices.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <returns>True if the determinant of the <paramref name="value1"/> is less than the determinant of the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator <(Matrix4 value1, Matrix4 value2)
		{
			return value1.Determinant < value2.Determinant;
		}

		/// <summary>
		/// Compares the determinants of the specified matrices.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <returns>True if the determinant of the <paramref name="value1"/> is less than or equal to the determinant of the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator <=(Matrix4 value1, Matrix4 value2)
		{
			return value1.Determinant <= value2.Determinant;
		}

		/// <summary>
		/// Compares the determinants of the specified matrices.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <returns>True if the determinant of the <paramref name="value1"/> is greater than the determinant of the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator >(Matrix4 value1, Matrix4 value2)
		{
			return value1.Determinant > value2.Determinant;
		}

		/// <summary>
		/// Compares the determinants of the specified matrices.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <returns>True if the determinant of the <paramref name="value1"/> is greater than or equal to the determinant of the <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator >=(Matrix4 value1, Matrix4 value2)
		{
			return value1.Determinant >= value2.Determinant;
		}

		/// <summary>
		/// Returns the sum of the specified matrices.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <returns>The sum of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Matrix4 operator +(Matrix4 value1, Matrix4 value2)
		{
			return new Matrix4
			{
				M11 = value1.M11 + value2.M11,
				M21 = value1.M21 + value2.M21,
				M31 = value1.M31 + value2.M31,
				M41 = value1.M41 + value2.M41,
				M12 = value1.M12 + value2.M12,
				M22 = value1.M22 + value2.M22,
				M32 = value1.M32 + value2.M32,
				M42 = value1.M42 + value2.M42,
				M13 = value1.M13 + value2.M13,
				M23 = value1.M23 + value2.M23,
				M33 = value1.M33 + value2.M33,
				M43 = value1.M43 + value2.M43,
				M14 = value1.M14 + value2.M14,
				M24 = value1.M24 + value2.M24,
				M34 = value1.M34 + value2.M34,
				M44 = value1.M44 + value2.M44,
			};
		}

		/// <summary>
		/// Returns the negation of the specified matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The negation of the <paramref name="value"/>.</returns>
		public static Matrix4 operator -(Matrix4 value)
		{
			return new Matrix4
			{
				M11 = -value.M11,
				M21 = -value.M21,
				M31 = -value.M31,
				M41 = -value.M41,
				M12 = -value.M12,
				M22 = -value.M22,
				M32 = -value.M32,
				M42 = -value.M42,
				M13 = -value.M13,
				M23 = -value.M23,
				M33 = -value.M33,
				M43 = -value.M43,
				M14 = -value.M14,
				M24 = -value.M24,
				M34 = -value.M34,
				M44 = -value.M44,
			};
		}

		/// <summary>
		/// Returns the difference between the specified matrices.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <returns>The difference between the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Matrix4 operator -(Matrix4 value1, Matrix4 value2)
		{
			return new Matrix4
			{
				M11 = value1.M11 - value2.M11,
				M21 = value1.M21 - value2.M21,
				M31 = value1.M31 - value2.M31,
				M41 = value1.M41 - value2.M41,
				M12 = value1.M12 - value2.M12,
				M22 = value1.M22 - value2.M22,
				M32 = value1.M32 - value2.M32,
				M42 = value1.M42 - value2.M42,
				M13 = value1.M13 - value2.M13,
				M23 = value1.M23 - value2.M23,
				M33 = value1.M33 - value2.M33,
				M43 = value1.M43 - value2.M43,
				M14 = value1.M14 - value2.M14,
				M24 = value1.M24 - value2.M24,
				M34 = value1.M34 - value2.M34,
				M44 = value1.M44 - value2.M44,
			};
		}

		/// <summary>
		/// Returns the elements of the specified matrix multiplied by the specified scalar.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The elements of the <paramref name="value1"/> multiplied by the <paramref name="value2"/>.</returns>
		public static Matrix4 operator *(Matrix4 value1, Number value2)
		{
			return new Matrix4
			{
				M11 = value1.M11 * value2,
				M21 = value1.M21 * value2,
				M31 = value1.M31 * value2,
				M41 = value1.M41 * value2,
				M12 = value1.M12 * value2,
				M22 = value1.M22 * value2,
				M32 = value1.M32 * value2,
				M42 = value1.M42 * value2,
				M13 = value1.M13 * value2,
				M23 = value1.M23 * value2,
				M33 = value1.M33 * value2,
				M43 = value1.M43 * value2,
				M14 = value1.M14 * value2,
				M24 = value1.M24 * value2,
				M34 = value1.M34 * value2,
				M44 = value1.M44 * value2,
			};
		}

		/// <summary>
		/// Returns the elements of the specified matrix multiplied by the specified scalar.
		/// </summary>
		/// <param name="value1">A scalar.</param>
		/// <param name="value2">A matrix.</param>
		/// <returns>The elements of the <paramref name="value2"/> multiplied by the <paramref name="value1"/>.</returns>
		public static Matrix4 operator *(Number value1, Matrix4 value2)
		{
			return new Matrix4
			{
				M11 = value1 * value2.M11,
				M21 = value1 * value2.M21,
				M31 = value1 * value2.M31,
				M41 = value1 * value2.M41,
				M12 = value1 * value2.M12,
				M22 = value1 * value2.M22,
				M32 = value1 * value2.M32,
				M42 = value1 * value2.M42,
				M13 = value1 * value2.M13,
				M23 = value1 * value2.M23,
				M33 = value1 * value2.M33,
				M43 = value1 * value2.M43,
				M14 = value1 * value2.M14,
				M24 = value1 * value2.M24,
				M34 = value1 * value2.M34,
				M44 = value1 * value2.M44,
			};
		}

		/// <summary>
		/// Returns the product of the specified matrix and the specified vector.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A vector.</param>
		/// <returns>The product of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Vector4 operator *(Matrix4 value1, Vector4 value2)
		{
			return new Vector4
			{
				X = value1.M11 * value2.X + value1.M12 * value2.Y + value1.M13 * value2.Z + value1.M14 * value2.W,
				Y = value1.M21 * value2.X + value1.M22 * value2.Y + value1.M23 * value2.Z + value1.M24 * value2.W,
				Z = value1.M31 * value2.X + value1.M32 * value2.Y + value1.M33 * value2.Z + value1.M34 * value2.W,
				W = value1.M41 * value2.X + value1.M42 * value2.Y + value1.M43 * value2.Z + value1.M44 * value2.W,
			};
		}

		/// <summary>
		/// Returns the product of the specified matrices.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A matrix.</param>
		/// <returns>The product of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Matrix4 operator *(Matrix4 value1, Matrix4 value2)
		{
			return new Matrix4
			{
				M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41,
				M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41,
				M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41,
				M41 = value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41,
				M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42,
				M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42,
				M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42,
				M42 = value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42,
				M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43,
				M23 = value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43,
				M33 = value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43,
				M43 = value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43,
				M14 = value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44,
				M24 = value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44,
				M34 = value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44,
				M44 = value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44,
			};
		}

		/// <summary>
		/// Returns the elements of the specified matrix divided by the specified scalar.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The elements of the <paramref name="value1"/> divided by the <paramref name="value2"/>.</returns>
		public static Matrix4 operator /(Matrix4 value1, Number value2)
		{
			return new Matrix4
			{
				M11 = value1.M11 / value2,
				M21 = value1.M21 / value2,
				M31 = value1.M31 / value2,
				M41 = value1.M41 / value2,
				M12 = value1.M12 / value2,
				M22 = value1.M22 / value2,
				M32 = value1.M32 / value2,
				M42 = value1.M42 / value2,
				M13 = value1.M13 / value2,
				M23 = value1.M23 / value2,
				M33 = value1.M33 / value2,
				M43 = value1.M43 / value2,
				M14 = value1.M14 / value2,
				M24 = value1.M24 / value2,
				M34 = value1.M34 / value2,
				M44 = value1.M44 / value2,
			};
		}

		/// <summary>
		/// Returns the remainder of the elements of the specified matrix divided by the specified scalar.
		/// </summary>
		/// <param name="value1">A matrix.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The remainder of the elements of the <paramref name="value1"/> divided by the <paramref name="value2"/>.</returns>
		public static Matrix4 operator %(Matrix4 value1, Number value2)
		{
			return new Matrix4
			{
				M11 = value1.M11 % value2,
				M21 = value1.M21 % value2,
				M31 = value1.M31 % value2,
				M41 = value1.M41 % value2,
				M12 = value1.M12 % value2,
				M22 = value1.M22 % value2,
				M32 = value1.M32 % value2,
				M42 = value1.M42 % value2,
				M13 = value1.M13 % value2,
				M23 = value1.M23 % value2,
				M33 = value1.M33 % value2,
				M43 = value1.M43 % value2,
				M14 = value1.M14 % value2,
				M24 = value1.M24 % value2,
				M34 = value1.M34 % value2,
				M44 = value1.M44 % value2,
			};
		}
		#endregion

		#region Indexers
		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <param name="index">Element index.</param>
		/// <value>Value of the element at the <paramref name="index"/>.</value>
		/// <exception cref="IndexOutOfRangeException">Thrown if the <paramref name="index"/> is less than zero or greater than or equal to the number of elements in the matrix.</exception>
		public Number this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return this.M11;

					case 1:
						return this.M21;

					case 2:
						return this.M31;

					case 3:
						return this.M41;

					case 4:
						return this.M12;

					case 5:
						return this.M22;

					case 6:
						return this.M32;

					case 7:
						return this.M42;

					case 8:
						return this.M13;

					case 9:
						return this.M23;

					case 10:
						return this.M33;

					case 11:
						return this.M43;

					case 12:
						return this.M14;

					case 13:
						return this.M24;

					case 14:
						return this.M34;

					case 15:
						return this.M44;

					default:
						throw new IndexOutOfRangeException();
				}
			}

			set
			{
				switch (index)
				{
					case 0:
						this.M11 = value;
						break;

					case 1:
						this.M21 = value;
						break;

					case 2:
						this.M31 = value;
						break;

					case 3:
						this.M41 = value;
						break;

					case 4:
						this.M12 = value;
						break;

					case 5:
						this.M22 = value;
						break;

					case 6:
						this.M32 = value;
						break;

					case 7:
						this.M42 = value;
						break;

					case 8:
						this.M13 = value;
						break;

					case 9:
						this.M23 = value;
						break;

					case 10:
						this.M33 = value;
						break;

					case 11:
						this.M43 = value;
						break;

					case 12:
						this.M14 = value;
						break;

					case 13:
						this.M24 = value;
						break;

					case 14:
						this.M34 = value;
						break;

					case 15:
						this.M44 = value;
						break;

					default:
						throw new IndexOutOfRangeException();
				}
			}
		}

		/// <summary>
		/// Gets or sets the element at the specified row and column.
		/// </summary>
		/// <param name="row">Index of the row.</param>
		/// <param name="column">Index of the column.</param>
		/// <value>Value of the element at the <paramref name="row"/> and the <paramref name="column"/>.</value>
		/// <exception cref="IndexOutOfRangeException">Thrown if the <paramref name="row"/> or the <paramref name="column"/> is less than zero or greater than or equal to the number of rows and columns in the matrix.</exception>
		public Number this[int row, int column]
		{
			get
			{
				switch (row)
				{
					case 0:
						switch (column)
						{
							case 0:
								return this.M11;

							case 1:
								return this.M12;

							case 2:
								return this.M13;

							case 3:
								return this.M14;

							default:
								throw new IndexOutOfRangeException();
						}

					case 1:
						switch (column)
						{
							case 0:
								return this.M21;

							case 1:
								return this.M22;

							case 2:
								return this.M23;

							case 3:
								return this.M24;

							default:
								throw new IndexOutOfRangeException();
						}

					case 2:
						switch (column)
						{
							case 0:
								return this.M31;

							case 1:
								return this.M32;

							case 2:
								return this.M33;

							case 3:
								return this.M34;

							default:
								throw new IndexOutOfRangeException();
						}

					case 3:
						switch (column)
						{
							case 0:
								return this.M41;

							case 1:
								return this.M42;

							case 2:
								return this.M43;

							case 3:
								return this.M44;

							default:
								throw new IndexOutOfRangeException();
						}

					default:
						throw new IndexOutOfRangeException();
				}
			}

			set
			{
				switch (row)
				{
					case 0:
						switch (column)
						{
							case 0:
								this.M11 = value;
								break;

							case 1:
								this.M12 = value;
								break;

							case 2:
								this.M13 = value;
								break;

							case 3:
								this.M14 = value;
								break;

							default:
								throw new IndexOutOfRangeException();
						}

						break;

					case 1:
						switch (column)
						{
							case 0:
								this.M21 = value;
								break;

							case 1:
								this.M22 = value;
								break;

							case 2:
								this.M23 = value;
								break;

							case 3:
								this.M24 = value;
								break;

							default:
								throw new IndexOutOfRangeException();
						}

						break;

					case 2:
						switch (column)
						{
							case 0:
								this.M31 = value;
								break;

							case 1:
								this.M32 = value;
								break;

							case 2:
								this.M33 = value;
								break;

							case 3:
								this.M34 = value;
								break;

							default:
								throw new IndexOutOfRangeException();
						}

						break;

					case 3:
						switch (column)
						{
							case 0:
								this.M41 = value;
								break;

							case 1:
								this.M42 = value;
								break;

							case 2:
								this.M43 = value;
								break;

							case 3:
								this.M44 = value;
								break;

							default:
								throw new IndexOutOfRangeException();
						}

						break;

					default:
						throw new IndexOutOfRangeException();
				}
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the bottom clipping plane.
		/// </summary>
		/// <value>The bottom clipping plane.</value>
		public Plane BottomClipPlane
		{
			get
			{
				return Plane.Normalize(
					this.M41 + this.M21,
					this.M42 + this.M22,
					this.M43 + this.M23,
					this.M44 + this.M24);
			}
		}

		/// <summary>
		/// Gets or sets the elements on the 1st column.
		/// </summary>
		/// <value>A vector representing the 1st column of the matrix.</value>
		public Vector4 Column1
		{
			get
			{
				return new Vector4
				{
					X = this.M11,
					Y = this.M21,
					Z = this.M31,
					W = this.M41,
				};
			}

			set
			{
				this.M11 = value.X;
				this.M21 = value.Y;
				this.M31 = value.Z;
				this.M41 = value.W;
			}
		}

		/// <summary>
		/// Gets or sets the elements on the 2nd column.
		/// </summary>
		/// <value>A vector representing the 2nd column of the matrix.</value>
		public Vector4 Column2
		{
			get
			{
				return new Vector4
				{
					X = this.M12,
					Y = this.M22,
					Z = this.M32,
					W = this.M42,
				};
			}

			set
			{
				this.M12 = value.X;
				this.M22 = value.Y;
				this.M32 = value.Z;
				this.M42 = value.W;
			}
		}

		/// <summary>
		/// Gets or sets the elements on the 3rd column.
		/// </summary>
		/// <value>A vector representing the 3rd column of the matrix.</value>
		public Vector4 Column3
		{
			get
			{
				return new Vector4
				{
					X = this.M13,
					Y = this.M23,
					Z = this.M33,
					W = this.M43,
				};
			}

			set
			{
				this.M13 = value.X;
				this.M23 = value.Y;
				this.M33 = value.Z;
				this.M43 = value.W;
			}
		}

		/// <summary>
		/// Gets or sets the elements on the 4th column.
		/// </summary>
		/// <value>A vector representing the 4th column of the matrix.</value>
		public Vector4 Column4
		{
			get
			{
				return new Vector4
				{
					X = this.M14,
					Y = this.M24,
					Z = this.M34,
					W = this.M44,
				};
			}

			set
			{
				this.M14 = value.X;
				this.M24 = value.Y;
				this.M34 = value.Z;
				this.M44 = value.W;
			}
		}

		/// <summary>
		/// Gets the determinant of the matrix. The determinant indicates whether the matrix can be inverted.
		/// </summary>
		/// <value>The determinant of the matrix.</value>
		public Number Determinant
		{
			get
			{
				var det1 = (this.M33 * this.M44) - (this.M34 * this.M43);
				var det2 = (this.M32 * this.M44) - (this.M34 * this.M42);
				var det3 = (this.M32 * this.M43) - (this.M33 * this.M42);
				var det4 = (this.M31 * this.M44) - (this.M34 * this.M41);
				var det5 = (this.M31 * this.M43) - (this.M33 * this.M41);
				var det6 = (this.M31 * this.M42) - (this.M32 * this.M41);

				return
					this.M11 * (this.M22 * det1 - this.M23 * det2 + this.M24 * det3) -
					this.M12 * (this.M21 * det1 - this.M23 * det4 + this.M24 * det5) +
					this.M13 * (this.M21 * det2 - this.M22 * det4 + this.M24 * det6) -
					this.M14 * (this.M21 * det3 - this.M22 * det5 + this.M23 * det6);
			}
		}

		/// <summary>
		/// Gets or sets the elements on the diagonal.
		/// </summary>
		/// <value>A vector representing the diagonal of the matrix.</value>
		public Vector4 Diagonal
		{
			get
			{
				return new Vector4
				{
					X = this.M11,
					Y = this.M22,
					Z = this.M33,
					W = this.M44,
				};
			}

			set
			{
				this.M11 = value.X;
				this.M22 = value.Y;
				this.M33 = value.Z;
				this.M44 = value.W;
			}
		}

		/// <summary>
		/// Gets the far clip plane.
		/// </summary>
		/// <value>The far clip plane.</value>
		public Plane FarClipPlane
		{
			get
			{
				return Plane.Normalize(
					this.M41 - this.M31,
					this.M42 - this.M32,
					this.M43 - this.M33,
					this.M44 - this.M34);
			}
		}

		/// <summary>
		/// Gets a matrix with the diagonal elements set to one and other elements set to zero.
		/// </summary>
		/// <value>The matrix with the diagonal elements set to one and other elements set to zero.</value>
		public static Matrix4 Identity
		{
			get { return identity; }
		}

		/// <summary>
		/// Gets the left clipping plane.
		/// </summary>
		/// <value>The left clipping plane.</value>
		public Plane LeftClipPlane
		{
			get
			{
				return Plane.Normalize(
					this.M41 + this.M11,
					this.M42 + this.M12,
					this.M43 + this.M13,
					this.M44 + this.M14);
			}
		}

		/// <summary>
		/// Gets a matrix with all the elements set to their maximum values.
		/// </summary>
		/// <value>The matrix with all the elements set to their maximum values.</value>
		public static Matrix4 MaxValue
		{
			get { return Matrix4.maxValue; }
		}

		/// <summary>
		/// Gets a matrix with all the elements set to their minimum values.
		/// </summary>
		/// <value>The matrix with all the elements set to their minimum values.</value>
		public static Matrix4 MinValue
		{
			get { return Matrix4.minValue; }
		}

		/// <summary>
		/// Gets a matrix with all the elements set to a value that is not a number.
		/// </summary>
		/// <value>The matrix with all the elements set to a value that is not a number.</value>
		public static Matrix4 NaN
		{
			get { return Matrix4.nan; }
		}

		/// <summary>
		/// Gets the near clipping plane.
		/// </summary>
		/// <value>The near clipping plane.</value>
		public Plane NearClipPlane
		{
			get
			{
				return Plane.Normalize(
					this.M41 + this.M31,
					this.M42 + this.M32,
					this.M43 + this.M33,
					this.M44 + this.M34);
			}
		}

		/// <summary>
		/// Gets a matrix with all the elements set to negative infinity.
		/// </summary>
		/// <value>The matrix with all the elements set to negative infinity.</value>
		public static Matrix4 NegativeInfinity
		{
			get { return Matrix4.negativeInfinity; }
		}

		/// <summary>
		/// Gets a matrix with all the elements set to one.
		/// </summary>
		/// <value>The matrix with all the elements set to one.</value>
		public static Matrix4 One
		{
			get { return Matrix4.one; }
		}

		/// <summary>
		/// Gets a matrix with all the elements set to positive infinity.
		/// </summary>
		/// <value>The matrix with all the elements set to positive infinity.</value>
		public static Matrix4 PositiveInfinity
		{
			get { return Matrix4.positiveInfinity; }
		}

		/// <summary>
		/// Gets the right clipping plane.
		/// </summary>
		/// <value>The right clipping plane.</value>
		public Plane RightClipPlane
		{
			get
			{
				return Plane.Normalize(
					this.M41 - this.M11,
					this.M42 - this.M12,
					this.M43 - this.M13,
					this.M44 - this.M14);
			}
		}

		/// <summary>
		/// Gets or sets the elements on the 1st row.
		/// </summary>
		/// <value>A vector representing the 1st row of the matrix.</value>
		public Vector4 Row1
		{
			get
			{
				return new Vector4
				{
					X = this.M11,
					Y = this.M12,
					Z = this.M13,
					W = this.M14,
				};
			}

			set
			{
				this.M11 = value.X;
				this.M12 = value.Y;
				this.M13 = value.Z;
				this.M14 = value.W;
			}
		}

		/// <summary>
		/// Gets or sets the elements on the 2nd row.
		/// </summary>
		/// <value>A vector representing the 2nd row of the matrix.</value>
		public Vector4 Row2
		{
			get
			{
				return new Vector4
				{
					X = this.M21,
					Y = this.M22,
					Z = this.M23,
					W = this.M24,
				};
			}

			set
			{
				this.M21 = value.X;
				this.M22 = value.Y;
				this.M23 = value.Z;
				this.M24 = value.W;
			}
		}

		/// <summary>
		/// Gets or sets the elements on the 3rd row.
		/// </summary>
		/// <value>A vector representing the 3rd row of the matrix.</value>
		public Vector4 Row3
		{
			get
			{
				return new Vector4
				{
					X = this.M31,
					Y = this.M32,
					Z = this.M33,
					W = this.M34,
				};
			}

			set
			{
				this.M31 = value.X;
				this.M32 = value.Y;
				this.M33 = value.Z;
				this.M34 = value.W;
			}
		}

		/// <summary>
		/// Gets or sets the elements on the 4th row.
		/// </summary>
		/// <value>A vector representing the 4th row of the matrix.</value>
		public Vector4 Row4
		{
			get
			{
				return new Vector4
				{
					X = this.M41,
					Y = this.M42,
					Z = this.M43,
					W = this.M44,
				};
			}

			set
			{
				this.M41 = value.X;
				this.M42 = value.Y;
				this.M43 = value.Z;
				this.M44 = value.W;
			}
		}

		/// <summary>
		/// Gets the top clipping plane.
		/// </summary>
		/// <value>The top clipping plane.</value>
		public Plane TopClipPlane
		{
			get
			{
				return Plane.Normalize(
					this.M41 - this.M21,
					this.M42 - this.M22,
					this.M43 - this.M23,
					this.M44 - this.M24);
			}
		}

		/// <summary>
		/// Gets the sum of the elements on the main diagonal.
		/// </summary>
		/// <value>The trace of the matrix.</value>
		public Number Trace
		{
			get { return this.M11 + this.M22 + this.M33 + this.M44; }
		}

		/// <summary>
		/// Gets or sets the translation vector.
		/// </summary>
		/// <value>The translation vector.</value>
		public Vector3 Translation
		{
			get
			{
				return new Vector3
				{
					X = this.M14,
					Y = this.M24,
					Z = this.M34,
				};
			}

			set
			{
				this.M14 = value.X;
				this.M24 = value.Y;
				this.M34 = value.Z;
			}
		}

		/// <summary>
		/// Gets or sets the X axis vector.
		/// </summary>
		/// <value>The X axis vector.</value>
		public Vector3 XAxis
		{
			get
			{
				return new Vector3
				{
					X = this.M11,
					Y = this.M21,
					Z = this.M31,
				};
			}

			set
			{
				this.M11 = value.X;
				this.M21 = value.Y;
				this.M31 = value.Z;
			}
		}

		/// <summary>
		/// Gets or sets the Y axis vector.
		/// </summary>
		/// <value>The Y axis vector.</value>
		public Vector3 YAxis
		{
			get
			{
				return new Vector3
				{
					X = this.M12,
					Y = this.M22,
					Z = this.M32,
				};
			}

			set
			{
				this.M12 = value.X;
				this.M22 = value.Y;
				this.M32 = value.Z;
			}
		}

		/// <summary>
		/// Gets or sets the Z axis vector.
		/// </summary>
		/// <value>The Z axis vector.</value>
		public Vector3 ZAxis
		{
			get
			{
				return new Vector3
				{
					X = this.M13,
					Y = this.M23,
					Z = this.M33,
				};
			}

			set
			{
				this.M13 = value.X;
				this.M23 = value.Y;
				this.M33 = value.Z;
			}
		}

		/// <summary>
		/// Gets a matrix with all the elements set to zero.
		/// </summary>
		/// <value>The matrix with all the elements set to zero.</value>
		public static Matrix4 Zero
		{
			get { return zero; }
		}
		#endregion

		#region Fields
		/// <summary>
		/// Element at the 1st row, 1st column.
		/// </summary>
		public Number M11;

		/// <summary>
		/// Element at the 2nd row, 1st column.
		/// </summary>
		public Number M21;

		/// <summary>
		/// Element at the 3rd row, 1st column.
		/// </summary>
		public Number M31;

		/// <summary>
		/// Element at the 4th row, 1st column.
		/// </summary>
		public Number M41;

		/// <summary>
		/// Element at the 1st row, 2nd column.
		/// </summary>
		public Number M12;

		/// <summary>
		/// Element at the 2nd row, 2nd column.
		/// </summary>
		public Number M22;

		/// <summary>
		/// Element at the 3rd row, 2nd column.
		/// </summary>
		public Number M32;

		/// <summary>
		/// Element at the 4th row, 2nd column.
		/// </summary>
		public Number M42;

		/// <summary>
		/// Element at the 1st row, 3rd column.
		/// </summary>
		public Number M13;

		/// <summary>
		/// Element at the 2nd row, 3rd column.
		/// </summary>
		public Number M23;

		/// <summary>
		/// Element at the 3rd row, 3rd column.
		/// </summary>
		public Number M33;

		/// <summary>
		/// Element at the 4th row, 3rd column.
		/// </summary>
		public Number M43;

		/// <summary>
		/// Element at the 1st row, 4th column.
		/// </summary>
		public Number M14;

		/// <summary>
		/// Element at the 2nd row, 4th column.
		/// </summary>
		public Number M24;

		/// <summary>
		/// Element at the 3rd row, 4th column.
		/// </summary>
		public Number M34;

		/// <summary>
		/// Element at the 4th row, 4th column.
		/// </summary>
		public Number M44;

		/// <summary>
		/// The number of values in the matrix.
		/// </summary>
		public const int ValueCount = 16;

		/// <summary>
		/// A matrix with the diagonal elements set to one and other elements set to zero.
		/// </summary>
		private static readonly Matrix4 identity = new Matrix4 { M11 = 1, M22 = 1, M33 = 1, M44 = 1 };

		/// <summary>
		/// A matrix with all the elements set to their maximum values.
		/// </summary>
		private static readonly Matrix4 maxValue = new Matrix4(Number.MaxValue);

		/// <summary>
		/// A matrix with all the elements set to their minimum values.
		/// </summary>
		private static readonly Matrix4 minValue = new Matrix4(Number.MinValue);

		/// <summary>
		/// A matrix with all the elements set to a value that is not a number.
		/// </summary>
		private static readonly Matrix4 nan = new Matrix4(Number.NaN);

		/// <summary>
		/// A matrix with all the elements set to negative infinity.
		/// </summary>
		private static readonly Matrix4 negativeInfinity = new Matrix4(Number.NegativeInfinity);

		/// <summary>
		/// A matrix with all the elements set to one.
		/// </summary>
		private static readonly Matrix4 one = new Matrix4(1);

		/// <summary>
		/// A matrix with all the elements set to positive infinity.
		/// </summary>
		private static readonly Matrix4 positiveInfinity = new Matrix4(Number.PositiveInfinity);

		/// <summary>
		/// A matrix with all the elements set to zero.
		/// </summary>
		private static readonly Matrix4 zero = new Matrix4(0);
		#endregion

        public void Decompose(out Vector3 scale, out Quaternion quaternion, out Vector3 translation)
        {
            var rotationMatrix = Matrix4.Identity;

            var cols = new Vector3[]
            {
                new Vector3(this.M11,this.M12,this.M13),
                new Vector3(this.M21,this.M22,this.M23),
                new Vector3(this.M31,this.M32,this.M33)  
            };

            scale.X = cols[0].Magnitude;
            scale.Y = cols[1].Magnitude;
            scale.Z = cols[2].Magnitude;

            translation.X = this.M14;// / (scale.X == 0 ? 1 : scale.X);
            translation.Y = this.M24;// / (scale.Y == 0 ? 1 : scale.Y);
            translation.Z = this.M34;// / (scale.Z == 0 ? 1 : scale.Z);

            if (scale.X != 0)
            {
                cols[0].X /= scale.X;
                cols[0].Y /= scale.X;
                cols[0].Z /= scale.X;
            }

            if (scale.Y != 0)
            {
                cols[1].X /= scale.Y;
                cols[1].Y /= scale.Y;
                cols[1].Z /= scale.Y;
            }

            if (scale.Z != 0)
            {
                cols[2].X /= scale.Z;
                cols[2].Y /= scale.Z;
                cols[2].Z /= scale.Z;
            }

            rotationMatrix.M11 = cols[0].X;
            rotationMatrix.M12 = cols[0].Y;
            rotationMatrix.M13 = cols[0].Z;
            rotationMatrix.M14 = 0;
            rotationMatrix.M41 = 0;
            rotationMatrix.M21 = cols[1].X;
            rotationMatrix.M22 = cols[1].Y;
            rotationMatrix.M23 = cols[1].Z;
            rotationMatrix.M24 = 0;
            rotationMatrix.M42 = 0;
            rotationMatrix.M31 = cols[2].X;
            rotationMatrix.M32 = cols[2].Y;
            rotationMatrix.M33 = cols[2].Z;
            rotationMatrix.M34 = 0;
            rotationMatrix.M43 = 0;
            rotationMatrix.M44 = 1;

            quaternion = Quaternion.Rotate(rotationMatrix);
        }
    }
}
