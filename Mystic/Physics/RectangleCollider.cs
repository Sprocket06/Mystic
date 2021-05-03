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

        public override event EventHandler<CollisionEventArgs> Collision;

        public override void Move(Vector2 newPos)
        {
            Position = newPos;
            CollisionShape = new c2AABB(new c2v(Position.X, Position.Y), new c2v(Position.X + Size.X, Position.Y + Size.Y));
            List<Collider> Collisions = new List<Collider>();
            foreach (Collider shape in CollisionManager.Instance.Colliders)
            {
                if (Object.ReferenceEquals(shape, this)) continue;
                if (IsIntersecting(shape))
                {
                    Collisions.Add(shape);
                }
            }
            if (Collisions.Count != 0)
            {
                Collision?.Invoke(this, new CollisionEventArgs(Collisions));
            }
        }
    }
}
