using System;
using System.Collections.Generic;
using System.IO;


namespace aoc2019
{
    public class Day5 : IDay
    {
        private class Interpreter
        {
            private delegate int Accessor(int[] state, int i);

            private delegate int Op(string label, int mode, int[] state, int input, int offset);

            private static readonly Dictionary<int, Accessor> ParameterModes = new Dictionary<int, Accessor>()
            {
                {0, (state, i) => state[state[i]]},
                {1, (state, i) => state[i]}
            };

            private readonly Dictionary<int, Op> _ops = new Dictionary<int, Op>()
            {
                {1, Add},
                {2, Multi},
                {3, Store},
                {4, Echo},
                {5, JumpIfTrue},
                {6, JumpIfFalse},
                {7, LessThan},
                {8, Equal},
            };

            public void Run(string label, int first, int[] input)
            {
                var last = first;
                var state = new int[input.Length];
                input.CopyTo(state, 0);
                for (int i = 0; i < state.Length;)
                {
                    var instruction = state[i];
                    var oc = instruction % 100;
                    if (oc == 99)
                    {
                        return;
                    }

                    var op = _ops[oc];
                    i += op(label, instruction / 100, state, last, i);
                    last = state[i - 1];
                }
            }

            private static int Add(string label, int mode, int[] state, int input, int offset)
            {
                var a = ParameterModes[mode % 10](state, offset + 1);
                var b = ParameterModes[(mode / 10) % 10](state, offset + 2);
                var c = state[offset + 3];
                state[c] = a + b;
                return 4;
            }

            private static int Multi(string label, int mode, int[] state, int input, int offset)
            {
                var a = ParameterModes[mode % 10](state, offset + 1);
                var b = ParameterModes[(mode / 10) % 10](state, offset + 2);
                var c = state[offset + 3];
                state[c] = a * b;
                return 4;
            }

            private static int Store(string label, int mode, int[] state, int input, int offset)
            {
                state[state[offset + 1]] = input;
                return 2;
            }

            private static int Echo(string label, int mode, int[] state, int input, int offset)
            {
                var a = ParameterModes[mode % 10](state, offset + 1);
                Console.WriteLine("{0}: {1}", label, a);
                return 2;
            }

            private static int JumpIfTrue(string label, int mode, int[] state, int input, int offset)
            {
                var a = ParameterModes[mode % 10](state, offset + 1);
                var b = ParameterModes[(mode / 10) % 10](state, offset + 2);
                if (a != 0)
                {
                    return b - offset;
                }

                return 3;
            }

            private static int JumpIfFalse(string label, int mode, int[] state, int input, int offset)
            {
                var a = ParameterModes[mode % 10](state, offset + 1);
                var b = ParameterModes[(mode / 10) % 10](state, offset + 2);
                if (a == 0)
                {
                    return b - offset;
                }

                return 3;
            }

            private static int LessThan(string label, int mode, int[] state, int input, int offset)
            {
                var a = ParameterModes[mode % 10](state, offset + 1);
                var b = ParameterModes[(mode / 10) % 10](state, offset + 2);
                var c = state[offset + 3];
                state[c] = a < b ? 1 : 0;
                return 4;
            }

            private static int Equal(string label, int mode, int[] state, int input, int offset)
            {
                var a = ParameterModes[mode % 10](state, offset + 1);
                var b = ParameterModes[(mode / 10) % 10](state, offset + 2);
                var c = state[offset + 3];
                state[c] = a == b ? 1 : 0;
                return 4;
            }
        }

        public void Run(params string[] args)
        {
            using var input = new FileStream(args[0], FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(input);
            var line = reader.ReadLine();
            var state = Array.ConvertAll(line.Split(","), int.Parse);
            var interpreter = new Interpreter();
            interpreter.Run("Part1",int.Parse(args[1]), state);
            interpreter.Run("Part2", int.Parse(args[2]), state);
        }
    }
}