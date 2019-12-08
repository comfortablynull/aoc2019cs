using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Schema;

namespace aoc2019
{
    public class Day3 : IDay
    {
        private enum LineType {Invalid, Horizontal, Vertical};
        private struct Segment
        {
            public readonly LineType Lt;
            public readonly int C;
            public readonly int Start;
            public readonly int End;
            public readonly int Lower;
            public readonly int Upper;
            public readonly int StepsB;

            public Segment(int c, int start, int end,int stepsB, LineType lt)
            {
                C = c;
                Start = start;
                End = end;
                Lower = Math.Min(start, end);
                Upper = Math.Max(start, end);
                StepsB = stepsB;
                Lt = lt;
            }
        }
        
        private struct Point
        {
            public readonly int X;
            public readonly int Y;
            public readonly int S;
            public Point(int x, int y, int s)
            {
                X = x;
                Y = y;
                S = s;
            }
        }


        private static Segment MakeSegment(Point start, Point end)
        {
            if (start.X == end.X)
            {
                return new Segment(start.X, start.Y, end.Y, start.S, LineType.Vertical);
            }
            return new Segment(start.Y, start.X, end.X, start.S,  LineType.Horizontal);
        }


        private static Point MakePoint(in string s, Point previous)
        {
            var x = previous.X;
            var y = previous.Y;
            var d = s[0];
            var m = int.Parse(s[1..]);
            switch (d)
            {
                case 'D':
                    return new Point(x, y - m, previous.S + m);
                case 'U':
                    return new Point(x, y + m, previous.S + m);
                case 'L':
                    return new Point(x -m , y, previous.S + m);
                case 'R':
                    return new Point(x + m, y, previous.S + m);
                default:
                    throw new ApplicationException("dunno");
            }

        }

        private class Path
        {
            private struct intersection
            {
                public int MD;
                public int S;
            }
            private Point previous;
            private List<Segment> verticalSegements;
            private List<Segment> horizontalSegments;

            public Path()
            {
                previous = new Point(0, 0, 0);
                verticalSegements = new List<Segment>();
                horizontalSegments = new List<Segment>();
            }

            public void Step(string s)
            {
                var current = MakePoint(s, previous);
                var seg = MakeSegment(previous, current);
                var target = horizontalSegments;
                if (seg.Lt == LineType.Vertical)
                {
                    target = verticalSegements;
                }
                target.Add(seg);
                previous = current;
            }

            private intersection intersect(List<Segment> horizontals, List<Segment> verticals)
            {
                var i = new intersection {MD = int.MaxValue, S = int.MaxValue};
                foreach (var horizontal in horizontals)
                {
                    foreach (var vertical in verticals)
                    {
                        int ix = vertical.C, iy = horizontal.C;
                        var md = Math.Abs(ix) + Math.Abs(iy);
                        if ( md == 0 || 
                            !(vertical.Lower <= iy && iy <= vertical.Upper) ||
                            !(horizontal.Lower <= ix && ix <= horizontal.Upper))
                        {
                            continue;
                        }
                        var vs = vertical.StepsB + Math.Abs(iy - vertical.Start);
                        var hs = horizontal.StepsB + Math.Abs(ix - horizontal.Start);
                        i.MD = Math.Min(md, i.MD);
                        i.S = Math.Min(i.S, vs + hs);
                    }
                }

                return i;
            }
            public void Intersect(Path p)
            {
                var l1 = intersect(p.horizontalSegments, verticalSegements);
                var l2 = intersect(horizontalSegments, p.verticalSegements);
                var lm = Math.Min(l1.MD, l2.MD);
                var s = Math.Min(l1.S, l2.S);
                Console.WriteLine("Part 1: {0}", lm);
                Console.WriteLine("Part 2: {0}", s);
            }

        }

        public void Run(StreamReader reader, params string[] args)
        {
            var first = reader.ReadLine();
            var second = reader.ReadLine();
            var line = new Path();
            var line2 = new Path();
            foreach (var i in first.Split(","))
            {
                line.Step(i);
            }

            foreach (var i in second.Split(","))
            {
                line2.Step(i);
            }

            line.Intersect(line2);
        }
    }
}