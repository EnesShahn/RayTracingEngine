using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	sealed class Plane : Mesh
	{

		private Vector3 normal;

		public Plane(Vector3 normal, Color01 color) : base(color)
		{
			this.normal = normal;
		}

		public override void Intersect(Ray ray, Hit hit, float tmin)
		{
			float denom = Vector3.Dot(ray.Direction, normal);
			float t = tmin;
			if(denom > 1e-6)
			{
				Vector3 p = ray.Origin - object3D.position;
				t = Vector3.Dot(p, normal);
			}

			if(t >= 0 && t > tmin && t < hit.TCurrent)
			{
				hit.TCurrent = t;
				hit.Color = color;
				hit.Intersection = true;
			}
		}
	}
}
