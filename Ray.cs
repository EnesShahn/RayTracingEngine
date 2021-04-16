using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	class Ray
	{

		private Vector3 origin;
		private Vector3 direction;

		public Vector3 Origin {
			get { return origin; }
		}
		public Vector3 Direction {
			get { return direction; }
		}

		public Ray(Vector3 origin, Vector3 direction)
		{
			this.origin = origin;
			this.direction = direction;
		}
	}
}
