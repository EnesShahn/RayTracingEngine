namespace SimpleRayTracingEngine
{
	abstract class Mesh : Component
	{
		protected Color01 color;

		public Mesh(Color01 color)
		{
			this.color = color;
		}

		public abstract void Intersect(Ray ray, Hit hit, float tmin);
	}
}