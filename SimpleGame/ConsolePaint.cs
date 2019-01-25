using System;

namespace SimpleGame
{
    public static class ConsolePaint
    {
        public static void Clear() {
            while (already) {

            }

            if (Program.Pause)
                return;

            already = true;
            Console.ForegroundColor = DEFAULT_FOREGROUND;
            Console.BackgroundColor = DEFAULT_BACKGROUND;
            Console.Clear();
            already = false;
        }
        public static void Paint(int x, int y, ConsoleColor backGround, ConsoleColor foreGround, string symbol)
        {
            while(already)
            {

            }

            if (Program.Pause)
               return;

            try {
                already = true;
                Console.ForegroundColor = foreGround;
                Console.BackgroundColor = backGround;
                Console.SetCursorPosition(x, y);
                Console.Write(symbol);
                Console.ForegroundColor = DEFAULT_FOREGROUND;
                Console.BackgroundColor = DEFAULT_BACKGROUND;
                already = false;
            }
            catch {
                SoundCore.Play("Error");
                already = false;
            }
        }
        public const ConsoleColor DEFAULT_BACKGROUND = ConsoleColor.Black;
        public const ConsoleColor DEFAULT_FOREGROUND = ConsoleColor.White;
        private static bool already = false;
    }
}
