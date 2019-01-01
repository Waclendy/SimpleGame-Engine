using System;
using System.Threading;

namespace SimpleGame.Items
{
    public class NoClip : Base
    {

        public int AviableStock = 20;
        private int curr = 0;
        public bool already = true;
        public bool already2 = true;
        private bool Enabled = false;
        private int enabledTime = 10;
        private int curr2 = 0;
        public NoClip()
        {
            itemName = "NoClip Drink";
            playerHad = true;
        }
        protected override void Main()
        {
            SoundCore.Play("Drink");
            Thread.Sleep(3000);
            SoundCore.Play("Beep");
            Program.Player.inGravity = false;
            curr = AviableStock;
            curr2 = enabledTime;
            Enabled = true;

        }
        public void Check()
        {
            Thread thrd = new Thread(_check);
            thrd.Start();
        }
        private void _check()
        {
            while (!playerHad)
            {

                if(Enabled)
                {
                   

                    if (curr2 != 0)
                    {
                        curr2--;
                        Thread.Sleep(1000);
                        already2 = false;
                    }
                    else if (!already2)
                    {
                        Enabled = false;
                        Program.Player.inGravity = true;
                        SoundCore.Play("Beep");
                    }
                }
                else
                if (curr != 0)
                {
                    curr--;
                    Thread.Sleep(1000);
                    already = false;
                }
                else if (!already)
                {
                    already = true;
                    playerHad = true;
                   
                    SoundCore.Play("TeleportReload");
                }
            }
        }
    }
}
