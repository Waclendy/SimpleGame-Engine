using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleGame;

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
        public ivector lastPos = new ivector();
        public int JUMP_WEIGHT = 7;
        public int JUMP_DEFAULT_WEIGHT = 7;
        public int JUMP_SPEED = 50;
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
            _this.type = Type.ENT_PLAYER;
            _this.oEvent = Event.EV_NONE;
            _this.X = X;
            _this.Y = Y;
        }
        public void Teleport(ivector POS, bool likenew) => Teleport(POS.X, POS.Y, likenew);
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
                    if (Program.World.getTile(X, Y - 1) == Tile.NoneId)
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

            try {

                if (Program.World.getTile(X, Y + 1) != Tile.NoneId) {
                    lastPos = new ivector(X, Y);
                }

                if (Program.World.getTile(lastPos.X, lastPos.Y + 1) == Tile.NoneId) {
                    lastPos = new ivector(Program.World.panels[0].X + 2, Program.World.panels[0].Y - 5);
                }
            } catch { }

            try {

            }
            catch
            {
                playerFall.Invoke(this, new playerEventArgs(this));
            }

            //for(int i = 4; i <Program.World.panels.Count; i ++)
            //if (Program.World.panels[i].X <= X && Program.World.panels[i].Y - 1 <= Y)
            //{
            //    playerOnMiddle.Invoke(this, new playerEventArgs(this));
            //}
            try {
                if (Program.Player.X >= (World.CHUNK_X - 1) / 2 && Program.World.getTile(Program.Player.X, Program.Player.Y + 1) != "0") {
                    playerOnMiddle.Invoke(this, new playerEventArgs(this));
                }
            }
            catch {

            }

            try
            {

                if (jump && jumpstep != 0)
                {
                    Thread.Sleep(JUMP_SPEED);
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
                            playerFallSuccess.Invoke(this, new playerEventArgs(this));
                            isFalling = false;
                        }


                        JUMP_SPEED = 0;
                    }
                }
                catch
                {
                    // playerFall.Invoke(this, new playerEventArgs(this));
                    _this.oEvent = Event.EV_PLAYER_DEAD;
                }
            }
            catch
            {
                jump = false;
            }
        }
    }
}
