using System;
using System.Collections.Generic;
using System.Text;

namespace EnesShahn
{
	namespace RayTracingEngine
	{
		struct Vector3Int
	{
		public int x, y, z;
		public Vector3Int(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		#region Operator Overloading
		public static Vector3Int operator +(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x + b.x, a.y + b.y, a.z + b.z);
		}
		public static Vector3Int operator -(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x - b.x, a.y - b.y, a.z - b.z);
		}
		public static Vector3Int operator -(Vector3Int a)
		{
			return new Vector3Int(-a.x, -a.y, -a.z);
		}
		public static Vector3Int operator *(int c, Vector3Int a)
		{
			return new Vector3Int(a.x * c, a.y * c, a.z * c);
		}
		public static Vector3Int operator /(int c, Vector3Int a)
		{
			return new Vector3Int(a.x / c, a.y / c, a.z / c);
		}
		public static Vector3Int operator *(Vector3Int a, int c)
		{
			return new Vector3Int(a.x * c, a.y * c, a.z * c);
		}
		public static Vector3Int operator /(Vector3Int a, int c)
		{
			return new Vector3Int(a.x / c, a.y / c, a.z / c);
		}
		#endregion

		#region Overridden Method
		public override string ToString()
		{
			return $"Vector3Int({x}, {y}, {z})";
		}
		#endregion
	}
}
}