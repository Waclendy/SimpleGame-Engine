using System;

namespace SimpleGame
{
    public static class ConsolePaint
    {
        public static void Paint(int x, int y, ConsoleColor backGround, ConsoleColor foreGround, string symbol)
        {
            while(already)
            {

            }

            already = true;
            Console.ForegroundColor = foreGround;
            Console.BackgroundColor = backGround;
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
            Console.ForegroundColor = DEFAULT_FOREGROUND;
            Console.BackgroundColor = DEFAULT_BACKGROUND;
            already = false;
        }
        public const ConsoleColor DEFAULT_BACKGROUND = ConsoleColor.Black;
        public const ConsoleColor DEFAULT_FOREGROUND = ConsoleColor.White;
        private static bool already = false;
    }
}
