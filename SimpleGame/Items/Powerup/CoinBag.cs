using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGame.Items.Powerup {
    public class CoinBag : Base {
        public CoinBag(int x, int y) {

            artifactColor = ConsoleColor.Yellow;
            artifactbackColor = ConsoleColor.Black;
            artifactChar = "@";
            useTime = 0;
            _this.spawnSoundEnabled = false;
            _this.type = Type.ENT_ITEM;
            _this.item = Item.IT_COIN;
            _this.oEvent = Event.EV_ITEM_SPAWN;
            _this.X = x;
            _this.Y = y;
            X = x;
            Y = y;
        }

        protected override void _pickup() {

            SoundCore.Play("Coin");
            Program.Coins += 5;
            Program.Coins += Program.coinbonus;
        }
    }
}
