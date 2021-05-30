using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace SimpleRayTracingEngine
{
	class SceneManager
	{
		private enum LoggingMode
		{
			LogAll,
			LogType,
			None
		}
		private const LoggingMode loggingMode = LoggingMode.LogAll;
		
		public static Scene ParseSceneData(string sceneFileJson)
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
			scene.ambientLightColor = ambient;

			#region Material Parsing
			List<Material> materials = new List<Material>();

			JToken materialsJT = jsonDecoded["materials"];
			if (materialsJT != null)
			{
				foreach (JToken materialInfo in materialsJT)
				{
					if (materialInfo["phongMaterial"] != null)
					{
						JToken matJson = materialInfo["phongMaterial"];
						PhongMaterial material = new PhongMaterial();
						if (matJson["diffuseColor"] != null)
						{
							Color01 diffuseColor = ParseColor01(matJson["diffuseColor"]);
							material.diffuseColor = diffuseColor;
						}
						if (matJson["specularColor"] != null)
						{
							Color01 specularColor = ParseColor01(matJson["specularColor"]);
							material.specularColor = specularColor;
						}
						if (matJson["exponent"] != null)
						{
							float exponent = (float)matJson["exponent"];
							material.exponent = exponent;
						}
						if (matJson["transparentColor"] != null)
						{
							Color01 transparentColor = ParseColor01(matJson["transparentColor"]);
							material.transparentColor = transparentColor;
						}
						if (matJson["reflectiveColor"] != null)
						{
							Color01 reflectiveColor = ParseColor01(matJson["reflectiveColor"]);
							material.reflectiveColor = reflectiveColor;
						}
						if (matJson["indexOfRefraction"] != null)
						{
							float indexOfRefraction = (float)matJson["indexOfRefraction"];
							material.indexOfRefraction = indexOfRefraction;
						}
						materials.Add(material);
					}
				}
			}
			#endregion

			#region Object3D Parsing
			JToken groupO = jsonDecoded["group"];
			Object3D root = new Object3D();
			scene.root = root;
			int objectId = 0; 
			foreach (var item in groupO)
			{
				Object3D newObject3D = new Object3D();
				Vector3 position = Vector3.Zero;
				Vector3 rotation = Vector3.Zero;
				Vector3 scale = Vector3.Zero;
				Matrix4x4 modelMatrix = Matrix4x4.Identity;

				#region Transformation Matrix
				JToken object3D_json = item;
				if (item["transform"] != null)
				{
					List<Matrix4x4> transformations = new List<Matrix4x4>();

					foreach (var transformationType in item["transform"]["transformations"])
					{
						if (transformationType["translate"] != null)
						{
							transformations.Add(Matrix4x4.CreateTranslation(ParseVector3(transformationType["translate"])));
						}
						else if (transformationType["scale"] != null)
						{
							transformations.Add(Matrix4x4.CreateScale(ParseVector3(transformationType["scale"])));
						}
						else if (transformationType["xrotate"] != null)
						{
							transformations.Add(Matrix4x4.CreateRotationX((float)transformationType["xrotate"]));
						}
						else if (transformationType["yrotate"] != null)
						{
							transformations.Add(Matrix4x4.CreateRotationY((float)transformationType["yrotate"]));
						}
						else if (transformationType["zrotate"] != null)
						{
							transformations.Add(Matrix4x4.CreateRotationZ((float)transformationType["zrotate"]));
						}
					}
					object3D_json = item["transform"]["object"];

					for (int i = 0; i < transformations.Count; i++)
						modelMatrix *= transformations[i];

				}
				#endregion

				if (object3D_json["sphere"] != null)
				{
					position = ParseVector3(object3D_json["sphere"]["center"]);
					float radius = (float)object3D_json["sphere"]["radius"];
					int materialIndex = (int)object3D_json["sphere"]["material"];
					Sphere sphereMesh = newObject3D.AddComponent<Sphere>();
					sphereMesh.Init(radius, materials[materialIndex]);
					Console.Write($"Created Sphere ID:{objectId} \nPosition: {position}\nRotation: {rotation}\nScale: {scale}\nRadius: {radius}\n\n");
				}
				else if (object3D_json["plane"] != null)
				{
					float offset = (float)object3D_json["plane"]["offset"];
					position = new Vector3(0, offset, 0);
					Vector3 normal = ParseVector3(object3D_json["plane"]["normal"]).Normalized;
					int materialIndex = (int)object3D_json["plane"]["material"];
					Plane planeMesh = newObject3D.AddComponent<Plane>();
					planeMesh.Init(normal, materials[materialIndex]);
					Console.Write($"Created Plane ID:{objectId} \nPosition: {position}\nRotation: {rotation}\nScale: {scale}\nNormal: {normal}\n\n");
				}
				else if (object3D_json["triangle"] != null)
				{
					Vector3 v1 = ParseVector3(object3D_json["triangle"]["v1"]);
					Vector3 v2 = ParseVector3(object3D_json["triangle"]["v2"]);
					Vector3 v3 = ParseVector3(object3D_json["triangle"]["v3"]);
					int materialIndex = (int)object3D_json["triangle"]["material"];
					Triangle triangleMesh = newObject3D.AddComponent<Triangle>();
					triangleMesh.Init(v1, v2, v3, materials[materialIndex]);
					Console.Write($"Created Triangle ID:{objectId} \nPosition: {position}\nRotation: {rotation}\nScale: {scale}\nV1: {v1}\nV2: {v2}\nV3: {v3}\n\n");
				}

				newObject3D.position = position;
				newObject3D.rotation = rotation;
				newObject3D.scale = scale;
				newObject3D.modelMatrix = modelMatrix;
				newObject3D.id = objectId;
				root.AddChild(newObject3D);
				objectId++;
			}
			#endregion

			#region Camera Parsing
			if (jsonDecoded["orthocamera"] != null)
			{
				JToken cameraO = jsonDecoded["orthocamera"];

				Vector3 camPos = ParseVector3(cameraO["center"]);
				Vector3 camDirection = ParseVector3(cameraO["direction"]).Normalized;
				Vector3 camUpDirection = ParseVector3(cameraO["up"]).Normalized;
				int size = (int)cameraO["size"];

				Object3D cameraObject3D = new Object3D() { position = camPos };
				OrthographicCamera camera = cameraObject3D.AddComponent<OrthographicCamera>();
				camera.Init(camDirection, camUpDirection, size);

				root.AddChild(cameraObject3D);
				scene.MainCamera = cameraObject3D;
				Console.Write($"Created Orthographic Camera \nPosition: {camPos}\nRotation: {Vector3.Zero}\nScale: {Vector3.One}\nDirection: {camDirection}\nUp: {camUpDirection}\nSize: {size}\n\n");
			}
			else if (jsonDecoded["perspectivecamera"] != null)
			{
				JToken cameraO = jsonDecoded["perspectivecamera"];

				Vector3 camPos = ParseVector3(cameraO["center"]);
				Vector3 camDirection = ParseVector3(cameraO["direction"]).Normalized;
				Vector3 camUpDirection = ParseVector3(cameraO["up"]).Normalized;

				int fov = (int)cameraO["angle"];

				Object3D cameraObject3D = new Object3D() { position = camPos };

				PerspectiveCamera camera = cameraObject3D.AddComponent<PerspectiveCamera>();
				camera.Init(camDirection, camUpDirection, fov);
				root.AddChild(cameraObject3D);
				scene.MainCamera = cameraObject3D;
				Console.Write($"Created Perspective Camera \nPosition: {camPos}\nRotation: {Vector3.Zero}\nScale: {Vector3.One}\nDirection: {camDirection}\nFOV: {fov}\n\n");
			}

			#endregion

			#region Light Parsing
			//If light data exists then load it and add it to the scene.
			if (jsonDecoded["light"] != null) // TEMP TO SUPPORT OLD SCENE DATA
			{
				Vector3 direction = ParseVector3(jsonDecoded["light"]["direction"]).Normalized;
				Color01 color = ParseColor01(jsonDecoded["light"]["color"]);
				Object3D lightObject = new Object3D();
				DirectionalLight light = lightObject.AddComponent<DirectionalLight>();
				light.Init(direction, color);
				root.AddChild(lightObject);
				Console.Write($"Created Directional Light \nPosition: {Vector3.Zero}\nRotation: {Vector3.Zero}\nScale: {Vector3.One}\nDirection: {direction}\nColor: {color}\n\n");
			}
			JToken lightsJ = jsonDecoded["lights"];
			if (lightsJ != null)
			{
				foreach (JToken lightJ in lightsJ)
				{
					if (lightJ["directionalLight"] != null)
					{
						Vector3 direction = ParseVector3(lightJ["directionalLight"]["direction"]).Normalized;
						Color01 color = ParseColor01(lightJ["directionalLight"]["color"]);
						Object3D lightObject = new Object3D();
						DirectionalLight light = lightObject.AddComponent<DirectionalLight>();
						light.Init(direction, color);
						root.AddChild(lightObject);
						Console.Write($"Created Directional Light \nPosition: {Vector3.Zero}\nRotation: {Vector3.Zero}\nScale: {Vector3.One}\nDirection: {direction}\nColor: {color}\n\n");
					}
					else if (lightsJ["pointLight"] != null)
					{

					}
				}
			}
			#endregion

			return scene;
		}

		private static Vector3 ParseVector3(JToken token)
		{
			return Vector3.FromArray(token.ToObject<float[]>());
		}
		private static Color01 ParseColor01(JToken token)
		{
			return Color01.FromArray(token.ToObject<float[]>());
		}
	}
}
