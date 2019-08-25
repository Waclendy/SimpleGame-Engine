using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static SimpleGame.Tile;
using static SimpleGame.Misc;
using static SimpleGame.GAME;
using static SimpleGame.Program;

namespace SimpleGame {
    public static partial class Misc {

        public static bool chance(float perc) => (Program.Random.Next(0, 101) < perc);
        public static float fromPerc(float defaultint, float perc) => ((defaultint / 100) * perc);

    }
}
