﻿using Chroma.Graphics;
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
                [0] = Color.White,
                [1] = Color.Black
            };
            gameWorld = new Map(new Grid(10,10), tileColorMap, 32);
            gameWorld.DrawRect(new Vector2(2, 2), new Vector2(6, 6));
        }

        public override void Draw(RenderContext context)
        {
            gameWorld.Render(context, new Vector2(10, 10));
        }
    }
}
