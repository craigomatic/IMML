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
	/// Representation of an angle.
	/// </summary>
	[DebuggerDisplay("Degrees = {Degrees} Radians = {Radians}")]
	public struct Angle : IComparable, IComparable<Angle>, IEquatable<Angle>, IFormattable, IXmlSerializable
	{
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the struct.
		/// </summary>
		/// <param name="radians">Angle in radians.</param>
		public Angle(Number radians)
		{
			this.radians = radians;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Returns the absolute value of the specified angle.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>The absolute value of the <paramref name="value"/>.</returns>
		public static Angle Abs(Angle value)
		{
			return new Angle(Math.Abs(value.radians));
		}

		/// <summary>
		/// Returns an angle whose cosine is the specified value.
		/// </summary>
		/// <param name="value">Cosine value.</param>
		/// <returns>An angle whose cosine is the <paramref name="value"/>.</returns>
		public static Angle Acos(Number value)
		{
			return new Angle((Number)Math.Acos(value));
		}

		/// <summary>
		/// Returns the sum of the specified angles.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>The sum of the angles the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Angle Add(Angle value1, Angle value2)
		{
			return new Angle(value1.radians + value2.radians);
		}

		/// <summary>
		/// Returns an angle whose sine is the specified value.
		/// </summary>
		/// <param name="value">Sine value.</param>
		/// <returns>An angle whose sine is the <paramref name="value"/>.</returns>
		public static Angle Asin(Number value)
		{
			return new Angle((Number)Math.Asin(value));
		}

		/// <summary>
		/// Returns an angle whose tangent is the specified value.
		/// </summary>
		/// <param name="value">Tangent value.</param>
		/// <returns>An angle whose tangent is the <paramref name="value"/>.</returns>
		public static Angle Atan(Number value)
		{
			return new Angle((Number)Math.Atan(value));
		}

		/// <summary>
		/// Returns an angle whose tangent is quotient of the specified values.
		/// </summary>
		/// <param name="y">Y coordinate of a point.</param>
		/// <param name="x">X coordinate of a point.</param>
		/// <returns>An angle whose tanget is quatient of the <paramref name="y"/> and the <paramref name="x"/>.</returns>
		public static Angle Atan2(Number y, Number x)
		{
			return new Angle((Number)Math.Atan2(y, x));
		}

		/// <summary>
		/// Returns the linear blend of the specified angles.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <param name="amount">Blend amount, in the range of [0, 1].</param>
		/// <returns>The linear blend of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Angle Blend(Angle value1, Angle value2, Number amount)
		{
			return new Angle(value1.radians + amount * (value2.radians - value1.radians));
		}

		/// <summary>
		/// Returns the specified angle restricted to the specified interval.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <param name="minimum">Minimum value for the angle.</param>
		/// <param name="maximum">Maximum value for the angle.</param>
		/// <returns>The <paramref name="value"/> restricted between the <paramref name="minimum"/> and the <paramref name="maximum"/>.</returns>
		public static Angle Clamp(Angle value, Angle minimum, Angle maximum)
		{
			return new Angle(Math.Max(minimum.radians, Math.Min(value.radians, maximum.radians)));
		}

		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <param name="value">An object to compare with this object.</param>
		/// <returns>A 32-bit signed integer that indicates the relative order of the objects being compared.</returns>
		public int CompareTo(object value)
		{
			if (value is Angle)
			{
				return this.CompareTo((Angle)value);
			}

			throw new ArgumentException();
		}

		/// <summary>
		/// Compares the current angle to the specified angle.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>
		/// Zero if the angles are equal.
		/// Less than zero if the current angle is less than the <paramref name="value"/>.
		/// Greater than zero if the current angle is greater than the <paramref name="value"/>.
		/// </returns>
		public int CompareTo(Angle value)
		{
			return Math.Sign(this.radians - value.radians);
		}

		/// <summary>
		/// Returns the cosine of the specified angle.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>The cosine of the <paramref name="value"/>.</returns>
		public static Number Cos(Angle value)
		{
			return (Number)Math.Cos(value.radians);
		}

		/// <summary>
		/// Returns the hyperbolic cosine of the specified angle.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>The hyperbolic cosine of the <paramref name="value"/>.</returns>
		public static Number Cosh(Angle value)
		{
			return (Number)Math.Cosh(value.radians);
		}

		/// <summary>
		/// Returns the specified angle divided by the specified scalar.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The <paramref name="value1"/> divided by the <paramref name="value2"/>.</returns>
		public static Angle Divide(Angle value1, Number value2)
		{
			return new Angle(value1.radians / value2);
		}

		/// <summary>
		/// Returns the quatient of the specified angles.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>The quatient of the <paramref name="value1"/> divided by the <paramref name="value2"/>.</returns>
		public static Number Divide(Angle value1, Angle value2)
		{
			return value1.radians / value2.radians;
		}

		/// <summary>
		/// Determines whether the current object is equal to the specified object of the same type.
		/// </summary>
		/// <param name="value">An object.</param>
		/// <returns>True if the current object is equal to the <paramref name="value"/>, otherwise false.</returns>
		public override bool Equals(object value)
		{
			if (value is Angle)
			{
				return this.Equals((Angle)value);
			}

			return false;
		}

		/// <summary>
		/// Determines whether the current angle is equal to the specified angle.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>True if the current angle is equal to the <paramref name="value"/>, otherwise false.</returns>
		public bool Equals(Angle value)
		{
			return this.radians == value.radians;
		}

		/// <summary>
		/// Creates an angle from the specified degrees.
		/// </summary>
		/// <param name="angleInDegrees">Angle in degrees.</param>
		/// <returns>An angle.</returns>
		public static Angle FromDegrees(Number angleInDegrees)
		{
			return new Angle(angleInDegrees * 0.0174532925f);
		}

		/// <summary>
		/// Creates an angle from the specified radians.
		/// </summary>
		/// <param name="angleInRadians">Angle in radians.</param>
		/// <returns>An angle.</returns>
		public static Angle FromRadians(Number angleInRadians)
		{
			return new Angle(angleInRadians);
		}

		/// <summary>
		/// Returns a hash code for the current angle.
		/// </summary>
		/// <returns>A hash code.</returns>
		/// <remarks>
		/// The hash code is not unique.
		/// If two angles are equal, their hash codes are guaranteed to be equal.
		/// If the angles are not equal, their hash codes are not guaranteed to be different.
		/// </remarks>
		public override int GetHashCode()
		{
			return this.radians.GetHashCode();
		}

		/// <summary>
		/// Determines whether the specified angle evaluates to an infinity.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>True if the <paramref name="value"/> evaluates to an infinity, otherwise false.</returns>
		public static bool IsInfinity(Angle value)
		{
			return Number.IsInfinity(value.radians);
		}

		/// <summary>
		/// Determines whether the specified angle evaluates to a value that is not a number.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>True if the <paramref name="value"/> evaluates to a value that is not a number, otherwise false.</returns>
		public static bool IsNaN(Angle value)
		{
			return Number.IsNaN(value.radians);
		}

		/// <summary>
		/// Determines whether the specified angle evaluates to negative infinity.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>True if the <paramref name="value"/> evaluates to negative infinity, otherwise false.</returns>
		public static bool IsNegativeInfinity(Angle value)
		{
			return Number.IsNegativeInfinity(value.radians);
		}

		/// <summary>
		/// Determines whether the specified angle evaluates to positive infinity.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>True if the <paramref name="value"/> evaluates to positive infinity, otherwise false.</returns>
		public static bool IsPositiveInfinity(Angle value)
		{
			return Number.IsPositiveInfinity(value.radians);
		}

		/// <summary>
		/// Returns the larger of the specified angles.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>The larger of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Angle Max(Angle value1, Angle value2)
		{
			return new Angle(Math.Max(value1.radians, value2.radians));
		}

		/// <summary>
		/// Returns the smaller of the specified angles.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>The smaller of the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Angle Min(Angle value1, Angle value2)
		{
			return new Angle(Math.Min(value1.radians, value2.radians));
		}

		/// <summary>
		/// Returns the remainder of the specified angle divided by the other specified angle.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>The remainder of the <paramref name="value1"/> divided by the <paramref name="value2"/>.</returns>
		public static Angle Modulo(Angle value1, Angle value2)
		{
			return new Angle(value1.radians % value2.radians);
		}

		/// <summary>
		/// Returns the specified angle multiplied by the specified scalar.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The <paramref name="value1"/> multiplied by the <paramref name="value2"/>.</returns>
		public static Angle Multiply(Angle value1, Number value2)
		{
			return new Angle(value1.radians * value2);
		}

		/// <summary>
		/// Returns the negation of the specified angle.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>The negation of the <paramref name="value"/>.</returns>
		public static Angle Negate(Angle value)
		{
			return new Angle(-value.radians);
		}

		/// <summary>
		/// Returns the specified angle normalized to the unit circle range.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>The <paramref name="value"/> normalized to the unit circle range.</returns>
		public static Angle Normalize(Angle value)
		{
			return new Angle(value.radians % (Number)(Math.PI * 2));
		}

		/// <summary>
		/// Returns the angle parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <returns>The angle parsed from the <paramref name="value"/>.</returns>
		public static Angle Parse(string value)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, null);
		}

		/// <summary>
		/// Returns the angle parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style.</param>
		/// <returns>The angle parsed from the <paramref name="value"/>.</returns>
		public static Angle Parse(string value, NumberStyles numberStyle)
		{
			return Parse(value, numberStyle, null);
		}

		/// <summary>
		/// Returns the angle parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider.</param>
		/// <returns>The angle parsed from the <paramref name="value"/>.</returns>
		public static Angle Parse(string value, IFormatProvider formatProvider)
		{
			return Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider);
		}

		/// <summary>
		/// Returns the angle parsed from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style.</param>
		/// <param name="formatProvider">The format provider.</param>
		/// <returns>The angle parsed from the <paramref name="value"/>.</returns>
		public static Angle Parse(string value, NumberStyles numberStyle, IFormatProvider formatProvider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			return new Angle(Number.Parse(value, numberStyle, formatProvider));
		}

		/// <summary>
		/// Returns a value indicating the sign of the specified angle.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>A value indicating the sign of the <paramref name="value"/>.</returns>
		public static Number Sign(Angle value)
		{
			return Math.Sign(value.radians);
		}

		/// <summary>
		/// Returns the sine of the specified angle.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>The sine of the <paramref name="value"/>.</returns>
		public static Number Sin(Angle value)
		{
			return (Number)Math.Sin(value.radians);
		}

		/// <summary>
		/// Returns the hyperbolic sine of the specified angle.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>The hyperbolic sine of the <paramref name="value"/>.</returns>
		public static Number Sinh(Angle value)
		{
			return (Number)Math.Sinh(value.radians);
		}

		/// <summary>
		/// Returns the difference between the specified angles.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>The difference between the <paramref name="value1"/> and the <paramref name="value2"/>.</returns>
		public static Angle Subtract(Angle value1, Angle value2)
		{
			return new Angle(value1.radians - value2.radians);
		}

		/// <summary>
		/// Returns the tangent of the specified angle.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>The tangent of the <paramref name="value"/>.</returns>
		public static Number Tan(Angle value)
		{
			return (Number)Math.Tan(value.radians);
		}

		/// <summary>
		/// Returns the hyperbolic tangent of the specified angle.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>The hyperbolic tangent of the <paramref name="value"/>.</returns>
		public static Number Tanh(Angle value)
		{
			return (Number)Math.Tanh(value.radians);
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
			return this.radians.ToString(format, formatProvider);
		}

		/// <summary>
		/// Attempts to parse the angle from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="result">The output variable for the angle parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, out Angle result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, null, out result);
		}

		/// <summary>
		/// Attempts to parse the angle from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style.</param>
		/// <param name="result">The output variable for the angle parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, out Angle result)
		{
			return TryParse(value, numberStyle, null, out result);
		}

		/// <summary>
		/// Attempts to parse the angle from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="formatProvider">The format provider.</param>
		/// <param name="result">The output variable for the angle parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, IFormatProvider formatProvider, out Angle result)
		{
			return TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider, out result);
		}

		/// <summary>
		/// Attempts to parse the angle from the specified string.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="numberStyle">The number style.</param>
		/// <param name="formatProvider">The format provider.</param>
		/// <param name="result">The output variable for the angle parsed from the <paramref name="value"/>.</param>
		/// <returns>True if the <paramref name="value"/> was parsed successfully, otherwise false.</returns>
		public static bool TryParse(string value, NumberStyles numberStyle, IFormatProvider formatProvider, out Angle result)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			Number angle;

			if (Number.TryParse(value, numberStyle, formatProvider, out angle))
			{
				result = new Angle(angle);
				return true;
			}
			else
			{
				result = new Angle();
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

				if (numbers.Length == 1)
				{
					this.radians = numbers[0];
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
			Float.WriteXml(writer, this.radians);
		}
		#endregion

		#region Operators
		/// <summary>
		/// Determines whether the angles are equal.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>True if the angles are equal, otherwise false.</returns>
		public static bool operator ==(Angle value1, Angle value2)
		{
			return value1.radians == value2.radians;
		}

		/// <summary>
		/// Determines whether the angles are not equal.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>True if the angles are not equal, otherwise false.</returns>
		public static bool operator !=(Angle value1, Angle value2)
		{
			return value1.radians != value2.radians;
		}

		/// <summary>
		/// Compares the angles.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>True if <paramref name="value1"/> is less than <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator <(Angle value1, Angle value2)
		{
			return value1.radians < value2.radians;
		}

		/// <summary>
		/// Compares the angles.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>True if <paramref name="value1"/> is less than or equal to <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator <=(Angle value1, Angle value2)
		{
			return value1.radians <= value2.radians;
		}

		/// <summary>
		/// Compares the angles.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>True if <paramref name="value1"/> is greater than <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator >(Angle value1, Angle value2)
		{
			return value1.radians > value2.radians;
		}

		/// <summary>
		/// Compares the angles.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>True if <paramref name="value1"/> is greater than or equal to <paramref name="value2"/>, otherwise false.</returns>
		public static bool operator >=(Angle value1, Angle value2)
		{
			return value1.radians >= value2.radians;
		}

		/// <summary>
		/// Returns the negation of the angle.
		/// </summary>
		/// <param name="value">An angle.</param>
		/// <returns>Negation of <paramref name="value"/>.</returns>
		public static Angle operator -(Angle value)
		{
			return new Angle(-value.radians);
		}

		/// <summary>
		/// Returns the sum of the angles.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>Sum of the angles <paramref name="value1"/> and <paramref name="value2"/>.</returns>
		public static Angle operator +(Angle value1, Angle value2)
		{
			return new Angle(value1.radians + value2.radians);
		}

		/// <summary>
		/// Returns the difference between the angles.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>Difference between the angles <paramref name="value1"/> and <paramref name="value2"/>.</returns>
		public static Angle operator -(Angle value1, Angle value2)
		{
			return new Angle(value1.radians - value2.radians);
		}

		/// <summary>
		/// Returns the angle multiplied by a scalar.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The <paramref name="value1"/> multiplied by <paramref name="value2"/>.</returns>
		public static Angle operator *(Angle value1, Number value2)
		{
			return new Angle(value1.radians * value2);
		}

		/// <summary>
		/// Returns the angle multiplied by a scalar.
		/// </summary>
		/// <param name="value1">A scalar.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>The <paramref name="value1"/> multiplied by <paramref name="value2"/>.</returns>
		public static Angle operator *(Number value1, Angle value2)
		{
			return new Angle(value1 * value2.radians);
		}

		/// <summary>
		/// Returns the angle divided by the scalar.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">A scalar.</param>
		/// <returns>The <paramref name="value1"/> divided by <paramref name="value2"/>.</returns>
		public static Angle operator /(Angle value1, Number value2)
		{
			return new Angle(value1.radians / value2);
		}

		/// <summary>
		/// Returns the quatient of the angles.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>Quatient of <paramref name="value1"/> divided by <paramref name="value2"/>.</returns>
		public static Number operator /(Angle value1, Angle value2)
		{
			return value1.radians / value2.radians;
		}

		/// <summary>
		/// Returns the remainder of the angle divided by the other angle.
		/// </summary>
		/// <param name="value1">An angle.</param>
		/// <param name="value2">An angle.</param>
		/// <returns>Remainder of <paramref name="value1"/> divided by <paramref name="value2"/>.</returns>
		public static Angle operator %(Angle value1, Angle value2)
		{
			return new Angle(value1.radians % value2.radians);
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the angle in degrees.
		/// </summary>
		/// <value>The angle in degrees.</value>
		public Number Degrees
		{
			get { return this.radians * 57.2957795f; }
		}

		/// <summary>
		/// Gets the angle in radians.
		/// </summary>
		/// <value>The angle in radians.</value>
		public Number Radians
		{
			get { return this.radians; }
		}

		/// <summary>
		/// Gets an angle that evaluates to a value that is not a number.
		/// </summary>
		/// <value>An angle that evaluates to a value that is not a number.</value>
		public static Angle NaN
		{
			get { return Angle.nan; }
		}

		/// <summary>
		/// Gets an angle that evaluates to negative infinity.
		/// </summary>
		/// <value>An angle that evaluates to negative infinity.</value>
		public static Angle NegativeInfinity
		{
			get { return Angle.negativeInfinity; }
		}

		/// <summary>
		/// Gets an angle that evaluates to positive infinity.
		/// </summary>
		/// <value>An angle that evaluates to positive infinity.</value>
		public static Angle PositiveInfinity
		{
			get { return Angle.positiveInfinity; }
		}

		/// <summary>
		/// Gets An angle that evaluates to zero.
		/// </summary>
		/// <value>An angle that evaluates to zero.</value>
		public static Angle Zero
		{
			get { return Angle.zero; }
		}
		#endregion

		#region Fields
		/// <summary>
		/// An angle that evaluates to a value that is not a number.
		/// </summary>
		private static readonly Angle nan = new Angle(Number.NaN);

		/// <summary>
		/// An angle that evaluates to negative infinity.
		/// </summary>
		private static readonly Angle negativeInfinity = new Angle(Number.NegativeInfinity);

		/// <summary>
		/// An angle that evaluates to positive infinity.
		/// </summary>
		private static readonly Angle positiveInfinity = new Angle(Number.PositiveInfinity);

		/// <summary>
		/// An angle that evaluates to zero.
		/// </summary>
		private static readonly Angle zero = new Angle(0);

		/// <summary>
		/// Angle in radians.
		/// </summary>
		private Number radians;
		#endregion
	}
}
