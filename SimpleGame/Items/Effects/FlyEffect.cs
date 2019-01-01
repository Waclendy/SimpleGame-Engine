using System;
using System.Threading;

namespace SimpleGame.Items.Effects
{
   
    public class FlyEffect : Base
    {
        public FlyEffect()
        {
            ButhTime = 1500;
        }

        protected override void preMain()
        {
            
                Program.Player.npcColor = ConsoleColor.Magenta;
                SoundCore.Play("Beep");
                Program.Player.inGravity = false;
        }
        protected override void Main()
        {
            Thread.Sleep(ButhTime);
            if (!Program.Player.inGravity)
            {
                Program.Player.npcColor = ConsoleColor.Cyan;
                SoundCore.Play("Beep");
                Program.Player.inGravity = true;
            }
        }

    }
}
