using System;

namespace SimpleGame.UI
{
    class UI
    {
        public string[] mMenuList = new string[]
        {
            
        };


        public void MainMenuList(int selectedID)
        {
           for(int i = 0; i < mMenuList.Length; i++)
           {
               if(i == selectedID)
               {
                   Console.ForegroundColor = ConsoleColor.Yellow;
               }
               else
               {
                    Console.ForegroundColor = ConsoleColor.White;
               }
               
               Console.SetCursorPosition(0, 2 + i);
                if(mMenuList[i] != "X")
               Console.WriteLine("    "+ (i+1) + ". " + mMenuList[i]);
           }

        }

    }
}
