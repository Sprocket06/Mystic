using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using Chroma.Graphics;
using Mystic.Physics;

namespace Mystic.World
{
    class Map
    {
        public Vector2 DrawPosition { get; set; }

        private Grid TileGrid;
        private Dictionary<int, Color> ColorMap;
        private int TileSize;
        private List<Collider> Colliders;
        private Dictionary<int, bool> TileCollisionMap;

        public Map(Grid tileMap,Dictionary<int, bool> tileCollisionMap, Dictionary<int, Color> colorMap, int tileSize)
        {
            TileGrid = tileMap;
            ColorMap = colorMap;
            TileSize = tileSize;
            TileCollisionMap = tileCollisionMap;
            Colliders = new();
        }

        public void Render(RenderContext ctx, Vector2 pos)
        {
            for (int y = 0; y < TileGrid.Size.Height; y++)
            {
                for (int x = 0; x < TileGrid.Size.Width; x++)
                {
                    Vector2 tilePos = new((x * TileSize), (y * TileSize));
                    tilePos += pos;
                    ctx.Rectangle(ShapeMode.Fill, tilePos, TileSize, TileSize, ColorMap[TileGrid[x, y]]);
                    //ctx.Rectangle(ShapeMode.Stroke, tilePos, TileSize, TileSize, Color.Green);
                }
            }
            Dictionary<int, bool> collisionTiles = new()
            {
                [1] = false,
                [0] = true
            };
            List<GridRect> collisionRects = GenerateColliders(collisionTiles);
            foreach (GridRect r in collisionRects)
            {
                ctx.Rectangle(ShapeMode.Stroke, (new Vector2(r.x, r.y) * TileSize) + pos, r.width, r.height, Color.Red);
            }
        }

        public void DrawRect(Vector2 pos, Vector2 size)
        {
            for (float y = pos.Y; y < pos.Y + size.Y; y++)
            {
                for (float x = pos.X; x < pos.X + size.X; x++)
                {   
                    if (x > TileGrid.Size.Width || y > TileGrid.Size.Height)
                    {
                        throw new System.Exception("Out of bounds error");
                    }
                    TileGrid[(int)x, (int)y] = 1;
                }
            }
        }

        private List<GridRect> GenerateColliders(Dictionary<int, bool> collisionTiles)
        {
            List<GridRect> combinedRects = new();
            GridRect currentRect = new(0, 0, 0, 0);

            for (int y = 0; y < TileGrid.Size.Height; y++)
            {
                for (int x = 0; x < TileGrid.Size.Width; x++)
                { //iterate through all tiles, top->bottom left->right
                    int tileID = TileGrid[x, y];
                    if (collisionTiles[tileID])
                    { //the tile we are looking at is collidable
                        if (currentRect.width != 0)
                        { //we are extending an existing rectangle
                            currentRect.width += 1;
                        }
                        else
                        { //we are making a *new* rectangle 
                            currentRect.x = x;
                            currentRect.y = y;
                            currentRect.width = 1;
                            currentRect.height = 1;
                        }
                    }
                    else
                    { //the tile we are looking at is not collidable
                        if (currentRect.width != 0)
                        { //if we have an existing rectangle, it is time to stick it in the list and prepare for a new one
                            combinedRects.Add(currentRect);
                            currentRect = new(0, 0, 0, 0);
                        }
                    }
                }
                //do not carry rects over between rows
                if (currentRect.width != 0)
                { //if we have an existing rectangle, it is time to stick it in the list and prepare for a new one
                    combinedRects.Add(currentRect);
                    currentRect = new(0, 0, 0, 0);
                }
            }

            //step 1 is complete
            //now, we must combine all rects with same X coord and same width
            List<GridRect> secondPass = new();
            List<GridRect> alreadyProcessed = new();
            foreach(GridRect r in combinedRects)
            {
                GameCore.Log.Info($"RectList: {r.x} {r.y}");
            }
            for (int i = 0; i < combinedRects.Count; i++)
            {
                GridRect rect = combinedRects[i];
                if (alreadyProcessed.Contains(rect))
                {
                    GameCore.Log.Info($"Already processed rect {rect.x} {rect.y}, skipping.");
                    continue;
                }
                GameCore.Log.Info($"Checking {rect.x} {rect.y} for combinability.");
                List<GridRect> combinable = combinedRects.Where<GridRect>(r => (r.x == rect.x && r.width == rect.width) && !alreadyProcessed.Contains(r) ).ToList();
                combinable.Sort((a, b) => a.y > b.y ? 1 : -1);
                bool abort = false;
                combinable = combinable.Where<GridRect>((r, i) =>
                {
                    if (abort) return false;
                    if (i != 0)
                    {
                        GameCore.Log.Info($"Current coords {r.x}, {r.y}; Prev Coords {combinable[i - 1].x}, {combinable[i - 1].y}");
                    }
                    if (i != 0 && combinable[i - 1].y != r.y - 1)
                    {
                        abort = true;
                        GameCore.Log.Info("Abort");
                        return false;
                    };
                    return true;
                }).ToList();
                foreach(GridRect r in combinable)
                {
                    GameCore.Log.Info($"{r.x} {r.y}");
                }
                GameCore.Log.Info(combinable.Count);
                foreach(GridRect r in combinable)
                {
                    alreadyProcessed.Add(r);
                }

                if (combinable.Count != 1)
                {
                    
                    int startY = combinable[0].y;
                    GridRect combinedRect = new(combinable[0].x, startY, combinable[0].width, combinable.Count);
                    secondPass.Add(combinedRect);
                }
                else
                {
                    secondPass.Add(combinable[0]);
                }
            }
            return secondPass;
            /*
            //now that we have our optimized rectangles, lets turn them into actual physics objects
            List<RectangleCollider> colliderList = new();
            foreach(GridRect r in secondPass)
            {
                RectangleCollider rect = new(new Vector2(r.x, r.y) * TileSize, new Vector2(r.width, r.height) * TileSize);
                colliderList.Add(rect);
            }

            return colliderList;
            */
        }

        private struct GridRect
        {
            public GridRect(int xP, int yP, int w, int h)
            {
                x = xP;
                y = yP;
                width = w;
                height = h;
            }
            public int x;
            public int y;
            public int width;
            public int height;
        }
        /* Collision Optimization algorithm
         * - Start with your grid
         * - Iterate over the grid, and combine all tiles that are horizontally adjacent
         * Combinaion {}
         * - Iterate over the combined tiles, and combine all groups that are vertically adjacent AND the same width
         * 
         */
    }
}
