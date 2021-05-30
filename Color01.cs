using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	class Color01
	{
		public float R { get; set; }
		public float G { get; set; }
		public float B { get; set; }
		public float A { get; set; }

		//TODO: To Hex Conversion
		//TODO: Parse from Hex

		public Color01(float r, float g, float b, float a)
		{
			R = r;
			G = g;
			B = b;
			A = a;
			Normalize();
		}
		public Color01(float r, float g, float b) : this(r, g, b, 1f) { }

		#region Primitive Colors
		public static Color01 Black {
			get { return new Color01(0, 0, 0, 1); }
		}
		public static Color01 White {
			get { return new Color01(1, 1, 1, 1); }
		}
		public static Color01 Red {
			get { return new Color01(1, 0, 0, 1); }
		}
		public static Color01 Green {
			get { return new Color01(0, 1, 0, 1); }
		}
		public static Color01 Blue {
			get { return new Color01(0, 0, 1, 1); }
		}
		#endregion
		
		
		public void Normalize()
		{
			R = Math.Clamp(R, 0f, 1f);
			G = Math.Clamp(G, 0f, 1f);
			B = Math.Clamp(B, 0f, 1f);
			A = Math.Clamp(A, 0f, 1f);
		}

		public static Color01 FromArray(float[] rgba)
		{
			if(rgba.Length > 4)
				throw new Exception("Color field in json file has more than 4 values");
			else if(rgba.Length < 3)
				throw new Exception("Color field in json file needs at least 3 values");

			if(rgba.Length == 4)
				return new Color01(rgba[0], rgba[1], rgba[2], rgba[3]);
			return new Color01(rgba[0], rgba[1], rgba[2]);
		}

		#region Overloaded Operators
		public static Color01 operator *(Color01 color, float b)
		{
			return new Color01(color.R * b, color.G * b, color.B * b, color.A);
		}
		public static Color01 operator *(float b, Color01 color)
		{
			return new Color01(color.R * b, color.G * b, color.B * b, color.A);
		}
		public static Color01 operator *(Color01 color1, Color01 color2)
		{
			return new Color01(color1.R * color2.R, color1.G * color2.G, color1.B * color2.B, color1.A * color2.A);
		}
		public static Color01 operator +(Color01 color1, Color01 color2)
		{
			return new Color01(color1.R + color2.R, color1.G + color2.G, color1.B + color2.B, color1.A + color2.A);
		}
		
		public static bool operator ==(Color01 color1, Color01 color2)
		{
			return (color1.R == color2.R && color1.G == color2.G && color1.B == color2.B && color1.A == color2.A);
		}
		public static bool operator !=(Color01 color1, Color01 color2)
		{
			return (color1.R != color2.R || color1.G != color2.G || color1.B != color2.B || color1.A != color2.A);
		}
		#endregion

		public override string ToString()
		{
			return $"RGBA({R}, {G}, {B}, {A})";
		}
	}
}
