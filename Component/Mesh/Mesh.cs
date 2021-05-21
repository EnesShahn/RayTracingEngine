namespace SimpleRayTracingEngine
{
	abstract class Mesh : Component
	{
		protected Color01 color = new Color01(1, 1, 1, 1);

		public abstract void Intersect(Ray ray, Hit hit, float tmin);
	}
}