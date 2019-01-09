using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using SimpleGame.NPC;
using SFML.Audio;
using System.Collections.Generic;

namespace SimpleGame
{

    partial class Program
    {

        public static bool StarFruitEnabled = false;
        public static bool ingame = true;
        public const int FRAME_RATE = 0;
        public const int GRAVITY = 50;
        private static int speakerTarget = 0;
        public static bool speakerEnabed = false;
        public static Sound glitchSound;
        public static Music mainMusic = new Music(SoundCore.CONTENT_PATH + "\\Sounds\\World\\world_main_2.wav");
        public static Music firstMusic = new Music(SoundCore.CONTENT_PATH + "\\Music\\island.wav");
        public static Music secondMusic = new Music(SoundCore.CONTENT_PATH + "\\Sounds\\World\\space_bed_02.ogg");
        public static int loseCounter = 14;
        public static bool stopAll = false;
        public static bool Excepted = false;
        public static Random Random = new Random();
        public static DirectionX pdx = DirectionX.NONE;
        public static DirectionY pdy = DirectionY.NONE;
        public static List<Items.Powerup.Base> Reluics;
        static ProcessThread mThread;
        public static Thread thrd;
        public static World World { get; private set; }
        public static Player Player { get; private set; }
        public static BOT Bot { get; private set; }

        static void Intil()
        {

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            World = new World();
            World.panels = new List<Panel>();
            Player = new Player();
            Bot = new BOT();
            Reluics = new List<Items.Powerup.Base>();

            Console.CursorVisible = false;

            stopAll = false;

            glitchSound = SoundCore.Sounds["Glitch"];
            Player.playerFall += onPlayerFalled;
            Player.playerJump += onPlayerJumped;
            Player.playerOnMiddle += onPlayerInMiddle;
            Player.playerTeleportUse += onPlayerTeleportUsed;
            Player.playerFallSuccess += onSuccessFall;

            World.chunks = new Tile[World.CHUNK_X][];
            mThread = new ProcessThread();

            for (int chunk = 0; chunk < World.CHUNK_X; chunk++)
            {
                World.chunks[chunk] = new Tile[World.CHUNK_Y];
            }


        }
       
        static void Main(string[] args)
        {
            

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WindowHeight = 40;
            Console.WindowWidth = 103;

            mainMusic.Volume = 55;
            mainMusic.Loop = true;

            secondMusic.Loop = true;
            firstMusic.Loop = true;

            SoundCore.Load();
            Intil();

            UI.Menu menu = new UI.Menu();
            string[] str = { "Начать Приключение", "Игровые Звуки", "Тест Диалога", "Пещера Правды", "Выход" };
            int num5 = 0;
            gt:
            int menu_Select = 0; 
            menu_Select = menu.MainMenu(str);
            switch(menu_Select)
            {
                case 0:
                    SoundCore.Play("Menu Select");
                    mainMusic.Play();
                    break;
                case 2:
                    SoundCore.Play("Menu Select");
                    speakerSay(60, Voice.Default, File.ReadAllLines("..\\..\\Program.cs"));
                    goto gt;
                case 3:
                    if (num5 == 0)
                    {
                        SoundCore.Play("Menu Select");
                        Thread.Sleep(1000);
                        SoundCore.Play("Damage Taken");
                        str[3] = "Отдать душу";
                        
                    }
                    else if(num5 == 1)
                    {
                        SoundCore.Play("Menu Select");
                        Thread.Sleep(1000);
                        SoundCore.Play("Damage Taken");
                        str[3] = "По?№чить пр№*у";
                    }
                    else if(num5 <= 5)
                    {
                        SoundCore.Play("Menu Select");
                        Thread.Sleep(500);
                        SoundCore.Play("Damage Taken");
                    }
                    else if(num5 <= 10)
                    {
                        SoundCore.Play("Menu Select");
                        Thread.Sleep(500);
                        SoundCore.Play("Damage Taken");
                        str[3] = "120";
                    }
                    else if(num5 <= 15)
                    {
                        SoundCore.Play("Menu Select");
                        Thread.Sleep(500);
                        SoundCore.Play("Damage Taken");
                        str[3] = "";
                    }
                    else
                    {
                        str[3] = "X";
                    }
                    num5++;
                    goto gt;
                case 4:
                    SoundCore.Play("Menu Select");
                    speakerSay(70, Voice.Default, "Скоро увидимся, герой!**");
                    Environment.Exit(0);
                    break;
                case 1:
                    SoundCore.Play("Menu Select");
                    List<string> sounds = new List<string>();
                    foreach(KeyValuePair<string, Sound> stre in SoundCore.Sounds)
                    {
                        sounds.Add(stre.Key);
                    }
                    sounds.Add("Начать Игру");
                    UI.Menu menu2 = new UI.Menu();
                gt2:
                    int menu_Select2 = 0;

                    menu_Select2 = menu2.MainMenu(sounds.ToArray());

                    try
                    {
                        if (menu_Select2 == sounds.Count - 1)
                            break;
                        SoundCore.Play(sounds[menu_Select2]);
                    }
                    catch
                    {
                        
                    }
                    goto gt2;
                default:

                    goto gt;
            }
            
            ConsoleKeyInfo Key;

            World.Generate();

            for (int x = 0; x < World.CHUNK_X; x++)
            {

                for (int y = 0; y < World.CHUNK_Y; y++)
                {
                    if (World.chunks[x][y] == null) continue;

                    else if(World.chunks[x][y].Type == TileType.Spawnpoint)
                    {
                        Player.X = x;
                        Player.Y = y;

                        //BOT LAUNCH
                        Bot.X = Player.X;
                        Bot.Y = Player.Y - 2;
                        World.chunks[x][y] = null;
                    }
                }
            }

            thrd = new Thread(mThread.Run);
            thrd.Start();
            Bot.Process();
            while (ingame)
                {
                    Key = Console.ReadKey(true);
                if (stopAll)
                    continue;
                try
                {
                
                    switch (Key.Key)
                    {

                        
                        case ConsoleKey.D:
                            Player.playerDirect = Direction.RIGHT;
                            Player.direct = Player.playerDirect;
                            if (!Player.inGravity)
                            {
                                if(World.getTile(Player.X + 1, Player.Y) != TileType.None)
                                {
                                    SoundCore.Play("NoClip");
                                    while (World.getTile(Player.X + 1, Player.Y) != TileType.None)
                                    {
                                        Player.X++;
                                    }
                                }
                                Player.X++;


                            }
                            else
                            if (World.getTile(Player.X + 1, Player.Y) == TileType.None)
                            {
                                Player.X++;
                            }
                            break;

                        case ConsoleKey.A:
                            Player.playerDirect = Direction.LEFT;
                            Player.direct = Player.playerDirect;
                            if (!Player.inGravity)
                            {
                                if (World.getTile(Player.X - 1, Player.Y) != TileType.None)
                                {
                                    SoundCore.Play("NoClip");
                                    while (World.getTile(Player.X - 1, Player.Y) != TileType.None)
                                    {
                                        Player.X--;
                                    }
                                }
                                Player.X--;


                            }
                            else
                            if (World.getTile(Player.X - 1, Player.Y) == TileType.None)
                            {
                                Player.X--;
                            }
                            break;
                        case ConsoleKey.S:
                            if (!Player.inGravity)
                            {
                                if (World.getTile(Player.X, Player.Y + 1) != TileType.None)
                                {
                                    SoundCore.Play("NoClip");
                                    while (World.getTile(Player.X, Player.Y + 1) != TileType.None)
                                    {
                                        Player.Y++;
                                    }
                                }
                                Player.Y++;


                            }
                            else
                                Player.playerDirect = Direction.NONE;
                            break;

                        case ConsoleKey.Spacebar:
                            Player.Jump();
                            break;
                        case ConsoleKey.O:
                            int num1 = Random.Next(0, World.panels.Count);
                            Reluics.Add(new Items.Powerup.TestPowerup(World.panels[num1].X + 2, World.panels[num1].Y - 7));
                            break;
                        case ConsoleKey.W:
                            if (!Player.inGravity)
                            {
                                if (World.getTile(Player.X, Player.Y - 1) != TileType.None)
                                {
                                    SoundCore.Play("NoClip");
                                    while (World.getTile(Player.X, Player.Y - 1) != TileType.None)
                                    {
                                        Player.Y--;
                                    }
                                }
                                Player.Y--;


                            }
                            else
                                Player.Jump();
                            break;
                          
                    }

                }
                catch(Exception e)
                {
                }

            }


        }
    
