using System;
using System.IO;

namespace SimpleGame
{
    partial class Program
    {
        public static void Explode(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("*");

            Console.SetCursorPosition(x - 1, y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("*");

            Console.SetCursorPosition(x + 1, y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("*");

            Console.SetCursorPosition(x, y - 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("*");

            Console.SetCursorPosition(x, y + 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("*");

        }
        public static bool emptyCheck(int localx, int localy) => World.getTile(localx, localy) == TileType.None;
       
    }
}
