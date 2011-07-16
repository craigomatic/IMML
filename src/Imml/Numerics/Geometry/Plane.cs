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
using System.Collections;

namespace Imml.Numerics.Geometry
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
	/// Representation of a plane.
	/// </summary>
	[DebuggerDisplay("A = {A} B = {B} C = {C} D = {D}")]
	public struct Plane : IEnumerable<Number>, IEquatable<Plane>, IFormattable, IXmlSerializable
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="abcd">The value for all the components.</param>
		public Plane(Number abcd)
		{
			this.A = abcd;
			this.B = abcd;
			this.C = abcd;
			this.D = abcd;
		}

		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="plane">Plane to copy.</param>
		public Plane(Plane plane)
		{
			this.A = plane.A;
			this.B = plane.B;
			this.C = plane.C;
			this.D = plane.D;
		}

		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="normal">The plane normal.</param>
		/// <param name="d">The D component of the plane equation.</param>
		public Plane(Vector3 normal, Number d)
		{
			this.A = normal.X;
			this.B = normal.Y;
			this.C = normal.Z;
			this.D = d;
		}

		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="normal">The plane normal.</param>
		/// <param name="pointOnPlane">A point on the plane.</param>
		public Plane(Vector3 normal, Vector3 pointOnPlane)
		{
			this.A = normal.X;
			this.B = normal.Y;
			this.C = normal.Z;
			this.D = -Vector3.Dot(normal, pointOnPlane);
		}

		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="a">The A component of the plane equation.</param>
		/// <param name="b">The B component of the plane equation.</param>
		/// <param name="c">The C component of the plane equation.</param>
		/// <param name="d">The D component of the plane equation.</param>
		public Plane(Number a, Number b, Number c, Number d)
		{
			this.A = a;
			this.B = b;
			this.C = c;
			this.D = d;
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
			if (value is Plane)
			{
				return this.Equals((Plane)value);
			}

			return false;
		}

		/// <summary>
		/// Determines whether the current plane is equal to the specified plane.
		/// </summary>
		/// <param name="value">A plane.</param>
		/// <returns>True if the current plane is equal to the <paramref name="value"/>, otherwise false.</returns>
		public bool Equals(Plane value)
		{
			return
				this.A == value.A &&
				this.B == value.B &&
				this.C == value.C &&
				this.D == value.D;
		}

		/// <summary>
		/// Returns the plane created from the specified points.
		/// </summary>
		/// <param name="point1">A point.</param>
		/// <param name="point2">A point.</param>
		/// <param name="point3">A point.</param>
		/// <returns>The plane created from the <paramref name="point1"/>, <paramref name="point2"/> and the <paramref name="point3"/>.</returns>
		public static Plane FromPoints(Vector3 point1, Vector3 point2, Vector3 point3)
		{
			return new Plane(Vector3.Normalize(Vector3.Cross(point2 - point1, point3 - point1)), point1);
		}

		/// <summary>
		/// Returns a hash code for the current plane.
		/// </summary>
		/// <returns>A hash code.</returns>
		/// <remarks>
		/// The hash code is not unique.
		/// If two planes are equal, their hash codes are guaranteed to be equal.
		/// If the planes are not equal, their hash codes are not guaranteed to be different.
		/// </remarks>
		public override int GetHashCode()
		{
			return HashCode.GetHashCode(
				this.A.GetHashCode(),
				this.B.GetHashCode(),
				this.C.GetHashCode(),
				this.D.GetHashCode());
		}

		/// <summary>
		/// Returns an enumerator that iterates through the elements.
		/// </summary>
		/// <returns>
		/// An enumerator object that can be used to iterate through the elements.
		/// </returns>
		public IEnumerator<Number> GetEnumerator()
		{
			yield return this.A;
			yield return this.B;
			yield return this.C;
			yield return this.D;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the elements.
		/// </summary>
		/// <returns>
		/// An enumerator object that can be used to iterate through the elements.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>
		/// Determines whether any of the components of the specified plane evaluates to an infinity.
		/// </summary>
		/// <param name="value">A plane.</param>
		/// <returns>True if any of the components of <paramref name="value"/> evaluates to an infinity, otherwise false.</returns>
		public static bool IsInfinity(Plane value)
		{
			return
				Number.IsInfinity(value.A) ||
				Number.IsInfinity(value.B) ||
				Number.IsInfinity(value.C) ||
				Number.IsInfinity(value.D);
		}

		/// <summary>
		/// Determines whether any of the components of the specified plane evaluates to a value that is not a number.
		/// </summary>
		/// <param name="value">A plane.</param>
		/// <returns>True if any of the components of <paramref name="value"/> evaluates to a value that is not a number, otherwise false.</returns>
		public static bool IsNaN(Plane value)
		{
			return
				Number.IsNaN(value.A) ||
				Number.IsNaN(value.B) ||
				Number.IsNaN(value.C) ||
				Number.IsNaN(value.D);
		}

		/// <summary>
		/// Determines whether any of the components of the specified plane evaluates to negative infinity.
		/// </summary>
		/// <param name="value">A plane.</param>
		/// <returns>True if any of the components of <paramref name="value"/> evaluates to negative infinity, otherwise false.</returns>
		public static bool IsNegativeInfinity(Plane value)
		{
			return
				Number.IsNegativeInfinity(value.A) ||
				Number.IsNegativeInfinity(value.B) ||
				Number.IsNegativeInfinity(value.C) ||
				Number.IsNegativeInfinity(value.D);
		}

		/// <summary>
		/// Determines whether any of the components of the specified plane evaluates to positive infinity.
		/// </summary>
		/// <param name="value">A plane.</param>
		/// <returns>True if any of the components of <paramref name="value"/> evaluates to positive infinity, otherwise false.</returns>
		public static bool IsPositiveInfinity(Plane value)
		{
			return
				Number.IsPositiveInfinity(value.A) ||
				Number.IsPositiveInfinity(value.B) ||
				Number.IsPositiveInfinity(value.C) ||
				Number.IsPositiveInfinity(value.D);
		}

		/// <summary>
		/// Returns a plane with the normal pointing to the opposite direction.
		/// </summary>
		/// <param name="value">A plane.</param>
		/// <returns>A plane with the normal pointing to the opposite direction.</returns>
		public static Plane Negate(Plane value)
		{
			return new Plane
			{
				A = -value.A,
				B = -value.B,
				C = -value.C,
				D = -value.D,
			};
		}

		/// <summary>
		/// Returns a plane with a normal with the magnitude of one, pointing to the same direction as the specified plane.
		/// </summary>
		/// <param name="value">A plane.</param>
		/// <returns>A plane with a normal with the magnitude of one, pointing to the same direction as the <paramref name="value"/>.</returns>
		public static Plane Normalize(Plane value)
		{
			var scale = (Number)Math.Sqrt(value.A * value.A + value.B * value.B + value.C * value.C);

			if (Float.Near(scale, 0))
			{
				return new Plane(Vector3.UnitX, 0);
			}
			else
			{
				scale = 1 / scale;

				return new Plane
				{
					A = value.A * scale,
					B = value.B * scale,
					C = value.C * scale,
					D = value.D * scale,
				};
			}
		}

		/// <summary>
		/// Returns a plane with a normal with the magnitude of one, pointing to the same direction as the specified plane.
		/// </summary>
		/// <param name="a">The A component of the plane equation.</param>
		/// <param name="b">The B component of the plane equation.</param>
		/// <param name="c">The C component of the plane equation.</param>
		/// <param name="d">The D component of the plane equation.</param>
		/// <returns>A plane with a normal with the magnitude of one, pointing to the same direction as the specified plane.</returns>
		public static Plane Normalize(Number a, Number b, Number c, Number d)
		{
			var scale = (Number)Math.Sqrt(a * a + b * b + c * c);

			if (Float.Near(scale, 0))
			{
				return new Plane(Vector3.UnitX, 0);
			}
			else
			{
				scale = 1 / scale;

				return new Plane
				{
					A = a * scale,
					B = b * scale,
					C = c * scale,
					D = d * scale,
				};
			}
		}

		/// <summary>
		/// Returns the plane parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <returns>The plane parsed from the <paramref name="value"/>.</returns>
		public static Plane Parse(string value)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, null);
		}

		/// <summary>
		/// Returns the plane parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <returns>The plane parsed from the <paramref name="value"/>.</returns>
		public static Plane Parse(string value, NumberStyles numberStyle)
		{
			return Parse(value, numberStyle, null);
		}

		/// <summary>
		/// Returns the plane parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <returns>The plane parsed from the <paramref name="value"/>.</returns>
		public static Plane Parse(string value, IFormatProvider formatProvider)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider);
		}

		/// <summary>
		/// Returns the plane parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <returns>The plane parsed from the <paramref name="value"/>.</returns>
		public static Plane Parse(string value, NumberStyles numberStyle, IFormatProvider formatProvider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var numbers = Float.Parse(value, numberStyle, formatProvider);

			if (numbers.Length == ValueCount)
			{
				return new Plane
				{
					A = numbers[0],
					B = numbers[1],
					C = numbers[2],
					D = numbers[3],
				};
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
			return Float.ToString(format, formatProvider, this.A, this.B, this.C, this.D);
		}

		/// <summary>
		/// Attempts to parse the plane from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="result">The output variable for the plane parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, out Plane result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, null, out result);
		}

		/// <summary>
		/// Attempts to parse the plane from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="result">The output variable for the plane parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, out Plane result)
		{
			return TryParse(value, numberStyle, null, out result);
		}

		/// <summary>
		/// Attempts to parse the plane from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <param name="result">The output variable for the plane parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, IFormatProvider formatProvider, out Plane result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider, out result);
		}

		/// <summary>
		/// Attempts to parse the plane from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style for each component.</param>
		/// <param name="formatProvider">The format provider for each component.</param>
		/// <param name="result">The output variable for the plane parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, IFormatProvider formatProvider, out Plane result)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var numbers = Float.Parse(value, numberStyle, formatProvider);

			if (numbers.Length == ValueCount)
			{
				result.A = numbers[0];
				result.B = numbers[1];
				result.C = numbers[2];
				result.D = numbers[3];
				return true;
			}
			else
			{
				result = default(Plane);
				return false;
			}
		}

		/// <summary>
		/// Returns the current plane with the specified components replaced with the specified values.
		/// </summary>
		/// <param name="a">The new value for the <see cref="A"/> component. Use null to keep the old value.</param>
		/// <param name="b">The new value for the <see cref="B"/> component. Use null to keep the old value.</param>
		/// <param name="c">The new value for the <see cref="C"/> component. Use null to keep the old value.</param>
		/// <param name="d">The new value for the <see cref="D"/> component. Use null to keep the old value.</param>
		/// <returns>The current plane with the specified components replaced with the specified values.</returns>
		public Plane With(Number? a = null, Number? b = null, Number? c = null, Number? d = null)
		{
			return new Plane
			{
				A = a ?? this.A,
				B = b ?? this.B,
				C = c ?? this.C,
				D = d ?? this.D,
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
					this.A = numbers[0];
					this.B = numbers[1];
					this.C = numbers[2];
					this.D = numbers[3];
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
			Float.WriteXml(writer, this.A, this.B, this.C, this.D);
		}
		#endregion

		#region Operators
		/// <summary>
		/// Determines whether the planes are equal.
		/// </summary>
		/// <param name="value1">A plane.</param>
		/// <param name="value2">A plane.</param>
		/// <returns>True if the planes are equal, otherwise false.</returns>
		public static bool operator ==(Plane value1, Plane value2)
		{
			return
				value1.A == value2.A &&
				value1.B == value2.B &&
				value1.C == value2.C &&
				value1.D == value2.D;
		}

		/// <summary>
		/// Determines whether the planes are not equal.
		/// </summary>
		/// <param name="value1">A plane.</param>
		/// <param name="value2">A plane.</param>
		/// <returns>True if the planes are not equal, otherwise false.</returns>
		public static bool operator !=(Plane value1, Plane value2)
		{
			return
				value1.A != value2.A ||
				value1.B != value2.B ||
				value1.C != value2.C ||
				value1.D != value2.D;
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
						return this.A;

					case 1:
						return this.B;

					case 2:
						return this.C;

					case 3:
						return this.D;

					default:
						throw new IndexOutOfRangeException();
				}
			}

			set
			{
				switch (index)
				{
					case 0:
						this.A = value;
						break;

					case 1:
						this.B = value;
						break;

					case 2:
						this.C = value;
						break;

					case 3:
						this.D = value;
						break;

					default:
						throw new IndexOutOfRangeException();
				}
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets a plane with all the components set to a value that is not a number.
		/// </summary>
		/// <value>The plane with all the components set to a value that is not a number.</value>
		public static Plane NaN
		{
			get { return nan; }
		}

		/// <summary>
		/// Gets a plane with all the components set to negative infinity.
		/// </summary>
		/// <value>The plane with all the components set to negative infinity.</value>
		public static Plane NegativeInfinity
		{
			get { return negativeInfinity; }
		}

		/// <summary>
		/// Gets or sets the plane normal vector.
		/// </summary>
		/// <value>The plane normal vector.</value>
		public Vector3 Normal
		{
			get
			{
				return new Vector3
				{
					X = this.A,
					Y = this.B,
					Z = this.C,
				};
			}

			set
			{
				this.A = value.X;
				this.B = value.Y;
				this.C = value.Z;
			}
		}

		/// <summary>
		/// Gets a plane with all the components set to positive infinity.
		/// </summary>
		/// <value>The plane with all the components set to positive infinity.</value>
		public static Plane PositiveInfinity
		{
			get { return positiveInfinity; }
		}

		/// <summary>
		/// Gets a plane with all the components set to zero.
		/// </summary>
		/// <value>The plane with all the components set to zero.</value>
		public static Plane Zero
		{
			get { return zero; }
		}
		#endregion

		#region Fields
		/// <summary>
		/// The A component of the plane equation.
		/// </summary>
		public Number A;

		/// <summary>
		/// The B component of the plane equation.
		/// </summary>
		public Number B;

		/// <summary>
		/// The C component of the plane equation.
		/// </summary>
		public Number C;

		/// <summary>
		/// The D component of the plane equation.
		/// </summary>
		public Number D;

		/// <summary>
		/// The number of values in the plane equation.
		/// </summary>
		public const int ValueCount = 4;

		/// <summary>
		/// A plane with all the components set to a value that is not a number.
		/// </summary>
		private static readonly Plane nan = new Plane(Number.NaN);

		/// <summary>
		/// A plane with all the components set to negative infinity.
		/// </summary>
		private static readonly Plane negativeInfinity = new Plane(Number.NegativeInfinity);

		/// <summary>
		/// A plane with all the components set to positive infinity.
		/// </summary>
		private static readonly Plane positiveInfinity = new Plane(Number.PositiveInfinity);

		/// <summary>
		/// A plane with all the components set to zero.
		/// </summary>
		private static readonly Plane zero = new Plane(0);
		#endregion
	}
}
