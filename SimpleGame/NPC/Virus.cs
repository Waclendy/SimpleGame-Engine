using System;
using System.Threading;

namespace SimpleGame.NPC
{
    public class Virus : Base
    {
        public Virus()
        {
            X = 0;
            Y = 0;
            npcChar = ".";
            npcColor = ConsoleColor.DarkRed;
            NPC_WALKABLE = true;
            NPC_SPEED = 40;
            Draw();
        }

        public override void Main()
        {

            inProcess = true;


            Program.World.setTile(X, Y, TileType.Object);

            Random rnd = new Random();
            int direct = rnd.Next(0, 4);

            switch (direct)
            {
                case 0:
                        X--;
                    break;

                case 1:
                        X++;
                    break;

                case 2:
                        Y--;
                    break;

                case 3:
                        Y++;
                    break;
            }

            Thread.Sleep(NPC_SPEED);

            //Обязательный параметр
            inProcess = false;



        }
    }
}
