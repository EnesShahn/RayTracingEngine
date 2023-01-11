using System;
using System.Collections.Generic;
using System.Text;

namespace EnesShahn
{
    namespace RayTracingEngine
    {
        sealed class Sphere : Mesh
        {
            private float radius;
            private float radius2; // Cache to improve speed

            public void Init(float radius, Material material)
            {
                this.radius = radius;
                this.radius2 = radius * radius;
                this.material = material;
            }

            public override void Intersect(Ray ray, Hit hit, float tmin)
            {
                Matrix4x4 modelMatrix = object3D.modelMatrix;
                Matrix4x4 modelInverseMatrix = modelMatrix.GetInverse();

                Vector3 transformedOrigin = Vector3.TransformPoint(ray.Origin, modelInverseMatrix);
                Vector3 transformedDir = Vector3.TransformNormal(ray.Direction, modelInverseMatrix).Normalized;

                Vector3 L = object3D.position - transformedOrigin;
                //if (L.SqrMagnitude < radius2) // Ray inside sphere (Square both sides to get rid of the Square root = Better Performance)
                //	return;
                float tca = Vector3.Dot(L, transformedDir);
                if (tca < 0) // Sphere behind the origin of the ray
                    return;

                float d2 = Vector3.Dot(L, L) - tca * tca;
                if (d2 > radius2)
                    return;

                float thc = (float)Math.Sqrt(radius2 - d2);

                float t1LocalSpace = tca - thc;
                float t2LocalSpace = tca + thc;

                float tLocalSpace = t1LocalSpace;

                if (t1LocalSpace <= tmin)
                    tLocalSpace = t2LocalSpace;

                if (tLocalSpace > 0)
                {
                    //Calculated t (distance) is in object space, we need to get distance in world space.
                    Vector3 pObject = transformedOrigin + transformedDir * tLocalSpace;
                    Vector3 pWorld = Vector3.TransformPoint(pObject, modelMatrix);
                    float t = (pWorld - ray.Origin).Magnitude;

                    if (t > 0 && t > tmin && t < hit.TCurrent)
                    {
                        hit.Intersection = true;
                        hit.Material = material;
                        hit.TCurrent = t;
                        hit.Normal = Vector3.TransformNormal((pObject - object3D.position), modelInverseMatrix.GetTranspose()).Normalized;
                        hit.Point = pWorld;
                        hit.ID = object3D.id;
                    }
                }

            }
        }
    }
}