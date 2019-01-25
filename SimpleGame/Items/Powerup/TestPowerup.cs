﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleGame.Items.Powerup
{
    public class TestPowerup : Base
    {
        
        public TestPowerup(int x, int y)
        {
            artifactColor = ConsoleColor.Yellow;
            artifactChar = "?";
            _this.type = Type.ENT_ITEM;
            _this.item = Item.IT_TEST_POWERUP;
            _this.oEvent = Event.EV_ITEM_SPAWN;
            _this.X = x;
            _this.Y = y;
            X = x;
            Y = y;
        }

        protected override void _pickup()
        {
            if (Program.Random.Next(0, 2) == 1)
            {
                SoundCore.Play("Drink");
                Thread.Sleep(2000);
                SoundCore.Play("Transformation");
                Program.Player.JUMP_WEIGHT = 15;
                Program.Player.npcColor = ConsoleColor.DarkMagenta;
                Thread.Sleep(useTime);
                SoundCore.Play("Star Falling");
                Program.Player.JUMP_WEIGHT = NPC.Player.JUMP_DEFAULT_WEIGHT;
                Program.Player.npcColor = ConsoleColor.Cyan;
            }
            else
            {
                SoundCore.Play("Drink");
                Thread.Sleep(2000);
                SoundCore.Play("Transformation");
                Program.Player.JUMP_WEIGHT = 4;
                Program.Player.npcColor = ConsoleColor.DarkGreen;
                Thread.Sleep(useTime);
                SoundCore.Play("Star Falling");
                Program.Player.JUMP_WEIGHT = NPC.Player.JUMP_DEFAULT_WEIGHT;
                Program.Player.npcColor = ConsoleColor.Cyan;
            }
        }
    }
}
