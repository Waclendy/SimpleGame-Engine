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
            useTime = 8;
            _this.spawnSoundEnabled = false;
            _this.type = Type.ENT_ITEM;
            _this.item = Item.IT_STAR;
            _this.oEvent = Event.EV_ITEM_SPAWN;
            _this.X = x;
            _this.Y = y;
            X = x;
            Y = y;
        }

        protected override void _pickup()
        {

            SoundCore.Play("Eat");
            Effects.StarEffect starEffect = new Effects.StarEffect();
            starEffect.Enable(useTime);
        }
    }
}
