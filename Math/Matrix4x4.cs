using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace SimpleRayTracingEngine
{
	class Matrix4x4
	{
		private float[,] m = new float[4, 4];

		public float this[int i, int j] {
			private set { m[i, j] = value; }
			get { return m[i, j]; }
		}

		public static Matrix4x4 Identity {
			get {
				Matrix4x4 mt = new Matrix4x4();
				mt[0, 0] = 1;
				mt[1, 1] = 1;
				mt[2, 2] = 1;
				mt[3, 3] = 1;
				return mt;
			}
		}

		#region Overloaded Operator
		public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
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
		#endregion

		#region Creating Matrix
		public static Matrix4x4 CreateScale(Vector3 scale)
		{
			Matrix4x4 result = new Matrix4x4();
			result[0, 0] = scale.x;
			result[1, 1] = scale.y;
			result[2, 2] = scale.z;
			result[3, 3] = 1;
			return result;
		}
		public static Matrix4x4 CreateRotationX(float angle)
		{
			float cosA = MathF.Cos(angle * Mathf.DegreesToRad);
			float sinA = MathF.Sin(angle * Mathf.DegreesToRad);
			Matrix4x4 result = new Matrix4x4();
			result[1, 1] = cosA;
			result[1, 2] = -sinA;
			result[2, 1] = sinA;
			result[2, 2] = cosA;
			result[0, 0] = 1;
			result[3, 3] = 1;
			return result;
		}
		public static Matrix4x4 CreateRotationY(float angle)
		{
			float cosA = MathF.Cos(angle * Mathf.DegreesToRad);
			float sinA = MathF.Sin(angle * Mathf.DegreesToRad);
			Matrix4x4 result = new Matrix4x4();
			result[0, 0] = cosA;
			result[0, 2] = sinA;
			result[2, 0] = -sinA;
			result[2, 2] = cosA;
			result[1, 1] = 1;
			result[3, 3] = 1;
			return result;
		}
		public static Matrix4x4 CreateRotationZ(float angle)
		{
			float cosA = MathF.Cos(angle * Mathf.DegreesToRad);
			float sinA = MathF.Sin(angle * Mathf.DegreesToRad);
			Matrix4x4 result = new Matrix4x4();
			result[0, 0] = cosA;
			result[0, 1] = -sinA;
			result[1, 0] = sinA;
			result[1, 1] = cosA;
			result[2, 2] = 1;
			result[3, 3] = 1;
			return result;
		}
		public static Matrix4x4 CreateRotation(float pitch, float yaw, float roll)
		{
			return CreateRotationX(pitch) * CreateRotationY(yaw) * CreateRotationZ(roll);
		}
		public static Matrix4x4 CreateTranslation(Vector3 translation)
		{
			Matrix4x4 result = new Matrix4x4();
			result[0, 0] = 1;
			result[1, 1] = 1;
			result[2, 2] = 1;
			result[3, 3] = 1;
			result[0, 3] = translation.x;
			result[1, 3] = translation.y;
			result[2, 3] = translation.z;
			return result;
		}

		#endregion

		private Matrix4x4 transpose;
		public Matrix4x4 GetTranspose()
		{
			if (transpose != null)
				return transpose;
			transpose = new Matrix4x4();
			for (int row = 0; row < 4; row++)
			{
				for (int col = 0; col < 4; col++)
				{
					transpose[row, col] = m[col, row];
				}
			}
			return transpose;
		}

		private Matrix4x4 inverse;
		public Matrix4x4 GetInverse() 
		{
			if (inverse != null)
				return inverse;

			inverse = new Matrix4x4();

			var M = this.m;
			float a = M[0, 0], b = M[0, 1], c = M[0, 2], d = M[0, 3];
			float e = M[1, 0], f = M[1, 1], g = M[1, 2], h = M[1, 3];
			float i = M[2, 0], j = M[2, 1], k = M[2, 2], l = M[2, 3];
			float m = M[3, 0], n = M[3, 1], o = M[3, 2], p = M[3, 3];

			float kp_lo = k * p - l * o;
			float jp_ln = j * p - l * n;
			float jo_kn = j * o - k * n;
			float ip_lm = i * p - l * m;
			float io_km = i * o - k * m;
			float in_jm = i * n - j * m;

			float a11 = +(f * kp_lo - g * jp_ln + h * jo_kn);
			float a12 = -(e * kp_lo - g * ip_lm + h * io_km);
			float a13 = +(e * jp_ln - f * ip_lm + h * in_jm);
			float a14 = -(e * jo_kn - f * io_km + g * in_jm);

			float det = a * a11 + b * a12 + c * a13 + d * a14;

			if (Math.Abs(det) < float.Epsilon)
			{
				for (int row = 0; row < 4; row++)
				{
					for (int col = 0; col < 4; col++)
					{
						inverse[row, col] = float.NaN;
					}
				}
				return inverse;
			}

			float invDet = 1f / det;

			inverse[0, 0] = a11 * invDet;
			inverse[1, 0] = a12 * invDet;
			inverse[2, 0] = a13 * invDet;
			inverse[3, 0] = a14 * invDet;

			inverse[0, 1] = -(b * kp_lo - c * jp_ln + d * jo_kn) * invDet;
			inverse[1, 1] = +(a * kp_lo - c * ip_lm + d * io_km) * invDet;
			inverse[2, 1] = -(a * jp_ln - b * ip_lm + d * in_jm) * invDet;
			inverse[3, 1] = +(a * jo_kn - b * io_km + c * in_jm) * invDet;

			float gp_ho = g * p - h * o;
			float fp_hn = f * p - h * n;
			float fo_gn = f * o - g * n;
			float ep_hm = e * p - h * m;
			float eo_gm = e * o - g * m;
			float en_fm = e * n - f * m;

			inverse[0, 2] = +(b * gp_ho - c * fp_hn + d * fo_gn) * invDet;
			inverse[1, 2] = -(a * gp_ho - c * ep_hm + d * eo_gm) * invDet;
			inverse[2, 2] = +(a * fp_hn - b * ep_hm + d * en_fm) * invDet;
			inverse[3, 2] = -(a * fo_gn - b * eo_gm + c * en_fm) * invDet;

			float gl_hk = g * l - h * k;
			float fl_hj = f * l - h * j;
			float fk_gj = f * k - g * j;
			float el_hi = e * l - h * i;
			float ek_gi = e * k - g * i;
			float ej_fi = e * j - f * i;

			inverse[0, 3] = -(b * gl_hk - c * fl_hj + d * fk_gj) * invDet;
			inverse[1, 3] = +(a * gl_hk - c * el_hi + d * ek_gi) * invDet;
			inverse[2, 3] = -(a * fl_hj - b * el_hi + d * ej_fi) * invDet;
			inverse[3, 3] = +(a * fk_gj - b * ek_gi + c * ej_fi) * invDet;
			return inverse;
		}
		
		private float mDeterminant = float.NaN;
		public float GetDeterminant()
		{
			//if (mDeterminant != float.NaN)
			//	return mDeterminant;

			var M = this.m;
			float a = M[0,0], b = M[0, 1], c = M[0, 2], d = M[0, 3];
			float e = M[1, 0], f = M[1, 1], g = M[1, 2], h = M[1, 3];
			float i = M[2, 0], j = M[2, 1], k = M[2, 2], l = M[2, 3];
			float m = M[3, 0], n = M[3, 1], o = M[3, 2], p = M[3, 3];

			float kp_lo = k * p - l * o;
			float jp_ln = j * p - l * n;
			float jo_kn = j * o - k * n;
			float ip_lm = i * p - l * m;
			float io_km = i * o - k * m;
			float in_jm = i * n - j * m;

			mDeterminant = a * (f * kp_lo - g * jp_ln + h * jo_kn) -
				   b * (e * kp_lo - g * ip_lm + h * io_km) +
				   c * (e * jp_ln - f * ip_lm + h * in_jm) -
				   d * (e * jo_kn - f * io_km + g * in_jm);
			return mDeterminant;
		}


		public override string ToString()
		{
			string output;
			output =  $"| {m[0, 0]} {m[0, 1]} {m[0, 2]} {m[0, 3]} |\n";
			output += $"| {m[1, 0]} {m[1, 1]} {m[1, 2]} {m[1, 3]} |\n";
			output += $"| {m[2, 0]} {m[2, 1]} {m[2, 2]} {m[2, 3]} |\n";
			output += $"| {m[3, 0]} {m[3, 1]} {m[3, 2]} {m[3, 3]} |\n";
			return output;
		}
	}
}
