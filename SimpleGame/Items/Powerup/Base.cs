using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleGame.Items.Powerup
{
    public class Base
    {

        public string artifactChar = "@";
        public ConsoleColor artifactColor = ConsoleColor.DarkMagenta;
        protected Thread Processor;

        protected int useTime = 5 * 1000;
        public int X;
        public int Y;
        public entity _this;

        protected bool spawnSoundEnabled = true;

        public bool isPickedUp = false;


        protected virtual void Draw()
        {
            if (!isPickedUp)
            {

                ConsolePaint.Paint(X, Y, ConsolePaint.DEFAULT_BACKGROUND, artifactColor, artifactChar);
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