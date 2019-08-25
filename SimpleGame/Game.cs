using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using static SimpleGame.Tile;
using static SimpleGame.Misc;
using static SimpleGame.GAME;
using static SimpleGame.Program;

namespace SimpleGame
{
    public class Game
    {
        stimer Timer = new stimer(-1, 0, 4, 41);
       // internal stimer EscapeTimer = new stimer(2, 24, 0, 0);
        bool showscoreplum = true;
        int bestscore = 73;
        public Game() {
           // Timer.Start();
            Timer.TimeEnd += BestTimeEnd;
        }
        public void BestTimeEnd() {
            if (ScorePoints < bestscore) {
                SoundCore.Play("Error");
                showscoreplum = false;
            } else {
                SoundCore.Play("Error");
                showscoreplum = false;
            }
        }
        private void _update() {
            Program.shop.Process();
        }
        public void Update()
        {
            Thread thrd = new Thread(_update);
            if(!shop.processing)
            thrd.Start();

            shop.Draw();
            shop.Update();
            if (Program.Pause)
                    return;

                Program.World.Update();
                Player.Update();
                Player.ProcessPlayer();

            if (!EscapeMode) {
                ConsolePaint.Paint(World.CHUNK_X / 2 - 5, 0, ConsoleColor.Black, ConsoleColor.Yellow, "ОЧКИ: " + ScorePoints);
                if (showscoreplum) {
                    ConsolePaint.Paint(World.CHUNK_X / 2 - 13, 1, ConsoleColor.Black, ConsoleColor.Yellow, "ЛУЧШЕЕ ВРЕМЯ: " + Timer.Lable);
                    ConsolePaint.Paint(World.CHUNK_X / 2 - 7, 2, ConsoleColor.Black, ConsoleColor.Yellow, "РЕКОРД: " + bestscore);
                }
            } else {
               // string text = "ВРЕМЯ ДО ПОБЕГА: " + EscapeTimer.Lable;
               // ConsolePaint.Paint(World.CHUNK_X / 2 - text.Length, 0, ConsoleColor.Black, ConsoleColor.Yellow, text);
            }

            try
                {
                    foreach (Items.Powerup.Base basic in Program.Reluics)
                    {
                        basic.Update();
                    }
                }
                catch
                {

                }
            }
        

    }
}
