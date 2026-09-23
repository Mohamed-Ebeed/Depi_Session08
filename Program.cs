using System;

namespace Depi_Session08
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========== First Project: Point3D ==========");
            RunProject1();

            Console.WriteLine("\n========== Second Project: Maths ==========");
            RunProject2();

            Console.WriteLine("\n========== Third Project: Duration ==========");
            RunProject3();
        }

        // ------------------------------------------------------------
        // First Project
        // ------------------------------------------------------------
        // ---- 3) Three ways to validate input ----

        // (a) TryParse: no exception, keep asking until valid
        static int ReadUsingTryParse(string label)
        {
            int value;
            Console.Write($"Enter {label}: ");
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write($"Invalid number, enter {label} again: ");
            }
            return value;
        }

        // (b) Parse: throws exception, so we use try/catch
        static int ReadUsingParse(string label)
        {
            while (true)
            {
                try
                {
                    Console.Write($"Enter {label}: ");
                    return int.Parse(Console.ReadLine());
                }
                catch (Exception ex) when (ex is FormatException || ex is OverflowException || ex is ArgumentNullException)
                {
                    Console.WriteLine("Invalid number, try again.");
                }
            }
        }

        // (c) Convert: also throws exception on bad input
        static int ReadUsingConvert(string label)
        {
            while (true)
            {
                try
                {
                    Console.Write($"Enter {label}: ");
                    return Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex) when (ex is FormatException || ex is OverflowException)
                {
                    Console.WriteLine("Invalid number, try again.");
                }
            }
        }

        static void RunProject1()
        {
            // 2) Test ToString
            Point3D P = new Point3D(10, 10, 10);
            Console.WriteLine(P.ToString());   // Point Coordinates: (10, 10, 10)

            // 3) Read two points from the user
            Console.WriteLine("\n--- Point P1 (using TryParse) ---");
            Point3D P1 = new Point3D(
                ReadUsingTryParse("X1"),
                ReadUsingTryParse("Y1"),
                ReadUsingTryParse("Z1"));

            Console.WriteLine("\n--- Point P2 (using Parse / Convert) ---");
            Point3D P2 = new Point3D(
                ReadUsingParse("X2"),
                ReadUsingConvert("Y2"),
                ReadUsingTryParse("Z2"));

            Console.WriteLine($"\nP1 = {P1}");
            Console.WriteLine($"P2 = {P2}");

            // 4) Does == work properly?
            // WITHOUT overloading ==, it compares REFERENCES (addresses in memory),
            // so two different objects with the same coordinates give false.
            // That is why we overloaded == and != (and overrode Equals/GetHashCode).
            Console.WriteLine($"\nReference equality: {ReferenceEquals(P1, P2)}");
            if (P1 == P2)
                Console.WriteLine("P1 == P2  -> the points are equal");
            else
                Console.WriteLine("P1 != P2  -> the points are different");

            // 5) Array of points sorted by X then Y
            Point3D[] points =
            {
                new Point3D(5, 2, 1),
                new Point3D(3, 9, 4),
                new Point3D(5, 1, 7),
                new Point3D(1, 1, 1),
                P1,
                P2
            };

            Array.Sort(points);   // uses IComparable.CompareTo

            Console.WriteLine("\nSorted points (by X then Y):");
            foreach (Point3D p in points)
                Console.WriteLine(p);

            // 6) Clone
            Point3D P3 = (Point3D)P1.Clone();
            P3.X = 999;   // changing the clone does not affect the original
            Console.WriteLine($"\nOriginal: {P1}");
            Console.WriteLine($"Clone   : {P3}");
        }

        // ------------------------------------------------------------
        // Second Project
        // ------------------------------------------------------------
        static void RunProject2()
        {
            Console.WriteLine($"10 + 5 = {Maths.Add(10, 5)}");
            Console.WriteLine($"10 - 5 = {Maths.Subtract(10, 5)}");
            Console.WriteLine($"10 * 5 = {Maths.Multiply(10, 5)}");
            Console.WriteLine($"10 / 5 = {Maths.Divide(10, 5)}");

            try
            {
                Console.WriteLine(Maths.Divide(10, 0));
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // ------------------------------------------------------------
        // Third Project
        // ------------------------------------------------------------
        static void RunProject3()
        {
            // 3) Constructors
            Duration D1 = new Duration(1, 10, 15);
            Console.WriteLine(D1.ToString());   // Hours: 1, Minutes :10, Seconds :15

            Duration d = new Duration(3600);
            Console.WriteLine(d.ToString());    // Hours: 1, Minutes :0, Seconds :0

            Duration D2 = new Duration(7800);
            Console.WriteLine(D2.ToString());   // Hours: 2, Minutes :10, Seconds :0

            Duration D3 = new Duration(666);
            Console.WriteLine(D3.ToString());   // Minutes :11, Seconds :6

            // Operators
            Console.WriteLine("\n--- Operators ---");
            D1 = new Duration(1, 10, 15);

            D3 = D1 + D2;
            Console.WriteLine($"D1 + D2   = {D3}");

            D3 = D1 + 7800;
            Console.WriteLine($"D1 + 7800 = {D3}");

            D3 = 666 + D3;
            Console.WriteLine($"666 + D3  = {D3}");

            D3 = ++D1;
            Console.WriteLine($"++D1      = {D3}   (D1 is now {D1})");

            D3 = --D2;
            Console.WriteLine($"--D2      = {D3}   (D2 is now {D2})");

            D1 = D1 - D2;
            Console.WriteLine($"D1 - D2   = {D1}");

            D1 = new Duration(1, 10, 15);
            D2 = new Duration(7800);

            if (D1 > D2) Console.WriteLine("D1 > D2");
            else Console.WriteLine("D1 is not greater than D2");

            if (D1 <= D2) Console.WriteLine("D1 <= D2");
            else Console.WriteLine("D1 is not less than or equal to D2");

            if (D1) Console.WriteLine("D1 is not empty (true)");
            else Console.WriteLine("D1 is empty (false)");

            DateTime obj = (DateTime)D1;
            Console.WriteLine($"(DateTime)D1 = {obj}");
        }
    }
} 
