using System;
using System.Collections.Generic;
using System.Text;

namespace EnesShahn
{
	namespace RayTracingEngine
	{
		struct Vector2
	{
		public float x, y;
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		#region Properties
		public float Magnitude {
			get { return (float)Math.Sqrt(x * x + y * y); }
		}
		public float SqrMagnitude {
			get { return x * x + y * y ; }
		}
		public Vector2 Normalized {
			get {
				float magnitude = (float)Math.Sqrt(x * x + y * y);
				return new Vector2(x / magnitude, y / magnitude);
			}
		}
		public static Vector2 Zero {
			get { return new Vector2(0, 0); }
		}
		public static Vector2 One {
			get { return new Vector2(1, 1); }
		}
		#endregion

		#region Methods
		public void Normalize()
		{
			float magnitude = (float)Math.Sqrt(x * x + y * y);
			this.x /= magnitude;
			this.y /= magnitude;
		}
		public static float Dot(Vector2 a, Vector2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		public static Vector2 Reflect(Vector2 incoming, Vector2 normal)
		{
			return incoming - 2 * (Vector2.Dot(incoming, normal) * normal);
		}
		#endregion

		#region Operator Overloading
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}
		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}
		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.x, -a.y);
		}
		public static Vector2 operator *(int c, Vector2 a)
		{
			return new Vector2(a.x * c, a.y * c);
		}
		public static Vector2 operator /(int c, Vector2 a)
		{
			return new Vector2(a.x / c, a.y / c);
		}
		public static Vector2 operator *(float c, Vector2 a)
		{
			return new Vector2(a.x * c, a.y * c);
		}
		public static Vector2 operator /(float c, Vector2 a)
		{
			return new Vector2(a.x / c, a.y / c);
		}
		public static Vector2 operator *(Vector2 a, int c)
		{
			return new Vector2(a.x * c, a.y * c);
		}
		public static Vector2 operator /(Vector2 a, int c)
		{
			return new Vector2(a.x / c, a.y / c);
		}
		public static Vector2 operator *(Vector2 a, float c)
		{
			return new Vector2(a.x * c, a.y * c);
		}
		public static Vector2 operator /(Vector2 a, float c)
		{
			return new Vector2(a.x / c, a.y / c);
		}
		#endregion

		#region Overridden Methods
		public override string ToString()
		{
			return $"Vector2({x}, {y})";
		}
		#endregion
	}
}
}