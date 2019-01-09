using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleGame.NPC
{
    public class playerEventArgs : EventArgs
    {
        public playerEventArgs(int playerX, int playerY)
        {
            this.playerX = playerX;
            this.playerY = playerY;
        }
        public playerEventArgs(Player player)
        {
            this.playerX = player.X;
            this.playerY = player.Y;
        }
        public int playerX { get; private set; }
        public int playerY { get; private set; }
    }
    public delegate void playerEvent(object sender, playerEventArgs eventArgs);

    public class Player : Base
    {


        public event playerEvent playerFall = null;
        public event playerEvent playerOnMiddle = null;
        public event playerEvent playerJump = null;
        public event playerEvent playerTeleportUse = null;
        public event playerEvent playerFallSuccess = null;
        public int JUMP_WEIGHT = 7;
        public const int JUMP_DEFAULT_WEIGHT = 7;
        public int JUMP_SPEED = 60;
        public bool isFalling = false;
        public Direction playerDirect;
        public Direction direct = Direction.NONE;
        public int jumpstep = 0;
        public bool jump = false;

        public void _playerfall()
        {
            playerFall.Invoke(this, new playerEventArgs(this));
        }
        public Player()
        {
            lastx = X;
            lasty = Y;
            playerDirect = Direction.NONE;
            npcChar = "X";
            npcColor = ConsoleColor.Cyan;
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

            Program.World.setTile(X, Y, TileType.Object);

            if(likenew)
            {
                playerDirect = Direction.NONE;
            }

            playerTeleportUse.Invoke(this, new playerEventArgs(this));
        }
        public void Jump()
        {
            try
            {
                if (inGravity)
                {
                    if (Program.World.getTile(X, Y - 1) == TileType.None)
                    {
                        if (!Program.emptyCheck(X, Y + 1))
                        {
                            jump = true;
                            jumpstep = JUMP_WEIGHT;
                            playerJump.Invoke(this, new playerEventArgs(this));
                        }
                    }
                }
                else
                {
                    if (Program.World.getTile(X, Y - 1) == TileType.None)
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
                if (Program.World.chunks[X][Y - 1] != null) ;
            }
            catch
            {
                playerFall.Invoke(this, new playerEventArgs(this));
            }

            for(int i = 4; i <Program.World.panels.Count; i ++)
            if (Program.World.panels[i].X <= X && Program.World.panels[i].Y - 1 == Y)
            {
                playerOnMiddle.Invoke(this, new playerEventArgs(this));
            }
           

            try
            {

                if (jump && jumpstep != 0)
                {
                    Thread.Sleep(40);
                    if (jumpstep != 0)
                    {
                        if (Program.World.getTile(X, Y - 1) == TileType.None)
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
                                if (Program.World.getTile(X + 1, Y) == TileType.None)
                                    X++;
                                break;
                            case Direction.LEFT:
                                if (Program.World.getTile(X - 1, Y) == TileType.None)
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
                        if (Program.World.getTile(X, Y + 1) == TileType.None)
                        {
                            isFalling = true;
                            Y++;
                            Thread.Sleep(Program.GRAVITY);
                            switch (playerDirect)
                            {
                                case Direction.NONE:
                                    break;
                                case Direction.RIGHT:
                                    if (Program.World.getTile(X + 1, Y) == TileType.None)
                                        X++;
                                    break;
                                case Direction.LEFT:
                                    if (Program.World.getTile(X - 1, Y) == TileType.None)
                                        X--;
                                    break;
                            }

                            return;
                        }
                        else if (isFalling)
                        {
                            playerFallSuccess.Invoke(this, new playerEventArgs(this));
                            isFalling = false;
                        }


                        JUMP_SPEED = 0;
                    }
                }
                catch
                {
                    playerFall.Invoke(this, new playerEventArgs(this));
                }
                Thread.Sleep(Program.FRAME_RATE);
            }
            catch
            {
                jump = false;
            }
        }
    }
}
