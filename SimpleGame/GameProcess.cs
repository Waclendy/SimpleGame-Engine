using System;
using System.Threading;

using static SimpleGame.Tile;
using static SimpleGame.Misc;
using static SimpleGame.GAME;
using static SimpleGame.Program;

namespace SimpleGame
{
    partial class Program
    {
        public static bool glitcheScreen = false;
        public static srender RenderProjectiles = new srender();
        class ProcessThread
        {
           public void Run2()
            {
                while(ingame)
                {
                    //Bot.Draw();
                    //Bot.ProcessPlayer();
                }
            }
            public void Run()
            {
                Thread thrd = new Thread(Run2);
                RenderProjectiles.Sleep = 7;
                RenderProjectiles.Enable();

                //thrd.Start();

                for (int x = 0; x < World.CHUNK_X; x++)
                    {

                        for (int y = 0; y < World.CHUNK_Y; y++)
                        {
                            if (World.chunks[x][y] == null) continue;

                            else if (World.chunks[x][y].Id == Tile.SpawnpointId)
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

                    ConsoleKeyInfo Key;
                    while (ingame)
                    {
                    if (Pause)
                        continue;

                    if(EscapeMode) {
                        if(mainMusic.Status == SFML.Audio.SoundStatus.Playing)
                        mainMusic.Stop();
                        if (escape.Status != SFML.Audio.SoundStatus.Playing)
                            escape.Play();
                    }
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
                                        if (World.getTile(Player.X + 1, Player.Y) != Tile.NoneId)
                                        {
                                            SoundCore.Play("NoClip");
                                            while (World.getTile(Player.X + 1, Player.Y) != Tile.NoneId)
                                            {
                                                Player.X++;
                                            }
                                        }
                                        Player.X++;


                                    }
                                    else
                                    if (World.getTile(Player.X + 1, Player.Y) == Tile.NoneId)
                                    {
                                        Player.X++;
                                    }
                                    break;

                                case ConsoleKey.A:
                                    Player.playerDirect = Direction.LEFT;
                                    Player.direct = Player.playerDirect;
                                    if (!Player.inGravity)
                                    {
                                        if (World.getTile(Player.X - 1, Player.Y) != Tile.NoneId)
                                        {
                                            SoundCore.Play("NoClip");
                                            while (World.getTile(Player.X - 1, Player.Y) != Tile.NoneId)
                                            {
                                                Player.X--;
                                            }
                                        }
                                        Player.X--;


                                    }
                                    else
                                    if (World.getTile(Player.X - 1, Player.Y) == Tile.NoneId)
                                    {
                                        Player.X--;
                                    }
                                    break;
                            case ConsoleKey.U:
                                shop.Open();
                                break;
                                case ConsoleKey.S:
                                    if (!Player.inGravity)
                                    {
                                        if (World.getTile(Player.X, Player.Y + 1) != Tile.NoneId)
                                        {
                                            SoundCore.Play("NoClip");
                                            while (World.getTile(Player.X, Player.Y + 1) != Tile.NoneId)
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
                                Player._this.oEvent = Event.EV_PLAYER_JUMP;
                                    break;
                            case ConsoleKey.Tab:
                                Projectiles.Base.Push(Player.X, Player.Y, (int)Player.direct);
                                break;
                                case ConsoleKey.W:
                                    if (!Player.inGravity)
                                    {
                                        if (World.getTile(Player.X, Player.Y - 1) != Tile.NoneId)
                                        {
                                            SoundCore.Play("NoClip");
                                            while (World.getTile(Player.X, Player.Y - 1) != Tile.NoneId)
                                            {
                                                Player.Y--;
                                            }
                                        }
                                        Player.Y--;


                                    }
                                    else
                                    Player._this.oEvent = Event.EV_PLAYER_JUMP;
                                break;

                            }

                        }
                        catch
                        {
                    }

                    }




                
            }
        }

