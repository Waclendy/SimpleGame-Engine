using System;
using System.Collections.Generic;
using System.Threading;

namespace SimpleGame
{
    public class World
    {
        public List<Panel> panels;

        public Tile[][] chunks;

        public bool SFXEnabled = true;
        public const int CHUNK_SIZE = 30;
        public const int CHUNK_X = 100;
        public const int CHUNK_Y = 30;
        
        public World()
        {
        }
        public TileType getTile(int x, int y)
        {
            bool access = false;

            if (Program.StarFruitEnabled)
                access = chunks[x][y] == null || chunks[x][y].Type == TileType.JumpEffectBackground || chunks[x][y].Type == TileType.Ground;
            else
                access = chunks[x][y] == null || chunks[x][y].Type == TileType.Ground || chunks[x][y].Type == TileType.JumpEffectBackground || chunks[x][y].Type == TileType.Background;

            if (access)
                return TileType.None;

            return chunks[x][y].Type;
        }
        public void setTile(int x, int y, Tile copy)
        {
            if (copy.Type != TileType.None)
            {
                chunks[x][y] = new Tile(copy, x, y);
            }
        }
        public void setTile(int x, int y, TileType type)
        {
            try
            {
                switch (type)
                {
                    case TileType.None:
                        chunks[x][y] = null;
                        break;
                    case TileType.JumpEffectBackground:
                        chunks[x][y] = new Tile(type, x, y);
                        chunks[x][y].Update();
                        break;
                    default:
                        chunks[x][y] = new Tile(type, x, y);
                        break;
                }

            }
            catch
            {

            }
        }

