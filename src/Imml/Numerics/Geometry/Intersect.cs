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
	/// Provides methods for calculating intersections between geometric objects.
	/// </summary>
	public static class Intersect
	{
		/// <summary>
		/// Determines whether the specified bounding boxes intersect.
		/// </summary>
		/// <param name="boundingBox1">A bounding box.</param>
		/// <param name="boundingBox2">A bounding box.</param>
		/// <returns>True if the <paramref name="boundingBox1"/> and the <paramref name="boundingBox2"/> intersect, otherwise false.</returns>
		public static bool BoundingBoxWithBoundingBox(BoundingBox boundingBox1, BoundingBox boundingBox2)
		{
			var L = Math.Max(boundingBox1.Minimum.X, boundingBox2.Minimum.X);
			var R = Math.Min(boundingBox1.Maximum.X, boundingBox2.Maximum.X);
			var B = Math.Max(boundingBox1.Minimum.Y, boundingBox2.Minimum.Y);
			var T = Math.Min(boundingBox1.Maximum.Y, boundingBox2.Maximum.Y);
			var F = Math.Max(boundingBox1.Minimum.Z, boundingBox2.Minimum.Z);
			var N = Math.Min(boundingBox1.Maximum.Z, boundingBox2.Maximum.Z);

			return R >= L && T >= B && F >= N;
		}

		/// <summary>
		/// Determines whether the specified bounding boxes intersect.
		/// </summary>
		/// <param name="boundingBox1">A bounding box.</param>
		/// <param name="boundingBox2">A bounding box.</param>
		/// <param name="intersection">Output variable for the intersection.</param>
		/// <returns>True if the <paramref name="boundingBox1"/> and the <paramref name="boundingBox2"/> intersect, otherwise false.</returns>
		public static bool BoundingBoxWithBoundingBox(BoundingBox boundingBox1, BoundingBox boundingBox2, out BoundingBox intersection)
		{
			var L = Math.Max(boundingBox1.Minimum.X, boundingBox2.Minimum.X);
			var R = Math.Min(boundingBox1.Maximum.X, boundingBox2.Maximum.X);
			var B = Math.Max(boundingBox1.Minimum.Y, boundingBox2.Minimum.Y);
			var T = Math.Min(boundingBox1.Maximum.Y, boundingBox2.Maximum.Y);
			var F = Math.Max(boundingBox1.Minimum.Z, boundingBox2.Minimum.Z);
			var N = Math.Min(boundingBox1.Maximum.Z, boundingBox2.Maximum.Z);

			if (R >= L && T >= B && F >= N)
			{
				intersection = new BoundingBox
				{
					Minimum = new Vector3(L, B, N),
					Maximum = new Vector3(R, T, F),
				};

				return true;
			}
			else
			{
				intersection = default(BoundingBox);
				return false;
			}
		}

		/// <summary>
		/// Determines whether the specified bounding box intersects with the specified bounding sphere.
		/// </summary>
		/// <param name="boundingBox">A bounding box.</param>
		/// <param name="boundingSphere">A bounding sphere.</param>
		/// <returns>True if the <paramref name="boundingBox"/> and the <paramref name="boundingSphere"/> intersect, otherwise false.</returns>
		public static bool BoundingBoxWithBoundingSphere(BoundingBox boundingBox, BoundingSphere boundingSphere)
		{
			var closestPoint = Vector3.Clamp(boundingSphere.Position, boundingBox.Minimum, boundingBox.Maximum);
			return Distance.PointToPointSquared(boundingSphere.Position, closestPoint) <= boundingSphere.Radius * boundingSphere.Radius;
		}

		/// <summary>
		/// Determines whether the specified bounding box intersects with the specified bounding sphere.
		/// </summary>
		/// <param name="boundingBox">A bounding box.</param>
		/// <param name="boundingSphere">A bounding sphere.</param>
		/// <param name="intersection">Output variable for the intersection.</param>
		/// <returns>True if the <paramref name="boundingBox"/> and the <paramref name="boundingSphere"/> intersect, otherwise false.</returns>
		public static bool BoundingBoxWithBoundingSphere(BoundingBox boundingBox, BoundingSphere boundingSphere, out Vector3 intersection)
		{
			var closestPoint = Vector3.Clamp(boundingSphere.Position, boundingBox.Minimum, boundingBox.Maximum);

			if (Distance.PointToPointSquared(closestPoint, boundingSphere.Position) <= boundingSphere.Radius * boundingSphere.Radius)
			{
				intersection = closestPoint;
				return true;
			}
			else
			{
				intersection = default(Vector3);
				return false;
			}
		}

		/// <summary>
		/// Determines whether the specified bounding box intersects with the specified point.
		/// </summary>
		/// <param name="boundingBox">A bounding box.</param>
		/// <param name="point">A point.</param>
		/// <returns>True if the <paramref name="boundingBox"/> and the <paramref name="point"/> intersect, otherwise false.</returns>
		public static bool BoundingBoxWithPoint(BoundingBox boundingBox, Vector3 point)
		{
			return
				boundingBox.Minimum.X <= point.X && point.X <= boundingBox.Maximum.X &&
				boundingBox.Minimum.Y <= point.Y && point.Y <= boundingBox.Maximum.Y &&
				boundingBox.Minimum.Z <= point.Z && point.Z <= boundingBox.Maximum.Z;
		}

		/// <summary>
		/// Determines whether the specified bounding box intersects with the specified ray.
		/// </summary>
		/// <param name="boundingBox">A bounding box.</param>
		/// <param name="ray">A ray.</param>
		/// <returns>True if the <paramref name="boundingBox"/> and the <paramref name="ray"/> intersect, otherwise false.</returns>
		public static bool BoundingBoxWithRay(BoundingBox boundingBox, Ray ray)
		{
			var n = Number.MinValue;
			var f = Number.MaxValue;

			if (Float.Near(ray.Direction.X, 0))
			{
				if (ray.Position.X < boundingBox.Minimum.X || ray.Position.X > boundingBox.Maximum.X)
				{
					return false;
				}
			}
			else
			{
				var inv = 1 / ray.Direction.X;
				var d1 = (boundingBox.Minimum.X - ray.Position.X) * inv;
				var d2 = (boundingBox.Maximum.X - ray.Position.X) * inv;

				n = Math.Max(n, Math.Min(d1, d2));
				f = Math.Min(f, Math.Max(d1, d2));

				if (n > f || f < 0)
				{
					return false;
				}
			}

			if (Float.Near(ray.Direction.Y, 0))
			{
				if (ray.Position.Y < boundingBox.Minimum.Y || ray.Position.Y > boundingBox.Maximum.Y)
				{
					return false;
				}
			}
			else
			{
				var inv = 1 / ray.Direction.Y;
				var d1 = (boundingBox.Minimum.Y - ray.Position.Y) * inv;
				var d2 = (boundingBox.Maximum.Y - ray.Position.Y) * inv;

				n = Math.Max(n, Math.Min(d1, d2));
				f = Math.Min(f, Math.Max(d1, d2));

				if (n > f || f < 0)
				{
					return false;
				}
			}

			if (Float.Near(ray.Direction.Z, 0))
			{
				if (ray.Position.Z < boundingBox.Minimum.Z || ray.Position.Z > boundingBox.Maximum.Z)
				{
					return false;
				}
			}
			else
			{
				var inv = 1 / ray.Direction.Z;
				var d1 = (boundingBox.Minimum.Z - ray.Position.Z) * inv;
				var d2 = (boundingBox.Maximum.Z - ray.Position.Z) * inv;

				n = Math.Max(n, Math.Min(d1, d2));
				f = Math.Min(f, Math.Max(d1, d2));

				if (n > f || f < 0)
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Determines whether the specified bounding box intersects with the specified ray.
		/// </summary>
		/// <param name="boundingBox">A bounding box.</param>
		/// <param name="ray">A ray.</param>
		/// <param name="intersection">Output variable for the intersection.</param>
		/// <returns>True if the <paramref name="boundingBox"/> and the <paramref name="ray"/> intersect, otherwise false.</returns>
		public static bool BoundingBoxWithRay(BoundingBox boundingBox, Ray ray, out LineSegment intersection)
		{
			var n = Number.MinValue;
			var f = Number.MaxValue;

			if (Float.Near(ray.Direction.X, 0))
			{
				if (ray.Position.X < boundingBox.Minimum.X || ray.Position.X > boundingBox.Maximum.X)
				{
					intersection = default(LineSegment);
					return false;
				}
			}
			else
			{
				var inv = 1 / ray.Direction.X;
				var d1 = (boundingBox.Minimum.X - ray.Position.X) * inv;
				var d2 = (boundingBox.Maximum.X - ray.Position.X) * inv;

				n = Math.Max(n, Math.Min(d1, d2));
				f = Math.Min(f, Math.Max(d1, d2));

				if (n > f || f < 0)
				{
					intersection = default(LineSegment);
					return false;
				}
			}

			if (Float.Near(ray.Direction.Y, 0))
			{
				if (ray.Position.Y < boundingBox.Minimum.Y || ray.Position.Y > boundingBox.Maximum.Y)
				{
					intersection = default(LineSegment);
					return false;
				}
			}
			else
			{
				var inv = 1 / ray.Direction.Y;
				var d1 = (boundingBox.Minimum.Y - ray.Position.Y) * inv;
				var d2 = (boundingBox.Maximum.Y - ray.Position.Y) * inv;

				n = Math.Max(n, Math.Min(d1, d2));
				f = Math.Min(f, Math.Max(d1, d2));

				if (n > f || f < 0)
				{
					intersection = default(LineSegment);
					return false;
				}
			}

			if (Float.Near(ray.Direction.Z, 0))
			{
				if (ray.Position.Z < boundingBox.Minimum.Z || ray.Position.Z > boundingBox.Maximum.Z)
				{
					intersection = default(LineSegment);
					return false;
				}
			}
			else
			{
				var inv = 1 / ray.Direction.Z;
				var d1 = (boundingBox.Minimum.Z - ray.Position.Z) * inv;
				var d2 = (boundingBox.Maximum.Z - ray.Position.Z) * inv;

				n = Math.Max(n, Math.Min(d1, d2));
				f = Math.Min(f, Math.Max(d1, d2));

				if (n > f || f < 0)
				{
					intersection = default(LineSegment);
					return false;
				}
			}

			intersection = new LineSegment
			{
				End1 = ray.Position + ray.Direction * n,
				End2 = ray.Position + ray.Direction * f,
			};

			return true;
		}

		/// <summary>
		/// Determines whether the specified bounding spheres intersect.
		/// </summary>
		/// <param name="boundingSphere1">A bounding sphere.</param>
		/// <param name="boundingSphere2">A bounding sphere.</param>
		/// <returns>True if the <paramref name="boundingSphere1"/> and the <paramref name="boundingSphere2"/> intersect, otherwise false.</returns>
		public static bool BoundingSphereWithBoundingSphere(BoundingSphere boundingSphere1, BoundingSphere boundingSphere2)
		{
			var distance = Distance.PointToPoint(boundingSphere1.Position, boundingSphere2.Position);

			return distance <= boundingSphere1.Radius + boundingSphere2.Radius;
		}

		/// <summary>
		/// Determines whether the specified bounding spheres intersect.
		/// </summary>
		/// <param name="boundingSphere1">A bounding sphere.</param>
		/// <param name="boundingSphere2">A bounding sphere.</param>
		/// <param name="intersection">Output variable for the intersection.</param>
		/// <returns>True if the <paramref name="boundingSphere1"/> and the <paramref name="boundingSphere2"/> intersect, otherwise false.</returns>
		public static bool BoundingSphereWithBoundingSphere(BoundingSphere boundingSphere1, BoundingSphere boundingSphere2, out BoundingSphere intersection)
		{
			var x = boundingSphere2.Position.X - boundingSphere1.Position.X;
			var y = boundingSphere2.Position.Y - boundingSphere1.Position.Y;
			var z = boundingSphere2.Position.Z - boundingSphere1.Position.Z;

			var distance = (Number)Math.Sqrt(x * x + y * y + z * z);

			if (distance > boundingSphere1.Radius + boundingSphere2.Radius)
			{
				intersection = default(BoundingSphere);
				return false;
			}
			else if (distance <= boundingSphere1.Radius - boundingSphere2.Radius)
			{
				intersection = boundingSphere2;
				return true;
			}
			else if (distance <= boundingSphere2.Radius - boundingSphere1.Radius)
			{
				intersection = boundingSphere1;
				return true;
			}
			else
			{
				var radius = (Number)(Math.Sqrt(
					(boundingSphere1.Radius - boundingSphere2.Radius - distance) *
					(boundingSphere2.Radius - boundingSphere1.Radius - distance) *
					(boundingSphere1.Radius + boundingSphere2.Radius - distance) *
					(boundingSphere2.Radius + boundingSphere1.Radius + distance)) / distance);

				var r1 = boundingSphere1.Radius * boundingSphere1.Radius;
				var r2 = boundingSphere2.Radius * boundingSphere2.Radius;
				var d1 = distance * distance;
				var d2 = (d1 + r1 - r2) / (2 * d1);

				intersection.Position.X = boundingSphere1.Position.X + x * d2;
				intersection.Position.Y = boundingSphere1.Position.Y + y * d2;
				intersection.Position.Z = boundingSphere1.Position.Z + z * d2;
				intersection.Radius = radius;
				return true;
			}
		}

		/// <summary>
		/// Determines whether the specified bounding sphere intersects with the specified point.
		/// </summary>
		/// <param name="boundingSphere">A bounding sphere.</param>
		/// <param name="point">A point.</param>
		/// <returns>True if the <paramref name="boundingSphere"/> and the <paramref name="point"/> intersect, otherwise false.</returns>
		public static bool BoundingSphereWithPoint(BoundingSphere boundingSphere, Vector3 point)
		{
			return Distance.PointToPointSquared(point, boundingSphere.Position) <= boundingSphere.Radius * boundingSphere.Radius;
		}

		/// <summary>
		/// Determines whether the specified bounding sphere intersects with the specified ray.
		/// </summary>
		/// <param name="boundingSphere">A bounding sphere.</param>
		/// <param name="ray">A ray.</param>
		/// <returns>True if the <paramref name="boundingSphere"/> and the <paramref name="ray"/> intersect, otherwise false.</returns>
		public static bool BoundingSphereWithRay(BoundingSphere boundingSphere, Ray ray)
		{
			var separation = boundingSphere.Position - ray.Position;
			var lengthSquared = separation.MagnitudeSquared;
			var radiusSquared = boundingSphere.Radius * boundingSphere.Radius;

			if (lengthSquared <= radiusSquared)
			{
				return true;
			}
			else
			{
				var dot = Vector3.Dot(ray.Direction, separation);

				if (dot >= 0)
				{
					var discriminant = lengthSquared - (dot * dot);

					if (discriminant <= radiusSquared)
					{
						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Determines whether the specified bounding sphere intersects with the specified ray.
		/// </summary>
		/// <param name="boundingSphere">A bounding sphere.</param>
		/// <param name="ray">A ray.</param>
		/// <param name="intersection">Output variable for the intersection.</param>
		/// <returns>True if the <paramref name="boundingSphere"/> and the <paramref name="ray"/> intersect, otherwise false.</returns>
		public static bool BoundingSphereWithRay(BoundingSphere boundingSphere, Ray ray, out Vector3 intersection)
		{
			var separation = boundingSphere.Position - ray.Position;
			var lengthSquared = separation.MagnitudeSquared;
			var radiusSquared = boundingSphere.Radius * boundingSphere.Radius;

			if (lengthSquared <= radiusSquared)
			{
				intersection = ray.Position;
				return true;
			}
			else
			{
				var dot = Vector3.Dot(ray.Direction, separation);

				if (dot >= 0)
				{
					var discriminant = lengthSquared - (dot * dot);

					if (discriminant <= radiusSquared)
					{
						var u = dot - (Number)Math.Sqrt(radiusSquared - discriminant);
						intersection = ray.Position + ray.Direction * u;
						return true;
					}
				}
			}

			intersection = default(Vector3);
			return false;
		}

		/// <summary>
		/// Determines whether the specified plane intersects with the specified bounding box.
		/// </summary>
		/// <param name="plane">A plane.</param>
		/// <param name="boundingBox">A bounding box.</param>
		/// <returns>True if the <paramref name="plane"/> and the <paramref name="boundingBox"/> intersect, otherwise false.</returns>
		public static bool PlaneWithBoundingBox(Plane plane, BoundingBox boundingBox)
		{
			var min = new Vector3
			{
				X = plane.A > 0 ? boundingBox.Minimum.X : boundingBox.Maximum.X,
				Y = plane.B > 0 ? boundingBox.Minimum.Y : boundingBox.Maximum.Y,
				Z = plane.C > 0 ? boundingBox.Minimum.Z : boundingBox.Maximum.Z,
			};

			if (Distance.PointToPlane(min, plane) > 0)
			{
				return false;
			}

			var max = new Vector3
			{
				X = plane.A < 0 ? boundingBox.Minimum.X : boundingBox.Maximum.X,
				Y = plane.B < 0 ? boundingBox.Minimum.Y : boundingBox.Maximum.Y,
				Z = plane.C < 0 ? boundingBox.Minimum.Z : boundingBox.Maximum.Z,
			};

			if (Distance.PointToPlane(max, plane) < 0)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Determines whether the specified plane intersects with the specified bounding sphere.
		/// </summary>
		/// <param name="plane">A plane.</param>
		/// <param name="boundingSphere">A bounding sphere.</param>
		/// <returns>True if the <paramref name="plane"/> and the <paramref name="boundingSphere"/> intersect, otherwise false.</returns>
		public static bool PlaneWithBoundingSphere(Plane plane, BoundingSphere boundingSphere)
		{
			var distance =
				plane.A * boundingSphere.Position.X +
				plane.B * boundingSphere.Position.Y +
				plane.C * boundingSphere.Position.Z +
				plane.D;

			return Math.Abs(distance) < boundingSphere.Radius;
		}

		/// <summary>
		/// Determines whether the specified plane intersects with the specified bounding sphere.
		/// </summary>
		/// <param name="plane">A plane.</param>
		/// <param name="boundingSphere">A bounding sphere.</param>
		/// <param name="intersection">Output variable for the intersection.</param>
		/// <returns>True if the <paramref name="plane"/> and the <paramref name="boundingSphere"/> intersect, otherwise false.</returns>
		public static bool PlaneWithBoundingSphere(Plane plane, BoundingSphere boundingSphere, out Vector3 intersection)
		{
			var distance = Distance.PointToPlane(boundingSphere.Position, plane);

			if (Math.Abs(distance) < boundingSphere.Radius)
			{
				intersection = new Vector3
				{
					X = boundingSphere.Position.X - plane.A * distance,
					Y = boundingSphere.Position.Y - plane.B * distance,
					Z = boundingSphere.Position.Z - plane.C * distance,
				};
				return true;
			}
			else
			{
				intersection = default(Vector3);
				return false;
			}
		}

		/// <summary>
		/// Determines whether the specified plane intersects with the specified line.
		/// </summary>
		/// <param name="plane">A plane.</param>
		/// <param name="line">A line.</param>
		/// <returns>True if the <paramref name="plane"/> and the <paramref name="line"/> intersect, otherwise false.</returns>
		public static bool PlaneWithLine(Plane plane, Line line)
		{
			var tangent = Vector3.Normalize(line.Point2 - line.Point1);

			var dot =
				tangent.X * plane.A +
				tangent.Y * plane.B +
				tangent.Z * plane.C;

			return !Float.Near(dot, 0);
		}

		/// <summary>
		/// Determines whether the specified plane intersects with the specified line.
		/// </summary>
		/// <param name="plane">A plane.</param>
		/// <param name="line">A line.</param>
		/// <param name="intersection">Output variable for the intersection.</param>
		/// <returns>True if the <paramref name="plane"/> and the <paramref name="line"/> intersect, otherwise false.</returns>
		public static bool PlaneWithLine(Plane plane, Line line, out Vector3 intersection)
		{
			var tangent = Vector3.Normalize(line.Point2 - line.Point1);

			var dot =
				tangent.X * plane.A +
				tangent.Y * plane.B +
				tangent.Z * plane.C;

			if (!Float.Near(dot, 0))
			{
				var u = (plane.A * line.Point1.X + plane.B * line.Point1.Y + plane.C * line.Point1.Z + plane.D) / -dot;
				intersection = line.Point1 + u * tangent;
				return true;
			}

			intersection = default(Vector3);
			return false;
		}

		/// <summary>
		/// Determines whether the specified plane intersects with the specified line segment.
		/// </summary>
		/// <param name="plane">A plane.</param>
		/// <param name="lineSegment">A line segment.</param>
		/// <returns>True if the <paramref name="plane"/> and the <paramref name="lineSegment"/> intersect, otherwise false.</returns>
		public static bool PlaneWithLineSegment(Plane plane, LineSegment lineSegment)
		{
			var distance1 = Distance.PointToPlane(lineSegment.End1, plane);
			var distance2 = Distance.PointToPlane(lineSegment.End2, plane);
			return Math.Sign(distance1) != Math.Sign(distance2);
		}

		/// <summary>
		/// Determines whether the specified plane intersects with the specified line segment.
		/// </summary>
		/// <param name="plane">A plane.</param>
		/// <param name="lineSegment">A line segment.</param>
		/// <param name="intersection">Output variable for the intersection.</param>
		/// <returns>True if the <paramref name="plane"/> and the <paramref name="lineSegment"/> intersect, otherwise false.</returns>
		public static bool PlaneWithLineSegment(Plane plane, LineSegment lineSegment, out Vector3 intersection)
		{
			var distance1 = Distance.PointToPlane(lineSegment.End1, plane);
			var distance2 = Distance.PointToPlane(lineSegment.End2, plane);

			if (Math.Sign(distance1) != Math.Sign(distance2))
			{
				var tangent = Vector3.Normalize(lineSegment.End2 - lineSegment.End1);

				var dot =
					tangent.X * plane.A +
					tangent.Y * plane.B +
					tangent.Z * plane.C;

				var u = (plane.A * lineSegment.End1.X + plane.B * lineSegment.End1.Y + plane.C * lineSegment.End1.Z + plane.D) / -dot;

				intersection = lineSegment.End1 + u * tangent;
				return true;
			}
			else
			{
				intersection = default(Vector3);
				return false;
			}
		}

		/// <summary>
		/// Determines whether the specified planes intersect.
		/// </summary>
		/// <param name="plane1">A plane.</param>
		/// <param name="plane2">A plane.</param>
		/// <returns>True if the <paramref name="plane1"/> and the <paramref name="plane2"/> intersect, otherwise false.</returns>
		public static bool PlaneWithPlane(Plane plane1, Plane plane2)
		{
			var n1 = plane1.Normal;
			var n2 = plane2.Normal;
			var axis = Vector3.Cross(n1, n2);

			return !Float.Near(axis.MagnitudeSquared, 0);
		}

		/// <summary>
		/// Determines whether the specified planes intersect.
		/// </summary>
		/// <param name="plane1">A plane.</param>
		/// <param name="plane2">A plane.</param>
		/// <param name="intersection">Output variable for the intersection.</param>
		/// <returns>True if the <paramref name="plane1"/> and the <paramref name="plane2"/> intersect, otherwise false.</returns>
		public static bool PlaneWithPlane(Plane plane1, Plane plane2, out Line intersection)
		{
			var n1 = plane1.Normal;
			var n2 = plane2.Normal;
			var axis = Vector3.Cross(n1, n2);

			if (!Float.Near(axis.MagnitudeSquared, 0))
			{
				var dot = Vector3.Dot(n1, n2);
				var det = 1 / (dot * dot - 1);

				n1 *= (plane1.D - plane2.D * dot) * det;
				n2 *= (plane2.D - plane1.D * dot) * det;

				intersection.Point1 = n1 + n2;
				intersection.Point2 = n1 + n2 + axis;
				return true;
			}
			else
			{
				intersection = default(Line);
				return false;
			}
		}

		/// <summary>
		/// Determines whether the specified planes intersect.
		/// </summary>
		/// <param name="plane1">A plane.</param>
		/// <param name="plane2">A plane.</param>
		/// <param name="plane3">A plane.</param>
		/// <returns>True if the <paramref name="plane1"/>, <paramref name="plane2"/> and the <paramref name="plane3"/> intersect, otherwise false.</returns>
		public static bool PlaneWithPlaneWithPlane(Plane plane1, Plane plane2, Plane plane3)
		{
			var n1 = plane1.Normal;
			var n2 = plane2.Normal;
			var n3 = plane3.Normal;

			var c1 = Vector3.Cross(n2, n3);
			var dot = Vector3.Dot(n1, c1);

			return !Float.Near(dot, 0);
		}

		/// <summary>
		/// Determines whether the specified planes intersect.
		/// </summary>
		/// <param name="plane1">A plane.</param>
		/// <param name="plane2">A plane.</param>
		/// <param name="plane3">A plane.</param>
		/// <param name="intersection">Output variable for the intersection.</param>
		/// <returns>True if the <paramref name="plane1"/>, <paramref name="plane2"/> and the <paramref name="plane3"/> intersect, otherwise false.</returns>
		public static bool PlaneWithPlaneWithPlane(Plane plane1, Plane plane2, Plane plane3, out Vector3 intersection)
		{
			var n1 = plane1.Normal;
			var n2 = plane2.Normal;
			var n3 = plane3.Normal;

			var c1 = Vector3.Cross(n2, n3);
			var dot = Vector3.Dot(n1, c1);

			if (!Float.Near(dot, 0))
			{
				var c2 = Vector3.Cross(n3, n1);
				var c3 = Vector3.Cross(n1, n2);
				dot = 1 / -dot;

				intersection = new Vector3
				{
					X = (c1.X * plane1.D + c2.X * plane2.D + c3.X * plane3.D) * dot,
					Y = (c1.Y * plane1.D + c2.Y * plane2.D + c3.Y * plane3.D) * dot,
					Z = (c1.Z * plane1.D + c2.Z * plane2.D + c3.Z * plane3.D) * dot,
				};

				return true;
			}
			else
			{
				intersection = default(Vector3);
				return false;
			}
		}

		/// <summary>
		/// Determines whether the specified plane intersects with the specified ray.
		/// </summary>
		/// <param name="plane">A plane.</param>
		/// <param name="ray">A ray.</param>
		/// <returns>True if the <paramref name="plane"/> and the <paramref name="ray"/> intersect, otherwise false.</returns>
		public static bool PlaneWithRay(Plane plane, Ray ray)
		{
			var dot =
				ray.Direction.X * plane.A +
				ray.Direction.Y * plane.B +
				ray.Direction.Z * plane.C;

			if (!Float.Near(dot, 0))
			{
				var distance = (plane.A * ray.Position.X + plane.B * ray.Position.Y + plane.C * ray.Position.Z + plane.D) / -dot;

				if (distance >= 0)
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Determines whether the specified plane intersects with the specified ray.
		/// </summary>
		/// <param name="plane">A plane.</param>
		/// <param name="ray">A ray.</param>
		/// <param name="intersection">Output variable for the intersection.</param>
		/// <returns>True if the <paramref name="plane"/> and the <paramref name="ray"/> intersect, otherwise false.</returns>
		public static bool PlaneWithRay(Plane plane, Ray ray, out Vector3 intersection)
		{
			var dot =
				ray.Direction.X * plane.A +
				ray.Direction.Y * plane.B +
				ray.Direction.Z * plane.C;

			if (!Float.Near(dot, 0))
			{
				var distance = (plane.A * ray.Position.X + plane.B * ray.Position.Y + plane.C * ray.Position.Z + plane.D) / -dot;

				if (distance >= 0)
				{
					intersection = ray.Position + distance * ray.Direction;
					return true;
				}
			}

			intersection = default(Vector3);
			return false;
		}
	}
}
