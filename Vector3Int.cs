using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	class Vector3Int
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Z { get; set; }
		
		public Vector3Int() : this(0, 0, 0) { }
		public Vector3Int(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		#region Operator Overloading
		public static Vector3Int operator +(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}
		public static Vector3Int operator -(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}
		public static Vector3Int operator -(Vector3Int a)
		{
			return new Vector3Int(-a.X, -a.Y, -a.Z);
		}
		public static Vector3Int operator *(int c, Vector3Int a)
		{
			return new Vector3Int(a.X * c, a.Y * c, a.Z * c);
		}
		public static Vector3Int operator /(int c, Vector3Int a)
		{
			return new Vector3Int(a.X / c, a.Y / c, a.Z / c);
		}
		public static Vector3Int operator *(Vector3Int a, int c)
		{
			return new Vector3Int(a.X * c, a.Y * c, a.Z * c);
		}
		public static Vector3Int operator /(Vector3Int a, int c)
		{
			return new Vector3Int(a.X / c, a.Y / c, a.Z / c);
		}
		#endregion
	}
}