        private static void onSuccessFall(object sender, playerEventArgs eventArgs)
        {
            //Если игрок приземлился на объект - то издать звук приземления
            Player.playerDirect = Direction.NONE;
        }
        private static void onPlayerTeleportUsed(object sender, playerEventArgs eventArgs)
        {
            //Если игрок телепортировался - то издать характерный звук

        }
        private static void onPlayerInMiddle(object sender, playerEventArgs eventArgs)
        {
            //Если игрок в середине мира(над центральной платформой) - то телепортировать его на прошлую платформу.
            Thread.Sleep(200);
            World.Move();
            int _x = 0;
            _x = Player.X - World.panels[4].X;
            Player.Teleport(World.panels[3].X + _x, Player.Y, false);
        }
        private static void onPlayerJumped(object sender, playerEventArgs eventArgs)
        {
            //Если игрок прыгнул - то воспроизвести звук
            
        }
        private static void onPlayerFalled(object sender, playerEventArgs eventArgs)
        {
            string[] txts = {
                "Кажется, вы проиграли...",
                "Не падайте духом, у вас все получится!",
                "Кажется, вы проиграли...** Попробуем еще разок.",
                "Понимаете,*** вы проиграли.",
                "Когда-нибудь у вас точно получится выиграть эту игру.***% А пока что удолетворяйтесь этими навыками.**",
                "Не хочу огорчать вас, но...** вы проиграли.*"
            };
            Player.Kill();
            Thread.Sleep(300);
            //if (!Excepted)
            //{
            //    if (loseCounter < 4)
            //        speakerSay(70, Voice.Default, txts[Random.Next(0, txts.Length)]);
            //    else if (loseCounter < 15)
            //        speakerSay(70, Voice.Default, txts[Random.Next(0, txts.Length)], $"Уже {loseCounter} попытка игры.");
            //    else
            //        speakerSay(70, Voice.Default, txts[Random.Next(0, txts.Length)], $"Уже {loseCounter} попытка игры.***..** Вы же видете, что у вас** плохой скилл.");
            //}
            StarFruitEnabled = false;
            Console.Clear();
            Intil();
            World.Generate();
            Thread.Sleep(200);
            Excepted = false;
            Player.Teleport(World.panels[0].X + 2, World.panels[0].Y - 5, true);
        }
    }
}
