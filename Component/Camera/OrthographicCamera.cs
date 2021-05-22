using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	sealed class OrthographicCamera : Camera
	{
		private Vector3 direction = new Vector3(0, 0, 1);
		private Vector3 horizontal = new Vector3(1, 0, 0);
		private Vector3 vertical = new Vector3(0, 1, 0);
		private Vector3 lowerLeftCorner;
		public void Init(Vector3 direction, Vector3 up, int size)
		{
			this.direction = direction;
			//Just for error preventation as no much performance cost we normalize:
			Vector3 right = Vector3.CrossProduct(direction, up).Normalized;
			vertical = up * size;
			horizontal = right * size * Settings.aspectRatio;
			lowerLeftCorner = object3D.position - horizontal / 2f - vertical / 2f;
		}

		/// <summary>
		/// Generates Ray that has position interpolated from the lower left corner to the top right corner of screen coordinate and a constant direction
		/// </summary>
		/// <param name="screenCoord"> Goes form [0,0] (Lower Left Corner) to [1,1] (Top Right Corner)</param>
		/// <returns></returns>
		public override Ray GenerateRay(Vector2 screenCoord) //TODO: Use Lower Left Corner and Interpolate
		{
			Vector3 rayOrigin = lowerLeftCorner + (horizontal * screenCoord.x) + (vertical * screenCoord.y);
			return new Ray(rayOrigin, direction);
		}

	}
}
