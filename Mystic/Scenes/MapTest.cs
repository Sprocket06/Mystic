using Chroma.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mystic.World;
using System.Numerics;

namespace Mystic.Scenes
{
    class MapTest : Scene
    {
        private Map gameWorld;
        private Dictionary<int, Color> tileColorMap;

        public MapTest()
        {
            tileColorMap = new Dictionary<int, Color>
            {
                [0] = Color.White
            };
            gameWorld = new Map(new Grid(10,10), tileColorMap, 32);
        }

        public override void Draw(RenderContext context)
        {
            gameWorld.Render(context, new Vector2(0, 0));
        }
    }
}
