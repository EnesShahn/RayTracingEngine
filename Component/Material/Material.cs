using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	abstract class Material
	{
		public Color01 diffuseColor = new Color01(0, 0, 0, 1);
		public Color01 reflectiveColor = new Color01(0, 0, 0, 1);
		public Color01 transparentColor = new Color01(0, 0, 0, 1);
		public float indexOfRefraction = 1f;

		public abstract Color01 Shade(Vector3 rayDir, Light l, Vector3 normal, Vector3 point);
	}
}
