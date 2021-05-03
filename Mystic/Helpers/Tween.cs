using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mystic.Helpers
{
    class Tween
    {
        public Action<double> Callback;
        public Action<float> EasingFunc;
        public double start;
        public double end;
        public int millis;
        public Tween(Action<double> callback, Action<float> easingFunc, double start, double end, int millis)
        {

        }

        void Update(float dt)
        {

        }
    }
}
