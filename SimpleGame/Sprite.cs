using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleGame {
    public class Sprite {

        public ivector position;
        public int W;
        public int H;
        public int interval = 100;
        public bool Drawing = false;
        public bool Cycle = false;
        public List<Frame> Frames;

        public Sprite(ivector position, int w, int h, int interval) {
            this.position = position;
            W = w;
            H = h;
            this.interval = interval;
            Cycle = true;
            Frames = new List<Frame>();
        }
        public void Draw() {
            Drawing = true;
            Thread thrd = new Thread(_draw);
            thrd.Start();
        }
        private void _draw() {
            while (Cycle) {
                for (int i = 0; i < Frames.Count; i++) {
                    Frames[i].Draw(position.X, position.Y);
                    Thread.Sleep(interval);
                }
            }
            Drawing = false;
        }
        
        
    }

    public class Frame {
        public static string[][] fromFile(string file) {
            string[] lines = File.ReadAllLines(file);
            string[][] frame = new string[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
                frame[i] = new string[frame.Length];


            for (int x = 0; x < frame.Length; x++) {

                string str = lines[x];

                for (int y = 0; y < frame[x].Length; y++) {
                        frame[x][y] = str[y].ToString();
                }

            }
            return frame;

        }
        public int W;
        public int H;
        public string[][] Image;
        public Frame(int w, int h, string[][] image) {
            W = w;
            H = h;
            Image = new string[W][];

            for (int i = 0; i < W; i++)
                    Image[i] = new string[H];

            Image = image;
        }
        public void Draw(int i, int j) {
            try {
                if (Program.Pause)
                    return;
                Console.SetCursorPosition(i, j);
                for (int x = 0; x < W; x++) {
                    for (int y = 0; y < H; y++)
                        ConsolePaint.Paint(i + x, j + y, ConsoleColor.Black, ConsoleColor.White, Image[x][y]);
                }

            } catch { }
        }
    }
}
