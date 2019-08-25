#define PLATFORMS

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

using static SimpleGame.Tile;
using static SimpleGame.Misc;
using static SimpleGame.GAME;
using static SimpleGame.Program;

namespace SimpleGame {
    public partial class World {
        public List<Panel> panels;
        
        public Tile[][] chunks;
        public Tile[][] chunks_copy;
        public bool customgen = false;
        public bool SFXEnabled = true;
        public const int CHUNK_SIZE = 30;
        public const int CHUNK_X = 100;
        public const int CHUNK_Y = 40;

        public World() {
            
        }
        public void ForceMove() {
            Pause = true;
            Move();
            Pause = false;
            ConsolePaint.Clear();
        }
        public string getTile(int x, int y) {
                if (chunks[x][y] == null)
                    return "0";


                if (x == Program.Player.X && y - 1 == Program.Player.Y)
                    goto me;
                me:

                bool access = false;
                if (Program.StarFruitEnabled) {
                    if (chunks[x][y].Id != Tile.GroundId) {
                        access = chunks[x][y].Collusion;
                    }
                } else
                    access = chunks[x][y].Collusion;

                if (!access)
                    return "0";

                return chunks[x][y].Id;

        }
        public string getTileAsWorld(int x, int y) {

            if (chunks[x][y] == null)
                return "0";


            if (chunks[x][y].Id == Tile.BackgroundId)
                return "0";

            return chunks[x][y].Id;
        }
        public void setTile(int x, int y, Tile copy) {
            try {
                if (copy.Id != "0") {
                    chunks[x][y] = new Tile(copy, x, y);
                }
            }
            catch {

            }
        }
        public void setTile(int x, int y, string Id) {
            try {
                switch (Id) {
                    case "0":
                        if(chunks[x][y] != null && chunks[x][y].Id != BackgroundId)
                        chunks[x][y] = null;
                        break;
                    case Tile.JumpeffectbackgroundId:
                        chunks[x][y] = new Tile(Id, x, y);
                        chunks[x][y].Update();
                        break;
                    default:
                        chunks[x][y] = new Tile(Id, x, y);
                        break;
                }

            } catch {

            }
        }


