using System;
using System.Threading;
using System.Threading.Tasks;


namespace SimpleGame
{
    public class Info : IModInfo {
        public string Name => "Simple Game Plugin Test";

        public string Description => "This is a too Sumple Plugin";
    }
    public class GreenBricks : IModTile {
        public TileStruct NewTile => new TileStruct() {
                Id = "2",
                TileName = "world_bricks_colored",
                Char = " ",
                ForeColor = ConsoleColor.White,
                BackColor = ConsoleColor.Green,
                IsVisible = true,
                CollusionEnabled = true,
        };
    }
    public class OldStyleWalls : IModTile {
        public TileStruct NewTile => new TileStruct() {
                Id = "1",
                TileName = "world_wall_old",
                Char = "#",
                ForeColor = ConsoleColor.White,
                BackColor = ConsoleColor.Black,
                IsVisible = true,
                CollusionEnabled = true,
        };
    }
    public class ShopItem : IModShop {
        public string itemName => "PluginItem";

        public string trueName => "PluginItem";

        public float itemPrice => 0.0f;

        public bool itemExists => true;

        public string itemBuths => ">You should not pass!^";

        public string itemSLabel => "Price";

        public string itemBLabel => "Bought";

        public string itemCLabel => "Coins";

        public string itemDescription => "A item what was imported from mod";

        public Action Use => Buy;

        private void Buy() {
            Console.Beep();
        }
    }
    public class ShopItem2 : IModShop {
        public string itemName => "PluginItem2";

        public string trueName => "PluginItem2";

        public float itemPrice => -0.144141253125f;

        public bool itemExists => true;

        public string itemBuths => "";

        public string itemSLabel => "Price";

        public string itemBLabel => "Bought";

        public string itemCLabel => "Coins";

        public string itemDescription => "A item what was imported from mod";

        public Action Use => Buy;

        private void Buy() {
            ModMisc.Shake(5, true);
        }
    }
}

