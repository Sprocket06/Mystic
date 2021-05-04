using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mystic.Physics
{
    struct Collision
    {
        public Collision(Collider c, Manifold m)
        {
            this.collider = c;
            this.collisionInfo = m;
        }
        public Collider collider;
        public Manifold collisionInfo;
    }
}
