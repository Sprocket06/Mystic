using System;
using System.Collections.Generic;
using System.Numerics;
using static Mystic.cute_c2;

namespace Mystic.Physics
{
    class CircleCollider : Collider
    {
        public override dynamic CollisionShape { get; set; }

        public CircleCollider(Vector2 pos, float radius)
        {
            Position = pos;
            CollisionShape = new c2Circle(new c2v(pos.X, pos.Y), radius);
        }
    }
}
