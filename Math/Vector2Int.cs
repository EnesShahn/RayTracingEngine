using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	struct Vector2Int
	{
		public int x, y;
		public Vector2Int(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		#region Operator Overloading
		public static Vector2Int operator +(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x + b.x, a.y + b.y);
		}
		public static Vector2Int operator -(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x - b.x, a.y - b.y);
		}
		public static Vector2Int operator -(Vector2Int a)
		{
			return new Vector2Int(-a.x, -a.y);
		}
		public static Vector2Int operator *(int c, Vector2Int a)
		{
			return new Vector2Int(a.x * c, a.y * c);
		}
		public static Vector2Int operator /(int c, Vector2Int a)
		{
			return new Vector2Int(a.x / c, a.y / c);
		}
		public static Vector2Int operator *(Vector2Int a, int c)
		{
			return new Vector2Int(a.x * c, a.y * c);
		}
		public static Vector2Int operator /(Vector2Int a, int c)
		{
			return new Vector2Int(a.x / c, a.y / c);
		}
		#endregion

		#region Overridden Method
		public override string ToString()
		{
			return $"Vector2Int({x}, {y})";
		}
		#endregion
	}
}
