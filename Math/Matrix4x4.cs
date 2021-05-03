using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine.Math
{
	class Matrix4x4
	{
		float[,] m = new float[4,4] { 
			{ 1, 0, 0, 0},
			{ 0, 1, 0, 0},
			{ 0, 0, 1, 0},
			{ 0, 0, 0, 1},
		};


		public float this[int i, int j] {
			private set { m[i, j] = value; }
			get { return m[i, j]; }
		}

		public static Matrix4x4 operator * (Matrix4x4 lhs, Matrix4x4 rhs)
		{
			Matrix4x4 result = new Matrix4x4();
			for (int i = 0; i < 4; i++)
				for (int j = 0; j < 4; j++)
					result[i, j] = lhs[i, 0] * rhs[0, j] + lhs[i, 1] * rhs[1, j] + lhs[i, 2] * rhs[2, j] + lhs[i, 3] * rhs[3, j];
			return result;
		}
		public static Matrix4x4 operator +(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			Matrix4x4 result = new Matrix4x4();
			for (int i = 0; i < 4; i++)
				for (int j = 0; j < 4; j++)
					result[i, j] = lhs[i, j] + rhs[i, j];
			return result;
		}
		public static Matrix4x4 operator -(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			Matrix4x4 result = new Matrix4x4();
			for (int i = 0; i < 4; i++)
				for (int j = 0; j < 4; j++)
					result[i, j] = lhs[i, j] - rhs[i, j];
			return result;
		}
	}
}
