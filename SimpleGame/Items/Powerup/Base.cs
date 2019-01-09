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
        protected int gameLife;
        public int X;
        public int Y;

        protected bool spawnSoundEnabled = true;

        public bool isPickedUp = false;

        public void Spawn(int x, int y)
        {

            X = x;
            Y = y;

            if (X <= 0)
                X = 0;
            else if (X >= 100)
                X = 99;

            if (Y <= 0)
                Y = 0;
            if(spawnSoundEnabled)
            SoundCore.Play("Item Spawned");

            Draw();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
        }
        protected virtual void Draw()
        {
            if (!isPickedUp)
            {

                ConsolePaint.Paint(X, Y, ConsolePaint.DEFAULT_BACKGROUND, artifactColor, artifactChar);
            }

        }
        public void PickUp()
        {
            gameLife = Program.loseCounter;
            isPickedUp = true;

            Processor = new Thread(_pickup);
            Processor.Start();
        }
        protected virtual void _pickup()
        {

        }
        public void Update()
        {
            
            Draw();

            if (X == Program.Player.X && Y == Program.Player.Y && !isPickedUp)
            {
                PickUp();
            }
        }
    }
}