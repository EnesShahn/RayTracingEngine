using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	static class Settings
	{
		public static Vector2Int IMAGE_RESOLUTION = new Vector2Int(1280, 720);
		public static float aspectRatio;

		static Settings()
		{
			aspectRatio = (float)IMAGE_RESOLUTION.x / IMAGE_RESOLUTION.y;
		}

	}
}
