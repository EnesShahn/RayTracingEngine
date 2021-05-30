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

		public Vector3(Vector4 v)
		{
			x = v.x;
			y = v.y;
			z = v.z;
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
			return (float)(Math.Acos(dot / (a.Magnitude * b.Magnitude)) * Mathf.DegreesToRad);
		}

		public static Vector3 Reflect(Vector3 incoming, Vector3 normal)
		{
			return incoming - 2 * (Vector3.Dot(incoming, normal) * normal);
		}

		//w = 1
		public static Vector3 TransformPoint(Vector3 point, Matrix4x4 matrix)
		{
			return new Vector3(
				point.x * matrix[0, 0] + point.y * matrix[0, 1] + point.z * matrix[0, 2] + matrix[0, 3],
				point.x * matrix[1, 0] + point.y * matrix[1, 1] + point.z * matrix[1, 2] + matrix[1, 3],
				point.x * matrix[2, 0] + point.y * matrix[2, 1] + point.z * matrix[2, 2] + matrix[2, 3]
			);
		}

		//w = 0 
		public static Vector3 TransformNormal(Vector3 normal, Matrix4x4 matrix)
		{
			return new Vector3(
				normal.x * matrix[0, 0] + normal.y * matrix[0, 1] + normal.z * matrix[0, 2],
				normal.x * matrix[1, 0] + normal.y * matrix[1, 1] + normal.z * matrix[1, 2],
				normal.x * matrix[2, 0] + normal.y * matrix[2, 1] + normal.z * matrix[2, 2]
				);
		}

		#endregion

		public static Vector3 FromArray(float[] coords)
		{
			if (coords.Length > 3)
				throw new Exception("Vector3 can take only 3 floats");
			else if (coords.Length < 3)
				throw new Exception("Vector3 should take 3 floats");
			return new Vector3(coords[0], coords[1], coords[2]);
		}

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

