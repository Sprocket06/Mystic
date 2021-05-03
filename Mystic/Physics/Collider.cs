using System;
using static Mystic.cute_c2;
using System.Numerics;
using System.Collections.Generic;

namespace Mystic.Physics
{
    abstract class Collider
    {
        public Vector2 Position { get; set; }

        public abstract void Move(Vector2 newPos);
        public abstract dynamic CollisionShape { get; set; }

        public Collider()
        {
            CollisionManager.Instance.Register(this);
        }

        public bool IsIntersecting(Collider c)
        {
            bool result = false;
            if (this is RectangleCollider)
            {
                if (c is CircleCollider)
                {
                    return c2CircletoAABB(c.CollisionShape, this.CollisionShape);
                }
                else if (c is RectangleCollider)
                {
                    result = c2AABBtoAABB(c.CollisionShape, this.CollisionShape);
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
            return result;
            throw new Exception("Unsupported Collider Type");
        }

        public abstract event EventHandler<CollisionEventArgs> Collision;

    }
}
