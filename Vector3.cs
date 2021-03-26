using System;

namespace RayCastingEngine
{
	public class Vector3
	{
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }

		public Vector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}
		public Vector3() : this(0, 0, 0) { }

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

		public override string ToString()
		{
			return $"Vector3({X}, {Y}, {Z})";
		}

		public static Vector3 FromArray(float[] coords)
		{
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
	}
}

