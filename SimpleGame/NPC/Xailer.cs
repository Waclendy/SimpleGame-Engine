using System;
using System.Threading;

namespace SimpleGame.NPC
{
    public class Xailer : Base
    {
        bool direction = true;
        public Xailer()
        {
            X = 0;
            Y = 0;
            npcChar = "*";
            npcColor = ConsoleColor.Green;
            NPC_WALKABLE = true;
            NPC_SPEED = 70;
            Draw();
            _this.type = Type.ENT_GLOBAL;
        }

        public override void Main()
        {
            base.Main();

            if (Walking && NPC_WALKABLE)
            {

                switch (direction)
                {
                    case true:
                        if(Program.emptyCheck(X - 1, Y))
                        {
                            X--;
                            if (!Program.emptyCheck(X - 1, Y))
                                direction = false;
                        }
                        break;

                    case false:
                        if (Program.emptyCheck(X + 1, Y))
                        {
                            X++;
                            if (!Program.emptyCheck(X + 1, Y))
                                direction = true;
                        }
                        break;

                    default:
                        break;
                }

                Thread.Sleep(NPC_SPEED);
            }

            //Обязательный параметр
            inProcess = false;

        }

    }
}
