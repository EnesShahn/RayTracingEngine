using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	class Object3D
	{
		public int id;

		public Vector3 position = new Vector3(0, 0, 0);
		public Vector3 rotation = new Vector3(0, 0, 0); // TODO: Currently Using Euler Rotation, Convert to Quaternion
		public Vector3 scale = new Vector3(1, 1, 1);

		public Matrix4x4 modelMatrix = Matrix4x4.Identity;

		private Object3D parent;
		private readonly List<Component> components = new List<Component>();
		private readonly List<Object3D> children = new List<Object3D>();

		public Object3D()
		{

		}
		public Object3D(Vector3 position, Vector3 rotation, Vector3 scale)
		{
			this.position = position;
			this.rotation = rotation;
			this.scale = scale;
		}


		public T AddComponent<T>() where T : Component, new()
		{
			T newComp = new T();
			newComp.SetOwner(this);
			components.Add(newComp);
			return newComp;
		}
		public void RemoveComponent(Component component) => components.Remove(component);
		public bool HasComponent<T>() where T: Component
		{
			Type tType = typeof(T);
			foreach (Component component in components)
			{
				if (component.GetType().IsSubclassOf(tType) || component.GetType() == typeof(T))
					return true;
			}
			return false;
		}
		public T GetComponent<T>() where T : Component
		{
			Type tType = typeof(T);
			foreach (Component component in components)
			{
				if (component.GetType().IsSubclassOf(tType) || component.GetType() == typeof(T))
					return (T)component;
			}
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"Error: No {tType} Component Attached The Object3D");
			Console.ForegroundColor = ConsoleColor.White;
			return null;
		}


		public int ChildCount() => children.Count;
		public void RemoveChild(Object3D child)
		{
			child.parent = null;
			children.Remove(child);
		}

		public Object3D GetChild(int index) => children[index];

		public void AddChild(Object3D child)
		{
			if(parent != null)
			{
				parent.RemoveChild(this);
			}
			children.Add(child);
			child.parent = this;
		}

	}
}
