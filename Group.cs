using System;
using System.Collections.Generic;
using System.Text;

namespace RayCastingEngine
{
	class Group : Object3D
	{
		private List<Object3D> objects = new List<Object3D>();

		public List<Object3D> Objects { get { return objects; } }

		public void AddObject3D(Object3D object3D)
		{
			objects.Add(object3D);
		}
		public void RemoveObject3D(Object3D object3D)
		{
			objects.Remove(object3D);
		}

		public override void Intersect(Ray ray, Hit hit, float tmin)
		{
			foreach (Object3D item in objects)
			{
				item.Intersect(ray, hit, tmin);
			}
		}
	}
}
