namespace Pathing.Math
{
    public struct Coordinate
    {
        public readonly int X;
        public readonly int Y;

        public Coordinate(int x, int y) { X = x; Y = y; }

        #region Boilerplate crap
        public override string ToString() { return string.Format("{{X = {0}, Y = {1}}}", X, Y); }
        public bool Equals(Coordinate other) { return X.Equals(other.X) && Y.Equals(other.Y); }
        public override bool Equals(object obj) { if (ReferenceEquals(null, obj)) { return false; } return obj is Coordinate && Equals((Coordinate) obj); }
        public override int GetHashCode() { unchecked { return (X.GetHashCode() * 397) ^ Y.GetHashCode(); } }
        public static bool operator ==(Coordinate left, Coordinate right) { return left.Equals(right); }
        public static bool operator !=(Coordinate left, Coordinate right) { return !left.Equals(right); }
        public static Coordinate operator +(Coordinate left, Coordinate right) { return new Coordinate(left.X + right.X, left.Y + right.Y); }
        public static Coordinate operator -(Coordinate left, Coordinate right) { return new Coordinate(left.X - right.X, left.Y - right.Y); }
        public static Coordinate operator *(Coordinate left, Coordinate right) { return new Coordinate(left.X * right.X, left.Y * right.Y); }
        public static Coordinate operator /(Coordinate left, Coordinate right) { return new Coordinate((int)System.Math.Round((double)left.X / right.X), (int)System.Math.Round((double)left.Y / right.Y)); }
        public static Coordinate operator %(Coordinate left, Coordinate right) { return new Coordinate(left.X % right.X, left.Y % right.Y); }
        #endregion
    }
}
