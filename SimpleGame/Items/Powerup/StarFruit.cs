using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleGame.Items.Powerup
{
    public class StarFruit : Base
    {
        
        public StarFruit(int x, int y)
        {
            artifactColor = ConsoleColor.Green;
            artifactChar = "*";
            useTime = 8 * 1000;
            Spawn(x, y);
        }

        protected override void _pickup()
        {

                SoundCore.Play("Eat");
                Thread.Sleep(2000);
                SoundCore.Play("Transformation");
                Program.Player.npcColor = ConsoleColor.Green;
                Program.StarFruitEnabled = true;
                Thread.Sleep(useTime);

            if (gameLife != Program.loseCounter)
                return;

                SoundCore.Play("Star Falling");
                Program.Player.npcColor = ConsoleColor.Cyan;
                Program.StarFruitEnabled = false;
           
        }
    }
}
