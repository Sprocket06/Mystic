using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mystic.Physics
{
    class CollisionManager
    {
        public static CollisionManager Instance;
        public List<Collider> Colliders;

        static CollisionManager()
        {
            Instance = new CollisionManager();
        }
        public CollisionManager()
        {
            Colliders = new List<Collider>();
        }

        public void Register(Collider c)
        {
            Colliders.Add(c);
        }
        
    }
}
