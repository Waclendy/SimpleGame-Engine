using System;
using System.Threading;

namespace SimpleGame.Items
{
    public class itemEventArgs : EventArgs
    {
        public itemEventArgs(string itemName)
        {
            this.itemName = itemName ?? throw new ArgumentNullException(nameof(itemName));
        }
        public string itemName { get; private set; }
    }
    public delegate void itemEvent(itemEventArgs eventArgs);
    public class Base
    {
        public event itemEvent itemUse = null;

        public string itemName = "player";
        public bool playerHad = false;
        protected Thread Processor;
        public void Use()
        {
            if (Program.stopAll)
                return;

            if (playerHad)
            {
                Processor = new Thread(Main);
                Processor.Start();
                playerHad = false;
                if(itemUse != null)
                    itemUse.Invoke(new itemEventArgs(itemName));
            }
        }
        protected virtual void Main()
        {
        }

    }
}
