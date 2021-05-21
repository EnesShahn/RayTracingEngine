using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	sealed class Triangle : Mesh
	{
		private Vector3 v0 = Vector3.Zero;
		private Vector3 v1 = Vector3.Zero;
		private Vector3 v2 = Vector3.Zero;

		public void Init(Vector3 v0, Vector3 v1, Vector3 v2, Color01 color)
		{
			this.v0 = v0;
			this.v1 = v1;
			this.v2 = v2;
			this.color = color;
		}

		public override void Intersect(Ray ray, Hit hit, float tmin)
		{
			Vector3 v0v1 = v1 - v0;
			Vector3 v0v2 = v2 - v0;
			Vector3 pvec = Vector3.CrossProduct(ray.Direction, v0v2);
			float det = Vector3.Dot(v0v1, pvec);
			
			// if the determinant is negative the triangle is backfacing
			// if the determinant is close to 0, the ray misses the triangle
			if (det < float.Epsilon) return;
			
			// ray and triangle are parallel if det is close to 0
			
			if (MathF.Abs(det) < float.Epsilon) return;
			
			float invDet = 1 / det;

			Vector3 tvec = ray.Origin - v0;
			float u = Vector3.Dot(tvec, pvec) * invDet;
			if (u < 0 || u > 1) return;

			Vector3 qvec = Vector3.CrossProduct(tvec, v0v1);
			float v = Vector3.Dot(ray.Direction, qvec) * invDet;
			if (v < 0 || u + v > 1) return;

			float t = Vector3.Dot(v0v2, qvec) * invDet;


			if (t > 0 && t > tmin && t < hit.TCurrent)
			{
				hit.Color = this.color;
				hit.Intersection = true;
				hit.TCurrent = t;
				hit.Normal = Vector3.CrossProduct(v0v1, v0v2);
				//hit.Normal = Vector3.CrossProduct(v0v1, v0v2);
			}
		}
	}
}
