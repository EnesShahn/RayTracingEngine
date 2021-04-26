using System;

namespace SimpleRayTracingEngine
{
	struct Vector3
	{
		public float x, y, z;
		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		#region Properties
		public float Magnitude {
			get { return (float)Math.Sqrt(x * x + y * y + z * z);}
		}
		public float SqrMagnitude {
			get { return x * x + y * y + z * z; }
		}
		public Vector3 Normalized {
			get {
				float magnitude = (float)Math.Sqrt(x * x + y * y + z * z);
				return new Vector3(x/magnitude, y / magnitude, z / magnitude);
			}
		}
		public static Vector3 Zero {
			get { return new Vector3(0, 0, 0); }
		}
		public static Vector3 One {
			get { return new Vector3(1, 1, 1); }
		}
		#endregion

		#region Methods
		public void Normalize()
		{
			float magnitude = (float)Math.Sqrt(x * x + y * y + z * z);
			this.x /= magnitude;
			this.y /= magnitude;
			this.z /= magnitude;
		}
		public static float Dot(Vector3 a, Vector3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}
		public static Vector3 CrossProduct(Vector3 a, Vector3 b)
		{
			Vector3 cp = new Vector3
			{
				x = a.y * b.z - a.z * b.y,
				y = a.z * b.x - a.x * b.z,
				z = a.x * b.y - a.y * b.x
			};
			return cp;
		}



		/// <summary>
		/// Calculates the angle between 2 vectors.
		/// </summary>
		/// <param name="a">Vector a</param>
		/// <param name="b">Vector b</param>
		/// <returns>The angle between vector a and b.</returns>
		public static float AngleBetween(Vector3 a, Vector3 b)
		{
			float dot = a.x * b.x + a.y * b.y + a.z * b.z;
			return (float)(Math.Acos(dot / (a.Magnitude * b.Magnitude)) * (180f / Math.PI));
		}

		public static Vector3 FromArray(float[] coords)
		{
			if(coords.Length > 3)
				throw new Exception("Vector3 can take only 3 floats");
			else if (coords.Length < 3)
				throw new Exception("Vector3 should take 3 floats");
			return new Vector3(coords[0], coords[1], coords[2]);
		}
		#endregion

		#region Operator Overloading
		public static Vector3 operator+(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}
		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.x, -a.y, -a.z);
		}
		public static Vector3 operator *(int c, Vector3 a)
		{
			return new Vector3(a.x * c, a.y * c, a.z * c);
		}
		public static Vector3 operator /(int c, Vector3 a)
		{
			return new Vector3(a.x / c, a.y / c, a.z / c);
		}
		public static Vector3 operator *(float c, Vector3 a)
		{
			return new Vector3(a.x * c, a.y * c, a.z * c);
		}
		public static Vector3 operator /(float c, Vector3 a)
		{
			return new Vector3(a.x / c, a.y / c, a.z / c);
		}
		public static Vector3 operator*(Vector3 a, int c)
		{
			return new Vector3(a.x * c, a.y * c, a.z * c);
		}
		public static Vector3 operator /(Vector3 a, int c)
		{
			return new Vector3(a.x / c, a.y / c, a.z / c);
		}
		public static Vector3 operator *(Vector3 a, float c)
		{
			return new Vector3(a.x * c, a.y * c, a.z * c);
		}
		public static Vector3 operator /(Vector3 a, float c)
		{
			return new Vector3(a.x / c, a.y / c, a.z / c);
		}

		#endregion

		#region Overridden Method
		public override string ToString()
		{
			return $"Vector3({x}, {y}, {z})";
		}
		#endregion
	}
}

