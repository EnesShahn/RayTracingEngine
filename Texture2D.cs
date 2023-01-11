using System;
using System.Collections.Generic;
using System.Text;

namespace EnesShahn
{
    namespace RayTracingEngine
    {
        class Texture2D
        {
            private int width;
            private int height;
            private float aspectRation;

            private Color01[,] pixels;

            public Texture2D(int width, int height)
            {
                this.width = width;
                this.height = height;
                aspectRation = (float)width / height;
                pixels = new Color01[width, height];
            }

            public Color01 this[int x, int y]
            {
                get { return pixels[x, y]; }
                set { pixels[x, y] = value; }
            }
        }
    }
}