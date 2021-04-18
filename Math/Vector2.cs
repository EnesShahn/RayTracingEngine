using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	class Vector2
	{
		private float magnitude;
		private Vector2 unitVector;
		private float x, y;

		public float X {
			get { return x; }
			set { x = value; }
		}
		public float Y {
			get { return y; }
			set { y = value; }
		}
		public float Magnitude {
			get { CalculateMagnitudeAndUnitVector(); return magnitude; }
		}
		public Vector2 Normalized {
			get { CalculateMagnitudeAndUnitVector(); return unitVector; }
		}
		
		public static Vector2 Zero {
			get { return new Vector2(0, 0); }
		}
		public static Vector2 One {
			get { return new Vector2(1, 1); }
		}

		public Vector2() : this(0, 0) { }
		public Vector2(float x, float y)
		{
			X = x;
			Y = y;
		}

		private bool calculated;
		private void CalculateMagnitudeAndUnitVector()
		{
			if (calculated)
				return;
			magnitude = (float)Math.Sqrt((y * y + x * x));
			unitVector = new Vector2(x / magnitude, y / magnitude);
			calculated = true;
		}


		public static Vector2 Normalize(Vector2 v)
		{
			float magnitude = (float)Math.Sqrt((v.y * v.y - v.x * v.x));
			return new Vector2(v.x / magnitude, v.y / magnitude);
		}

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
		#endregion

		public override string ToString()
		{
			return $"Vector2({X}, {Y})";
		}
	}
}
