namespace EnesShahn
{
    namespace RayTracingEngine
    {
        abstract class Mesh : Component
        {
            protected Material material = new PhongMaterial();

            public abstract void Intersect(Ray ray, Hit hit, float tmin);
        }
    }
}