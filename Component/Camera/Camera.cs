using System;
using System.Collections.Generic;
using System.Text;

namespace EnesShahn
{
    namespace RayTracingEngine
    {
        abstract class Camera : Component
        {
            public abstract Ray GenerateRay(Vector2 pixelPosition);
        }
    }
}