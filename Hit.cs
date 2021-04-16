﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	class Hit
	{
		private float tCurrent = int.MaxValue;
		private Color32 color;
		private bool intersection;

		public float TCurrent 
		{
			get { return tCurrent; }
			set { tCurrent = value; }
		}
		public Color32 Color
		{
			get { return color; }
			set { color = value; }
		}
		public bool Intersection {
			get { return intersection; }
			set { intersection = value; }
		}

	}
}
