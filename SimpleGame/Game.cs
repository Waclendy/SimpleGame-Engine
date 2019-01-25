using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGame
{
    public class Game
    {
        public void Update()
        {

                if (Program.Pause)
                    return;

                Program.World.Update();
                Program.Player.Update();
                Program.Player.ProcessPlayer();




                try
                {
                    foreach (Items.Powerup.Base basic in Program.Reluics)
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
