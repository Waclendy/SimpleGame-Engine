using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using SimpleGame.NPC;
using SFML.Audio;
using SFML.Window;
using System.Collections.Generic;
using SFML.Graphics;
using Newtonsoft.Json;

using static SimpleGame.Tile;
using static SimpleGame.Misc;
using static SimpleGame.GAME;
using static SimpleGame.Program;

namespace SimpleGame
{

    partial class Program
    {

        public static bool StarFruitEnabled = false;
        public static bool ingame = true;

        public static sboolean EscapeMode = 0;

        public static WorldShop shop;
        public static sboolean shopClosed = 1;
        public const int FRAME_RATE = 0;
        public static int GRAVITY = 50;

        public static int worldframe = 0;

        public static double buthbonus = 0;
        public static float coinbonus = 0;
        public static int lifebonus = 0;
        public static float luckbonus = 0;
        public static float bluckbonus = 0;

        public static float Coins = 0.0f;

        public static int ScorePoints = 0;

        private static int speakerTarget = 0;
        public static bool speakerEnabed = false;


        //escape
        //mainMusic
        public static Music mainMusic = new Music(SoundCore.CONTENT_PATH + "\\Music\\_endframe1.wav");
        public static Music escape = new Music(SoundCore.CONTENT_PATH + "\\Music\\_bossframe1.wav");
        public static Music mainmenu = new Music(SoundCore.CONTENT_PATH + "\\Music\\_empireearth.wav");
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
            Coins = 10000.0f;
            buthbonus = 0;
            coinbonus = 0;
            lifebonus = 0;
            luckbonus = 0;
            bluckbonus = 0;
            ScorePoints = 0;
            WorldTime = 0;



            shop = new WorldShop();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            World = new World();
            World.panels = new List<Panel>();
            Player = new Player();
            Bot = new BOT();
            Game = new Game();
            Timer = new Timer(new TimerCallback(Tick), null, 3500, 3500);
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
            GAME_InitChunkCopy();
        }
        public static void Intro() {
            mainmenu.Play();
            mainmenu.Volume = 100;
            mainmenu.Pitch = 2f;
        }
        private static void Tick(object state)
        {
            WorldTime++;
            try {
                if (EscapeMode) {
                    World.ForceMove();
                }
            }
            catch { }
        }
        static unsafe void DebugMethod() {

            // TileStruct* test = Tiles[1];
            //test->BackColor = ConsoleColor.Black;
        }

      

        static void Main(string[] args)
        {
            Startup.OnStart();

            

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WindowHeight = 40;
            Console.WindowWidth = 103;

            mainMusic.Volume = 55;
            mainMusic.Loop = true;


            SoundCore.Load();
            Intil();
           // Intro();
            escape.Volume = 40;
            // Game.EscapeTimer.Pause();

            UI.Menu menu = new UI.Menu();
            string[] str = { "Начать Приключение", "Тестовый Метод", "Тест Диалога", "Перезапустить", "Выход" };
            gt:
            int menu_Select = 0;
            menu_Select = menu.MainMenu(str);


            switch (menu_Select)
            {
                case 0:
                    SoundCore.Play("Menu Select");
                    mainMusic.Play();
                    mainmenu.Stop();
                   
                    break;
                case 2:
                    SoundCore.Play("Menu Select");
                    speakerSay(100, Voice.Asmodeus, "1241141435425145145344343463463");
                    goto gt;
                case 3:
                    SoundCore.Play("Menu Select");
                    Process.Start("SimpleGame.exe");
                    Environment.Exit(0);
                    break;
                case 4:
                    SoundCore.Play("Menu Select");
                    Environment.Exit(0);
                    break;
                case 1:
                    DebugMethod();
                    SoundCore.Play("Error");
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
