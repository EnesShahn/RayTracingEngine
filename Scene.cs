using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	class Scene
	{
		public Object3D root { get; set; }
		public Object3D MainCamera { get; set; }

		public Color01 Background { get; set; }
		public Color01 ambientLightColor { get; set; }

	}
}