        public void drawObject(int x, int y, int xSize, int ySize, string Id) {
            for (int ix = 0; ix < xSize; ix++) {
                for (int iy = 0; iy < ySize; iy++) {
                    try {
                        if (ix + x > CHUNK_X || iy + y > CHUNK_Y)
                            continue;

                        if (ix + x <= 0 || iy + y <= 0)
                            continue;

                        setTile(ix + x, iy + y, Id);
                    } catch {
                        continue;
                    }
                }
            }
        }
        public void removeObject(int x, int y, int xSize, int ySize) {
            for (int ix = 0; ix < xSize; ix++) {
                for (int iy = 0; iy < ySize; iy++) {

                    if (ix + x > CHUNK_X || iy + y > CHUNK_Y)
                        continue;

                    if (ix + x <= 0 || iy + y <= 0)
                        continue;

                    chunks[ix + x][iy + y] = null;
                }
            }
            Console.Clear();
        }
        public void Update() {
            CheckValidate();
            Draw();



#if PLATFORMS

            int num2 = Program.Random.Next(0, 6000);
            if (num2 == 3001) {
                int num1 = Program.Random.Next(0, Program.World.panels.Count);
                Program.Reluics.Add(new Items.Powerup.TestPowerup(panels[num1].X + 2, panels[num1].Y - 7));
            } else if (num2 >= 5997) {
                int num1 = Program.Random.Next(0, Program.World.panels.Count);
                Program.Reluics.Add(new Items.Powerup.StarFruit(panels[num1].X + 8, panels[num1].Y - 7));
            }
#endif
            for (int x = 0; x < CHUNK_X; x++) {

                for (int y = 0; y < CHUNK_Y; y++) {

                    try {
                        if (getTile(x, y) != "0") {
                            if (Program.Player.X == x && Program.Player.Y == y - 1) {
                                chunks[x][y].Update();
                            } else if (getTile(x, y) == Tile.GlitchwallId && getTile(Program.Player.X, Program.Player.Y + 1) != Tile.GlitchwallId) {
                                if (ConsolePaint.Flipped) {
                                    ConsolePaint.Flipped = false;
                                    Program.Shake(3, true);
                                }
                            }

                        }
                    }
                    catch {

                    }
                }
            }
        }
        public void Draw() {
            Console.ForegroundColor = ConsoleColor.White;

            for (int x = 0; x < CHUNK_X; x++) {

                for (int y = 0; y < CHUNK_Y; y++) {
                    try {

                            if (chunks_copy[x][y] == chunks[x][y])
                                continue;

                            chunks_copy[x][y] = chunks[x][y];

                        if (chunks[x][y] == null || chunks[x][y].Id == "") continue;
                        if (chunks[x][y].Id == Tile.ObjectId || chunks[x][y].Id == Tile.ObjectId) {
                            chunks[x][y] = new Tile(" ", x, y);
                        }
                        //ConsolePaint.Clear();
                        if (chunks[x][y].Visible) {
                            chunks[x][y].Draw();
                        }

                    } catch { }

                }
            }

        }
        private ivector lastpos, curpos;
        public void CheckValidate() {
            foreach (Panel p in panels) {

                if (p.tileType == Tile.BricksId || p.tileType == Tile.KeywallId)
                    continue;
                for (int i = p.X; i < p.X + p.size; i++) {
                    if (getTile(i, p.Y) != p.tileType) {
                        setTile(i, p.Y, p.tileType);
                    }
                }

            }
        }
        public void Move() {
            //GAME_InitChunkCopy();
            int l = panels.Count - 1;
            //Движение всех объектов мира

            for (int x = 0; x < CHUNK_X; x++) {

                for (int y = 0; y < CHUNK_Y; y++) {
                    try {
                        if (getTileAsWorld(x, y) != "0" && getTileAsWorld(x, y) != BackgroundId) {
                            chunks[x][y].Move(new ivector(Offsets.OF_MEDIUM, 0));
                            setTile(x - Offsets.OF_MEDIUM, y, chunks[x][y]);

                            removeObject(x, y, 1, 1);
                        }
                    } catch {
                        if (chunks[x][y].Id == Tile.KeynoneId) {
                            chunks[x][y].Move(new ivector(Offsets.OF_MEDIUM, 0));
                            setTile(x - Offsets.OF_MEDIUM, y, chunks[x][y]);

                            removeObject(x, y, 1, 1);
                        } else {
                            setTile(x, y, Tile.NoneId);

                            removeObject(x, y, 1, 1);
                        }
                    }
                }
            }


            for (int i = 0; i < Program.Reluics.Count; i++) {
                gg:
                ivector lP = new ivector(Program.Reluics[i].X, Program.Reluics[i].Y);
                
                int num1 = Program.Reluics[i].X - Offsets.OF_MEDIUM;
                if (num1 <= 0)
                    Program.Reluics.Remove(Program.Reluics[i]);
                else
                    Program.Reluics[i].X = num1;
                try {
                    if (lP.X - Offsets.OF_MEDIUM != Program.Reluics[i].X) goto gg;
                }
                catch {
                    try {
                        if (SoundCore.Sounds["Destroy"].Status != SFML.Audio.SoundStatus.Playing)
                            SoundCore.Play("Destroy");
                        Program.Reluics.Remove(Program.Reluics[i]);
                    } catch { }
                }
            }

            //Движение игрока
            int _x = 0;
            _x = Program.Player.X -= Offsets.OF_MEDIUM;
            Program.Player.Teleport(_x, Program.Player.Y, false);

#if PLATFORMS

#else
            return;
#endif

            //Движение платформ
            for (int i = 0; i < panels.Count - 1; i++) {
                panels[i].Y = panels[i + 1].Y;
                panels[i].tileType = panels[i + 1].tileType;
                panels[i].size = panels[i + 1].size;
            }

            ScorePoints++;

            //Генерация

            int yy = 0;

            try {
                yy = Program.Random.Next(4, lastpos.Y + 5);
            } catch {
                yy = Program.Random.Next(6, 14);
            }
            panels[panels.Count - 1].X = 75;
            panels[panels.Count - 1].Y = CHUNK_Y - yy;
            panels[panels.Count - 1].size = 5;



            int rnd1 = Program.Random.Next(0, 9);

            lastpos = new ivector(panels[6].X, panels[6].Y);
            curpos = new ivector(panels[7].X, panels[7].Y);
            switch (rnd1) {

                case 4:
                    panels[panels.Count - 1].tileType = Tile.GlitchwallId;
                    panels[panels.Count - 1].size = 5;
                    break;
                case 3:
                    panels[panels.Count - 1].tileType = Tile.JumpfloorId;
                    panels[panels.Count - 1].size = 7;
                    break;
                case 2:
                    panels[panels.Count - 1].tileType = Tile.BricksId;
                    break;
                case 1:
                    panels[panels.Count - 1].tileType = Tile.KeywallId;
                    break;
                default:
                    panels[panels.Count - 1].tileType = Tile.WallId;
                    break;

            }
            //if (Misc.chance((15 + Program.luckbonus))) {
            //    int num1 = panels.Count - 1;
            //    Program.Reluics.Add(new Items.Powerup.Coin(panels[num1].X + 2, panels[num1].Y - 4));
            //}

            //if (Misc.chance(Program.bluckbonus)) {
            //    int num1 = panels.Count - 1;
            //    Program.Reluics.Add(new Items.Powerup.CoinBag(panels[num1].X + 2, panels[num1].Y - 1));
            //}

            drawObject(panels[panels.Count - 1].X, panels[panels.Count - 1].Y, panels[panels.Count - 1].size, 1, panels[panels.Count - 1].tileType);
            if (curpos.Y - lastpos.Y <= -7) {
                drawObject(panels[l].X - 1, panels[l - 1].Y + 1, 7, 1, Tile.WallId);
            }

            try {
                for (int i = 0; i < panels.Count; i++) {
                    if (panels[i + 1].Y - panels[i].Y <= -7) {
                        if (panels[i + 2].Y - panels[i].Y <= -5) {
                            drawObject(panels[i + 2].X - 1, panels[i].Y - 2, 7, 1, Tile.WallId);
                        }
                    }
                }
            } catch {
            }
            //Проверка платформ на наличие деформации : - )
            GAME_InitChunkCopy();
            CheckValidate();

        }
        public void Generate() {
            Console.Clear();
            for (int i = 0; i < 8; i++)
                panels.Add(new Panel());




            


            //Another Objects
            for (int i = 0; i < 30; i++) {
                int rndx = Program.Random.Next(0, CHUNK_X-5);
                int rndy = Program.Random.Next(0, CHUNK_Y-1);
                chunks[rndx][rndy] = new Tile(Tile.BackgroundId, rndx, rndy);
            }



#if PLATFORMS
            
#else
            int groundLevelMax = rand(10, 20);
            int groundLevelMin = groundLevelMax + rand(10, 20);

            // Генерация уровня ландшафта
            int[] arr = new int[CHUNK_X];
            for (int i = 0; i < CHUNK_X; i++) {
                int dir = rand(0, 2) == 1 ? 1 : -1;

                if (i > 0) {
                    if (arr[i - 1] + dir < groundLevelMax || arr[i - 1] + dir > groundLevelMin)
                        dir = -dir;

                    arr[i] = arr[i - 1] + dir;
                } else
                    arr[i] = groundLevelMin;
            }

            // Сглаживание
            for (int i = 1; i < CHUNK_X - 1; i++) {
                float sum = arr[i];
                int count = 1;
                for (int k = 1; k <= 5; k++) {
                    int i1 = i - k;
                    int i2 = i + k;

                    if (i1 > 0) {
                        sum += arr[i1];
                        count++;
                    }

                    if (i2 < CHUNK_X) {
                        sum += arr[i2];
                        count++;
                    }
                }

                arr[i] = (int)(sum / count);
            }

            // Ставим плитки на карту
            for (int i = 0; i < CHUNK_X; i++) {
                for (int j = arr[i] + 1; j < CHUNK_Y; j++)
                    setTile(i, j, WallId);
            }

            return;
#endif
            //Platforms
            int yy = 0;
            int pos = 0;

            try {
                yy = Program.Random.Next(4, lastpos.Y + 5);
            } catch {
                yy = Program.Random.Next(6, 14);
            }
            panels[0].X = 5;
            panels[0].Y = CHUNK_Y - yy;
            panels[0].size = 5;
            panels[0].tileType = Tile.WallId;

            drawObject(panels[0].X, panels[0].Y, panels[0].size, 1, Tile.WallId);

            for (int i = 1; i < panels.Count; i++) {


                yy = Program.Random.Next(6, 14);
                pos += 10;
                panels[i].X = 5 + pos;
                panels[i].Y = CHUNK_Y - yy;
                panels[i].size = 5;

                int rnd1 = Program.Random.Next(0, 15);

                switch (rnd1) {
                    case 4:
                        panels[i].tileType = Tile.GlitchwallId;
                        panels[i].size = 5;
                        break;
                    case 3:
                        panels[i].tileType = Tile.JumpfloorId;
                        panels[i].size = 7;
                        break;
                    case 2:
                        panels[i].tileType = Tile.KeywallId;
                        panels[i].size = 5;
                        break;
                    case 1:
                        panels[i].tileType = Tile.BricksId;
                        panels[i].size = 5;
                        break;
                    default:
                        panels[i].tileType = Tile.WallId;
                        break;

                }

                try {
                    lastpos = new ivector(panels[i - 1].X, panels[i - 1].Y);
                } catch {
                    lastpos = new ivector(panels[i].X, panels[i].Y);
                }
                curpos = new ivector(panels[i].X, panels[i].Y);

                drawObject(panels[i].X, panels[i].Y, panels[i].size, 1, panels[i].tileType);

                if (curpos.Y - lastpos.Y <= -7) {
                    drawObject(panels[i].X - 1, panels[i - 1].Y + 1, 7, 1, Tile.WallId);

                }
            }
           

            try {
                for (int i = 0; i < panels.Count; i++) {
                    if (panels[i + 1].Y - panels[i].Y <= -7) {
                        if (panels[i + 2].Y - panels[i].Y <= -5) {
                            drawObject(panels[i + 2].X - 1, panels[i].Y - 2, 7, 1, Tile.WallId);
                        }
                    }
                }
            } catch {
                setTile(panels[0].X + 2, panels[0].Y - 1, Tile.SpawnpointId);
                //  setTile(5, 3, TileType.Spawnpoint);


            }




        }
    }
}
