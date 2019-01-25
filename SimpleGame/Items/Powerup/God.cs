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
            artifactColor = ConsoleColor.DarkRed;
            artifactChar = "x";
            useTime = 8 * 1000;
            spawnSoundEnabled = false;
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
            Thread.Sleep(1400);
            Program.Clear();
            Program.Player.npcColor = ConsoleColor.Red;
            Program.Player.npcChar = "E";
            Program.fastRespawn = true;
            Thread.Sleep(5400);
            Program.Clear();
            Program.Player.npcColor = ConsoleColor.Cyan;
            Program.Player.npcChar = "X";
            Program.fastRespawn = false;
        }
    }
}
