using SFML.Audio;
using SimpleGame.Shop;
using System;
using System.Collections.Generic;

using static SimpleGame.Tile;
using static SimpleGame.Misc;
using static SimpleGame.GAME;
using static SimpleGame.Program;
using System.Text;
using System.Threading;

namespace SimpleGame {
    public class WorldShop {

        public List<Shop.Item> items;
        public List<string> tables;

        public Music theme = new Music(SoundCore.CONTENT_PATH + "\\Music\\_shopframe1.wav");
        public Music sanc = new Music(SoundCore.CONTENT_PATH + "\\Music\\_activateframe.wav");
        public Music theme_order = new Music(SoundCore.CONTENT_PATH + "\\Music\\_island_borken.wav");
        public bool processing = false;
        public bool shopOpened = false;
        public bool freeBuy = false;
        private int selectedID = 0;
        public WorldShop() {
            items = new List<Shop.Item>();
            int itam = Program.Random.Next(5, 12);

            for(int i = 0; i < itam; i ++) {
                int num1 = Program.Random.Next(0, 8);
                switch(num1) {

                    case 0:
                        //items.Add((Jump)new Jump().Load());
                    break;
                    case 1:
                        //items.Add((Buth)new Buth().Load());
                        break;
                    case 2:
                    ;
                        break;
                    case 3:
               
                        break;
                    case 4:
                        items.Add((ExtraLife)new ExtraLife().Load());
                        break;
                    case 5:
                        
                        break;
                    default:
                        try {
                            int num2 = Program.Random.Next(0, Plugin.modShops.Length);
                            items.Add(new Shop.Item(Plugin.modShops[num2]));
                        }
                        catch {

                        }
                        break;
                }
            }
            items.Add((AsgardRune)new AsgardRune().Load());
            theme.Volume = 50;
            theme.Loop = true;
        }
        public void Buy(int id) {

            if(!items[id].itemExists) {
                SoundCore.Play("Shop Already");
                return;
            }

            if(freeBuy) {
                float i = 99999;
                items[id].Buy(ref i);
                freeBuy = false;
                SoundCore.Play("Shop Buy");
                tables = new List<string>();
                foreach (Shop.Item item in items) {
                    string result = "";
                    result += item.itemName;
                    int sum1 = 40 - result.Length;

                    for (int i2 = 0; i2 < sum1; i2++) {
                        result += " ";
                    }

                    if (item.itemExists)
                        result += item.itemSLabel +": " + item.itemPrice + Misc.format(" " + item.itemCLabel, (int)item.itemPrice);
                    else
                        result += item.itemSLabel +": " + item.itemBLabel;

                    tables.Add(result);
                }
                tables.Add("Обмен");
                tables.Add("Выход");
                return;
            }

            bool success = items[id].Buy(ref Program.Coins);

            if(success) {
                SoundCore.Play("Shop Buy");

                string result = "";
                result += items[id].itemName;
                int sum1 = 40 - result.Length;

                for (int i = 0; i < sum1; i++) {
                    result += " ";
                }
                    result += items[id].itemSLabel + ": " + items[id].itemBLabel;
               
                tables[id] = result;
            }
            else {
                SoundCore.Play("Shop NotEnough");
            }
        }
        private void _randomtext(string text) {
            Console.SetCursorPosition(rand(0, 99), rand(10, 40));
            Console.Write(text);
        }
        public void Open() {

            if (shopOpened)
                return;

            Program.mainMusic.Pause();
            ConsolePaint.Clear();

            Program.Pause = true;
            SoundCore.Play("Shop Open");

            sanc.Loop = true;
            
            shopOpened = true;
            theme.Play();
            if (freeBuy) {

                tables = new List<string>();
                foreach (Shop.Item item in items) {
                    string result = "";
                    result += item.itemName;
                    int sum1 = 40 - result.Length;

                    for (int i = 0; i < sum1; i++) {
                        result += " ";
                    }

                    if (item.itemExists)
                        result += item.itemSLabel + ": БЕСПЛАТНО";
                    else
                        result += item.itemSLabel + ": " + item.itemBLabel;

                    tables.Add(result);
                }
                tables.Add("Обмен");
                tables.Add("Выход");
            } else {
                tables = new List<string>();
                foreach (Shop.Item item in items) {
                    string result = "";
                    result += item.itemName;
                    int sum1 = 40 - result.Length;

                    for (int i = 0; i < sum1; i++) {
                        result += " ";
                    }

                    if (item.itemExists)
                        result += item.itemSLabel + ": " + item.itemPrice + Misc.format(" " + item.itemCLabel, (int)item.itemPrice);
                    else
                        result += item.itemSLabel + ": " + item.itemBLabel;

                    tables.Add(result);
                }
                tables.Add("Обмен");
                tables.Add("Выход");
            }

        }
        
