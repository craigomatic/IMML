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

namespace System
{
	/// <summary>
	/// Provides methods for calculating and combining hash codes.
	/// </summary>
	public static class HashCode
	{
		/// <summary>
		/// Returns a hash code for the specified object.
		/// </summary>
		/// <param name="instance">Object instance.</param>
		/// <returns>A hash code.</returns>
		public static int GetHashCode(object instance)
		{
			return (instance != null) ? instance.GetHashCode() : 0x61E04917;
		}

		/// <summary>
		/// Returns the combined hash code of the specified hash codes.
		/// </summary>
		/// <param name="hash1">A hash code.</param>
		/// <param name="hash2">A hash code.</param>
		/// <returns>Combined hash code.</returns>
		public static int GetHashCode(int hash1, int hash2)
		{
			return ((hash1 << 5) + hash1) ^ hash2;
		}

		/// <summary>
		/// Returns the combined hash code of the specified hash codes.
		/// </summary>
		/// <param name="hash1">A hash code.</param>
		/// <param name="hash2">A hash code.</param>
		/// <param name="hash3">A hash code.</param>
		/// <returns>Combined hash code.</returns>
		public static int GetHashCode(int hash1, int hash2, int hash3)
		{
			return GetHashCode(GetHashCode(hash1, hash2), hash3);
		}

		/// <summary>
		/// Returns the combined hash code of the specified hash codes.
		/// </summary>
		/// <param name="hash1">A hash code.</param>
		/// <param name="hash2">A hash code.</param>
		/// <param name="hash3">A hash code.</param>
		/// <param name="hash4">A hash code.</param>
		/// <returns>Combined hash code.</returns>
		public static int GetHashCode(int hash1, int hash2, int hash3, int hash4)
		{
			return GetHashCode(GetHashCode(hash1, hash2, hash3), hash4);
		}
	}
}
