using System;
using static Mystic.cute_c2;
using System.Numerics;
using System.Collections.Generic;

namespace Mystic.Physics
{
    abstract class Collider
    {
        public Vector2 Position { get; set; }

        public void Move(Vector2 newPos)
        {
            Position = newPos;
            List<Collider> Collisions = new List<Collider>();
            foreach (Collider shape in CollisionManager.Instance.Colliders)
            {
                if (Object.ReferenceEquals(shape, this)) continue;
                if (IsIntersecting(shape))
                {
                    Collisions.Add(shape);
                }
            }
            if(Collisions.Count != 0)
            {
                Collision?.Invoke(this, new CollisionEventArgs(Collisions));
            }
        }
        public abstract dynamic CollisionShape { get; set; }

        public Collider()
        {
            CollisionManager.Instance.Register(this);
        }

        public bool IsIntersecting(Collider c)
        {
            if (this is RectangleCollider)
            {
                if (c is CircleCollider)
                {
                    return c2CircletoAABB(c.CollisionShape, this.CollisionShape);
                }
                else if (c is RectangleCollider)
                {
                    return c2AABBtoAABB(c.CollisionShape, this.CollisionShape);
                }
            }
            else if (this is CircleCollider)
            {
                if (c is CircleCollider)
                {
                    return c2CircletoCircle(this.CollisionShape, c.CollisionShape);
                }
                else if (c is RectangleCollider)
                {
                    return c2CircletoAABB(this.CollisionShape, c.CollisionShape);
                }
            }
            throw new Exception("Unsupported Collider Type");
        }

        public event EventHandler<CollisionEventArgs> Collision;

    }
}
