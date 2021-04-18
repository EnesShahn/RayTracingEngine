using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	class OrthographicCamera : Camera
	{
		private Vector3 up;
		private Vector3 direction;
		private Vector3 right;

		private float size;

		public OrthographicCamera(Vector3 direction, Vector3 up, int size)
		{
			this.direction = direction;
			this.up = up;
			this.size = size;
			right = Vector3.CrossProduct(up, direction);
		}

		public override Ray GenerateRay(Vector2 pixelPosition)
		{
			Vector3 horizontal = (pixelPosition.X - 0.5f) * size * right;
			Vector3 vertical = (pixelPosition.Y - 0.5f) * size * up ;
			Vector3 rayOrigin = object3D.position + horizontal + vertical;
			return new Ray(rayOrigin, direction);
		}

	}
}
