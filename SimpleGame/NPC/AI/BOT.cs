using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleGame.NPC
{


    public class BOT : Base
    {

        public int JUMP_WEIGHT = 7;
        public const int JUMP_DEFAULT_WEIGHT = 7;
        public int JUMP_SPEED = 60;


        public bool isFalling = false;

        public Direction playerDirect = Direction.NONE;

        public int jumpstep = 0;
        public bool jump = false;

        public BOT()
        {
            lastx = X;
            lasty = Y;
            playerDirect = Direction.NONE;
            npcChar = "V";
            npcColor = ConsoleColor.Green;
        }
        public void Teleport(int x, int y, bool likenew)
        {
            X = x;
            Y = y;

            if (X <= 0)
                X = 0;
            else if (X >= 100)
                X = 99;

            if (Y <= 0)
                Y = 0;

            ConsolePaint.Paint(lastx, lasty, ConsolePaint.DEFAULT_BACKGROUND, ConsolePaint.DEFAULT_FOREGROUND, " ");


            lastx = X;
            lasty = Y;
            ConsolePaint.Paint(X, Y, ConsolePaint.DEFAULT_BACKGROUND, npcColor, npcChar);

            Program.World.setTile(lastx, lasty, trailType);

            lasty = Y;
            lastx = X;

            Program.World.setTile(X, Y, Tile.ObjectId);

            if (likenew)
            {
                playerDirect = Direction.NONE;
            }

        }
        public void Jump()
        {
            try
            {
                if (inGravity)
                {
                    if (Program.World.getTile(X, Y - 1) == Tile.NoneId)
                    {
                        if (!Program.emptyCheck(X, Y + 1))
                        {
                            jump = true;
                            jumpstep = JUMP_WEIGHT;
                        }
                    }
                }
                else
                {
                    if (Program.World.getTile(X, Y - 1) == Tile.NoneId)
                    {
                        Y--;
                    }
                }
            }
            catch
            {

            }
        }
        public void Kill()
        {
            SoundCore.Play("PlayerKilled");
            npcChar = "";
        }
        public void ProcessPlayer()
        {

            try
            {

                if (Program.World.panels[4].X <= X && Program.World.panels[4].Y - 1 == Y)
                {
                    Thread.Sleep(200);
                    Program.World.Move();
                    int _x = 0;
                    _x = Program.Bot.X - Program.World.panels[4].X;
                   
                    Teleport(Program.World.panels[3].X + _x, Program.Bot.Y, false);
                }

                if (jump && jumpstep != 0)
                {
                    Thread.Sleep(40);
                    if (jumpstep != 0)
                    {
                        if (Program.World.getTile(X, Y - 1) == Tile.NoneId)
                        {
                            JUMP_SPEED += 10;
                            Y -= 1;
                            jumpstep--;
                        }
                        else
                            jumpstep = 0;

                        switch (playerDirect)
                        {
                            case Direction.NONE:
                                break;
                            case Direction.RIGHT:
                                if (Program.World.getTile(X + 1, Y) == Tile.NoneId)
                                    X++;
                                break;
                            case Direction.LEFT:
                                if (Program.World.getTile(X - 1, Y) == Tile.NoneId)
                                    X--;
                                break;
                        }

                        return;
                    }
                }
                else
                {
                    jump = false;
                }

                try
                {
                    if (inGravity)
                    {
                        if (Program.World.getTile(X, Y + 1) == Tile.NoneId)
                        {
                            isFalling = true;
                            Y++;
                            Thread.Sleep(Program.GRAVITY);
                            switch (playerDirect)
                            {
                                case Direction.NONE:
                                    break;
                                case Direction.RIGHT:
                                    if (Program.World.getTile(X + 1, Y) == Tile.NoneId)
                                        X++;
                                    break;
                                case Direction.LEFT:
                                    if (Program.World.getTile(X - 1, Y) == Tile.NoneId)
                                        X--;
                                    break;
                            }

                            return;
                        }
                        else if (isFalling)
                        {
                            isFalling = false;
                        }


                        JUMP_SPEED = 0;
                    }
                }
                catch
                {
                    X = Program.Player.X;
                    Y = Program.Player.Y;
                    this.Process();
                }
                Thread.Sleep(Program.FRAME_RATE);
            }
            catch
            {
                jump = false;

            }

        }
        public void Move(Direction direct)
        {
            switch (direct)
            {
                case Direction.NONE:
                    break;
                case Direction.RIGHT:
                    if (Program.World.getTile(X + 1, Y) == Tile.NoneId)
                        X++;
                    break;
                case Direction.LEFT:
                    if (Program.World.getTile(X - 1, Y) == Tile.NoneId)
                        X--;
                    break;
            }
        }
        public override void Main()
        {
           // Program.Player.inGravity = false;
            Direction AI_Direction = Direction.NONE;
            while (true)
            {
              

                try
                {

                        if (isFalling)
                        {

                            bool was = false;
                            try
                            {
                                for (int i = 0; i < 30; i++)
                                {
                                    if (!was)
                                        was = Program.World.getTile(X, Y + i) != Tile.NoneId;
                                }
                            }
                            catch
                            {

                            }

                            if (was)
                            {
                                playerDirect = Direction.NONE;
                                AI_Direction = Direction.NONE;
                            }
                            else
                            {
                                playerDirect = Direction.LEFT;
                                AI_Direction = Direction.LEFT;
                            }


                        }
                        else
                        {
                            playerDirect = Direction.RIGHT;
                            AI_Direction = Direction.RIGHT;
                        }

                        Move(AI_Direction);

                        if (Program.World.getTile(X + 1, Y + 1) == Tile.NoneId)
                        {
                            Jump();
                            Thread.Sleep(200);
                            continue;
                        }
                    else
                    {

                    }
                    Thread.Sleep(200);
                }
                catch
                {
                    X = Program.Player.X;
                    Y = Program.Player.Y;
                }

            }

        }
    }
}
