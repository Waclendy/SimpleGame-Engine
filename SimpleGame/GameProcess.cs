using System;
using System.Threading;

namespace SimpleGame
{
    partial class Program
    {
        public static bool glitcheScreen = false;

        class ProcessThread
        {
           public void Run2()
            {
                while(ingame)
                {
                    Bot.Draw();
                    Bot.ProcessPlayer();
                }
            }
            public void Run()
            {
                Thread thrd = new Thread(Run2);


                //thrd.Start();

                    for (int x = 0; x < World.CHUNK_X; x++)
                    {

                        for (int y = 0; y < World.CHUNK_Y; y++)
                        {
                            if (World.chunks[x][y] == null) continue;

                            else if (World.chunks[x][y].Type == TileType.Spawnpoint)
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
                                        if (World.getTile(Player.X + 1, Player.Y) != TileType.None)
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
                                Player._this.oEvent = Event.EV_PLAYER_JUMP;
                                    break;
                                case ConsoleKey.O:
                                //if (!World.customgen) {
                                //    Misc.vrMenu(Element.EL_MOD_IMPORT_BLOCK_GEN, Element.EL_WORLD_LOAD, Element.EL_MOD_IMPORT_BLOCK_GEN, Element.EL_CHUNKS_LOAD, Element.EL_MOD_IMPORT_BLOCK_GEN, Element.EL_MOD_IMPORT_BLOCK_GEN);
                                //    World.customgen = true;
                                //}
                                Reluics.Add(new Items.Powerup.God(30, 17));
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
                                    Player._this.oEvent = Event.EV_PLAYER_JUMP;
                                break;

                            }

                        }
                        catch
                        {
                        Program.makeGlitch(1000, false);
                        Program.speakerEnabed = false;
                        Program.speakerSay(100, Voice.Soup, "В последний раз ты видел ошибку в игре доволно давно.");
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
            while (Pause) { }

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
                        while (Pause) { }
                        if (o == '*')
                        {
                            Thread.Sleep(500);
                            continue;
                        }
                        else if (o == '&')
                        {
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

                        if(o != ' ' && Voice == Voice.Gaster)
                        {
                            SoundCore.Play("Under", "wav", "speak_g", 0, 5, 100);
                        }
                        else if (o != ' ' && Voice != Voice.None && Voice != Voice.Gaster)
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
                if (text.Length != 1)
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
            return;

        f1:
            Thread.Sleep(600);
            foreach (string txt in text) {
                alstr = "";
                Clear();
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
                        while (Pause) { }
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

                        if (o != ' ' && Voice == Voice.Gaster) {
                            SoundCore.Play("Under", "wav", "speak_g", 0, 5, 100);
                        } else if (o != ' ' && Voice != Voice.None && Voice != Voice.Gaster)
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
