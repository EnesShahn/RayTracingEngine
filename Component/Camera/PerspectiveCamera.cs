using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	class PerspectiveCamera : Camera
	{
		private Vector3 up;
		private Vector3 direction;
		private Vector3 right;
		private float fov = 5;

		public PerspectiveCamera(Vector3 direction, Vector3 up, int fov)
		{
			this.direction = direction;
			this.up = up;
			this.fov = fov;
			right = Vector3.CrossProduct(up, direction);
		}

		public override Ray GenerateRay(Vector2 pixelPosition)
		{
			//TODO: 
			throw new NotImplementedException();
		}
	}
}
