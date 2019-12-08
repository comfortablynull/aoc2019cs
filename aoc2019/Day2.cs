using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace aoc2019
{
    public class Day2 : IDay
    {
        private const int PART_2_LIMIT = 100;
        private static int Process(ReadOnlyCollection<int> input, in int noun, in int verb)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            var state = new int[input.Count];
            input.CopyTo(state, 0);
            state[1] = noun;
            state[2] = verb;
            for (var i = 0; i < state.Length; i += 4)
            {
                if (state[i] == 99)
                {
                    break;
                }
                int x = state[i + 1], y = state[i + 2], o = state[i + 3];
                switch (state[i])
                {
                    case 1:
                        var r = state[x] + state[y];
                        state[o] = r;
                        break;
                    case 2:
                        var xx = state[x] * state[y];
                        state[o] = xx;
                        break;
                }
                
            }

            return state[0];
        }
        public void Run(params string[] args)
        {
            using var i = new FileStream(args[0], FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(i);
            if (args.Length < 2)
            {
                throw new ApplicationException("noun and verb must be specified");
            }
            int noun = int.Parse(args[1]), verb = int.Parse(args[2]);
            var line = reader.ReadLine();
            if (line == null)
            {
                throw new ApplicationException("cannot read line");
            }
            
            ReadOnlyCollection<int> input = new ReadOnlyCollection<int>(Array.ConvertAll(line.Split(","), int.Parse));
            Console.WriteLine("Part1: {0}", Process(input, noun, verb));
            if (args.Length < 3)
            {
                return;
            }

            var target = int.Parse(args[3]);
            for (var n = 0; n < input.Count; n++)
            {
                for (var v = 0; v < input.Count; v++)
                {
                    if (Process(input, n, v) != target) continue;
                    Console.WriteLine("Part2: {0}", 100 * n + v);
                    return;
                }
            }
            Console.WriteLine("No solution for part 2 found");
        }
    }
}