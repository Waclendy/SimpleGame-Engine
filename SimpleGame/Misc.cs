using System;
using System.Collections.Generic;
using System.Threading;

using static SimpleGame.Misc;
using static SimpleGame.GAME;
using static SimpleGame.Tile;
using static SimpleGame.Program;

namespace SimpleGame {

    public struct entity {
        public Type type;
        public Item item;
        public Event oEvent;
        public Flag oFlags;
        public bool spawnSoundEnabled;
        public int X;
        public int Y;

    }
    //OLD ENUMS
    public enum Direction : int {
        NONE = 0,
        RIGHT = 1,
        LEFT = -1
    }
    public enum DirectionY : int {
        NONE = 0,
        UP = -1,
        DOWN = 1
    }
    public enum DirectionX : int {
        NONE = 0,
        RIGHT = 1,
        LEFT = -1
    }
    public enum Voice : int {
        Default = 0,
        Flowey = 2,
        Soup = 3,
        Buer = 4,
        Gaster = 6,
        Asmodeus = 7, 
        None = -1
    }

    //NEWEST ENUMS
    public enum Type {
        ENT_PLAYER,
        ENT_GLOBAL,
        ENT_ITEM,
        ENT_NONE
    }
    public enum Event {
        EV_ITEM_SPAWN,
        EV_PLAYER_JUMP,
        EV_PLAYER_DEAD,
        EV_NONE
    }
    public enum Item {
        IT_STAR_FRUIT,
        IT_STAR,
        IT_TEST_POWERUP,
        IT_GOD,
        IT_COIN,
        IT_URAN
    }
    [Flags]
    public enum Flag {
        FL_NONE,
        FL_NOTDEADABLE,
        FL_NOGRAVITY
    }
    public enum Element {
        EL_CHUNKS_LOAD,
        EL_WORLD_LOAD,
        EL_MOD_IMPORT_UPAD,
        EL_MOD_IMPORT_JUMPPAD,
        EL_MOD_IMPORT_URAN,
        EL_MOD_IMPORT_URAN_USAGE,
        EL_MOD_IMPORT_BLOCK_GEN
    }
    public static class Offsets {
        public static int OF_NONE = 0;
        public static int OF_SMALL = 1;
        public static int OF_MEDIUM = 10;
        public static int OF_HUGE = 15;
    }
    public static partial class Misc {
        public static void LOG(string message) {
            System.Windows.Forms.MessageBox.Show(message + "                                                                                ", "LOG");
        }
        public static string format(string text, int num) {
            //if (num == 1)
            //    return text + "a";
            //if (num == 2)
            //    return text + "ы";
            //if (num == 3)
            //    return text + "ы";
            //if (num == 4)
            //    return text + "ы";

            //for (int i = 0; i < 300; i += 10)
            //    if (num > 21 && num > i - 10 && num <= (4 + i - 10))
            //        return text + "ы";

            return text;

        }
        public static void writeFormat(string text) {
            ConsoleColor currentColor = ConsoleColor.White;
            for(int i = 0; i < text.Length; i++) {
                if(text[i] == '^') {
                    currentColor = ConsoleColor.White;
                    continue;
                }
                if (text[i] == '<') {
                    currentColor = ConsoleColor.Green;
                    continue;
                }
                if (text[i] == '>') {
                    currentColor = ConsoleColor.Red;
                    continue;
                }

                ConsolePaint.Paint(Console.CursorLeft, Console.CursorTop, ConsoleColor.Black, currentColor, text[i].ToString());
            }
        }
        public static int calcWorldPathSum(ivector startpos, ivector endpos) {
            int num1 = (endpos.X - startpos.X) + (endpos.Y - startpos.Y);
            if (num1 < 0)
                num1 *= -1;
            return num1;
        }
        public static string[] calcWorldPath(ivector startpos, ivector endpos) {
            List<string> path = new List<string>();
            var x = 0;
            var y = 0;

            for(int i = 0; i < calcWorldPathSum(startpos, endpos); i ++) {
                if (startpos.X + x < endpos.X && Program.World.getTile(startpos.X + x, startpos.Y + y) == "0") {
                    path.Add("+X");
                    x++;
                } else if (startpos.X + x >= endpos.X && Program.World.getTile(startpos.X + x, startpos.Y + y) == "0")
                    if (startpos.Y + y < endpos.Y && Program.World.getTile(startpos.X + x, startpos.Y + y) == "0") {
                        path.Add("+Y");
                        y++;
                    } else if (startpos.Y + y > endpos.Y && Program.World.getTile(startpos.X + x, startpos.Y + y) == "0") {
                        path.Add("-Y");
                        y--;
                    }
            }

            return path.ToArray();
            
        }
        public static void drawCalcWorldPath(ivector startpos, ivector endpos) {
            string[] path = calcWorldPath(startpos, endpos);
            var x = 0;
            var y = 0;

            foreach(string pathelement in path) {
                switch (pathelement) {
                    case "-X":
                        x--;
                        break;
                    case "+X":
                        x++;
                        break;
                    case "-Y":
                        y--;
                        break;
                    case "+Y":
                        y++;
                        break;
                }
                ConsolePaint.Paint(startpos.X + x, startpos.Y + y, System.ConsoleColor.Black, System.ConsoleColor.Red, ".");
            }
        }
        public static void dispatchEvent(ref entity ent) {
            if (ent.oEvent == Event.EV_NONE)
              return;
            if (ent.type == Type.ENT_PLAYER) { //PLAYER
                switch (ent.oEvent) {
                    case Event.EV_PLAYER_JUMP:
                        Player.Jump();
                        break;
                    case Event.EV_PLAYER_DEAD:
                        if (ent.oFlags.HasFlag(Flag.FL_NOTDEADABLE))
                            return;
                        if (lifebonus == 0 ) {

                        Player.Kill();
                            worldframe++;
                        Thread.Sleep(300);

                        StarFruitEnabled = false;
                        Console.Clear();
                        Intil();
                        Program.World.Generate();
                        Thread.Sleep(200);
                        Player.Teleport(Program.World.panels[0].X + 2, Program.World.panels[0].Y - 5, true);
                         }
                         else
                         {
                            lifebonus -= 1;
                            StarFruitEnabled = false;
                            Console.Clear();
                            //Program.Intil();
                           // Program.World.Generate();
                            SoundCore.Play("Error");
                            Player.Teleport(Player.lastPos, true);

                         }
                        break;
                    default:
                            LOG($"Unknown event for this entity: {ent.type} : {ent.oEvent}");
                        break;
                }
            } else if (ent.type == Type.ENT_GLOBAL) { //GLOBAL
                switch (ent.oEvent) {
                    default:
                            LOG($"Unknown event for this entity: {ent.type} : {ent.oEvent}");
                        break;
                }
            } else if (ent.type == Type.ENT_ITEM) { //ITEM
                switch (ent.oEvent) {    
                    case Event.EV_ITEM_SPAWN:
                        if (ent.X <= 0)
                            ent.X = 0;
                        else if (ent.X >= 100)
                            ent.X = 99;
                        if (ent.Y <= 0)
                            ent.Y = 0;
                        if(ent.spawnSoundEnabled)
                            SoundCore.Play("Item Spawned");
                        break;
                    default:
                        LOG($"Unknown event for this entity: {ent.type} : {ent.oEvent}");
                        break;
                }
            } else {
                LOG($"Unknown entity type: {ent.type}");
            }

            ent.oEvent = Event.EV_NONE;
        }

    }

