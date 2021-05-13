using System.Drawing;

namespace Mystic.World
{
    class Grid
    {
        public Size Size { get; private set; }
        private int[] _grid;

        public int this[int x, int y]
        {
            get
            {
                return _grid[(y * (int)Size.Width) + x];
            }
            set
            {
                _grid[(y * (int)Size.Width) + x] = value;
            }
        }

        public Grid(int width, int height)
        {
            Size = new Size(width, height);
            _grid = new int[width * height];
            for(int i = 0; i < (width * height); i++)
            {
                _grid[i] = 0;
            }
        }
    }
}
