using System;
namespace SimpleGame
{
    public struct TileStruct {
        public string Id;
        public string TileName;
        public bool CollusionEnabled;
        public string Char;
        public ConsoleColor ForeColor;
        public ConsoleColor BackColor;
        public bool IsVisible;
        public bool Triggering;
        public Action Trigger;
    }

    public struct TSP {
        public int Id;
        public int TileName;
        public int CollusionEnabled;
        public int Char;
        public int ForeColor;
        public int BackColor;
        public int IsVisible;
        public int Triggering;
    }

    public interface IModInfo {
        string Name { get; }
        string Description { get; }
    }
    public interface IModShop {
         string itemName { get; }
         string trueName { get; }
         float itemPrice { get; }
         bool itemExists { get; }
         string itemBuths { get; }
         string itemSLabel { get; }
         string itemBLabel { get; }
         string itemCLabel { get; }
         string itemDescription { get; }
         Action Use { get; }
    }
    public interface IModTile {
        TileStruct NewTile { get; }
    }
}
