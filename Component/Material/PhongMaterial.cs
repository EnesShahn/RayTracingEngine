//#define USE_BLING_PHONG

using System;
using System.Collections.Generic;
using System.Text;


namespace SimpleRayTracingEngine
{
	class PhongMaterial : Material
	{

		public Color01 specularColor = new Color01(0, 0, 0, 1);
		public float exponent = 1;

		public override Color01 Shade(Vector3 rayDir, Light l, Vector3 normal, Vector3 point)
		{
			Vector3 viewDir = -rayDir;
			Vector3 lightDir = -l.GetDirection();

			float NDotL = Vector3.Dot(normal, lightDir);
			Color01 diffuse = diffuseColor * l.Color * MathF.Max(NDotL, 0);

#if USE_BLING_PHONG
			Vector3 h = (viewDir + lightDir).Normalized;
			float NDotH = Vector3.Dot(normal, h);
			Color01 specular = specularColor * l.Color * MathF.Pow(MathF.Max(NDotH, 0), exponent*4.0f);
#else
			Vector3 R = Vector3.Reflect(-lightDir, normal);
			float RDotV = Vector3.Dot(R, viewDir);
			Color01 specular = specularColor * l.Color * MathF.Pow(MathF.Max(RDotV, 0), exponent);
#endif

			return diffuse + specular;

		}
	}
}
