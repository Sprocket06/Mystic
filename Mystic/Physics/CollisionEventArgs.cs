using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mystic.Physics
{
    class CollisionEventArgs : EventArgs
    {
        public List<Collider> collisions;
        public CollisionEventArgs(List<Collider> c)
        {
            collisions = c;
        }
    }
}
