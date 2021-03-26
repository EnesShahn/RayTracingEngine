using System;
using System.Collections.Generic;
using System.Text;

namespace RayCastingEngine
{
	class Vector2
	{
		public float X { get; set; }
		public float Y { get; set; }
		public Vector2(float x, float y)
		{
			X = x;
			Y = y;
		}
		public Vector2() : this(0, 0) { }


		#region Operator Overloading
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.X + b.X, a.Y + b.Y);
		}
		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.X - b.X, a.Y - b.Y);
		}
		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.X, -a.Y);
		}
		public static Vector2 operator *(int c, Vector2 a)
		{
			return new Vector2(a.X * c, a.Y * c);
		}
		public static Vector2 operator /(int c, Vector2 a)
		{
			return new Vector2(a.X / c, a.Y / c);
		}
		public static Vector2 operator *(float c, Vector2 a)
		{
			return new Vector2(a.X * c, a.Y * c);
		}
		public static Vector2 operator /(float c, Vector2 a)
		{
			return new Vector2(a.X / c, a.Y / c);
		}
		public static Vector2 operator *(Vector2 a, int c)
		{
			return new Vector2(a.X * c, a.Y * c);
		}
		public static Vector2 operator /(Vector2 a, int c)
		{
			return new Vector2(a.X / c, a.Y / c);
		}
		public static Vector2 operator *(Vector2 a, float c)
		{
			return new Vector2(a.X * c, a.Y * c);
		}
		public static Vector2 operator /(Vector2 a, float c)
		{
			return new Vector2(a.X / c, a.Y / c);
		}

		public override string ToString()
		{
			return $"Vector2({X}, {Y})";
		}

		#endregion
	}
}
