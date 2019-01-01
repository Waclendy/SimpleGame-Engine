using System;
using System.Threading;

namespace SimpleGame.Items.Effects
{
   
    public class JumpPad : Base
    {
        public JumpPad()
        {
            ButhTime = 500;
        }

        protected override void preMain()
        {
            Program.Player.npcColor = ConsoleColor.Blue;
            Program.Player.trailType = TileType.JumpEffectBackground;
            Program.Player.JUMP_WEIGHT = 11;
        }
        protected override void Main()
        {
            Thread.Sleep(ButhTime);
            Program.Player.JUMP_WEIGHT = NPC.Player.JUMP_DEFAULT_WEIGHT;
            Program.Player.npcColor = ConsoleColor.Cyan;
            Program.Player.trailType = TileType.None;
        }

    }
}
