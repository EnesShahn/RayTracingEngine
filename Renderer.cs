using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Drawing.Imaging;

namespace SimpleRayTracingEngine
{
	class Renderer
	{
		public static void RenderToImage(string fileName, Scene scene, Vector2Int resolution)
		{
			Bitmap bmp = new Bitmap(resolution.X, resolution.Y);

			for (int y = 0; y < resolution.Y; y++)
			{
				for (int x = 0; x < resolution.X; x++)
				{
					bmp.SetPixel(x, y, Color.FromArgb(scene.Background.R, scene.Background.G, scene.Background.B));
				}
			}

			for (int y = 0; y <= resolution.Y; y++)
			{
				for (int x = 0; x <= resolution.X; x++)
				{
					Vector2 pos = new Vector2((float)x / resolution.X, (float)y / resolution.Y);
					Ray ray = scene.camera.GenerateRay(pos);
					Hit hit = new Hit();
					scene.group.Intersect(ray, hit, 0f);
					if (hit.Intersection)
					{
						bmp.SetPixel(y, x, Color.FromArgb(hit.Color.R, hit.Color.G, hit.Color.B));
					}
				}
			}

			bmp.Save(fileName, ImageFormat.Png);
		}
		public static void RenderDepthToImage(string fileName, Scene scene, Vector2Int resolution, float near, float far)
		{
			Bitmap bmp = new Bitmap(resolution.X, resolution.Y);

			for (int y = 0; y < resolution.Y; y++)
			{
				for (int x = 0; x < resolution.X; x++)
				{
					bmp.SetPixel(x, y, Color.FromArgb(scene.Background.R, scene.Background.G, scene.Background.B));
				}
			}

			for (int y = 0; y <= resolution.Y; y++)
			{
				for (int x = 0; x <= resolution.X; x++)
				{
					Vector2 pos = new Vector2((float)x / resolution.X, (float)y / resolution.Y);
					Ray ray = scene.camera.GenerateRay(pos);
					Hit hit = new Hit();
					scene.group.Intersect(ray, hit, 0f);
					if (hit.Intersection)
					{
						float depth = (far - hit.TCurrent) / (far - near);
						int depthRGB = (int)((depth) * 255);
						if (depthRGB < 0)
							depthRGB = 0;
						if (depthRGB > 255)
							depthRGB = 255;
						bmp.SetPixel(x, y, Color.FromArgb(depthRGB, depthRGB, depthRGB));
					}
				}
			}

			bmp.Save(fileName, ImageFormat.Png);
		}
	}
}
