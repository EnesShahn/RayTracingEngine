using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	class Light : Component
	{
		private Vector3 direction = new Vector3(0, 0, 1);
		private Color01 color;

		public Vector3 Direction {
			get { return direction; }
		}
		public Color01 Color {
			get { return color; }
		}
		public void Init(Vector3 direction, Color01 color)
		{
			this.direction = direction;
			this.color = color;
		}
	}
}
