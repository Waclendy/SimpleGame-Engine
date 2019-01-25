using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using SimpleGame.NPC;
using SFML.Audio;
using SFML.Window;
using System.Collections.Generic;
using SFML.Graphics;

namespace SimpleGame
{

    partial class Program
    {

        public static bool StarFruitEnabled = false;
        public static bool ingame = true;

        public const int FRAME_RATE = 0;
        public const int GRAVITY = 50;

        public static bool fastRespawn = false;

        private static int speakerTarget = 0;
        public static bool speakerEnabed = false;

        public static Music mainMusic = new Music(SoundCore.CONTENT_PATH + "\\Sounds\\World\\world_main_2.wav");
        public static bool stopAll = false;
        public static Random Random = new Random();
        public static List<Items.Powerup.Base> Reluics;
        static ProcessThread mThread;
        public static Thread thrd;

        public static bool Pause = false;

        public static int WorldTime { get; private set; }
        public static World World { get; private set; }
        public static Player Player { get; private set; }
        public static BOT Bot { get; private set; }
        public static Timer Timer { get; private set; }
        public static Game Game { get; private set; }

        public static void Intil()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            World = new World();
            World.panels = new List<Panel>();
            Player = new Player();
            Bot = new BOT();
            Game = new Game();
            Timer = new Timer(new TimerCallback(Tick), null, 0, 1000);
            Reluics = new List<Items.Powerup.Base>();

            Console.CursorVisible = false;

            stopAll = false;

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

        private static void Tick(object state)
        {
            WorldTime++;
            
        }

        static void Main(string[] args)
        {



            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WindowHeight = 40;
            Console.WindowWidth = 103;

            mainMusic.Volume = 55;
            mainMusic.Loop = true;


            SoundCore.Load();
            Intil();

            UI.Menu menu = new UI.Menu();
            string[] str = { "Начать Приключение", "Игровые Звуки", "Тест Диалога","Выход", "Настройки Графики", "Приключение", "Читы", "str[7] = \"Ultra читы\";"   };
            gt:
            int menu_Select = 0;
            menu_Select = menu.MainMenu(str);
            switch (menu_Select)
            {
                case 0:
                    SoundCore.Play("Menu Select");
                    mainMusic.Play();
                   
                    break;
                case 2:
                    SoundCore.Play("Menu Select");
                    speakerSay(100, Voice.Soup, "ДАВНО НЕ ВИДЕЛИСЬ");
                    goto gt;

                case 3:
                    SoundCore.Play("Menu Select");
                    speakerSay(70, Voice.Default, "Скоро увидимся, герой!**");
                    Environment.Exit(0);
                    break;
                case 1:
                    //    SoundCore.Play("Menu Select");
                    //    List<string> sounds = new List<string>();
                    //    foreach (KeyValuePair<string, Sound> stre in SoundCore.Sounds)
                    //    {
                    //        sounds.Add(stre.Key);
                    //    }
                    //    sounds.Add("Начать Игру");
                    //    UI.Menu menu2 = new UI.Menu();
                    //gt2:
                    //    int menu_Select2 = 0;

                    //    menu_Select2 = menu2.MainMenu(sounds.ToArray());

                    //    try
                    //    {
                    //        if (menu_Select2 == sounds.Count - 1)
                    //            break;
                    //        SoundCore.Play(sounds[menu_Select2]);
                    //    }
                    //    catch
                    //    {

                    //    }
                    //    goto gt2;

                        string str2 = str[menu_Select];
                        string str3 = "";
                        for(int i = 0; i < str2.Length; i ++) {
                            str3 += str2[Random.Next(0, str2.Length)];
                        }
                        SoundCore.Play("Error");
                    str[menu_Select] = str3;
                    goto gt;
                case 7:
                    if (str[menu_Select] != "X") {
                        makeGlitch(2000, true);

                        str = new string[] { "X", "error", "Dialog Test","Exit", "Graphics Settings", "pри$#%@ен^е", "Ultra читы" };

                    }
                    goto gt;
                default:
                    try {
                        string str22 = str[menu_Select];
                        string str32 = "";
                        for (int i = 0; i < str22.Length; i++) {
                            str32 += str22[Random.Next(0, str22.Length)];
                        }
                        SoundCore.Play("Error");
                        str[menu_Select] = str32;

                    }
                    catch {

                    }
                    goto gt;
            }
            World.Generate();
            thrd = new Thread(mThread.Run);
            thrd.Start();
            while (ingame)
            {
                Game.Update();
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

        }
        private static void onPlayerJumped(object sender, playerEventArgs eventArgs)
        {
            //Если игрок прыгнул - то воспроизвести звук
        }
        private static void onPlayerFalled(object sender, playerEventArgs eventArgs)
        {
          
       
        }



    }
}
