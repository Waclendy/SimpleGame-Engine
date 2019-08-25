using System;
using System.IO;
using Newtonsoft.Json;
namespace SimpleGame.Shop {
    public class Item {

        public string itemName = "Uran Powerup";
        public string trueName = "uran";
        public float itemPrice = -772.5f;
        public bool itemExists = true;
        public string itemBuths = "";
        /// <summary>
        /// Табличка цены
        /// </summary>
        public string itemSLabel = "Цена";
        /// <summary>
        /// Табличка при покупке
        /// </summary>
        public string itemBLabel = "КУПЛЕНО";
        /// <summary>
        /// Валюта
        /// </summary>
        public string itemCLabel = "деталей";
        public string itemDescription = "";
        private Action UseAction;
        public Item() {

        }
        public Item(IModShop mod) {
            itemName = mod.itemName;
            trueName = mod.trueName;
            itemPrice = mod.itemPrice;
            itemExists = mod.itemExists;
            itemBuths = mod.itemBuths;
            itemSLabel = mod.itemSLabel;
            itemBLabel = mod.itemBLabel;
            itemCLabel = mod.itemCLabel;
            itemDescription = mod.itemDescription;
            UseAction = mod.Use;
        }

        protected virtual void Use() {
            UseAction.Invoke();
        }
        public virtual object Load() {
            return JsonConvert.DeserializeObject<Item>(File.ReadAllText(SoundCore.CONTENT_PATH + $"\\Shop\\{trueName}.json"));
        }
        public bool Buy(ref float coins) {
            if (coins >= itemPrice) {
                itemExists = false;
                coins -= itemPrice;
                Use();
                return true;
            } else
                return false;
        }
    }

    public class Jump : Item {
        public Jump() {
            itemPrice = 3.0f;
            itemName = "Улучшение Прыжка";
            trueName = "jumpup";
            itemExists = true;
            itemDescription = "Увеличивает высоту прыжка игрока.";
            itemBuths = "<+1 блок к высоте прыжка игрока.";

        }
        protected override void Use() {
            Program.Player.JUMP_DEFAULT_WEIGHT += 1;
            Program.Player.JUMP_WEIGHT = Program.Player.JUMP_DEFAULT_WEIGHT;
        }
        public override object Load() {
            return this;
        }
    }
    public class Buth : Item {
        public Buth() {
            itemPrice = 8f;
            itemName = "Улучшение Артифактов";
            trueName = "poweruptime";
            itemExists = true;
            itemDescription = "Увеличивает продолжительность действия артифакта.";
            itemBuths = "<+3.7 секунд к продолжительности артифактов.";
        }
        protected override void Use() {
            Program.buthbonus += 3.7;
        }
        public override object Load() {
            return this;
        }
    }

    public class ExtraLife : Item {
        public ExtraLife() {
            itemPrice = 700;
            itemName = "Глюченая монетка";
            trueName = "extralife";
            itemExists = true;
            itemDescription = "Из-за неисправности кода, вы магическим образом оказываетесь на прошлой позиции при смерти.";
            itemBuths = "<Темная сила глюков сможет вернуть вас к жизни.\n\t>Скорее всего вы умрете.";
        }
        protected override void Use() {
            Program.lifebonus += 1;
        }
        public override object Load() {
            return this;
        }
    }
    public class AsgardRune : Item {
        public AsgardRune() {
            itemPrice = 1200;
            itemName = "Камень Норн";
            trueName = "norntherock";
            itemExists = true;
            itemDescription = "Асгардианская Руна с большим обЪёмом магии в ней. ";
            itemBuths = "<Цвет игрока изменится на другой случайный.\n\tМагазин перезагрузится.";
        }
        protected override void Use() {
            Program.Player.npcColor = (ConsoleColor)Program.Random.Next(0, 16);
            Program.shop.Close();
            Program.shop = new WorldShop();
            Program.shop.Open();
        }
        public override object Load() {
            return this;
        }
    }
    public class Debug : Item {
        public Debug() {
            itemPrice = -1;
            itemName = "Глюк";
            itemExists = true;
            itemBLabel = "CHEATER";
            itemDescription = "Хочешь увидеть конец света? Попробуй сделать так, чтобы играло две музыки магазина на фоне.";
        }
        protected override void Use() {
            Program.makeGlitch(3000, true);
        }

    }
}
