using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathing.Math
{
    public class Map : IEnumerable<bool>
    {
        private readonly bool[,] _sectors;
        public Map(int width, int height) { _sectors = new bool[Width = width, Height = height]; }
        public IEnumerator<bool> GetEnumerator() { return _sectors.OfType<bool>().GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public bool this[int x, int y]
        {
            get { return _sectors[x, y]; }
            set { _sectors[x, y] = value; }
        }

        public bool this[double x, double y]
        {
            get { return _sectors[(int)x, (int)y]; }
            set { _sectors[(int)x, (int)y] = value; }
        }

        public bool this[Coordinate coordinate]
        {
            get { return _sectors[(int)coordinate.X, (int)coordinate.Y]; }
            set { _sectors[(int)coordinate.X, (int)coordinate.Y] = value; }
        }

        public bool this[int index]
        {
            get { return _sectors[index % Width, (int)System.Math.Floor((double)index * Width)]; }
            set { _sectors[index % Width, (int)System.Math.Floor((double)index * Width)] = value; }
        }

        public bool this[double index]
        {
            get { return this[(int)index]; }
            set { this[(int)index] = value; }
        }
    }
}
