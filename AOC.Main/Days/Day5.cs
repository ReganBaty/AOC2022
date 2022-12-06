using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day5 : IDay
    {
        public const int Day = 5;


        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var filteredInput = input.Split("\n\n");
            var stackInput = filteredInput[0].Split('\n');
            var commands = filteredInput[1].Split('\n').Where(s => !String.IsNullOrEmpty(s));

            var stackCount = (stackInput.First().Length) / 4 + 1;

            var stacks = Enumerable.Range(0, stackCount).Select(s =>
            {
                return new Stack<char>();
            }).ToList();

            var stacks2 = Enumerable.Range(0, stackCount).Select(s =>
            {
                return new List<char>();
            }).ToList();


            foreach (var s in stackInput.Take(stackInput.Count() - 1))
            {
                for (var i = 0; i < stackCount; i += 1)
                {
                    char c = s[1 + (i * 4)];
                    if (c != ' ')
                    {
                        stacks[i].Push(c);
                        stacks2[i].Add(c);
                    }
                }
            }

            var moves = commands.Select(s =>
            {
                var split = s.Split(' ');
                var from = Int32.Parse(split[3]) - 1;
                var to = Int32.Parse(split[5]) - 1;
                var move = Int32.Parse(split[1]);
                return (from, to, move);
            });

            foreach (var m in moves)
            {
                for (int i = 0; i < m.move; i++)
                {
                    stacks[m.to].Push(stacks[m.from].Pop());
                }

                stacks2[m.to].InsertRange(0, stacks2[m.from].Take(m.move));
                stacks2[m.from].RemoveRange(0, m.move);
            }

            Console.WriteLine(stacks.Aggregate("", (s, d) =>
            {
                return s + String.Join(' ', d.Peek());
            }));

            Console.WriteLine(stacks2.Aggregate("", (s, d) =>
            {
                return s + String.Join(' ', d[0]);
            }));

            return new Results(0, 0);
        }

    }
}


