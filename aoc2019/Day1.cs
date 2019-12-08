using System;
using System.IO;

namespace aoc2019
{
    public class Day1:IDay
    {
        private const long Divisor = 3;
        private const long Sub = 2;

        private static long Fuel(in long mass)
        {
            return (mass / Divisor) - Sub;
        }

        private static long Fuelception(in long mass)
        {
            long intial = 0;
            for (var i = Fuel(mass); i > 0; i = Fuel(i))
            {
                intial += i;
            }

            return intial;
        }
        
        public void Run(params string[] args)
        {
            using var input = new FileStream(args[0], FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(input);
            long part1 = 0;
            long part2 = 0;
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }

                var mass = long.Parse(line);
                part1 += Fuel(mass);
                part2 += Fuelception(mass);
            }

            Console.WriteLine("Part 1: {0}", part1);
            Console.WriteLine("Part 2: {0}", part2);
        }
    }
}