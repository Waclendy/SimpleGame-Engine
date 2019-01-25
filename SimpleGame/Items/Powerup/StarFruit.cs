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
            _this.type = Type.ENT_ITEM;
            _this.item = Item.IT_STAR_FRUIT;
            _this.oEvent = Event.EV_ITEM_SPAWN;
            _this.X = x;
            _this.Y = y;
            X = x;
            Y = y;
        }

        protected override void _pickup()
        {

                SoundCore.Play("Eat");
                Thread.Sleep(2000);
                SoundCore.Play("Transformation");
                Program.Player.npcColor = ConsoleColor.Green;
                Program.StarFruitEnabled = true;
                Thread.Sleep(useTime);

                SoundCore.Play("Star Falling");
                Program.Player.npcColor = ConsoleColor.Cyan;
                Program.StarFruitEnabled = false;
           
        }
    }
}
