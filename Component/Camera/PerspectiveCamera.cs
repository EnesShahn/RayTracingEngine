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
			//Vector3 horizontal = (pixelPosition.X - 0.5f) * size * right;
			//Vector3 vertical = (pixelPosition.Y - 0.5f) * size * up;
			//Vector3 rayOrigin = object3D.position + horizontal + vertical;
			//return new Ray(rayOrigin, direction);
			//Vector3 w_p = (-width / 2) * u + (height / 2) * v - ((height / 2) / tan(fov_rad * 0.5);
			//Vector3 ray_dir = normalize(x * u + y * (-v) + w_p);

			throw new NotImplementedException();
		}
	}
}
