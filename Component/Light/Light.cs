using System;
using System.Collections.Generic;
using System.Text;

namespace EnesShahn
{
    namespace RayTracingEngine
    {
        abstract class Light : Component
        {
            protected Color01 color;
            public Color01 Color
            {
                get { return color; }
            }

            public abstract Vector3 GetDirection();
            public abstract float GetIntensity();
        }
    }
}
