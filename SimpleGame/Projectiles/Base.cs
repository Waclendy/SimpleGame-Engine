using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using static SimpleGame.GAME;

namespace SimpleGame.Projectiles {
    class Base : IRenderable {
        public string prjChar = ".";
        public ConsoleColor prjColor = ConsoleColor.Yellow;

        public int X = 0;
        public int Y = 0;

        public int currframe = 0;

        public sboolean exsists = 0;

        public int lastx = 2;
        public int lasty = 2;
        private int _dir = 0;
        public int direct { get { return _dir; } set { if (value < -1) value = -1; if (value > 1) value = 1; _dir = value; } }

        public sboolean Rendered { get; set; }

        public virtual void Draw() {

            int drawx = X;
            int drawy = Y;

            if (X <= 0)
                drawx = 0;
            else if (X >= 100)
                drawx = 99;

            if (Y <= 0)
                drawy = 0;

            ConsolePaint.Paint(lastx, lasty, ConsolePaint.DEFAULT_BACKGROUND, ConsolePaint.DEFAULT_FOREGROUND, " ");
            ConsolePaint.Paint(drawx, drawy, ConsolePaint.DEFAULT_BACKGROUND, prjColor, prjChar);

            lastx = X;
            lasty = Y;
        }

        public virtual void Update() {
            Draw();
            Main();
        }
        public static Base Push(int x = 0, int y = 0, int _direct = 0) {
            Base result = new Base();
            result.direct = _direct;
            result.X = x;
            result.Y = y;
            result.lastx = x;
            result.lasty = y;
            result.exsists = 1;
            result.currframe = Program.worldframe;
            result.Rendered = 0;
            return result;
        }
        public virtual void Main() {
                if (currframe != Program.worldframe)
                    return;
            if (Program.Pause)
                return;
               
                Draw();
                try {

                    lasty = Y;
                    lastx = X;

                    if (_dir == 0) {
                        Explode();
                        return;
                    }

                    try {
                    
                        if (X + _dir < 0 || X + _dir > World.CHUNK_X || Program.World.getTile(X + _dir, Y) != Tile.NoneId) {
                            Explode();
                            return;
                        }
                    }
                    catch {
                        Explode();
                        return;
                    }
                    if (Program.World.getTile(X + _dir, Y) != Tile.WallId)
                        if (Program.World.getTile(X + _dir, Y) == Tile.NoneId) {
                            X += _dir;
                        return;
                        }
                } catch {
                    prjChar = "";
                }
        }
        public void Explode() {
            exsists = 0;
            SoundCore.Play("Destroy");
            ConsolePaint.Paint(X, Y, ConsolePaint.DEFAULT_BACKGROUND, ConsolePaint.DEFAULT_FOREGROUND, " ");
            Rendered = 1;
        }




    }
}
