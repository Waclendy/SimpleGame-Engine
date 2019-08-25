using System;

using static SimpleGame.Tile;
using static SimpleGame.Misc;
using static SimpleGame.GAME;
using static SimpleGame.Program;

namespace SimpleGame
{
    public class Panel
    {

        public int X = 0;
        public int Y = 0;
        public int size = 0;
        public string tileType = "0";

        public Panel()
        {
        }

        public Panel(int x, int y, int size, string tileType)
        {
            X = x;
            Y = y;
            this.size = size;
            this.tileType = tileType;
        }
    }
}
