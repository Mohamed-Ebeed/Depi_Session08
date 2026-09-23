using System;

namespace DEPI_Assignment04
{
    // Step 1 (before modification): instance methods, you had to write:
    //     Maths m = new Maths();
    //     m.Add(5, 3);
    //
    // Step 2 (after modification): make the class and its methods STATIC,
    // so we call them directly through the class name: Maths.Add(5, 3);
    static class Maths
    {
        public static double Add(double a, double b)
        {
            return a + b;
        }

        public static double Subtract(double a, double b)
        {
            return a - b;
        }

        public static double Multiply(double a, double b)
        {
            return a * b;
        }

        public static double Divide(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Cannot divide by zero.");
            return a / b;
        }
    }
} 
