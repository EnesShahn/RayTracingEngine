using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	class DirectionalLight : Light
	{
		private Vector3 direction = new Vector3(0, 0, 1);
		private float intensity = 1f;

		public override Vector3 GetDirection()
		{
			return direction;
		}

		public override float GetIntensity()
		{
			return intensity;
		}

		public void Init(Vector3 direction, Color01 color)
		{
			this.direction = direction;
			this.color = color;
		}
	}
}
