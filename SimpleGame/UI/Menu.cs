using System;

namespace SimpleGame.UI
{
    class Menu
    {

        UI ui = new UI();
        Input input = new Input();

        public bool exitMenu = false;
        public int mMenuListSelectedId = 0;

        public int MainMenu(params string[] items)
        {
            ui.mMenuList = items;
            while (exitMenu == false)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("    Главное Меню");
                ui.MainMenuList(mMenuListSelectedId);
                int iu = input.MainMenuList(this, ui);
                return iu;
            }
            return 404;
        }
    }
}
