using System;

namespace Soap.ConsoleApp.Models
{
    public struct Point2D : IEquatable<Point2D>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }


        public override bool Equals(object obj)
        {
            if (!(obj is Point2D other)) return false;

            return X == other.X && Y == other.Y;
        }

        public bool Equals(Point2D other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public static bool operator ==(Point2D a, Point2D b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Point2D a, Point2D b)
        {
            return !(a == b);
        }
    }
}
