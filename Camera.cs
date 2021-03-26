using System;
using System.Collections.Generic;
using System.Text;

namespace RayCastingEngine
{
	abstract class Camera
	{
		public abstract Ray GenerateRay(Vector2 pixelPosition);
	}
}
