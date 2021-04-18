using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	sealed class Triangle : Mesh
	{
		private Vector3 v1, v2, v3;

		public Triangle(Vector3 v1, Vector3 v2, Vector3 v3, Color01 color) : base(color)
		{
			this.v1 = v1;
			this.v2 = v2;
			this.v3 = v3;
		}

		public override void Intersect(Ray ray, Hit hit, float tmin)
		{
			throw new NotImplementedException();
		}
	}
}
