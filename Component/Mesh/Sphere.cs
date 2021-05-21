using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	sealed class Sphere : Mesh
	{
		private float radius;
		private float radius2; // Cache to improve speed

		public void Init(float radius, Color01 color)
		{
			this.radius = radius;
			this.radius2 = radius * radius;
			this.color = color;
		}

		public override void Intersect(Ray ray, Hit hit, float tmin)
		{
			Matrix4x4 worldToObjectMatrix = object3D.objectToWorldMatrix.GetInverse();
			Matrix4x4 objectToWorldMatrix = object3D.objectToWorldMatrix;

			Vector3 transformedPos = worldToObjectMatrix * ray.Origin;
			Vector3 transformedDir = worldToObjectMatrix * ray.Direction;

			Vector3 L = object3D.position - transformedPos;
			if (L.SqrMagnitude < radius2) // Ray inside sphere (Square both sides to get rid of the Square root = Better Performance)
				return;
			float tca = Vector3.Dot(L, transformedDir);
			if (tca < 0) // Sphere behind the origin of the ray
				return;
			float d2 = Vector3.Dot(L, L) - tca * tca;
			if (d2 > radius || d2 < 0)
				return;

			float thc = (float)Math.Sqrt(radius2 - d2);


			float tObjectSpace = tca - thc;
			//float t1 = tca + thc; // Second intersection

			if(tObjectSpace > 0) 
			{
				//Calculated t (distance) is in object space, we need to get distance in world space.
				Vector3 pObject = transformedPos + transformedDir * tObjectSpace;
				Vector3 pWorld = objectToWorldMatrix * pObject;
				float t = (pWorld - ray.Origin).Magnitude;

				if (t > 0 && t > tmin && t < hit.TCurrent)
				{
					hit.TCurrent = t;
					hit.Color = color;
					hit.Intersection = true;
					hit.Normal = (worldToObjectMatrix.GetTranspose() * (pObject - object3D.position)).Normalized;
				}
			}

		}
	}
}
