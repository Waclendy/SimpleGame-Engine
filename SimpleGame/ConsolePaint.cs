using System;

namespace SimpleGame
{
    public static class ConsolePaint
    {
        public static void Clear() {
            while (already) {

            }

            already = true;
            Console.ForegroundColor = DEFAULT_FOREGROUND;
            Console.BackgroundColor = DEFAULT_BACKGROUND;
            Console.Clear();
            GAME.GAME_InitChunkCopy();
            already = false;
        }
        public static void Paint(int x, int y, ConsoleColor backGround, ConsoleColor foreGround, string symbol)
        {
            while(already)
            {

            }
            try {
                already = true;
                Console.ForegroundColor = foreGround;
                Console.BackgroundColor = backGround;

                try {
                    if(!Flipped)
                    Console.SetCursorPosition(x + offset.X, y + offset.Y);
                    else
                    Console.SetCursorPosition(World.CHUNK_X - x + offset.X, World.CHUNK_Y - y + offset.Y);
                }

                catch { }
                Console.Write(symbol);
                Console.ForegroundColor = DEFAULT_FOREGROUND;
                Console.BackgroundColor = DEFAULT_BACKGROUND;
                already = false;
            }
            catch {
                already = false;
            }
        }
        public static ivector offset = new ivector(0, 0);
        public const ConsoleColor DEFAULT_BACKGROUND = ConsoleColor.Black;
        public const ConsoleColor DEFAULT_FOREGROUND = ConsoleColor.White;
        private static bool already = false;

        public static bool Flipped = false;
    }
}
