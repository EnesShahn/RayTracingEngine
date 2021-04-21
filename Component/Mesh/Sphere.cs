using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	sealed class Sphere : Mesh
	{
		private float radius;
		private float radius2; // Cache to improve speed

		public Sphere(float radius, Color01 color) : base(color)
		{
			this.radius = radius;
			radius2 = radius * radius;
		}

		public override void Intersect(Ray ray, Hit hit, float tmin)
		{
			Vector3 L = object3D.position - ray.Origin;
			if (L.SqrMagnitude < radius2) // Ray inside sphere (Square both sides to get rid of the Square root = Better Performance)
				return;
			float tca = Vector3.Dot(L, ray.Direction);
			if (tca < 0) // Sphere behind the origin of the ray
				return;
			float d2 = Vector3.Dot(L, L) - tca * tca;
			if (d2 > radius || d2 < 0)
				return;

			float thc = (float)Math.Sqrt(radius2 - d2);
			float t0 = tca - thc;
			float t1 = tca + thc; // Second intersection

			if (t0 > 0 && t0 > tmin && t0 < hit.TCurrent)
			{
				hit.TCurrent = t0;
				hit.Color = color;
				hit.Intersection = true;
				Vector3 p0 = ray.Origin + ray.Direction * t0;
				hit.Normal = (object3D.position - p0).Normalized;
			}
		}
	}
}
