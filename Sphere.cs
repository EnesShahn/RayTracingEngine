using System;
using System.Collections.Generic;
using System.Text;

namespace RayCastingEngine
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
			Vector3 oc = ray.Origin - center;
			float a = Vector3.Dot(ray.Direction, ray.Direction);
			float b = 2.0f * Vector3.Dot(ray.Direction, oc);
			float c = Vector3.Dot(oc, oc) - radius * radius;
			float discriminant = b * b - 4 * a * c; // Used to identify if 1 intersection occured(tangent), no intersection or 2 intersection (from outside and inside)
			if(discriminant > 0) //There is intersection
			{
				float t = (- b + (float)Math.Sqrt(discriminant)) / 2*a;
				if (t > 0 && t > tmin && t > hit.TCurrent)
				{
					hit.TCurrent = t;
					hit.Color = this.Color;
					hit.Intersection = true;
				}
			}
		}
	}
}
