using System;
using System.IO;

namespace aoc2019
{
    interface IDay
    {
        void Run(params string[] args);
    }
    public static class Program
    {
        public static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            if (args.Length.Equals(0))
            {
                Console.Error.WriteLine("you must specify a day");
                return;
            }
            IDay day;
            switch(args[0])
            {
                case "1":
                    day = new Day1();
                    break;
                case "2":
                    day = new Day2();
                    break;
                case "3":
                    day = new Day3();
                    break;
                default:
                    Console.Error.WriteLine("invalid day {0}", args[0]);
                    return;
            }
            day.Run(args[1..]);
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }
    }
}