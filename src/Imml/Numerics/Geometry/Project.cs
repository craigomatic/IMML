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
	/// <summary>
	/// Provides methods for calculating projections between geometric objects.
	/// </summary>
	public static class Project
	{
		/// <summary>
		/// Projects the specified line on the specified plane.
		/// </summary>
		/// <param name="line">A line.</param>
		/// <param name="plane">A plane.</param>
		/// <returns>The <paramref name="line"/> projected on the <paramref name="plane"/>.</returns>
		public static Line LineOnPlane(Line line, Plane plane)
		{
			return new Line
			{
				Point1 = Project.PointOnPlane(line.Point1, plane),
				Point2 = Project.PointOnPlane(line.Point2, plane),
			};
		}

		/// <summary>
		/// Projects the specified line segment on the specified line.
		/// </summary>
		/// <param name="lineSegment">A line segment.</param>
		/// <param name="line">A line.</param>
		/// <returns>The <paramref name="lineSegment"/> projected on the <paramref name="line"/>.</returns>
		public static LineSegment LineSegmentOnLine(LineSegment lineSegment, Line line)
		{
			return new LineSegment
			{
				End1 = Project.PointOnLine(lineSegment.End1, line),
				End2 = Project.PointOnLine(lineSegment.End2, line),
			};
		}

		/// <summary>
		/// Projects the specified line segment on the specified line segment.
		/// </summary>
		/// <param name="lineSegment1">A line segment.</param>
		/// <param name="lineSegment2">A line segment.</param>
		/// <returns>The <paramref name="lineSegment1"/> projected on the <paramref name="lineSegment2"/>.</returns>
		public static LineSegment LineSegmentOnLineSegment(LineSegment lineSegment1, LineSegment lineSegment2)
		{
			return new LineSegment
			{
				End1 = Project.PointOnLineSegment(lineSegment1.End1, lineSegment2),
				End2 = Project.PointOnLineSegment(lineSegment1.End2, lineSegment2),
			};
		}

		/// <summary>
		/// Projects the specified line segment on the specified plane.
		/// </summary>
		/// <param name="lineSegment">A line segment.</param>
		/// <param name="plane">A plane.</param>
		/// <returns>The <paramref name="lineSegment"/> projected on the <paramref name="plane"/>.</returns>
		public static LineSegment LineSegmentOnPlane(LineSegment lineSegment, Plane plane)
		{
			return new LineSegment
			{
				End1 = Project.PointOnPlane(lineSegment.End1, plane),
				End2 = Project.PointOnPlane(lineSegment.End2, plane),
			};
		}

		/// <summary>
		/// Projects the specified line segment on the specified ray.
		/// </summary>
		/// <param name="lineSegment">A line segment.</param>
		/// <param name="ray">A ray.</param>
		/// <returns>The <paramref name="lineSegment"/> projected on the <paramref name="ray"/>.</returns>
		public static LineSegment LineSegmentOnRay(LineSegment lineSegment, Ray ray)
		{
			return new LineSegment
			{
				End1 = Project.PointOnRay(lineSegment.End1, ray),
				End2 = Project.PointOnRay(lineSegment.End2, ray),
			};
		}

		/// <summary>
		/// Projects the specified point on the specified line.
		/// </summary>
		/// <param name="point">A point.</param>
		/// <param name="line">A line.</param>
		/// <returns>The <paramref name="point"/> projected on the <paramref name="line"/>.</returns>
		public static Vector3 PointOnLine(Vector3 point, Line line)
		{
			return Vector3.ProjectOnLine(point, line.Point1, line.Point2);
		}

		/// <summary>
		/// Projects the specified point on the specified line segment.
		/// </summary>
		/// <param name="point">A point.</param>
		/// <param name="lineSegment">A line segment.</param>
		/// <returns>The <paramref name="point"/> projected on the <paramref name="lineSegment"/>.</returns>
		public static Vector3 PointOnLineSegment(Vector3 point, LineSegment lineSegment)
		{
			return Vector3.ProjectOnLineSegment(point, lineSegment.End1, lineSegment.End2);
		}

		/// <summary>
		/// Projects the specified point on the specified plane.
		/// </summary>
		/// <param name="point">A point.</param>
		/// <param name="plane">A plane.</param>
		/// <returns>The <paramref name="point"/> projected on the <paramref name="plane"/>.</returns>
		public static Vector3 PointOnPlane(Vector3 point, Plane plane)
		{
			var t = (point.X * plane.A + point.Y * plane.B + point.Z * plane.C + plane.D) / (plane.A * plane.A + plane.B * plane.B + plane.C * plane.C);

			return new Vector3
			{
				X = point.X - plane.A * t,
				Y = point.Y - plane.B * t,
				Z = point.Z - plane.C * t,
			};
		}

		/// <summary>
		/// Projects the specified point on the specified ray.
		/// </summary>
		/// <param name="point">A point.</param>
		/// <param name="ray">A ray.</param>
		/// <returns>The <paramref name="point"/> projected on the <paramref name="ray"/>.</returns>
		public static Vector3 PointOnRay(Vector3 point, Ray ray)
		{
			return Vector3.ProjectOnRay(point, ray.Position, ray.Direction);
		}

		/// <summary>
		/// Projects the specified ray on the specified plane.
		/// </summary>
		/// <param name="ray">A ray.</param>
		/// <param name="plane">A plane.</param>
		/// <returns>The <paramref name="ray"/> projected on the <paramref name="plane"/>.</returns>
		public static Ray RayOnPlane(Ray ray, Plane plane)
		{
			var position = Project.PointOnPlane(ray.Position, plane);
			var direction = Project.PointOnPlane(ray.Position + ray.Direction, plane) - position;
			return Ray.Normalize(position, direction);
		}
	}
}
