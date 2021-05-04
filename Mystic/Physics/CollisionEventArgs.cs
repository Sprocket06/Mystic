using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mystic.Physics
{
    class CollisionEventArgs : EventArgs
    {
        public List<Collision> collisions;
        public CollisionEventArgs(List<Collision> c)
        {
            collisions = c;
        }
    }
}
