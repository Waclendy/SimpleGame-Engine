using System;
using System.Collections.Generic;
using System.Threading;

namespace SimpleGame
{
    public struct ivector {
        public int X;
        public int Y;
        public ivector(ivector copy) {
            X = copy.X;
            Y = copy.Y;
        }
        public ivector(int x, int y) {
            X = x;
            Y = y;
        }

        public static ivector operator +(ivector a, ivector b) => new ivector() {
            X = a.X + b.X,
            Y = a.Y + b.Y,
           
        };
        public static ivector operator -(ivector a, ivector b) => new ivector() {
            X = a.X - b.X,
            Y = a.Y - b.Y,
        };
        public static implicit operator int[] (ivector target) => new int[] { target.X, target.Y };

       // public static explicit operator ivector (int[] arrey) => return new ivector() { };
    }

}
