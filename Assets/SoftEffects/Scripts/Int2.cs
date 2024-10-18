using System;
using UnityEngine;

#if UNITY_EDITOR
#endif

namespace Mkey
{
    [Serializable]
    public struct Int2
    {
        public int X;
        public int Y;
        public Int2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return (X + " : " + Y + " ;");
        }

        public static Int2 operator +(Int2 a, Int2 b)
        {
            Int2 i = new Int2();
            i.X = a.X + b.X;
            i.Y = a.Y + b.Y;
            return i;
        }

        public static Int2 operator -(Int2 a, Int2 b)
        {
            Int2 i = new Int2();
            i.X = a.X - b.X;
            i.Y = a.Y - b.Y;
            return i;
        }

        public static Int2 operator /(Int2 a, int b)
        {
            Int2 i = new Int2();
            i.X = a.X / b;
            i.Y = a.Y / b;
            return i;
        }

        public static Int2 operator *(Int2 a, int b)
        {
            Int2 i = new Int2();
            i.X = a.X * b;
            i.Y = a.Y * b;
            return i;
        }

        public static bool operator ==(Int2 a, Int2 b)
        {
            return (a.X == b.X && a.Y == b.Y);
        }

        public static bool operator !=(Int2 a, Int2 b)
        {
            return (a.X != b.X || a.Y != b.Y);
        }

        public int ChessLength
        {
            get { return (Mathf.Abs(X) + Mathf.Abs(Y)); }
        }
    }

}