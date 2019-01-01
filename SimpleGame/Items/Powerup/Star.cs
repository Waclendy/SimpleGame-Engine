using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleGame.Items.Powerup
{
    public class Star : Base
    {
        
        public Star(int x, int y)
        {
            artifactColor = ConsoleColor.Red;
            artifactChar = ".";
            useTime = 8 * 1000;
            spawnSoundEnabled = false;
            Spawn(x, y);
        }

        protected override void _pickup()
        {

            SoundCore.Play("Eat");
            Effects.StarEffect starEffect = new Effects.StarEffect();
            starEffect.Enable(7.4);
        }
    }
}
