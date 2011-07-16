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
#if USE_DOUBLE_PRECISION
	using Number = System.Double;
#else
	using Number = System.Single;
#endif

	/// <summary>
	/// Provides methods for calculating distances between geometric objects.
	/// </summary>
	public static class Distance
	{
		/// <summary>
		/// Returns the distance between the specified bounding boxes.
		/// </summary>
		/// <param name="boundingBox1">A bounding box.</param>
		/// <param name="boundingBox2">A bounding box.</param>
		/// <returns>The distance between the <paramref name="boundingBox1"/> and the <paramref name="boundingBox2"/>.</returns>
		public static Number BoundingBoxToBoundingBox(BoundingBox boundingBox1, BoundingBox boundingBox2)
		{
			var center = (boundingBox1.Minimum + boundingBox1.Maximum) / 2;
			var closestPoint1 = Vector3.Clamp(center, boundingBox2.Minimum, boundingBox2.Maximum);
			var closestPoint2 = Vector3.Clamp(closestPoint1, boundingBox1.Minimum, boundingBox1.Maximum);
			return Distance.PointToPoint(closestPoint1, closestPoint2);
		}

		/// <summary>
		/// Returns the distance between the specified bounding box and the specified bounding sphere.
		/// </summary>
		/// <param name="boundingBox">A bounding box.</param>
		/// <param name="boundingSphere">A bounding sphere.</param>
		/// <returns>The distance between the <paramref name="boundingBox"/> and the <paramref name="boundingSphere"/>.</returns>
		public static Number BoundingBoxToBoundingSphere(BoundingBox boundingBox, BoundingSphere boundingSphere)
		{
			var closestPoint = Vector3.Clamp(boundingSphere.Position, boundingBox.Minimum, boundingBox.Maximum);
			return Distance.PointToBoundingSphere(closestPoint, boundingSphere);
		}

		/// <summary>
		/// Returns the distance between the specified bounding box and the specified line.
		/// </summary>
		/// <param name="boundingBox">A bounding box.</param>
		/// <param name="line">A line.</param>
		/// <returns>The distance between the <paramref name="boundingBox"/> and the <paramref name="line"/>.</returns>
		public static Number BoundingBoxToLine(BoundingBox boundingBox, Line line)
		{
			var center = (boundingBox.Minimum + boundingBox.Maximum) / 2;
			var position = Project.PointOnLine(center, line);
			var normal = Vector3.Normalize(center - position);
			return Distance.BoundingBoxToPlane(boundingBox, new Plane(normal, position));
		}

		/// <summary>
		/// Returns the distance between the specified bounding box and the specified line segment.
		/// </summary>
		/// <param name="boundingBox">A bounding box.</param>
		/// <param name="lineSegment">A line segment.</param>
		/// <returns>The distance between the <paramref name="boundingBox"/> and the <paramref name="lineSegment"/>.</returns>
		public static Number BoundingBoxToLineSegment(BoundingBox boundingBox, LineSegment lineSegment)
		{
			var center = (boundingBox.Minimum + boundingBox.Maximum) / 2;
			var t = lineSegment.End2 - lineSegment.End1;
			var u = Vector3.Dot(center - lineSegment.End1, t) / t.MagnitudeSquared;

			if (u < 0)
			{
				u = 0;
			}

			if (u > 1)
			{
				u = 1;
			}

			var closestPoint = lineSegment.End1 + u * t;

			if (u > 0 && u < 1)
			{
				var normal = Vector3.Normalize(center - closestPoint);
				return BoundingBoxToPlane(boundingBox, new Plane(normal, closestPoint));
			}
			else
			{
				return PointToBoundingBox(closestPoint, boundingBox);
			}
		}

		/// <summary>
		/// Returns the distance between the specified bounding box and the specified plane.
		/// </summary>
		/// <param name="boundingBox">A bounding box.</param>
		/// <param name="plane">A plane.</param>
		/// <returns>The distance between the <paramref name="boundingBox"/> and the <paramref name="plane"/>.</returns>
		public static Number BoundingBoxToPlane(BoundingBox boundingBox, Plane plane)
		{
			var min = new Vector3
			{
				X = plane.A > 0 ? boundingBox.Minimum.X : boundingBox.Maximum.X,
				Y = plane.B > 0 ? boundingBox.Minimum.Y : boundingBox.Maximum.Y,
				Z = plane.C > 0 ? boundingBox.Minimum.Z : boundingBox.Maximum.Z,
			};

			var minDistance = Distance.PointToPlane(min, plane);

			if (minDistance >= 0)
			{
				return minDistance;
			}

			var max = new Vector3
			{
				X = plane.A < 0 ? boundingBox.Minimum.X : boundingBox.Maximum.X,
				Y = plane.B < 0 ? boundingBox.Minimum.Y : boundingBox.Maximum.Y,
				Z = plane.C < 0 ? boundingBox.Minimum.Z : boundingBox.Maximum.Z,
			};

			var maxDistance = Distance.PointToPlane(max, plane);

			if (maxDistance <= 0)
			{
				return maxDistance;
			}

			return 0;
		}

		/// <summary>
		/// Returns the distance between the specified bounding box and the specified ray.
		/// </summary>
		/// <param name="boundingBox">A bounding box.</param>
		/// <param name="ray">A ray.</param>
		/// <returns>The distance between the <paramref name="boundingBox"/> and the <paramref name="ray"/>.</returns>
		public static Number BoundingBoxToRay(BoundingBox boundingBox, Ray ray)
		{
			var center = (boundingBox.Minimum + boundingBox.Maximum) / 2;
			var u = Vector3.Dot(center - ray.Position, ray.Direction) / ray.Direction.MagnitudeSquared;

			if (u < 0)
			{
				u = 0;
			}

			var closestPoint = ray.Position + u * ray.Direction;

			if (u > 0 && u < 1)
			{
				var normal = Vector3.Normalize(center - closestPoint);
				return BoundingBoxToPlane(boundingBox, new Plane(normal, closestPoint));
			}
			else
			{
				return PointToBoundingBox(closestPoint, boundingBox);
			}
		}

		/// <summary>
		/// Returns the distance between the specified bounding spheres.
		/// </summary>
		/// <param name="boundingSphere1">A bounding sphere.</param>
		/// <param name="boundingSphere2">A bounding sphere.</param>
		/// <returns>The distance between the <paramref name="boundingSphere1"/> and the <paramref name="boundingSphere2"/>.</returns>
		public static Number BoundingSphereToBoundingSphere(BoundingSphere boundingSphere1, BoundingSphere boundingSphere2)
		{
			return Math.Max(0, Distance.PointToPoint(boundingSphere1.Position, boundingSphere2.Position) - boundingSphere1.Radius - boundingSphere2.Radius);
		}

		/// <summary>
		/// Returns the distance between the specified bounding sphere and the specified line.
		/// </summary>
		/// <param name="boundingSphere">A bounding sphere.</param>
		/// <param name="line">A line.</param>
		/// <returns>The distance between the <paramref name="boundingSphere"/> and the <paramref name="line"/>.</returns>
		public static Number BoundingSphereToLine(BoundingSphere boundingSphere, Line line)
		{
			return Math.Max(0, Distance.PointToLine(boundingSphere.Position, line) - boundingSphere.Radius);
		}

		/// <summary>
		/// Returns the distance between the specified bounding sphere and the specified line segment.
		/// </summary>
		/// <param name="boundingSphere">A bounding sphere.</param>
		/// <param name="lineSegment">A line segment.</param>
		/// <returns>The distance between the <paramref name="boundingSphere"/> and the <paramref name="lineSegment"/>.</returns>
		public static Number BoundingSphereToLineSegment(BoundingSphere boundingSphere, LineSegment lineSegment)
		{
			return Math.Max(0, Distance.PointToLineSegment(boundingSphere.Position, lineSegment) - boundingSphere.Radius);
		}

		/// <summary>
		/// Returns the distance between the specified bounding sphere and the specified plane.
		/// </summary>
		/// <param name="boundingSphere">A bounding sphere.</param>
		/// <param name="plane">A plane.</param>
		/// <returns>The distance between the <paramref name="boundingSphere"/> and the <paramref name="plane"/>.</returns>
		public static Number BoundingSphereToPlane(BoundingSphere boundingSphere, Plane plane)
		{
			var distance = Distance.PointToPlane(boundingSphere.Position, plane);

			if (distance < -boundingSphere.Radius)
			{
				return distance + boundingSphere.Radius;
			}

			if (distance > boundingSphere.Radius)
			{
				return distance - boundingSphere.Radius;
			}

			return 0;
		}

		/// <summary>
		/// Returns the distance between the specified bounding sphere and the specified ray.
		/// </summary>
		/// <param name="boundingSphere">A bounding sphere.</param>
		/// <param name="ray">A ray.</param>
		/// <returns>The distance between the <paramref name="boundingSphere"/> and the <paramref name="ray"/>.</returns>
		public static Number BoundingSphereToRay(BoundingSphere boundingSphere, Ray ray)
		{
			return Math.Max(0, Distance.PointToRay(boundingSphere.Position, ray) - boundingSphere.Radius);
		}

		/// <summary>
		/// Returns the distance between the specified point and the specified bounding box.
		/// </summary>
		/// <param name="point">A point.</param>
		/// <param name="boundingBox">A bounding box.</param>
		/// <returns>The distance between the <paramref name="point"/> and the <paramref name="boundingBox"/>.</returns>
		public static Number PointToBoundingBox(Vector3 point, BoundingBox boundingBox)
		{
			return Distance.PointToPoint(point, Vector3.Clamp(point, boundingBox.Minimum, boundingBox.Maximum));
		}

		/// <summary>
		/// Returns the distance between the specified point and the specified bounding sphere.
		/// </summary>
		/// <param name="point">A point.</param>
		/// <param name="boundingSphere">A bounding sphere.</param>
		/// <returns>The distance between the <paramref name="point"/> and the <paramref name="boundingSphere"/>.</returns>
		public static Number PointToBoundingSphere(Vector3 point, BoundingSphere boundingSphere)
		{
			return Math.Max(0, Distance.PointToPoint(point, boundingSphere.Position) - boundingSphere.Radius);
		}

		/// <summary>
		/// Returns the distance between the specified point and the specified line.
		/// </summary>
		/// <param name="point">A point.</param>
		/// <param name="line">A line.</param>
		/// <returns>The distance between the <paramref name="point"/> and the <paramref name="line"/>.</returns>
		public static Number PointToLine(Vector3 point, Line line)
		{
			var u = Vector3.Dot(point - line.Point1, line.Point2 - line.Point1) / Distance.PointToPointSquared(line.Point2, line.Point1);
			return Distance.PointToPoint(point, Vector3.Blend(line.Point1, line.Point2, u));
		}

		/// <summary>
		/// Returns the distance between the specified point and the specified line segment.
		/// </summary>
		/// <param name="point">A point.</param>
		/// <param name="lineSegment">A line segment.</param>
		/// <returns>The distance between the <paramref name="point"/> and the <paramref name="lineSegment"/>.</returns>
		public static Number PointToLineSegment(Vector3 point, LineSegment lineSegment)
		{
			var u = Vector3.Dot(point - lineSegment.End1, lineSegment.End2 - lineSegment.End1) / Distance.PointToPointSquared(lineSegment.End2, lineSegment.End1);

			if (u < 0)
			{
				u = 0;
			}

			if (u > 1)
			{
				u = 1;
			}

			return Distance.PointToPoint(point, Vector3.Blend(lineSegment.End1, lineSegment.End2, u));
		}

		/// <summary>
		/// Returns the distance between the specified point and the specified plane.
		/// </summary>
		/// <param name="point">A point.</param>
		/// <param name="plane">A plane.</param>
		/// <returns>The distance between the <paramref name="point"/> and the <paramref name="plane"/>.</returns>
		public static Number PointToPlane(Vector3 point, Plane plane)
		{
			return
				point.X * plane.A +
				point.Y * plane.B +
				point.Z * plane.C +
				plane.D;
		}

		/// <summary>
		/// Returns the distance between the specified points.
		/// </summary>
		/// <param name="point1">A point.</param>
		/// <param name="point2">A point.</param>
		/// <returns>The distance between the <paramref name="point1"/> and the <paramref name="point2"/>.</returns>
		public static Number PointToPoint(Vector3 point1, Vector3 point2)
		{
			return (point1 - point2).Magnitude;
		}

		/// <summary>
		/// Returns the squared distance between the specified points.
		/// </summary>
		/// <param name="point1">A point.</param>
		/// <param name="point2">A point.</param>
		/// <returns>The distance between the <paramref name="point1"/> and the <paramref name="point2"/>.</returns>
		public static Number PointToPointSquared(Vector3 point1, Vector3 point2)
		{
			return (point1 - point2).MagnitudeSquared;
		}

		/// <summary>
		/// Returns the distance between the specified point and the specified ray.
		/// </summary>
		/// <param name="point">A point.</param>
		/// <param name="ray">A ray.</param>
		/// <returns>The distance between the <paramref name="point"/> and the <paramref name="ray"/>.</returns>
		public static Number PointToRay(Vector3 point, Ray ray)
		{
			var u = Vector3.Dot(point - ray.Position, ray.Direction) / ray.Direction.MagnitudeSquared;

			if (u < 0)
			{
				u = 0;
			}

			return Distance.PointToPoint(point, Vector3.Blend(ray.Position, ray.Position + ray.Direction, u));
		}
	}
}
