using System;
using System.Collections.Generic;
using System.Text;

namespace EnesShahn
{
    namespace RayTracingEngine
    {
        class Hit
        {
            private float tCurrent = float.MaxValue;
            private Material material;
            private bool intersection;
            private Vector3 normal;
            private Vector3 point;
            private int id = -1;

            public float TCurrent
            {
                get { return tCurrent; }
                set { tCurrent = value; }
            }
            public Material Material
            {
                get { return material; }
                set { material = value; }
            }
            public Vector3 Normal
            {
                get { return normal; }
                set { normal = value; }
            }

            public bool Intersection
            {
                get { return intersection; }
                set { intersection = value; }
            }
            public Vector3 Point
            {
                get { return point; }
                set { point = value; }
            }
            public int ID
            {
                get { return id; }
                set { id = value; }
            }
        }
    }
}