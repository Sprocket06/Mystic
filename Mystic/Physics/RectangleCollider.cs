using System;
using System.Collections.Generic;
using System.Numerics;
using static Mystic.cute_c2;

namespace Mystic.Physics
{
    class RectangleCollider : Collider
    { 
        public override dynamic CollisionShape { get; set; }
        public Vector2 Size { get; set; }

        public RectangleCollider(Vector2 pos, Vector2 size)
        {
            Position = pos;
            Size = size;
            CollisionShape = new c2AABB(new c2v(pos.X, pos.Y), new c2v(pos.X + size.X, pos.Y + size.Y));
        }
    }
}
