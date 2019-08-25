using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using static SimpleGame.Tile;
using static SimpleGame.Misc;
using static SimpleGame.GAME;
using static SimpleGame.Program;

namespace SimpleGame {

    public partial class Tile {

        //public enum TileType : int { None = 0,KeyWall = 1, KeyNone = 2, Wall = 3, Bricks = 4, Background = 5, JumpEffectBackground = 6, Spawnpoint = 7, Object = 8, JumpFloor = 9, Ground = 10, GlitchWall = 11}

        public const string NoneId = "0";
        public const string WallId = "1";
        public const string BricksId = "2";
        public const string BackgroundId = "3";
        public const string SpawnpointId = "4";
        public const string ObjectId = "5";
        public const string JumpfloorId = "6";
        public const string GroundId = "7";
        public const string GlitchwallId = "8";
        public const string KeywallId = "9";
        public const string KeynoneId = "10";
        public const string JumpeffectbackgroundId = "11";

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void method();

        public static int MaxID => Tiles.Length - 1;
        public static TileStruct[] Tiles = {

            /*
            //
            new TileStruct(){
                Id = "",
                TileName = "",
                Char = "",
                ForeColor = ConsoleColor.Black,
                BackColor = ConsoleColor.Black,
                IsVisible = true,
                CollusionEnabled = false,
            },
             

            */

            //Void Tile (None)
            new TileStruct(){
                Id = "0",
                TileName = "world_none",
                Char = "",
                ForeColor = ConsoleColor.Black,
                BackColor = ConsoleColor.Black,
                IsVisible = false,
                CollusionEnabled = false,
            },

            //Wall Tile
            new TileStruct(){
                Id = "1",
                TileName = "world_wall",
                Char = " ",
                ForeColor = ConsoleColor.Black,
                BackColor = ConsoleColor.White,
                IsVisible = true,
                CollusionEnabled = true,
            },  

            //Bricks Tile
            new TileStruct(){
                Id = "2",
                TileName = "world_bricks",
                Char = " ",
                ForeColor = ConsoleColor.Black,
                BackColor = ConsoleColor.Yellow,
                IsVisible = true,
                CollusionEnabled = true,
            },

            //Bacground
            new TileStruct(){
                Id = "3",
                TileName = "world_background",
                Char = ".",
                ForeColor = ConsoleColor.White,
                BackColor = ConsoleColor.Black,
                IsVisible = true,
                CollusionEnabled = false,
            },

            //Spawnpoint
            new TileStruct(){
                Id = "4",
                TileName = "player_spawnpoint",
                Char = "",
                ForeColor = ConsoleColor.Black,
                BackColor = ConsoleColor.Black,
                IsVisible = false,
                CollusionEnabled = false,
            },

            //Object
            new TileStruct(){
                Id = "5",
                TileName = "world_object",
                Char = "S",
                ForeColor = ConsoleColor.Green,
                BackColor = ConsoleColor.Yellow,
                IsVisible = true,
                CollusionEnabled = true,
            },

            //Jump Pad
            new TileStruct(){
                Id = "6",
                TileName = "world_jumppad",
                Char = " ",
                ForeColor = ConsoleColor.Black,
                BackColor = ConsoleColor.Blue,
                IsVisible = true,
                CollusionEnabled = true,
            },

            //Ground - Void
            new TileStruct(){
                Id = "7",
                TileName = "world_ground",
                Char = "",
                ForeColor = ConsoleColor.Black,
                BackColor = ConsoleColor.Black,
                IsVisible = false,
                CollusionEnabled = false,
            },

            //Glitch Wall
            new TileStruct(){
                Id = "8",
                TileName = "world_glitchwall",
                Char = " ",
                ForeColor = ConsoleColor.Black,
                BackColor = ConsoleColor.DarkGray,
                IsVisible = true,
                CollusionEnabled = true,
            },

            //KeyWall
            new TileStruct(){
                Id = "9",
                TileName = "world_keywall",
                Char = "+",
                ForeColor = ConsoleColor.Black,
                BackColor = ConsoleColor.Yellow,
                IsVisible = true,
                CollusionEnabled = true,
            },

            //KeyNone
            new TileStruct(){
                Id = "10",
                TileName = "world_keynone",
                Char = "+",
                ForeColor = ConsoleColor.Yellow,
                BackColor = ConsoleColor.Black,
                IsVisible = true,
                CollusionEnabled = false,
            },

            //Jump Pad Background Trail
            new TileStruct(){
                Id = "11",
                TileName = "world_jumppadtrail",
                Char = ".",
                ForeColor = ConsoleColor.Blue,
                BackColor = ConsoleColor.Black,
                IsVisible = true,
                CollusionEnabled = false,
            },


        };
        private Tile() {

        }
        
    }
    
}
