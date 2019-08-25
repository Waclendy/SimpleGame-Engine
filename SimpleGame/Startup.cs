using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Tools;

using static SimpleGame.Tile;
using static SimpleGame.Misc;
using static SimpleGame.GAME;
using static SimpleGame.Program;

namespace SimpleGame {  

   public static class Plugin {
        public const string DIR = @"D:\Plugins\";
        public static IModTile[] modTiles;
        public static IModShop[] modShops;
        public static void Load() {
            foreach (string dll in Directory.GetFiles(DIR)) {
                try { 
                AssemblyConverter assembly = new AssemblyConverter(dll);
                modTiles = assembly.GetInterfaces<IModTile>();
                modShops = assembly.GetInterfaces<IModShop>();
                } catch { continue; }
            }
        }
    }
   public static class Startup {
        public static void OnStart() {
            Plugin.Load();
            if(Plugin.modTiles != null)
           foreach(IModTile tile in Plugin.modTiles) {
                GAME_AddNewTileType(tile.NewTile);
           }
        }
    }
}
