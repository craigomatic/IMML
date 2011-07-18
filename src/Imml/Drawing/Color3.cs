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
using System.Xml.Serialization;
using System.Diagnostics;
using System.Globalization;

namespace Imml.Drawing
{
    /// <summary>
    /// Representation of a color with 3 channels.
    /// </summary>
    [XmlType("color3")]
    [DebuggerDisplay("R = {R} G = {G} B = {B}")]
    public struct Color3 : IEquatable<Color3>, IFormattable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the structure.
        /// </summary>
        /// <param name="r">Red channel value.</param>
        /// <param name="g">Green channel value.</param>
        /// <param name="b">Blue channel value.</param>
        public Color3(float r, float g, float b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color3"/> struct.
        /// </summary>
        /// <param name="colorString">The color string.</param>
        public Color3(string colorString)
        {
            colorString = colorString.TrimStart('#');

            try
            {
                this.R = (float)byte.Parse(colorString.Substring(0, 2), NumberStyles.HexNumber) / 255f;
                this.G = (float)byte.Parse(colorString.Substring(2, 2), NumberStyles.HexNumber) / 255f;
                this.B = (float)byte.Parse(colorString.Substring(4, 2), NumberStyles.HexNumber) / 255f;
            }
            catch
            {
                this.R = 0;
                this.G = 0;
                this.B = 0;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Indicates whether the current object and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> and the current object are the same type and represent the same value; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Color3)
            {
                return Equals((Color3)obj);
            }

            return false;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><c>true</c> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <c>false</c>.</returns>
        public bool Equals(Color3 other)
        {
            return
                this.R == other.R &&
                this.G == other.G &&
                this.B == other.B;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <param name="delta">Maximum allowed error.</param>
        /// <returns><c>true</c> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <c>false</c>.</returns>
        public bool Equals(Color3 other, float delta)
        {
            return
                Math.Abs(this.R - other.R) <= delta &&
                Math.Abs(this.G - other.G) <= delta &&
                Math.Abs(this.B - other.B) <= delta;
        }

        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return
                this.R.GetHashCode() +
                this.G.GetHashCode() +
                this.B.GetHashCode();
        }

        /// <summary>
        /// Gets the string representation of the current object.
        /// </summary>
        /// <returns>A <see cref="String"/> containing a human-readable representation of the object.</returns>
        public override string ToString()
        {
            return string.Format("#{0:x2}{1:x2}{2:x2}", (byte)(this.R * 255), (byte)(this.G * 255), (byte)(this.B * 255));
        }

        /// <summary>
        /// Formats the value of the current object using the specified format.
        /// </summary>
        /// <param name="format">The <see cref="System.String"/> specifying the format to use. -or- null to use the default format defined for the type of the <see cref="System.IFormattable"/> implementation. </param>
        /// <param name="formatProvider">The <see cref="System.IFormatProvider"/> to use to format the value.-or- null to obtain the numeric format information from the current locale setting of the operating system. </param>
        /// <returns>A <see cref="String"/> containing a human-readable representation of the object.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return this.ToString();
        }
        #endregion

        #region Operators
        /// <summary>
        /// Determines whether two instances are equal.
        /// </summary>
        /// <param name="value1">A <see cref="Color3"/>.</param>
        /// <param name="value2">A <see cref="Color3"/>.</param>
        /// <returns>A boolean value indicating whether the instances are equal.</returns>
        public static bool operator ==(Color3 value1, Color3 value2)
        {
            return
                value1.R == value2.R &&
                value1.G == value2.G &&
                value1.B == value2.B;
        }

        /// <summary>
        /// Determines whether two instances are not equal.
        /// </summary>
        /// <param name="value1">A <see cref="Color3"/>.</param>
        /// <param name="value2">A <see cref="Color3"/>.</param>
        /// <returns>A boolean value indicating whether the instances are not equal.</returns>
        public static bool operator !=(Color3 value1, Color3 value2)
        {
            return
                value1.R != value2.R ||
                value1.G != value2.G ||
                value1.B != value2.B;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the empty color.
        /// </summary>
        /// <value>
        /// A <see cref="Color3"/>.
        /// </value>
        /// <remarks>
        /// This property is read-only.
        /// </remarks>
        public static Color3 Empty
        {
            get { return empty; }
        }

        /// <summary>
        /// Gets the transparent color.
        /// </summary>
        /// <value>
        /// A <see cref="Color3"/>.
        /// </value>
        /// <remarks>
        /// This property is read-only.
        /// </remarks>
        public static Color3 Transparent
        {
            get { return transparent; }
        }

        /// <summary>
        /// Gets the black color.
        /// </summary>
        /// <value>
        /// A <see cref="Color3"/>.
        /// </value>
        /// <remarks>
        /// This property is read-only.
        /// </remarks>
        public static Color3 Black
        {
            get { return black; }
        }

        /// <summary>
        /// Gets the white color.
        /// </summary>
        /// <value>
        /// A <see cref="Color3"/>.
        /// </value>
        /// <remarks>
        /// This property is read-only.
        /// </remarks>
        public static Color3 White
        {
            get { return white; }
        }

        /// <summary>
        /// Gets the red color.
        /// </summary>
        /// <value>
        /// A <see cref="Color3"/>.
        /// </value>
        /// <remarks>
        /// This property is read-only.
        /// </remarks>
        public static Color3 Red
        {
            get { return red; }
        }

        /// <summary>
        /// Gets the green color.
        /// </summary>
        /// <value>
        /// A <see cref="Color3"/>.
        /// </value>
        /// <remarks>
        /// This property is read-only.
        /// </remarks>
        public static Color3 Green
        {
            get { return green; }
        }

        /// <summary>
        /// Gets the blue color.
        /// </summary>
        /// <value>
        /// A <see cref="Color3"/>.
        /// </value>
        /// <remarks>
        /// This property is read-only.
        /// </remarks>
        public static Color3 Blue
        {
            get { return blue; }
        }
        #endregion

        #region Fields
        /// <summary>
        /// Empty color.
        /// </summary>
        private static readonly Color3 empty = new Color3();

        /// <summary>
        /// Transparent color.
        /// </summary>
        private static readonly Color3 transparent = new Color3(0, 0, 0);

        /// <summary>
        /// Black color.
        /// </summary>
        private static readonly Color3 black = new Color3(0, 0, 0);

        /// <summary>
        /// White color.
        /// </summary>
        private static readonly Color3 white = new Color3(1, 1, 1);

        /// <summary>
        /// Red color.
        /// </summary>
        private static readonly Color3 red = new Color3(1, 0, 0);

        /// <summary>
        /// Green color.
        /// </summary>
        private static readonly Color3 green = new Color3(0, 1, 0);

        /// <summary>
        /// Blue color.
        /// </summary>
        private static readonly Color3 blue = new Color3(0, 0, 1);

        /// <summary>
        /// Red channel value.
        /// </summary>
        [XmlAttribute("red")]
        public float R;

        /// <summary>
        /// Green channel value.
        /// </summary>
        [XmlAttribute("green")]
        public float G;

        /// <summary>
        /// Blue channel value.
        /// </summary>
        [XmlAttribute("blue")]
        public float B;
        #endregion
    }
}
