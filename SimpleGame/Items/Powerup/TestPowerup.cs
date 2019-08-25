using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleGame.Items.Powerup
{
    public class TestPowerup : Base
    {
        
        public TestPowerup(int x, int y)
        {
            artifactColor = ConsoleColor.Yellow;
            artifactChar = "?";
            _this.spawnSoundEnabled = true;
            _this.type = Type.ENT_ITEM;
            _this.item = Item.IT_TEST_POWERUP;
            _this.oEvent = Event.EV_ITEM_SPAWN;
            _this.X = x;
            _this.Y = y;
            X = x;
            Y = y;
        }

        protected override void _pickup()
        {
            int choice = Program.Random.Next(0, 2);
            if (choice == 1) {
                SoundCore.Play("Drink");
                if (!Wait(2, false))
                    return;
                SoundCore.Play("Transformation");
                Program.Player.JUMP_WEIGHT = 15;
                Program.Player.npcColor = ConsoleColor.DarkMagenta;
                if (!Wait(useTime, true))
                    return;
                SoundCore.Play("Star Falling");
                Program.Player.JUMP_WEIGHT = Program.Player.JUMP_DEFAULT_WEIGHT;
                Program.Player.npcColor = ConsoleColor.Cyan;
            } else {
                SoundCore.Play("Drink");
                if (!Wait(2, false))
                    return;
                SoundCore.Play("Transformation");
                Program.Player.JUMP_WEIGHT = 4;
                Program.Player.npcColor = ConsoleColor.DarkGreen;
                if (!Wait(useTime, true))
                    return;
                SoundCore.Play("Star Falling");
                Program.Player.JUMP_WEIGHT = Program.Player.JUMP_DEFAULT_WEIGHT;
                Program.Player.npcColor = ConsoleColor.Cyan;
            }
        }
    }
}
