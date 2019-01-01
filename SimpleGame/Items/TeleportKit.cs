using System;
using System.Threading;

namespace SimpleGame.Items
{
    public class TeleportKit : Base
    {

        public int AviableStock = 10;
        private int curr = 0;
        public bool already = true;
        public TeleportKit()
        {
            itemName = "Random Teleport Kit #514";
            playerHad = true;
        }
        protected override void Main()
        {
            SoundCore.Play("TeleportUse");
            Random rnd = new Random();
            Gen:
            int rx = rnd.Next(1, World.CHUNK_X);
            int ry = rnd.Next(1, World.CHUNK_Y);

            if (Program.World.chunks[rx][ry] != null)
                goto Gen;

            Program.Player.Teleport(rx, ry, true);

            Console.SetCursorPosition(Program.Player.lastx, Program.Player.lasty);
            Console.Write(" ");

            Console.Clear();

            curr = AviableStock;
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
