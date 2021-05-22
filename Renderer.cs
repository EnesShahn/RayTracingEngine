using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace SimpleRayTracingEngine
{
	class Renderer
	{
		public static void RenderToImage(string outputFolder, string sceneName, Scene scene, float near, float far)
		{
			Console.WriteLine($"Rendering \"{sceneName}\" ...");
			Stopwatch wt = new Stopwatch();
			wt.Start();
			Bitmap bmp = new Bitmap(Settings.IMAGE_RESOLUTION.x, Settings.IMAGE_RESOLUTION.y);
			Bitmap bmpDepth = new Bitmap(Settings.IMAGE_RESOLUTION.x, Settings.IMAGE_RESOLUTION.y);
			
			SetBGColor(bmp, scene.Background);
			SetBGColor(bmpDepth, new Color01(0, 0, 0));

			//Multipication faster than division so we cache it instead of dividing in the loop
			double oneDivResX = 1f / Settings.IMAGE_RESOLUTION.x;
			double oneDivResY = 1f / Settings.IMAGE_RESOLUTION.y;

			Camera mainCamera = scene.MainCamera.GetComponent<Camera>();
			Light light = scene.MainLight.GetComponent<Light>();

			for (int y = 0; y < Settings.IMAGE_RESOLUTION.y; y++)
			{
				for (int x = 0; x < Settings.IMAGE_RESOLUTION.x; x++)
				{
					//screenCoord goes from 0,0 to 1,1 (Screen Coord) x, y = 0 : lower left corner, = 1 : top right corner.
					Vector2 screenCoord = new Vector2((float)(x * oneDivResX), (float)(y * oneDivResY));
					//Console.WriteLine(screenCoord);
					Ray ray = mainCamera.GenerateRay(screenCoord);
					Hit hit = new Hit();
					
					for (int i = 0; i < scene.root.ChildCount(); i++)
					{
						if(scene.root.GetChild(i).HasComponent<Mesh>())
							scene.root.GetChild(i).GetComponent<Mesh>().Intersect(ray, hit, 0f);
					}
					
					if (hit.Intersection)
					{
						float intensity = Vector3.Dot(-light.Direction, hit.Normal);
						Color01 fColor = scene.ambient * hit.Color + Math.Max(intensity, 0) * (hit.Color * light.Color);

						//Depth Texture
						float depth = (far - hit.TCurrent) / (far - near);
						int depthRGB = Mathf.Clamp(0, 255, (int)((depth) * 255));

						// Bitmap sets pixels from top left corner to lower right corner, thats why we need to invert the y or else image will be flipped on the x axis
						bmp.SetPixel(x, Settings.IMAGE_RESOLUTION.y - y - 1, Color.FromArgb((int)(fColor.R * 255), (int)(fColor.G * 255), (int)(fColor.B * 255)));
						bmpDepth.SetPixel(x, Settings.IMAGE_RESOLUTION.y - y - 1, Color.FromArgb(depthRGB, depthRGB, depthRGB));
					}
				}
			}

			bmp.Save($"{outputFolder}/{sceneName}.png", ImageFormat.Png);
			bmpDepth.Save($"{outputFolder}/{sceneName}_depth.png", ImageFormat.Png);

			wt.Stop();
			Console.WriteLine($"Finished Rendering \"{sceneName}\", it took {wt.ElapsedMilliseconds / 1000f} seconds.");
		}
		public static void SetBGColor(Bitmap bmp, Color01 color)
		{
			for (int y = 0; y < Settings.IMAGE_RESOLUTION.y; y++)
			{
				for (int x = 0; x < Settings.IMAGE_RESOLUTION.x; x++)
				{
					bmp.SetPixel(x, y, Color.FromArgb((int)(color.R * 255), (int)(color.G * 255), (int)(color.B * 255)));
				}
			}
		}

		public static void DebugSampleScene(Vector2 screenCoord, Scene scene, float near, float far)
		{
			Console.WriteLine($"Sampling {screenCoord}");
			Camera mainCamera = scene.MainCamera.GetComponent<Camera>();
			Light light = scene.MainLight.GetComponent<Light>();
			Ray ray = mainCamera.GenerateRay(screenCoord);
			Hit hit = new Hit();

			for (int i = 0; i < scene.root.ChildCount(); i++)
			{
				if (scene.root.GetChild(i).HasComponent<Mesh>())
					scene.root.GetChild(i).GetComponent<Mesh>().Intersect(ray, hit, 0f);
			}

			if (hit.Intersection)
			{
				Console.WriteLine("Intersection");
				float intensity = Vector3.Dot(-light.Direction, hit.Normal);
				Color01 fColor = scene.ambient * hit.Color + Math.Max(intensity, 0) * (hit.Color * light.Color);

				//Depth Texture
				float depth = (far - hit.TCurrent) / (far - near);
				int depthRGB = Mathf.Clamp(0, 255, (int)((depth) * 255));

				Console.WriteLine($"INTENSITY {intensity}, NORMAL {hit.Normal}, LIGHT_DIRECTION {light.Direction}");
				Console.WriteLine($"COLOR LIB {Color.FromArgb((int)(fColor.R * 255), (int)(fColor.G * 255), (int)(fColor.B * 255))}, DEPTH {depth}");
				Console.WriteLine($"COLOR {fColor}, DEPTH {depth}");
			}
			else
			{
				Console.WriteLine("NO Intersection");
			}
			Console.WriteLine();
		}

	}
}
