using System.Collections.Generic;
using System.Numerics;
using Chroma.Graphics;

namespace Mystic.World
{
    class Map
    {
        private Grid TileGrid;
        private Dictionary<int, Color> ColorMap;
        private int TileSize;

        public Map(Grid tileMap, Dictionary<int, Color> colorMap, int tileSize)
        {
            TileGrid = tileMap;
            ColorMap = colorMap;
            TileSize = tileSize;
        }

        public void Render(RenderContext ctx, Vector2 pos)
        {
            for(int y = 0; y < TileGrid.Size.Height; y++)
            {
                for(int x = 0; x < TileGrid.Size.Width; x++)
                {
                    Vector2 tilePos = new((x * TileSize), (y * TileSize));
                    ctx.Rectangle(ShapeMode.Fill, tilePos, TileSize, TileSize, ColorMap[TileGrid[x,y]]);
                }
            }
        }
    }
}
