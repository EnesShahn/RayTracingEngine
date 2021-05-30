using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	static class Settings
	{
		public static Vector2Int IMAGE_RESOLUTION = new Vector2Int(500, 500);
		public static float aspectRatio;
		public static int MAX_BOUNCE = 3;

		static Settings()
		{
			aspectRatio = (float)IMAGE_RESOLUTION.x / IMAGE_RESOLUTION.y;
		}

	}
}
