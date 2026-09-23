using System;

namespace DEPI_Assignment04
{
    class Duration
    {
        // 1) Three attributes
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        // Helper: the whole duration in seconds (used by the operators)
        public int TotalSeconds => Hours * 3600 + Minutes * 60 + Seconds;

        // 3) Constructors
        public Duration() : this(0, 0, 0) { }

        public Duration(int hours, int minutes, int seconds)
        {
            // Normalize, e.g. (0, 70, 80) -> 1h 11m 20s
            int total = hours * 3600 + minutes * 60 + seconds;
            SetFromTotalSeconds(total);
        }

        // Duration(3600) -> 1h 0m 0s, Duration(666) -> 11m 6s
        public Duration(int totalSeconds)
        {
            SetFromTotalSeconds(totalSeconds);
        }

        private void SetFromTotalSeconds(int total)
        {
            if (total < 0) total = 0;   // a duration cannot be negative
            Hours = total / 3600;
            Minutes = (total % 3600) / 60;
            Seconds = total % 60;
        }

        // 2) Override System.Object members

        public override string ToString()
        {
            // Hours are omitted when they are zero (Duration(666) -> "Minutes :11, Seconds :6")
            if (Hours == 0)
                return $"Minutes :{Minutes}, Seconds :{Seconds}";

            return $"Hours: {Hours}, Minutes :{Minutes}, Seconds :{Seconds}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Duration other)
                return TotalSeconds == other.TotalSeconds;
            return false;
        }

        public override int GetHashCode()
        {
            return TotalSeconds.GetHashCode();
        }

        // ================= Operators overloading (Session 09) =================

        // D3 = D1 + D2
        public static Duration operator +(Duration a, Duration b)
        {
            return new Duration(a.TotalSeconds + b.TotalSeconds);
        }

        // D3 = D1 + 7800
        public static Duration operator +(Duration a, int seconds)
        {
            return new Duration(a.TotalSeconds + seconds);
        }

        // D3 = 666 + D3
        public static Duration operator +(int seconds, Duration a)
        {
            return new Duration(a.TotalSeconds + seconds);
        }

        // D1 = D1 - D2   (result is never negative, it stops at zero)
        public static Duration operator -(Duration a, Duration b)
        {
            return new Duration(Math.Max(0, a.TotalSeconds - b.TotalSeconds));
        }

        // D3 = ++D1   (increase one minute)
        public static Duration operator ++(Duration a)
        {
            return new Duration(a.TotalSeconds + 60);
        }

        // D3 = --D2   (decrease one minute)
        public static Duration operator --(Duration a)
        {
            return new Duration(Math.Max(0, a.TotalSeconds - 60));
        }

        // if (D1 > D2)  /  if (D1 < D2)      (must be defined as a pair)
        public static bool operator >(Duration a, Duration b)
        {
            return a.TotalSeconds > b.TotalSeconds;
        }

        public static bool operator <(Duration a, Duration b)
        {
            return a.TotalSeconds < b.TotalSeconds;
        }

        // if (D1 >= D2)  /  if (D1 <= D2)    (must be defined as a pair)
        public static bool operator >=(Duration a, Duration b)
        {
            return a.TotalSeconds >= b.TotalSeconds;
        }

        public static bool operator <=(Duration a, Duration b)
        {
            return a.TotalSeconds <= b.TotalSeconds;
        }

        // == and != (recommended since we override Equals)
        public static bool operator ==(Duration a, Duration b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.TotalSeconds == b.TotalSeconds;
        }

        public static bool operator !=(Duration a, Duration b)
        {
            return !(a == b);
        }

        // if (D1)  -> true when the duration is not zero
        // (operators true and false must be defined together)
        public static bool operator true(Duration a)
        {
            return a.TotalSeconds > 0;
        }

        public static bool operator false(Duration a)
        {
            return a.TotalSeconds <= 0;
        }

        // DateTime Obj = (DateTime) D1
        // Today at 00:00 + the duration
        public static explicit operator DateTime(Duration a)
        {
            return DateTime.Today.AddSeconds(a.TotalSeconds);
        }
    }
} 
