using System;
using System.Threading;

namespace SimpleGame.NPC
{
    public partial class Base{

        public string npcChar = " ";
        public ConsoleColor npcColor = ConsoleColor.White;
        public bool NPC_WALKABLE = false;
        public int NPC_SPEED = 0;

        public string trailType = "0";

        public bool inGravity = true;

        public entity _this;

        public int X = 0;
        public int Y = 0;

        public int lastx = 2;
        public int lasty = 2;

        public bool Walking = false;

        public bool inProcess = false;

        private Thread Processor;

        public virtual void Draw()
        {

            if (X <= 0)
                X = 0;
            else if (X >= 100)
                X = 99;

            if (Y <= 0)
                Y = 0;


            if (trailType != Tile.NoneId)
                Program.World.setTile(lastx, lasty, trailType);

            ConsolePaint.Paint(lastx, lasty, ConsolePaint.DEFAULT_BACKGROUND, ConsolePaint.DEFAULT_FOREGROUND, " ");
            ConsolePaint.Paint(X, Y, ConsolePaint.DEFAULT_BACKGROUND, npcColor, npcChar);

            lastx = X;
            lasty = Y;
        }
    
        public virtual void Update()
        {
            Draw();
            //X = _this.X;
            //Y = _this.Y;
            _this.X = X;
            _this.Y = Y;
            Misc.dispatchEvent(ref _this);
            
        }

        public virtual void Main()
        {
            if (Program.stopAll)
                return;
            try
            {
                inProcess = true;

                lasty = Y;
                lastx = X;

                Program.World.setTile(X, Y, Tile.ObjectId);

                if(Program.World.getTile(X, Y) != Tile.WallId)
                    if (Program.World.getTile(X, Y) == Tile.NoneId)
                    {
                        Y++;
                        Thread.Sleep(Program.GRAVITY);
                        Walking = false;
                        return;
                    }
                    else
                        Walking = true;
            }
            catch
            {
                inProcess = false;
                npcChar = "";
            }
        }
        public void Process()
        {
            Processor = new Thread(Main);
            Processor.Start();
        }

     

    }
}
