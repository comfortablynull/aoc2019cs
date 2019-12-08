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

            var n = String.Format("aoc2019.Day{0}", args[0]);
            IDay day = (IDay)Activator.CreateInstance(Type.GetType(n));
            Console.WriteLine(n);
            var dayWatch = new System.Diagnostics.Stopwatch();
            day.Run(args[1..]);
            Console.WriteLine($"Day Execution Time: {dayWatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");
        }
    }
}