using System;
using System.Collections.Generic;
using System.Text;

namespace EnesShahn
{
    namespace RayTracingEngine
    {
        sealed class PerspectiveCamera : Camera
        {
            private Vector3 horizontal = new Vector3(1, 0, 0);
            private Vector3 vertical = new Vector3(0, 1, 0);
            private Vector3 lowerLeftCorner = new Vector3(0, 0, 0);
            private float scale = 2.0f;

            public void Init(Vector3 direction, Vector3 up, int fov)
            {
                Vector3 right = Vector3.CrossProduct(direction, up).Normalized;
                up = Vector3.CrossProduct(right, direction).Normalized;

                float theta = fov * Mathf.DegreesToRad;

                float h = MathF.Tan(theta / 2);
                float viewport_height = scale * h;
                float viewport_width = viewport_height * Settings.aspectRatio;

                this.vertical = viewport_height * up;
                this.horizontal = viewport_width * right;
                this.lowerLeftCorner = direction - this.horizontal / 2 - this.vertical / 2;
            }

            /// <summary>
            /// Generate Ray that has position = Camera Position and a direction interpolated from the lower left corner to the top right corner of screen port
            /// </summary>
            /// <param name="screenCoord"> Goes form [0,0] (Lower Left Corner) to [1,1] (Top Right Corner)</param>
            /// <returns></returns>
            public override Ray GenerateRay(Vector2 screenCoord)
            {
                Vector3 rayDirection = lowerLeftCorner + (screenCoord.x * horizontal) + (screenCoord.y * vertical);
                return new Ray(object3D.position, rayDirection.Normalized);
            }

        }
    }
}