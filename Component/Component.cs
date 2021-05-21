using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	abstract class Component
	{
		protected Object3D _object3D;
		public Object3D object3D {
			get { return _object3D; }
		}

		/// <summary>
		/// Internal Method used by the engine ONLY, developers should avoid calling this method.
		/// </summary>
		public void SetOwner(Object3D object3d)
		{
			this._object3D = object3d;
		}

		protected virtual void Awake()
		{

		}
		protected virtual void Start()
		{

		}
		protected virtual void Update()
		{

		}

	}
}
