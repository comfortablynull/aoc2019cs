using System;

namespace aoc2019
{
    public class Day4 : IDay
    {
        public void Run(params string[] args)
        {
            var s = args[0].Split("-");
            int lower = int.Parse(s[0]), upper = int.Parse(s[1]);
            var pc = 0;
            var sc = 0;
            for (int i = lower; i <= upper; i++)
            {
                var t = i;
                var f = t % 10;
                var e = (t /= 10) % 10;
                var d = (t /= 10) % 10;
                var c = (t /= 10) % 10;
                var b = (t /= 10) % 10;
                var a = (t /= 10) % 10;
                if (a > b || b > c || c > d || d > e || e > f)
                {
                    continue;
                }
                if (a != b && b != c && c != d && d != e && e != f)
                {
                    continue;
                }
                pc++;
                if ((a == b && b != c) || ( a != b && b == c && c != d) || (b != c && c == d && d != e) || (c != d && d == e && e != f) || (d != e && e == f))
                {
                    sc++;
                }
            }
            Console.WriteLine("Part 1: {0}", pc);
            Console.WriteLine("Part 2: {0}", sc);
        }
    }
}