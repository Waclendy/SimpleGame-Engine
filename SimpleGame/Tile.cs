using System;
using System.Threading;
using SFML.System;
using SFML.Graphics;

using static SimpleGame.Tile;
using static SimpleGame.Misc;
using static SimpleGame.GAME;
using static SimpleGame.Program;

namespace SimpleGame
{
    
    public partial class Tile
    {
        public string tileChar = "E";

        public ConsoleColor tileForeColor = ConsoleColor.Red;
        public ConsoleColor tileBackColor = ConsoleColor.Black;
        public string Id = "E";

        public TileStruct Type;

        public bool triggered = false;

        public bool firstTime = false;
        public bool Visible = false;
        public bool Collusion = false;
        public int X = 0;
        public int Y = 0;
        public void Move(ivector offset) {
            X -= offset.X;
            Y -= offset.Y;
        }
        public void Update()
        {
            if (Program.Pause)
                return;
            Thread thrd = new Thread(_process);
            thrd.Start();

        }
        private bool Wait(int timeout) {
            int currframe = Program.worldframe;
            Thread.Sleep(timeout);
            return currframe == Program.worldframe;
        }

        public void _process()
        {

            
            if (Program.Pause)
                return;
            if (triggered)
                return;

            triggered = true;
            switch (Id) {


                case KeywallId:
                    if (!Wait(300))
                        return;
                    Program.World.setTile(X, Y, GroundId);
                    if (!Wait(200))
                        return;

                    Program.World.setTile(X, Y, KeynoneId);
                    ConsolePaint.Paint(X, Y, ConsoleColor.Black, ConsoleColor.Yellow, "+");
                    if (SoundCore.Sounds["Destroy"].Status != SFML.Audio.SoundStatus.Playing)
                        SoundCore.Play("Destroy");


                    if (Program.World.getTile(X - 1, Y) == KeywallId)
                        Program.World.chunks[X - 1][Y].Update();

                    if (Program.World.getTile(X + 1, Y) == KeywallId)
                        Program.World.chunks[X + 1][Y].Update();

                    if (!Wait(1300))
                        return;

                    Program.World.setTile(X, Y, GroundId);
                    if (!Wait(200))
                        return;
                    Program.World.setTile(X, Y, KeywallId);

                    break;

                case JumpfloorId:
                    Program.Player.playerDirect = Program.Player.direct;
                    Items.Effects.JumpPad JumpEffect = new Items.Effects.JumpPad();
                    JumpEffect.Enable(1.5);
                    SoundCore.Play("Object Flying");
                    Program.Player._this.oEvent = Event.EV_PLAYER_JUMP;
                    break;
                case GlitchwallId:
                    if (!ConsolePaint.Flipped) {
                        ConsolePaint.Flipped = true;
                        Program.Shake(3, true);
                    }
                    break;
                case BricksId:
                    if (!Wait(500))
                        return;
                    Program.World.setTile(X, Y, GroundId);
                    ConsolePaint.Paint(X, Y, ConsoleColor.Black, ConsoleColor.White, " ");
                    if (SoundCore.Sounds["Destroy"].Status != SFML.Audio.SoundStatus.Playing)
                        SoundCore.Play("Destroy");
                    break;

                case JumpeffectbackgroundId:

                    if (!Wait(1000))
                        return;

                    tileChar = " ";
                    if (!Wait(1000))
                        return;

                    Program.World.setTile(X, Y, GroundId);

                    break;
                default:
                    if (Type.Triggering)
                        Type.Trigger?.Invoke();
                    break;
            }
            triggered = false;
        } 
        public Tile(string Id, int x, int y)
        {
            X = x;
            Y = y;

            for(int i =0; i < Tiles.Length; i++) {

                TileStruct tile = GAME_GetTileType(Id);
                if (tile.Id != "-1") {
                    Type = tile;
                    this.Id = Id;
                    Collusion = tile.CollusionEnabled;
                    tileChar = tile.Char;
                    tileForeColor = tile.ForeColor;
                    tileBackColor = tile.BackColor;
                    Visible = tile.IsVisible;
                }
            }

            firstTime = true;
        }
        
        public Tile(Tile copy, int x, int y)
        {
            tileChar = copy.tileChar;
            tileForeColor = copy.tileForeColor;
            tileBackColor = copy.tileBackColor;
            Id = copy.Id;
            Collusion = copy.Collusion;
            Visible = copy.Visible;
            X = x;
            Y = y;
            firstTime = copy.firstTime;
        }
        public void Draw() {
            ConsolePaint.Paint(X, Y, tileBackColor, tileForeColor, tileChar);
        }
    }
}
