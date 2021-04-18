using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace SimpleRayTracingEngine
{
	class Program
	{
		
		static int Main(string[] args)
		{
			Console.WriteLine("Rendering");

			Scene scene1 = ReadSceneData("Resources/scene1_diffuse.json");
			Renderer.RenderToImage("Output/scene1.png", scene1);
			Renderer.RenderDepthToImage("Output/scene1_depth.png", scene1, 8, 11.5f);

			Scene scene2 = ReadSceneData("Resources/scene2_ambient.json");
			Renderer.RenderToImage("Output/scene2.png", scene2);
			Renderer.RenderDepthToImage("Output/scene2_depth.png", scene2, 8, 11.5f);

			//Scene scene3 = ReadSceneData("Resources/scene3_perspective.json");
			//Renderer.RenderToImage("Output/scene3.png", scene3);
			//Renderer.RenderDepthToImage("Output/scene3_depth.png", scene3, 8, 11.5f);

			//Scene scene4 = ReadSceneData("Resources/scene4_plane.json");
			//Renderer.RenderToImage("Output/scene4.png", scene4);
			//Renderer.RenderDepthToImage("Output/scene4_depth.png", scene4, 8, 11.5f);

			//Scene scene5 = ReadSceneData("Resources/scene44_sphere_triangle.json");
			//Renderer.RenderToImage("Output/scene4.png", scene5);
			//Renderer.RenderDepthToImage("Output/scene4_depth.png", scene5, 8, 11.5f);


			Console.WriteLine("Finished Rendering!");
			return 0;
		}

		static Scene ReadSceneData(string sceneFileJson)
		{
			string json;
			using (StreamReader r = new StreamReader(sceneFileJson))
			{
				json = r.ReadToEnd();
			}

			Scene scene = new Scene();

			JObject jsonDecoded = JObject.Parse(json);

			Color01 bg = Color01.FromArray(jsonDecoded["background"]["color"].ToObject<List<float>>().ToArray());
			Color01 ambient = Color01.FromArray(jsonDecoded["background"]["ambient"].ToObject<List<float>>().ToArray());
			scene.Background = bg;
			scene.ambient = ambient;

			#region Object3D Parsing
			JToken groupO = jsonDecoded["group"];
			Object3D root = new Object3D();
			scene.root = root;
			foreach (var item in groupO)
			{
				Object3D newObject3D = new Object3D();
				Vector3 position = Vector3.Zero;
				Vector3 rotation = Vector3.Zero;
				Vector3 scale = Vector3.Zero;

				if (item["sphere"] != null)
				{
					position = ParseVector3(item["sphere"]["center"]);
					float radius = (float)item["sphere"]["radius"];
					Color01 col = ParseColor32(item["sphere"]["color"]);
					Sphere sphereMesh = new Sphere(radius, col);
					newObject3D.AddComponent(sphereMesh);
					Console.Write($"Created Sphere \nPosition: {position}\nRotation: {rotation}\nScale: {scale}\nRadius: {radius}\nColor: {col}\n\n");
				}
				else if (item["plane"] != null)
				{
					Console.Write("Plane");
					float offset = (float)item["plane"]["offset"];
					position = new Vector3(0, 0, offset);
					Vector3 normal = ParseVector3(item["plane"]["normal"]);
					Color01 col = ParseColor32(item["plane"]["color"]);
					Plane planeMesh = new Plane(normal, col);
					newObject3D.AddComponent(planeMesh);
					Console.Write($"Created Plane \nPosition: {position}\nRotation: {rotation}\nScale: {scale}\nNormal: {normal}\nColor: {col}\n\n");

				}
				else if (item["triangle"] != null)
				{
					Console.Write("Triangle");
					Vector3 v1 = ParseVector3(item["triangle"]["v1"]);
					Vector3 v2 = ParseVector3(item["triangle"]["v2"]);
					Vector3 v3 = ParseVector3(item["triangle"]["v3"]);
					Color01 col = ParseColor32(item["triangle"]["color"]);
					Triangle triangleMesh = new Triangle(v1, v2, v3, col);
					newObject3D.AddComponent(triangleMesh);
					Console.Write($"Created Triangle \nPosition: {position}\nRotation: {rotation}\nScale: {scale}\nV1: {v1}\nV2: {v2}\nV3: {v3}\nColor: {col}\n\n");

				}

				newObject3D.position = position;
				newObject3D.rotation = rotation;
				newObject3D.scale = scale;
				root.AddChild(newObject3D);
			}
			#endregion

			#region Camera Parsing
			if(jsonDecoded["orthocamera"] != null)
			{
				JToken cameraO = jsonDecoded["orthocamera"];

				Vector3 camPos = ParseVector3(cameraO["center"]);
				Vector3 camDirection = ParseVector3(cameraO["direction"]);
				Vector3 camUpDirection = ParseVector3(cameraO["up"]);
				int size = (int)cameraO["size"];

				Object3D cameraObject3D = new Object3D() { position = camPos };
				Camera camera = new OrthographicCamera(camDirection, camUpDirection, size);
				cameraObject3D.AddComponent(camera);

				root.AddChild(cameraObject3D);
				scene.MainCamera = cameraObject3D;
				Console.Write($"Created Orthographic Camera \nPosition: {camPos}\nRotation: {Vector3.Zero}\nScale: {Vector3.One}\nDirection: {camDirection}\nUp: {camUpDirection}\nSize: {size}\n\n");
			}
			else if(jsonDecoded["perspectivecamera"] != null)
			{
				JToken cameraO = jsonDecoded["perspectivecamera"];

				Vector3 camPos = ParseVector3(cameraO["center"]);
				Vector3 camDirection = ParseVector3(cameraO["direction"]);
				Vector3 camUpDirection = ParseVector3(cameraO["up"]);
				int fov = (int)cameraO["angle"];

				Object3D cameraObject3D = new Object3D() { position = camPos };
				Camera camera = new PerspectiveCamera(camDirection, camUpDirection, fov);
				cameraObject3D.AddComponent(camera);

				root.AddChild(cameraObject3D);
				scene.MainCamera = cameraObject3D;
				Console.Write($"Created Perspective Camera \nPosition: {camPos}\nRotation: {Vector3.Zero}\nScale: {Vector3.One}\nDirection: {camDirection}\nUp: {camUpDirection}\nFOV: {fov}\n\n");

			}

			#endregion

			#region Light Parsing
			//If light data exists then load it and add it to the scene.
			if (jsonDecoded["light"] != null)
			{
				Vector3 direction = ParseVector3(jsonDecoded["light"]["direction"]);
				Color01 color = ParseColor32(jsonDecoded["light"]["color"]);
				Object3D lightObject = new Object3D();
				Light light = new Light(direction, color);
				lightObject.AddComponent(light);
				root.AddChild(lightObject);
				scene.MainLight = lightObject;
				Console.Write($"Created Light \nPosition: {Vector3.Zero}\nRotation: {Vector3.Zero}\nScale: {Vector3.One}\nDirection: {direction}\nColor: {color}\n\n");
			}
			#endregion

			return scene;
		}
		
		public static Vector3 ParseVector3(JToken token)
		{
			return Vector3.FromArray(token.ToObject<float[]>());
		}
		public static Color01 ParseColor32(JToken token)
		{
			return Color01.FromArray(token.ToObject<float[]>());
		}
	
	}
}