    public static partial class Misc {
        private static void Wait(int interval) => Thread.Sleep(interval);
        private static void Bar(string label, int interval) {
            Console.WriteLine("\n");
            textLabel(ConsoleColor.White, label, 0);
            Console.BackgroundColor = ConsoleColor.White;
            string bar = "";
            for(int i = 0; i < 104; i ++) {
                Wait(interval);
                bar += " ";
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(bar);
            }
        }
        private static void textHead(ConsoleColor color, string label, int interval) {
            Console.ForegroundColor = color;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(label + "\n");
            Thread.Sleep(interval);
        }
        private static void textBar(string label, int interval, int rmin, int rmax, bool result) {
            var i = 0;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            do {

                i += Program.Random.Next(rmin, rmax);
                if (i >= 100)
                    i = 100;

                Console.WriteLine($"{label} {i}%");
                Thread.Sleep(interval);

            } while (i < 100);
        }
        private static void textLabel(ConsoleColor color, string text, int interval) {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = color;
            Console.WriteLine($"{text}");
            Wait(interval);
        }
        public static void vrMenu(params Element[] elements) {

            Program.Pause = true;
            Program.mainMusic.Pause();
            SoundCore.Sounds["UI"].Play();
            SoundCore.Sounds["Glitch"].Play();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            foreach (Element el in elements) {
                switch (el) {
                    case Element.EL_CHUNKS_LOAD:
                        textLabel(ConsoleColor.White, "loading world chunks", 15);
                        textBar("reading chnk.txt", 10, 4, 8, true);
                        textBar("writing chunks[][]", 40, 7, 11, true);
                        textBar("drawing chnk.ui.mthd", 20, 2, 4, true);
                        Wait(600);
                        break;
                    case Element.EL_WORLD_LOAD:
                        textLabel(ConsoleColor.White, "creating world model", 15);
                        textBar("generating the world", 15, 2, 3, true);
                        textBar("writing chnk.txt", 15, 4, 7, true);
                        textBar("writing chnk.ui", 15, 2, 4, true);
                        textBar("making chnk.ui.mtdh", 15, 11, 12, true);
                        textBar("drawing chnk.ui.mthd", 15, 2, 4, true);
                        textLabel(ConsoleColor.Blue, "chunkset generated successfull", 15);
                        Wait(600);
                        break;
                    case Element.EL_MOD_IMPORT_UPAD:
                        textLabel(ConsoleColor.White, "start import uran_panel.mdl", 15);
                        textLabel(ConsoleColor.White, "start import uran_panel.trg", 15);
                        textLabel(ConsoleColor.White, "start import uran_panel.clr.p", 15);
                        textLabel(ConsoleColor.White, "System.ModImport.Misc.Parse(%arg1, %arg2, %arg3)", 15);
                        textBar("loading uran_panel.mdl*trg*p", 30, 5, 12, true);
                        Wait(1000);
                        break;
                    case Element.EL_MOD_IMPORT_JUMPPAD:
                        textLabel(ConsoleColor.White, "start import jmp.mdl", 15);
                        textLabel(ConsoleColor.White, "start import jmp.trg", 15);
                        textLabel(ConsoleColor.White, "start import jmp.clr.p", 15);
                        textLabel(ConsoleColor.White, "System.ModImport.Misc.Parse(%arg1, %arg2, %arg3)", 15);
                        textBar("loading jmp.mdl*trg*p", 30, 5, 12, true);
                        textBar("triggering ent<<player.jump()", 30, 5, 7, true);
                        break;
                    case Element.EL_MOD_IMPORT_URAN:
                        textLabel(ConsoleColor.White, "start import uran.mdl", 15);
                        textLabel(ConsoleColor.White, "start import uran.trg", 15);
                        textLabel(ConsoleColor.White, "start import uran.snd", 15);
                        textLabel(ConsoleColor.White, "start import uran.clr.p", 15);
                        textLabel(ConsoleColor.White, "System.ModImport.Misc.Parse(%arg1, %arg2, %arg4)", 15);
                        textBar("loading uran.mdl*trg*p", 15, 5, 12, true);
                        textLabel(ConsoleColor.White, "set EV_ITEM_PICKUP sound -%arg3", 15);
                        textBar("triggering world.setTile(tp**uran)", 15, 7, 8, true);
                        textBar("triggering world.reluics", 15, 5, 7, true);
                        textBar("triggering world.reluics.draw(uran)", 15, 8, 9, true);
                        Bar("RELOADING WORLD TRIGGERS", 7);
                        break;
                    case Element.EL_MOD_IMPORT_BLOCK_GEN:
                        textLabel(ConsoleColor.White, "exporting mod 'errwolrd.mod'", 15);
                        textBar("exporting assets", 15, 3, 11, true);
                        textLabel(ConsoleColor.White, "exporting file 'dev.dialog.txt'", 15);
                        textLabel(ConsoleColor.White, "exporting file 'uran.mdl'", 15);
                        textLabel(ConsoleColor.White, "exporting file 'block.struct'", 15);
                        textBar("exporting wrc files", 30, 3, 11, true);
                        textLabel(ConsoleColor.White, "exporting file 'dev.dialog.txt'", 15);
                        textLabel(ConsoleColor.White, "exporting file 'block.struct'", 15);
                        textLabel(ConsoleColor.White, "exporting file 'uran.mdl'", 15);
                        break;
                    case Element.EL_MOD_IMPORT_URAN_USAGE:
                        textLabel(ConsoleColor.White, "retrying import uran.trg", 15);
                        textLabel(ConsoleColor.White, "System.ModImport.Misc.Parse(import(uran).%arg1, %arg2, import(uran).%arg4)", 15);
                        textBar("triggering world.reluics", 15, 5, 7, true);
                        textBar("triggering world.reluics._pickup(x,y)", 15, 5, 7, false);
                        textLabel(ConsoleColor.Red, "error load uran.trg ~ empty trigger", 15);
                        break;
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            SoundCore.Sounds["UI"].Stop();
            SoundCore.Sounds["Glitch"].Stop();
            SoundCore.Sounds["Error"].Play();
            Program.Pause = false;
            Program.mainMusic.Play();
        }
    }
}
