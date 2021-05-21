using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	struct Vector4
	{
		public float x, y, z, w;
		public Vector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		#region Properties
		public static Vector4 Zero {
			get { return new Vector4(0, 0, 0, 0); }
		}
		public static Vector4 One {
			get { return new Vector4(1, 1, 1, 1); }
		}
		#endregion

		#region Methods
		public static Vector4 FromArray(float[] coords)
		{
			if (coords.Length > 4 || coords.Length < 4)
				throw new Exception("Vector4 should take 4 floats");
			return new Vector4(coords[0], coords[1], coords[2], coords[2]);
		}
		#endregion

		#region Operator Overloading
		public static Vector4 operator +(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		}
		public static Vector4 operator -(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		}
		public static Vector4 operator -(Vector4 a)
		{
			return new Vector4(-a.x, -a.y, -a.z, -a.w);
		}
		public static Vector4 operator *(int c, Vector4 a)
		{
			return new Vector4(a.x * c, a.y * c, a.z * c, a.w * c);
		}
		public static Vector4 operator /(int c, Vector4 a)
		{
			return new Vector4(a.x / c, a.y / c, a.z / c, a.w / c);
		}
		public static Vector4 operator *(float c, Vector4 a)
		{
			return new Vector4(a.x * c, a.y * c, a.z * c, a.w * c);
		}
		public static Vector4 operator /(float c, Vector4 a)
		{
			return new Vector4(a.x / c, a.y / c, a.z / c, a.w / c);
		}
		public static Vector4 operator *(Vector4 a, int c)
		{
			return new Vector4(a.x * c, a.y * c, a.z * c, a.w * c);
		}
		public static Vector4 operator /(Vector4 a, int c)
		{
			return new Vector4(a.x / c, a.y / c, a.z / c, a.w / c);
		}
		public static Vector4 operator *(Vector4 a, float c)
		{
			return new Vector4(a.x * c, a.y * c, a.z * c, a.w * c);
		}
		public static Vector4 operator /(Vector4 a, float c)
		{
			return new Vector4(a.x / c, a.y / c, a.z / c, a.w / c);
		}

		#endregion

		#region Overridden Method
		public override string ToString()
		{
			return $"Vector4({x}, {y}, {z}, {w})";
		}
		#endregion
	}
}