        public void Close() {
            Program.Pause = false;
            SoundCore.Play("Shop Close");
            shopOpened = false;
            ConsolePaint.Clear();
            Program.mainMusic.Play();
            theme.Pitch = 1;
            theme.Volume = 100;
            theme.Stop();
        }

        public void Update() {

            if (!shopOpened)
                return;

            
            if(freeBuy) {

                tables = new List<string>();
                foreach (Shop.Item item in items) {
                    string result = "";
                    result += item.itemName;
                    int sum1 = 40 - result.Length;

                    for (int i = 0; i < sum1; i++) {
                        result += " ";
                    }

                    if (item.itemExists)
                        result += "Цена: БЕСПЛАТНО";
                    else
                        result += item.itemSLabel + ": " + item.itemBLabel;

                    tables.Add(result);
                }
                tables.Add("Обмен");
                tables.Add("Выход");
        }

           
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key) {
                case ConsoleKey.Escape:
                case ConsoleKey.U:
                    Close();
                    break;
                case ConsoleKey.DownArrow:
                    SoundCore.Play("Menu Beep");
                    if (selectedID < tables.Count - 1) {
                        try {

                            if (selectedID + 1 == tables.Count)
                                selectedID = selectedID;
                            else if (items[selectedID + 1].itemExists != true)
                                selectedID += 2;
                            else
                                selectedID++;
                        } catch { selectedID++; }

                    } else
                        selectedID = 0;
                    break;
                case ConsoleKey.UpArrow:
                    SoundCore.Play("Menu Beep");
                    if (selectedID > 0) {
                        try {

                            if (selectedID - 1 < 0)
                                selectedID = selectedID;
                            else if (items[selectedID - 1].itemExists != true)
                                selectedID -= 2;
                            else
                                selectedID--;
                        } catch { selectedID--; }

                    } else
                        selectedID = tables.Count - 1;
                    break;

                case ConsoleKey.Enter:
                    if (tables[selectedID] == "Обмен") {
                        if(ScorePoints >= 5) {
                            ScorePoints -= 5;
                            Program.Coins += 100f;
                            SoundCore.Play("Shop Buy");
                        }
                        else {
                            SoundCore.Play("Shop NotEnough");
                        }
                        return;
                    }
                    else if (tables[selectedID] == "Выход") {
                        Close();
                        return;
                    }
                    Buy(selectedID);
                    break;
            }
        }
        public void Process() {
            if (processing)
                return;

            processing = true;
            while(shopOpened) {
                if(SoundCore.Sounds["Shop Open"].Status != SoundStatus.Playing) {
                    theme.Volume = 300;
                    processing = false;
                    return;
                }
                else {
                    theme.Volume = 50;
                }
            }
            processing = false;
        }
        public void Draw() {
            if (!shopOpened)
                return;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            ConsolePaint.Clear();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("     " + "Магазин");

            sboolean isselected = 0;

            for (int i = 0; i < tables.Count; i++) {

                if (i == selectedID) {

                    if (i < items.Count) {
                        Console.SetCursorPosition(0, 10 + tables.Count);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Misc.writeFormat("     ## " + items[i].itemDescription);
                        Console.WriteLine("\n");
                        Misc.writeFormat("        " + items[i].itemBuths);
                        //Console.WriteLine("     ## {0}", items[i].itemDescription);
                    } else if (i == tables.Count - 2) {
                        Console.SetCursorPosition(0, 10 + tables.Count);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("     ## Превратить очки в детали. 5 очков = 100 деталей");
                    } else if (i == tables.Count - 1){
                            Console.SetCursorPosition(0, 10 + tables.Count);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("     ## Выйти из магазина.");
                    }
                    isselected = 1;
                } else {
                    isselected = 0;
                }

                Console.SetCursorPosition(0, 2 + i);

           
                if (tables[i] != "X") {
                    if (!isselected) {
                        try {
                            if (items[i].itemExists == true) {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("     [ ] ");
                            } else {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("     [-] ");
                            }
                        }
                        catch {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("     [ ] ");
                        }
                    } 
                    else {
                        try {
                            if (items[i].itemExists != true) {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("     [X] ");
                                
                            } else {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("     [X] ");
                            }
                        }
                        catch {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("     [>] ");
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(tables[i]);
                 }

            }

            Console.SetCursorPosition(0, 3 + tables.Count);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("     " + "Деталей: {0}", Program.Coins);
        }
    }
}
