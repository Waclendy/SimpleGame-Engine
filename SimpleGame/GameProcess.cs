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
                while(ingame)
                {
                    if (stopAll)
                        continue;

                    if (speakerEnabed)
                        continue;



                    World.Update();

                    Player.Draw();
                    Player.ProcessPlayer();




                    try
                    {
                        foreach (Items.Powerup.Base basic in Reluics)
                        {
                            basic.Update();
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
        public static void speakerSay(int speed, Voice Voice, params string[] text)
        {
            while (stopAll) { }
            if (speakerEnabed)
                return;
            speakerEnabed = true;
            Thread.Sleep(300);
            Console.ForegroundColor = ConsoleColor.White;
            speakerTarget = 0;
            Console.SetCursorPosition(1, 32);
            Console.Write("+-------------------------------------------------------------------------------------------+");
            Console.SetCursorPosition(1, 33);
            Console.Write("|                                                                                           |");
            Console.SetCursorPosition(1, 34);
            Console.Write("| >>>                                                                                       |");
            Console.SetCursorPosition(1, 35);
            Console.Write("|                                                                                           |");
            Console.SetCursorPosition(1, 36);
            Console.Write("+-------------------------------------------------------------------------------------------+");

            foreach (string txt in text)
            {

                try
                {
                    foreach (char o in txt)
                    {
                        if(Console.CursorLeft==89)
                        {
                            for (int i = 0; i < 86; i++)
                            {
                                Console.SetCursorPosition(7 + i, 34);
                                Console.Write(" ");
                            }
                            speakerTarget = 0;
                            Console.SetCursorPosition(7, 34);
                        }
                        while (stopAll) { }
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
                                Console.Write(" ");
                            }
                            Console.SetCursorPosition(0, 0);
                            speakerTarget = 0;
                            continue;
                        }

                        Console.SetCursorPosition(7 + speakerTarget, 34);
                        Console.Write(o);
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

        }
    }
}
