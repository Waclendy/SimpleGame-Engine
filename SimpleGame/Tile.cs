using System;
using System.Threading;
using SFML.System;
using SFML.Graphics;

namespace SimpleGame
{
    public enum TileType { None, Wall, Bricks, Background, JumpEffectBackground, Spawnpoint, Object, UWall, GlitchWall, JumpFloor, Ground}
    public class Tile
    {
        public string tileChar = "E";

        public ConsoleColor tileForeColor = ConsoleColor.Red;
        public ConsoleColor tileBackColor = ConsoleColor.Black;
        public TileType Type = TileType.None;

        public bool firstTime = false;
        public bool Visible = true;
        public int X = 0;
        public int Y = 0;

        public void Update()
        {
            if (Program.Pause)
                return;
            Thread thrd = new Thread(_process);
            thrd.Start();

        }
        public void _process()
        {
            if (Program.Pause)
                return;
            switch (Type)
            {
             
                case TileType.JumpFloor:
                    Program.Player.playerDirect = Program.Player.direct;
                    Items.Effects.JumpPad JumpEffect = new Items.Effects.JumpPad();
                    JumpEffect.Enable(1.5);
                    SoundCore.Play("Object Flying");
                    Program.Player._this.oEvent = Event.EV_PLAYER_JUMP;
                    break; 
                case TileType.Bricks:
                        Thread.Sleep(500);
                        Program.World.setTile(X, Y, TileType.Ground);
                    break;
                case TileType.GlitchWall:
                    if (tileForeColor != ConsoleColor.Black)
                    {
                        tileForeColor = ConsoleColor.Black;

                        tileChar = Program.Random.Next(0, 2).ToString();
                        SoundCore.Play("Destroy");
                    }
                    break;
                
                case TileType.JumpEffectBackground:
                    Thread.Sleep(1000);
                    tileChar = " ";
                    Thread.Sleep(1000);
                    Program.World.setTile(X, Y, TileType.None);
                    break;
            }
        } 
        public Tile(TileType type, int x, int y)
        {
            Type = type;
            X = x;
            Y = y;
            switch (Type)
            {
                case TileType.None:
                    tileChar = "";
                    Visible = false;
                    break;
                case TileType.Wall:
                    tileChar = " ";
                    tileBackColor = ConsoleColor.White;
                    break;
                case TileType.UWall:
                    tileChar = "X";
                    tileForeColor = ConsoleColor.White;
                    tileBackColor = ConsoleColor.DarkMagenta;
                    break;
                case TileType.Bricks:
                    tileChar = " ";
                    tileBackColor = ConsoleColor.Yellow;
                    break;
                case TileType.Background:
                    tileChar = ".";
                    tileForeColor = ConsoleColor.White;
                    break;
                case TileType.Spawnpoint:
                    tileChar = "%";
                    Visible = false;
                    break;
                case TileType.Object:
                    tileChar = "OBJECT";
                    Visible = false;
                    break;
                case TileType.GlitchWall:
                    tileChar = "1";
                    tileForeColor = ConsoleColor.White;
                    tileBackColor = ConsoleColor.White;
                    break;
                case TileType.JumpFloor:
                    tileChar = " ";
                    tileBackColor = ConsoleColor.Blue;
                    break;
                case TileType.JumpEffectBackground:
                    tileChar = ".";
                    tileForeColor = ConsoleColor.Blue;
                    break;

                case TileType.Ground:
                    tileChar = " ";
                    tileForeColor = ConsoleColor.Black;
                    tileBackColor = ConsoleColor.Black;
                    Visible = false;
                    break;
               
            }
            firstTime = true;
        }
        public Tile(Tile copy, int x, int y)
        {
            tileChar = copy.tileChar;
            tileForeColor = copy.tileForeColor;
            tileBackColor = copy.tileBackColor;
            Type = copy.Type;
            Visible = copy.Visible;
            X = x;
            Y = y;
            firstTime = copy.firstTime;
        }
        public void Draw()
        {
            ConsolePaint.Paint(X, Y, tileBackColor, tileForeColor, tileChar);
        }
    }
}
