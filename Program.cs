using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;

namespace SimpleRayTracingEngine
{
	class Program
	{
		
		static int Main(string[] args)
		{
			Console.WriteLine("Rendering");

			Scene scene1 = ReadSceneData("Resources/scene1_diffuse.json");
			Renderer.RenderToImage("Output", "scene1", scene1, 8, 11.5f);

			Scene scene2 = ReadSceneData("Resources/scene2_ambient.json");
			Renderer.RenderToImage("Output", "scene2", scene2, 8, 11.5f);

			Scene scene3 = ReadSceneData("Resources/scene3_perspective.json");
			Renderer.RenderToImage("Output", "scene3", scene3, 8, 11.5f);

			Scene scene4 = ReadSceneData("Resources/scene4_plane.json");
			Renderer.RenderToImage("Output", "scene4", scene4, 8, 11.5f);

			Scene scene5 = ReadSceneData("Resources/scene5_sphere_triangle.json");
			Renderer.RenderToImage("Output", "scene5", scene5, 8, 11.5f);

			Scene scene6 = ReadSceneData("Resources/scene6_squashed_sphere.json");
			Renderer.RenderToImage("Output", "scene6", scene6, 8, 11.5f);

			Scene scene7 = ReadSceneData("Resources/scene7_squashed_rotated_sphere.json");
			Renderer.RenderToImage("Output", "scene7", scene7, 8, 11.5f);

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
			Console.WriteLine($"Parsing Scene File: {sceneFileJson}");

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
				Matrix4x4 transformationMatrix = Matrix4x4.Identity;

				#region Transformation Matrix
				JToken object3D_json = item;
				if(item["transform"] != null)
				{
					foreach (var transformationType in item["transform"]["transformations"])
					{
						if (transformationType["translate"] != null)
						{
							transformationMatrix *= Matrix4x4.CreateTranslation(ParseVector3(transformationType["translate"]));
						}
						else if (transformationType["scale"] != null)
						{
							transformationMatrix *= Matrix4x4.CreateScale(ParseVector3(transformationType["scale"]));
						}
						else if (transformationType["xrotate"] != null)
						{
							transformationMatrix *= Matrix4x4.CreateRotationX((float)transformationType["xrotate"]);
						}
						else if (transformationType["yrotate"] != null)
						{
							transformationMatrix *= Matrix4x4.CreateRotationY((float)transformationType["yrotate"]);
						}
						else if (transformationType["zrotate"] != null)
						{
							transformationMatrix *= Matrix4x4.CreateRotationZ((float)transformationType["zrotate"]);
						}
					}
				
					object3D_json = item["transform"]["object"];
				}
				#endregion

				if (object3D_json["sphere"] != null)
				{
					position = ParseVector3(object3D_json["sphere"]["center"]);
					float radius = (float)object3D_json["sphere"]["radius"];
					Color01 col = ParseColor32(object3D_json["sphere"]["color"]);
					Sphere sphereMesh = newObject3D.AddComponent<Sphere>();
					sphereMesh.Init(radius, col);
					Console.Write($"Created Sphere \nPosition: {position}\nRotation: {rotation}\nScale: {scale}\nRadius: {radius}\nColor: {col}\n\n");
				}
				else if (object3D_json["plane"] != null)
				{
					float offset = (float)object3D_json["plane"]["offset"];
					position = new Vector3(0, offset, 0);
					Vector3 normal = ParseVector3(object3D_json["plane"]["normal"]);
					Color01 col = ParseColor32(object3D_json["plane"]["color"]);
					Plane planeMesh = newObject3D.AddComponent<Plane>();
					planeMesh.Init(normal, col);
					Console.Write($"Created Plane \nPosition: {position}\nRotation: {rotation}\nScale: {scale}\nNormal: {normal}\nColor: {col}\n\n");

				}
				else if (object3D_json["triangle"] != null)
				{
					Vector3 v1 = ParseVector3(object3D_json["triangle"]["v1"]);
					Vector3 v2 = ParseVector3(object3D_json["triangle"]["v2"]);
					Vector3 v3 = ParseVector3(object3D_json["triangle"]["v3"]);
					Color01 col = ParseColor32(object3D_json["triangle"]["color"]);
					Triangle triangleMesh = newObject3D.AddComponent<Triangle>();
					triangleMesh.Init(v1, v2, v3, col);
					Console.Write($"Created Triangle \nPosition: {position}\nRotation: {rotation}\nScale: {scale}\nV1: {v1}\nV2: {v2}\nV3: {v3}\nColor: {col}\n\n");

				}

				newObject3D.position = position;
				newObject3D.rotation = rotation;
				newObject3D.scale = scale;
				newObject3D.objectToWorldMatrix = transformationMatrix;
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
				OrthographicCamera camera = cameraObject3D.AddComponent<OrthographicCamera>();
				camera.Init(camDirection, size);

				root.AddChild(cameraObject3D);
				scene.MainCamera = cameraObject3D;
				Console.Write($"Created Orthographic Camera \nPosition: {camPos}\nRotation: {Vector3.Zero}\nScale: {Vector3.One}\nDirection: {camDirection}\nUp: {camUpDirection}\nSize: {size}\n\n");
			}
			else if(jsonDecoded["perspectivecamera"] != null)
			{
				JToken cameraO = jsonDecoded["perspectivecamera"];

				Vector3 camPos = ParseVector3(cameraO["center"]);
				Vector3 camDirection = ParseVector3(cameraO["direction"]);
				int fov = (int)cameraO["angle"];

				Object3D cameraObject3D = new Object3D() { position = camPos };

				PerspectiveCamera camera = cameraObject3D.AddComponent<PerspectiveCamera>();
				camera.Init(camDirection, fov);

				root.AddChild(cameraObject3D);
				scene.MainCamera = cameraObject3D;
				Console.Write($"Created Perspective Camera \nPosition: {camPos}\nRotation: {Vector3.Zero}\nScale: {Vector3.One}\nDirection: {camDirection}\nFOV: {fov}\n\n");

			}

			#endregion

			#region Light Parsing
			//If light data exists then load it and add it to the scene.
			if (jsonDecoded["light"] != null)
			{
				Vector3 direction = ParseVector3(jsonDecoded["light"]["direction"]).Normalized;
				Color01 color = ParseColor32(jsonDecoded["light"]["color"]);
				Object3D lightObject = new Object3D();
				Light light = lightObject.AddComponent<Light>();
				light.Init(direction, color);
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
