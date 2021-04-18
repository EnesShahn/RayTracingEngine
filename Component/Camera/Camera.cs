using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	abstract class Camera : Component
	{
		public abstract Ray GenerateRay(Vector2 pixelPosition);
	}
}
