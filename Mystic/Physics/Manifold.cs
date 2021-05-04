using System;
using System.Numerics;

namespace Mystic.Physics
{
	public class Manifold
	{
		public int count;
		public float[] depths;
		public Vector2[] contact_points;
		public Vector2 normal;

		public Manifold()
		{
			count = 0;
			depths = new float[2];
			contact_points = new Vector2[2];
			normal = new Vector2();
		}
	}
}