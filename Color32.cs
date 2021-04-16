using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRayTracingEngine
{
	class Color32
	{
		public byte R { get; set; }
		public byte G { get; set; }
		public byte B { get; set; }
		public byte A { get; set; }

		//TODO: To Hex Conversion
		//TODO: Prase from Hex

		public Color32(byte r, byte g, byte b, byte a)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}
		public Color32(byte r, byte g, byte b) : this(r, g, b, 255) { }

		public static Color32 FromArray(byte[] rgba)
		{
			if(rgba.Length > 4)
				throw new Exception("Color field in json file has more than 4 values");
			else if(rgba.Length < 3)
				throw new Exception("Color field in json file needs at least 3 values");

			if(rgba.Length == 4)
				return new Color32(rgba[0], rgba[1], rgba[2], rgba[3]);
			return new Color32(rgba[0], rgba[1], rgba[2]);
		}

		public static Color32 operator*(Color32 color, float b)
		{
			return new Color32((byte)(color.R * b), (byte)(color.G * b), (byte)(color.B * b), color.R);
		}

		public override string ToString()
		{
			return $"RGBA({R}, {G}, {B}, {A})";
		}
	}
}
