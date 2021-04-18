using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	class Light : Component
	{
		private Vector3 direction;
		private Color01 color;

		public Vector3 Direction {
			get { return direction; }
		}
		public Color01 Color {
			get { return color; }
		}
		public Light(Vector3 direction, Color01 color)
		{
			this.direction = direction;
			this.color = color;
		}
	}
}
