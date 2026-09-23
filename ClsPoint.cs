using System;

namespace Depi_Session08
{
    // 5 + 6) implementing more than one interface: IComparable (sorting) and ICloneable (cloning)
    class Point3D : IComparable, ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        // 1) Constructors with chaining (everything ends up in the last constructor)
        public Point3D() : this(0, 0, 0) { }
        public Point3D(int x) : this(x, 0, 0) { }
        public Point3D(int x, int y) : this(x, y, 0) { }
        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // 2) ToString -> "Point Coordinates: (10, 10, 10)"
        public override string ToString()
        {
            return $"Point Coordinates: ({X}, {Y}, {Z})";
        }

        // 4) Make == work by value (see explanation in Main)
        public override bool Equals(object obj)
        {
            if (obj is Point3D other)
                return X == other.X && Y == other.Y && Z == other.Z;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public static bool operator ==(Point3D a, Point3D b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Point3D a, Point3D b)
        {
            return !(a == b);
        }

        // 5) Sort by X first, then by Y
        public int CompareTo(object obj)
        {
            if (obj is not Point3D other)
                throw new ArgumentException("Object is not a Point3D");

            int result = X.CompareTo(other.X);
            if (result != 0) return result;
            return Y.CompareTo(other.Y);
        }

        // 6) Clone
        public object Clone()
        {
            return new Point3D(X, Y, Z);   // or: return MemberwiseClone();
        }
    }
}
