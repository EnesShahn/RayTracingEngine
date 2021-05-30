using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	sealed class Plane : Mesh
	{
		private Vector3 normal;

		public void Init(Vector3 normal, Material material)
		{
			this.normal = normal;
			this.material = material;
		}

		public override void Intersect(Ray ray, Hit hit, float tmin)
		{
			float denom = Vector3.Dot(normal, ray.Direction);
			if (denom >= -1e-6) // That means either the ray is parallel to the plane's surface or looking from behing it, so we ignore it 
				return;
			
			float t = -Vector3.Dot(normal, ray.Origin - object3D.position ) / denom;

			if (t >= 0 && t > tmin && t < hit.TCurrent)
			{
				hit.Intersection = true;
				hit.Material = material;
				hit.TCurrent = t;
				hit.Normal = normal;
				hit.Point = ray.Origin + ray.Direction * t;
				hit.ID = object3D.id;
			}
		}
	}
}
