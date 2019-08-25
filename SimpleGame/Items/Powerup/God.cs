using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleGame.Items.Powerup
{
    public class God : Base
    {
        
        public God(int x, int y)
        {
            artifactColor = ConsoleColor.Yellow;
            artifactbackColor = ConsoleColor.Black;
            artifactChar = "$";
            useTime = 7.4;
            _this.spawnSoundEnabled = false;
            _this.type = Type.ENT_ITEM;
            _this.item = Item.IT_GOD;
            _this.oEvent = Event.EV_ITEM_SPAWN;
            _this.X = x;
            _this.Y = y;
            X = x;
            Y = y;
        }

        protected override void _pickup()
        {
           
            SoundCore.Play("Eat");
            if (!Wait(1.4, false))
                return;
            Program.Clear();
            Program.Player.npcChar = "^";
            Program.lifebonus = 9999;
              if (!Wait(useTime, true))
                        return;
            Program.Clear();
            Program.Player.npcChar = "X";
            Program.lifebonus = 0;
        }
    }
}
