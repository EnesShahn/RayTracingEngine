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
		private float size = 5;

		public void Init(Vector3 direction, int size)
		{
			this.direction = direction;
			Vector3 tmpUp = new Vector3(0, 1, 0);
			this.horizontal = Vector3.CrossProduct(tmpUp, direction);
			this.vertical = Vector3.CrossProduct(direction, horizontal);
			this.size = size;
		}

		/// <summary>
		/// Generates Ray that has position interpolated from the lower left corner to the top right corner of screen coordinate and a constant direction
		/// </summary>
		/// <param name="screenCoord"> Goes form [0,0] (Lower Left Corner) to [1,1] (Top Right Corner)</param>
		/// <returns></returns>
		public override Ray GenerateRay(Vector2 screenCoord) //TODO: Use Lower Left Corner and Interpolate
		{
			Vector3 horizontalLerped = (screenCoord.x - 0.5f) * size * horizontal;
			Vector3 verticalLerped = (screenCoord.y - 0.5f) * size * vertical;
			Vector3 rayOrigin = object3D.position + horizontalLerped + verticalLerped;
			return new Ray(rayOrigin, direction);
		}

	}
}