        public static void Phone(Voice actor, int speed, params string[] message)
        {
            while (stopAll) { }
            SoundCore.Play("Phone");
            speakerSay(20, Voice.Default, "> Вам& звонят по& телефону.**");
            foreach (string text in message)
            {
                speakerSay(speed, actor, "> " + text, "");

                while (stopAll) { }
            }

        }
        static int _speed = 0;
        static Voice _voice = Voice.Default;
        static string[] _text;
        public static void speakerSay(int speed, Voice Voice, params string[] text) {
            _speed = speed;
            _voice = Voice;
            _text = text;
            Thread thread = new Thread(thrdp);
            thread.Start();
        }
        private static void thrdp() {
            _speakerSay(_speed, _voice, _text);
        }
        public static void _speakerSay(int speed, Voice Voice, params string[] text)
        {

            if (speakerEnabed)
                return;
            speakerEnabed = true;

            if (Voice == Voice.Soup)
                goto f1;

            string alstr = "";

            Thread.Sleep(300);
            Console.ForegroundColor = ConsoleColor.White;
            speakerTarget = 0;
            Console.SetCursorPosition(1, 32);
            ConsolePaint.Paint(1, 32, ConsoleColor.Black, ConsoleColor.White, "+-------------------------------------------------------------------------------------------+");
            Console.SetCursorPosition(1, 33);
            ConsolePaint.Paint(1, 33, ConsoleColor.Black, ConsoleColor.White, "|                                                                                           |");
            Console.SetCursorPosition(1, 34);
            ConsolePaint.Paint(1, 34, ConsoleColor.Black, ConsoleColor.White, "| >>>                                                                                       |");
            Console.SetCursorPosition(1, 35);
            ConsolePaint.Paint(1, 35, ConsoleColor.Black, ConsoleColor.White, "|                                                                                           |");
            Console.SetCursorPosition(1, 36);
            ConsolePaint.Paint(1, 36, ConsoleColor.Black, ConsoleColor.White, "+-------------------------------------------------------------------------------------------+");

            foreach (string txt in text)
            {
               
                alstr = "";
                try
                {
                    foreach (char o in txt)
                    {
                        Console.SetCursorPosition(1, 32);
                        ConsolePaint.Paint(1, 32, ConsoleColor.Black, ConsoleColor.White, "+-------------------------------------------------------------------------------------------+");
                        Console.SetCursorPosition(1, 33);
                        ConsolePaint.Paint(1, 33, ConsoleColor.Black, ConsoleColor.White, "|                                                                                           |");
                        Console.SetCursorPosition(1, 34);
                        ConsolePaint.Paint(1, 34, ConsoleColor.Black, ConsoleColor.White, "| >>>                                                                                       |");
                        Console.SetCursorPosition(1, 35);
                        ConsolePaint.Paint(1, 35, ConsoleColor.Black, ConsoleColor.White, "|                                                                                           |");
                        Console.SetCursorPosition(1, 36);
                        ConsolePaint.Paint(1, 36, ConsoleColor.Black, ConsoleColor.White, "+-------------------------------------------------------------------------------------------+");

                        if (Console.CursorLeft==89)
                        {
                            for (int i = 0; i < 86; i++)
                            {
                                Console.SetCursorPosition(7 + i, 34);
                                ConsolePaint.Paint(7 + i, 34, ConsoleColor.Black, ConsoleColor.Black, " ");
                            }
                            speakerTarget = 0;
                            Console.SetCursorPosition(7, 34);
                        }
                        if (o == '*')
                        {
                            ConsolePaint.Paint(7, 34, ConsoleColor.Black, ConsoleColor.White, alstr);
                            Thread.Sleep(500);

                            continue;
                        }
                        else if (o == '&')
                        {
                            ConsolePaint.Paint(7, 34, ConsoleColor.Black, ConsoleColor.White, alstr);
                            Thread.Sleep(200);
                            continue;
                        }
                        else if (o == '%')
                        {
                            for (int i = 0; i < 86; i++)
                            {
                                Console.SetCursorPosition(7 + i, 34);
                                alstr += " ";
                            }
                            Console.SetCursorPosition(0, 0);
                            speakerTarget = 0;
                            continue;
                        }

                        Console.SetCursorPosition(7, 34);
                        alstr += o;
                        ConsolePaint.Paint(7, 34, ConsoleColor.Black, ConsoleColor.White, alstr);
                        speakerTarget++;

if (o != ' ' && Voice != Voice.None && Voice != Voice.Gaster)
                            SoundCore.Play("Speaker" + ((int)Voice).ToString());

                        if (o == '.' || o == ',')
                            Thread.Sleep(300);
                        else
                            Thread.Sleep(speed);
                    }
                }
                catch
                {

                }

                Thread.Sleep(1000);

                for (int i = 0; i < 86; i++)
                {
                    Console.SetCursorPosition(7 + i, 34);
                    Console.Write(" ");
                }
                Console.SetCursorPosition(0, 0);
                speakerTarget = 0;
            }

            speakerEnabed = false;
            Console.SetCursorPosition(1, 32);
            ConsolePaint.Paint(1, 32, ConsoleColor.Black, ConsoleColor.White, "                                                                                             ");
            Console.SetCursorPosition(1, 33);
            ConsolePaint.Paint(1, 33, ConsoleColor.Black, ConsoleColor.White, "                                                                                             ");
            Console.SetCursorPosition(1, 34);
            ConsolePaint.Paint(1, 34, ConsoleColor.Black, ConsoleColor.White, "                                                                                             ");
            Console.SetCursorPosition(1, 35);
            ConsolePaint.Paint(1, 35, ConsoleColor.Black, ConsoleColor.White, "                                                                                             ");
            Console.SetCursorPosition(1, 36);
            ConsolePaint.Paint(1, 36, ConsoleColor.Black, ConsoleColor.White, "                                                                                             ");
            return;

        f1:
            Thread.Sleep(600);
            foreach (string txt in text) {
                alstr = "";
                try {
                    foreach (char o in txt) {

                        if (Console.CursorLeft == 89) {
                            for (int i = 0; i < 86; i++) {
                                Console.SetCursorPosition(7 + i, 34);
                                ConsolePaint.Paint(7 + i, 34, ConsoleColor.Black, ConsoleColor.Black, " ");
                            }
                            speakerTarget = 0;
                            Console.SetCursorPosition(7, 34);
                        }
                        if (o == '*') {
                            Thread.Sleep(500);
                            continue;
                        } else if (o == '&') {
                            Thread.Sleep(200);
                            continue;
                        } else if (o == '%') {
                            for (int i = 0; i < 86; i++) {
                                Console.SetCursorPosition(7 + i, 34);
                                alstr += " ";
                            }
                            Console.SetCursorPosition(0, 0);
                            speakerTarget = 0;
                            continue;
                        }

                        Console.SetCursorPosition(7, 34);
                        alstr += o;
                        ConsolePaint.Paint(7, 34, ConsoleColor.Black, ConsoleColor.White, alstr);
                        speakerTarget++;
                        if (o != ' ' && Voice != Voice.None && Voice != Voice.Gaster)
                            SoundCore.Play("Speaker" + ((int)Voice).ToString());

                        if (o == '.' || o == ',')
                            Thread.Sleep(300);
                        else
                            Thread.Sleep(speed);
                    }
                } catch {

                }
                if (text.Length != 1)
                    Thread.Sleep(1000);

                for (int i = 0; i < 86; i++) {
                    Console.SetCursorPosition(7 + i, 34);
                    Console.Write(" ");
                }
                Console.SetCursorPosition(0, 0);
                speakerTarget = 0;
            }
            speakerEnabed = false;
            Clear();
            return;
        }

