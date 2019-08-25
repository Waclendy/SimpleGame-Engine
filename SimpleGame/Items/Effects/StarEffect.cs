using System;
using System.Threading;

namespace SimpleGame.Items.Effects
{
   
    public class StarEffect : Base
    {
        public StarEffect()
        {
            ButhTime = 500;
        }

        protected override void preMain()
        {
            Program.Player.npcColor = ConsoleColor.Red;
            Program.Player.npcChar = "*";
            Program.Player.JUMP_WEIGHT = 9;
        }
        protected override void Main()
        {
            Thread.Sleep(ButhTime);
            Program.Player.JUMP_WEIGHT = Program.Player.JUMP_DEFAULT_WEIGHT;
            Program.Player.npcColor = ConsoleColor.Cyan;
            Program.Player.npcChar = "X";
        }

    }
}
