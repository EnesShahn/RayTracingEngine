using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	abstract class Camera
	{
		public abstract Ray GenerateRay(Vector2 pixelPosition);
	}
}