        public void placeBlock(int x, int y, TileType type)
        {
            if(Program.glitchSound.Status != SFML.Audio.SoundStatus.Playing)
            if (getTile(x,y) == TileType.None)
            {
                SoundCore.Play("Feedback", "wav", "Dig_", 0, 3, 100);
                    setTile(x, y, type);
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Clear();

            }
        }
        public void destroyBlock(int x, int y)
        {
            if (Program.glitchSound.Status != SFML.Audio.SoundStatus.Playing)
                if (getTile(x, y) != TileType.None)
                {
                    SoundCore.Play("Feedback", "wav", "Tink_", 0, 3, 100);
                    setTile(x, y, TileType.None);
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Clear();
                }

        }
        public void drawObject(int x, int y, int xSize, int ySize, TileType type)
        {
            for(int ix = 0; ix < xSize; ix++)
            {
                for (int iy = 0; iy < ySize; iy++)
                {
                    try
                        {
                        if (ix + x > CHUNK_X || iy + y > CHUNK_Y)
                            continue;

                        if (ix + x <= 0 || iy + y <= 0)
                            continue;

                        setTile(ix + x, iy + y, type);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }
        public void removeObject(int x, int y, int xSize, int ySize)
        {
            for (int ix = 0; ix < xSize; ix++)
            {
                for (int iy = 0; iy < ySize; iy++)
                {

                    if (ix + x > CHUNK_X || iy + y > CHUNK_Y)
                        continue;

                    if (ix + x <= 0 || iy + y <= 0)
                        continue;

                    chunks[ix + x][iy + y] = null;
                }
            }
            Console.Clear();
        }
        public void Update()
        {
            Draw();

            if (Program.Random.Next(0, 10000) == 4001)
            {
                SoundCore.Play("Amb");
            }

            int num2 = Program.Random.Next(0, 6000);
            if (num2 == 3001)
            {
                int num1 = Program.Random.Next(0, Program.World.panels.Count);
                Program.Reluics.Add(new Items.Powerup.TestPowerup(panels[num1].X + 2, panels[num1].Y - 7));
            }
            else if(num2 >= 5997)
            {
                int num1 = Program.Random.Next(0, Program.World.panels.Count);
                Program.Reluics.Add(new Items.Powerup.StarFruit(panels[num1].X + 8, panels[num1].Y - 7));
            }

            for (int x = 0; x < CHUNK_X; x++)
            {

                for (int y = 0; y < CHUNK_Y; y++)
                {

                    if(getTile(x, y) != TileType.None)
                    {
                        if (Program.Player.X == x && Program.Player.Y == y - 1)
                        {
                            chunks[x][y].Update();
                        }
                    }
                }
            }
        }
        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.White;

            for (int x = 0; x < CHUNK_X; x++)
            {

                for (int y = 0; y < CHUNK_Y; y++)
                {
                    try
                    {
                        if (chunks[x][y] == null || chunks[x][y].Type == TileType.None) continue;
                        if (chunks[x][y].Type == TileType.Object)
                        {
                            chunks[x][y] = new Tile(TileType.None, x, y);
                        }

                        if (chunks[x][y].Visible)
                        {
                            chunks[x][y].Draw();
                        }
                     
                    }
                    catch { }

                }
            }

        }
        public void Move()
        {
           
            //Движение всех объектов мира

            for (int x = 0; x < CHUNK_X; x++)
            {

                for (int y = 0; y < CHUNK_Y; y++)
                {
                    try
                    {
                        if (getTile(x, y) != TileType.None)
                        {
                            setTile(x - 10, y, chunks[x][y]);

                            removeObject(x, y, 1, 1);
                        }
                    }
                    catch
                    {
                        setTile(x, y, TileType.None);

                        removeObject(x, y, 1, 1);
                    }
                }
            }

            for(int i = 0; i < Program.Reluics.Count; i ++)
            {
                int num1 = Program.Reluics[i].X - 10;
                if (num1 <= 0)
                    Program.Reluics.Remove(Program.Reluics[i]);
                else
                Program.Reluics[i].X = num1;
            }

            //Движение платформ
            for (int i = 0; i < panels.Count - 1; i++)
            {
                panels[i].Y = panels[i + 1].Y;
                panels[i].TileType = panels[i + 1].TileType;
                panels[i].Size = panels[i + 1].Size;
            }

            int yy = Program.Random.Next(4, 14);
            panels[panels.Count - 1].X = 75;
            panels[panels.Count - 1].Y = CHUNK_Y - yy;
            panels[panels.Count - 1].Size = 5;

           

            int rnd1 = Program.Random.Next(0, 9);
            switch (rnd1)
            {
                case 3:
                    panels[panels.Count - 1].TileType = TileType.JumpFloor;
                    panels[panels.Count - 1].Size = 7;
                    break;
                case 2:
                    panels[panels.Count - 1].TileType = TileType.Bricks;
                    break;
                case 5:
                    panels[panels.Count - 1].TileType = TileType.GlitchWall;
                    break;
                default:
                    panels[panels.Count - 1].TileType = TileType.Wall;
                    break;

            }

            drawObject(panels[panels.Count - 1].X, panels[panels.Count - 1].Y, panels[panels.Count - 1].Size, 1, panels[panels.Count - 1].TileType);
        }
        public void Generate()
        {
            Console.Clear();
            for (int i = 0; i < 8; i++)
                panels.Add(new Panel());

         

            //Another Objects
            for(int i = 0; i < 30; i++)
            {
                int rndx = Program.Random.Next(0, CHUNK_X-5);
                int rndy = Program.Random.Next(0, CHUNK_Y-1);
                if (i != 29)
                    chunks[rndx][rndy] = new Tile(TileType.Background, rndx, rndy);
                else
                    Program.Reluics.Add(new Items.Powerup.Star(rndx, rndy));
            }

            //Platforms
            int yy = 0;
            int pos = -10;
            for (int i = 0; i < panels.Count; i++)
            {
                yy = Program.Random.Next(6, 14);
                pos += 10;
                panels[i].X = 5 + pos;
                panels[i].Y = CHUNK_Y - yy;
                panels[i].Size = 5;

                int rnd1 = Program.Random.Next(0, 15);
                switch(rnd1)
                {
                    case 3:
                        panels[i].TileType = TileType.JumpFloor;
                        panels[i].Size = 7;
                        break;
                    case 2:
                        panels[i].TileType = TileType.Bricks;
                        break;
                    case 5:
                        panels[i].TileType = TileType.GlitchWall;
                        break;
                    default:
                        panels[i].TileType = TileType.Wall;
                        break;

                }


                drawObject(panels[i].X, panels[i].Y, panels[i].Size, 1, panels[i].TileType);
            }



        

            setTile(panels[0].X + 2, panels[0].Y - 5, TileType.Spawnpoint);




        }




    }
}
