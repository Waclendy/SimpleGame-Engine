using System;

namespace SimpleGame
{
    public class Panel
    {

        public int X = 0;
        public int Y = 0;
        public int size = 0;
        public TileType tileType = TileType.None;

        public Panel()
        {
        }

        public Panel(int x, int y, int size, TileType tileType)
        {
            X = x;
            Y = y;
            this.size = size;
            this.tileType = tileType;
        }
    }
}
