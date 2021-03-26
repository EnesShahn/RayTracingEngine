using System;
using System.Collections.Generic;
using System.Text;

namespace RayCastingEngine
{
	abstract class Object3D
	{
		private Color32 color;

		public Color32 Color {
			get { return color; }
		}
		public Object3D()
		{

		}
		public Object3D(Color32 color)
		{
			this.color = color;
		}

		public abstract void Intersect(Ray ray, Hit hit, float tmin);
	}
}
