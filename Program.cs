using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace RayCastingEngine
{
	class Program
	{
		private static Vector2Int resolution = new Vector2Int(2000, 2000);
		static void Main(string[] args)
		{
			Console.WriteLine("Rendering");
			Scene scene1 = ReadSceneData("Data/scene1.json");
			Renderer.RenderToImage("Output/scene1.png",  scene1, resolution);
			Renderer.RenderDepthToImage("Output/scene1_depth.png",  scene1, resolution, 9, 11);

			Scene scene2 = ReadSceneData("Data/scene2.json");
			Renderer.RenderToImage("Output/scene2.png", scene2, resolution);
			Renderer.RenderDepthToImage("Output/scene2_depth.png", scene2, resolution, 8, 11.5f);

			Console.WriteLine("Hello World!");
		}

		static Scene ReadSceneData(string sceneFileJson)
		{
			string json;
			using (StreamReader r = new StreamReader(sceneFileJson))
			{
				json = r.ReadToEnd();
			}

			JObject o = JObject.Parse(json);

			Color32 bg = Color32.FromArray(o["background"]["color"].ToObject<List<byte>>().ToArray());

			JArray groupO = JArray.Parse(o["group"].ToString());
			Group group = new Group();
			foreach (var item in groupO)
			{
				Sphere newObj = new Sphere(
					Vector3.FromArray(item["sphere"]["center"].ToObject<List<float>>().ToArray()),
					(float)item["sphere"]["radius"],
					Color32.FromArray(item["sphere"]["color"].ToObject<List<byte>>().ToArray())
				);
				group.AddObject3D(newObj);
			}

			JObject cameraO = JObject.Parse(o["orthocamera"].ToString());
			Camera camera = new OrthographicCamera(
				Vector3.FromArray(cameraO["center"].ToObject<List<float>>().ToArray()),
				Vector3.FromArray(cameraO["direction"].ToObject<List<float>>().ToArray()),
				Vector3.FromArray(cameraO["up"].ToObject<List<float>>().ToArray()),
				(int)cameraO["size"]
			);

			//Console.WriteLine(cameraO);

			Scene scene = new Scene();
			scene.group = group;
			scene.Background = bg;
			scene.camera = camera;

			//Console.WriteLine();
			//Console.WriteLine(json);

			return scene;
		}
	}
}