        public static void makeGlitch(int interval, bool aerror) {
            Pause = true;
            SoundCore.Play("Glitch");
            Thread.Sleep(interval);
            SoundCore.Sounds["Glitch"].Stop();
            if (aerror)
                Clear();

            Pause = false;
        }

        private static int _shakes = 0;
        private static int _intervalshake = 40;
        private static bool _shakesound = true;
        public static bool shakealready = false;
        public static void Shake(int shakecount, bool sound) {
            _shakes = shakecount;
            _shakesound = sound;
            _intervalshake = 40;
            Thread thrd = new Thread(_shake);
            thrd.Start();
        }
        public static void Shake(int shakecount, bool sound, int interval) {
            _intervalshake = interval;
            _shakes = shakecount;
            _shakesound = sound;
            Thread thrd = new Thread(_shake);
            thrd.Start();
        }
        private static void _shake() {
            if (shakealready)
                return;
            shakealready = true;
            if(_shakesound)
            SoundCore.Play("Error");
            for (int i = 0; i < _shakes; i++) {
                ConsolePaint.Clear();
                switch(Random.Next(0, 4)) {
                    case 0:
                        ConsolePaint.offset.X += Random.Next(0, 4);
                        break;
                    case 1:
                        ConsolePaint.offset.X -= Random.Next(0, 4);
                        break;
                    case 2:
                        ConsolePaint.offset.Y += Random.Next(0, 4);
                        break;
                    case 3:
                        ConsolePaint.offset.Y -= Random.Next(0, 4);
                        break;
                }
                Thread.Sleep(_intervalshake);
                ConsolePaint.offset = new ivector(0, 0);
            }
            ConsolePaint.Clear();
            ConsolePaint.offset = new ivector(0, 0);
            shakealready = false;
        }
        public static void Clear() {
            Pause = true;
            Console.BackgroundColor = ConsoleColor.Black;
            SoundCore.Play("Error");
            ConsolePaint.Clear();
            Thread.Sleep(150);
            Pause = false;
        }
    }
}
