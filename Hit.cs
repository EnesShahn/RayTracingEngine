using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	class Hit
	{
		private float tCurrent = float.MaxValue;
		private Color01 color;
		private bool intersection;
		private Vector3 normal;

		public float TCurrent 
		{
			get { return tCurrent; }
			set { tCurrent = value; }
		}
		public Color01 Color
		{
			get { return color; }
			set { color = value; }
		}
		public Vector3 Normal {
			get { return normal; }
			set { normal = value; }
		}

		public bool Intersection {
			get { return intersection; }
			set { intersection = value; }
		}

	}
}
