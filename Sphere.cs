using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	class Sphere : Object3D
	{
		private float radius;
		private Vector3 center;

		public Sphere(Vector3 center, float radius, Color32 color) : base(color)
		{
			this.center = center;
			this.radius = radius;
		}

		public override void Intersect(Ray ray, Hit hit, float tmin)
		{
			Vector3 L = center - ray.Origin;
			if (L.Magnitude < radius) // Ray inside sphere
				return;
			float tca = Vector3.Dot(L, ray.Direction);
			if(tca < 0) // Sphere behind the origin of the ray
				return;
			float d2 = Vector3.Dot(L, L) - tca * tca;
			if (d2 > radius || d2 < 0)
				return;

			float thc = (float)Math.Sqrt(radius * radius - d2);
			float t0 = tca - thc;
			float t1 = tca + thc; // Second intersection

			if(t0 > 0 && t0 > tmin && t0 < hit.TCurrent)
			{
				hit.TCurrent = t0;
				hit.Color = this.Color;
				hit.Intersection = true;
			}
		}
	}
}
