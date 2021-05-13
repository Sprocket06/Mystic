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
                    tilePos += pos;
                    ctx.Rectangle(ShapeMode.Fill, tilePos, TileSize, TileSize, ColorMap[TileGrid[x,y]]);
                }
            }
        }

        public void DrawRect(Vector2 pos, Vector2 size)
        {
            for(float y = pos.Y; y < pos.Y + size.Y; y++)
            {
                for(float x = pos.X; x < pos.X + size.X; x++)
                {
                    if(x > TileGrid.Size.Width || y > TileGrid.Size.Height)
                    {
                        throw new System.Exception("Out of bounds error");
                    }
                    TileGrid[(int)x, (int)y] = 1;
                }
            }
        }
    }
}
