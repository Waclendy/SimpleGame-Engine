using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleGame.Items.Powerup
{
    public class Uran : Base
    {
        
        public Uran(int x, int y)
        {
            artifactColor = ConsoleColor.DarkMagenta;
            artifactChar = "#";
            useTime = 0;
            _this.spawnSoundEnabled = false;
            _this.type = Type.ENT_ITEM;
            _this.item = Item.IT_URAN;
            _this.oEvent = Event.EV_ITEM_SPAWN;
            _this.X = x;
            _this.Y = y;
            X = x;
            Y = y;
            Misc.vrMenu(Element.EL_MOD_IMPORT_URAN);
        }

        protected override void _pickup()
        {
            // Misc.vrMenu(Element.EL_MOD_IMPORT_URAN_USAGE);
            SoundCore.Sounds["Error"].Play();
        }
    }
}
