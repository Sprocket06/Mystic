using System;
using System.Collections.Generic;
using System.Numerics;
using static Mystic.cute_c2;

namespace Mystic.Physics
{
    class CircleCollider : Collider
    {
        public override dynamic CollisionShape { get; set; }
        public float Radius { get; set; }

        public CircleCollider(Vector2 pos, float radius)
        {
            Position = pos;
            Radius = radius;
            CollisionShape = new c2Circle(new c2v(pos.X, pos.Y), radius);
        }

        public override event EventHandler<CollisionEventArgs> Collision;

        public override void Move(Vector2 newPos)
        {
            Position = newPos;
            CollisionShape = new c2Circle(new c2v(Position.X, Position.Y), Radius);
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
