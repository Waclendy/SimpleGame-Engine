using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleGame.Items.Powerup
{
    public partial class Base
    {

        public string artifactChar = "@";
        public ConsoleColor artifactColor = ConsoleColor.DarkMagenta;
        public ConsoleColor artifactbackColor = ConsoleColor.Black;
        protected Thread Processor;

        protected double useTime = 5;
        public int X;
        public int Y;
        public entity _this;

        protected bool spawnSoundEnabled = true;

        public bool isPickedUp = false;

        protected bool Wait(double time, bool isUse) {
            int currframe = Program.worldframe;

            if(!isUse)
            Thread.Sleep((int)(time * 1000));
            else
            Thread.Sleep((int)((time + Program.buthbonus) * 1000));

            return currframe == Program.worldframe;
        }
        protected virtual void Draw()
        {
            if (!isPickedUp)
            {

                ConsolePaint.Paint(X, Y, artifactbackColor, artifactColor, artifactChar);
            }

        }
        public entity PickUp()
        {
            isPickedUp = true;

            Processor = new Thread(_pickup);
            Processor.Start();

            return _this;
        }
        protected virtual void _pickup()
        {

        }
        public void Update()
        {
            _this.X = X;
            _this.Y = Y;
            Misc.dispatchEvent(ref _this);
            Draw();
            if (X == Program.Player.X && Y == Program.Player.Y && !isPickedUp)
            {
                PickUp();
            }
        }

    }
}