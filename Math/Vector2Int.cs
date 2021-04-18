using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	class Vector2Int
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Vector2Int() : this(0, 0) { }
		public Vector2Int(int x, int y)
		{
			X = x;
			Y = y;
		}


		#region Operator Overloading
		public static Vector2Int operator +(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.X + b.X, a.Y + b.Y);
		}
		public static Vector2Int operator -(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.X - b.X, a.Y - b.Y);
		}
		public static Vector2Int operator -(Vector2Int a)
		{
			return new Vector2Int(-a.X, -a.Y);
		}
		public static Vector2Int operator *(int c, Vector2Int a)
		{
			return new Vector2Int(a.X * c, a.Y * c);
		}
		public static Vector2Int operator /(int c, Vector2Int a)
		{
			return new Vector2Int(a.X / c, a.Y / c);
		}
		public static Vector2Int operator *(Vector2Int a, int c)
		{
			return new Vector2Int(a.X * c, a.Y * c);
		}
		public static Vector2Int operator /(Vector2Int a, int c)
		{
			return new Vector2Int(a.X / c, a.Y / c);
		}

		public override string ToString()
		{
			return $"Vector2Int({X}, {Y})";
		}
		#endregion
	}
}
