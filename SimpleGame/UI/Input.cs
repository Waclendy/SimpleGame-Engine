using System;

namespace SimpleGame.UI
{
    class Input
    {

        //jajaja
     
        public int MainMenuList(Menu menu, UI ui)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch(key.Key)
            {
                case ConsoleKey.DownArrow:
                    SoundCore.Play("Menu Beep");
                    if (menu.mMenuListSelectedId < ui.mMenuList.Length - 1)
                        menu.mMenuListSelectedId++;
                    else
                        menu.mMenuListSelectedId = 0;
                    break;
                case ConsoleKey.UpArrow:
                    SoundCore.Play("Menu Beep");
                    if (menu.mMenuListSelectedId > 0)
                        menu.mMenuListSelectedId--;
                    else
                        menu.mMenuListSelectedId = ui.mMenuList.Length - 1;
                    break;
                case ConsoleKey.LeftArrow:

                    break;
                case ConsoleKey.RightArrow:

                    break;
                case ConsoleKey.Enter:
                    return menu.mMenuListSelectedId;
            }
            return 404;
        }
    }
}
