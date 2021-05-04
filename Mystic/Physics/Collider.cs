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

        public bool IsIntersecting(Collider c, ref Manifold manifold)
        {
            c2Manifold m = new();
            if (this is RectangleCollider)
            {
                if (c is CircleCollider)
                {
                    c2CircletoAABBManifold(c.CollisionShape, this.CollisionShape, ref m);
                }
                else if (c is RectangleCollider)
                {
                    c2AABBtoAABBManifold(c.CollisionShape, this.CollisionShape, ref m);
                }
            }
            else if (this is CircleCollider)
            {
                if (c is CircleCollider)
                {
                    c2CircletoCircleManifold(this.CollisionShape, c.CollisionShape, ref m);
                }
                else if (c is RectangleCollider)
                {
                    c2CircletoAABBManifold(this.CollisionShape, c.CollisionShape, ref m);
                }
            }
            else
            {
                throw new Exception("Unsupported Collider Type");
            }
            //fuck

            unsafe
            {
                manifold.count = m.count;
                if(m.count != 0)
                {
                    manifold.depths[0] = m.depths[0];
                    manifold.depths[1] = m.depths[1];
                    manifold.contact_points[0] = new Vector2(m.contact_points[0].x, m.contact_points[0].y);
                    manifold.contact_points[1] = new Vector2(m.contact_points[1].x, m.contact_points[1].y);
                    manifold.normal = new Vector2(m.n.x, m.n.y);
                }
               
            }
            return m.count != 0;
        }

        public abstract event EventHandler<CollisionEventArgs> Collision;

    }
}
