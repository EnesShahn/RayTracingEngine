using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Drawing.Imaging;

namespace SimpleRayTracingEngine
{
	class Renderer
	{
		private static Vector2Int resolution = new Vector2Int(500, 500);

		public static void RenderToImage(string fileName, Scene scene)
		{
			Bitmap bmp = new Bitmap(resolution.x, resolution.y);

			SetBGColor(bmp, scene.Background);

			for (int y = 0; y <= resolution.y; y++)
			{
				for (int x = 0; x <= resolution.x; x++)
				{
					Vector2 pos = new Vector2((float)x / resolution.x, (float)y / resolution.y);
					Ray ray = scene.MainCamera.GetComponent<Camera>().GenerateRay(pos);
					Hit hit = new Hit();
					for (int i = 0; i < scene.root.ChildCount(); i++)
					{
						if(scene.root.GetChild(i).HasComponent<Mesh>())
							scene.root.GetChild(i).GetComponent<Mesh>().Intersect(ray, hit, 0f);
					} 
					if (hit.Intersection)
					{
						Light light = scene.MainLight.GetComponent<Light>();
						Color01 finalColor = (scene.ambient * hit.Color) + Math.Max(Vector3.Dot(light.Direction, hit.Normal), 0) * (hit.Color * light.Color);
						bmp.SetPixel(y, x, Color.FromArgb((int)(finalColor.R* 255), (int)(finalColor.G * 255), (int)(finalColor.B * 255)));
					}
				}
			}

			bmp.Save(fileName, ImageFormat.Png);
		}
		public static void RenderDepthToImage(string fileName, Scene scene, float near, float far)
		{
			Bitmap bmp = new Bitmap(resolution.x, resolution.y);

			SetBGColor(bmp, new Color01(0, 0, 0));

			for (int y = 0; y <= resolution.y; y++)
			{
				for (int x = 0; x <= resolution.x; x++)
				{
					Vector2 pos = new Vector2((float)x / resolution.x, (float)y / resolution.y);
					Ray ray = scene.MainCamera.GetComponent<Camera>().GenerateRay(pos);
					Hit hit = new Hit();
					for (int i = 0; i < scene.root.ChildCount(); i++)
					{
						if (scene.root.GetChild(i).HasComponent<Mesh>())
							scene.root.GetChild(i).GetComponent<Mesh>().Intersect(ray, hit, 0f);
					}
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

		public static void SetBGColor(Bitmap bmp, Color01 color)
		{
			for (int y = 0; y < resolution.y; y++)
			{
				for (int x = 0; x < resolution.x; x++)
				{
					bmp.SetPixel(x, y, Color.FromArgb((int)(color.R * 255), (int)(color.G * 255), (int)(color.B * 255)));
				}
			}
		}

	}
}
