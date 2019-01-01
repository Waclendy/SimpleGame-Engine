using System;
using System.Threading;

namespace SimpleGame.Items.Effects
{
   
    public class Base
    {

        protected Thread Processor;
        protected int ButhTime = 5 * 1000;
        public void Enable(double buthTime)
        {
            ButhTime = (int)(buthTime * 1000);
            preMain();
            Processor = new Thread(Main);
            Processor.Start();
        }
        protected virtual void preMain()
        {

        }
        protected virtual void Main()
        {
        }

    }
}
