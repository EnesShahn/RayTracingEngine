using System;

namespace SimpleRayTracingEngine
{
	public class Vector3
	{
		private float magnitude;
		private Vector3 unitVector;
		private float x, y, z;

		public float X {
			get { return x; }
			set { x = value; }
		}
		public float Y {
			get { return y; }
			set { y = value; }
		}
		public float Z {
			get { return z; }
			set { z = value; }
		}

		public float Magnitude {
			get { CalculateMagnitudeAndUnitVector(); return magnitude; }
		}
		public Vector3 Normalized {
			get { CalculateMagnitudeAndUnitVector(); return unitVector; }
		}

		public Vector3() : this(0, 0, 0) { }
		public Vector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		private bool calculated;
		private void CalculateMagnitudeAndUnitVector()
		{
			if (calculated)
				return;
			magnitude = (float)Math.Sqrt((y * y + x * x + z * z));
			unitVector = new Vector3(x / magnitude, y / magnitude, z / magnitude);
			calculated = true;
		}

		public static float Dot(Vector3 a, Vector3 b)
		{
			return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
		}

		public static Vector3 CrossProduct(Vector3 a, Vector3 b)
		{
			Vector3 cp = new Vector3
			{
				X = a.Y * b.Z - a.Z * b.Y,
				Y = a.Z * b.X - a.X * b.Z,
				Z = a.X * b.Y - a.Y * b.X
			};
			return cp;
		}


		public static Vector3 FromArray(float[] coords)
		{
			if(coords.Length > 3)
				throw new Exception("Vector3 can take only 3 floats");
			else if (coords.Length < 3)
				throw new Exception("Vector3 should take 3 floats");
			return new Vector3(coords[0], coords[1], coords[2]);
		}

		#region Operator Overloading
		public static Vector3 operator+(Vector3 a, Vector3 b)
		{
			return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}
		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.X, -a.Y, -a.Z);
		}
		public static Vector3 operator *(int c, Vector3 a)
		{
			return new Vector3(a.X * c, a.Y * c, a.Z * c);
		}
		public static Vector3 operator /(int c, Vector3 a)
		{
			return new Vector3(a.X / c, a.Y / c, a.Z / c);
		}
		public static Vector3 operator *(float c, Vector3 a)
		{
			return new Vector3(a.X * c, a.Y * c, a.Z * c);
		}
		public static Vector3 operator /(float c, Vector3 a)
		{
			return new Vector3(a.X / c, a.Y / c, a.Z / c);
		}
		public static Vector3 operator*(Vector3 a, int c)
		{
			return new Vector3(a.X * c, a.Y * c, a.Z * c);
		}
		public static Vector3 operator /(Vector3 a, int c)
		{
			return new Vector3(a.X / c, a.Y / c, a.Z / c);
		}
		public static Vector3 operator *(Vector3 a, float c)
		{
			return new Vector3(a.X * c, a.Y * c, a.Z * c);
		}
		public static Vector3 operator /(Vector3 a, float c)
		{
			return new Vector3(a.X / c, a.Y / c, a.Z / c);
		}

		#endregion

		public override string ToString()
		{
			return $"Vector3({X}, {Y}, {Z})";
		}

	}
}

