using System;
using System.Collections.Generic;
using System.Text;

namespace EnesShahn
{
    namespace RayTracingEngine
    {
        static class Mathf
        {

            public const float RadToDegrees = 57.295779513f;
            public const float DegreesToRad = 0.01745329252f;


            public static int Clamp(int min, int max, int value)
            {
                if (value < min) value = min;
                else if (value > max) value = max;
                return value;
            }
            public static float Clamp(float min, float max, float value)
            {
                if (value < min) value = min;
                else if (value > max) value = max;
                return value;
            }
        }
    }
}