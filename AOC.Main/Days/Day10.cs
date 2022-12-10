using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{

    public sealed class Day10 : IDay
    {
        public const int Day = 10;

        public char sdf(int pos, int cycle)
        {
            var charIndex = cycle % 40;
            charIndex -= 1;
            if (pos - 1 == charIndex || pos == charIndex || pos + 1 == charIndex)
                return '#';

            return ' ';
        }

        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var formatted = input.Split('\n').Select(s => s.Trim()).Where(s => !String.IsNullOrEmpty(s)).ToList();

            var cycle = 0;
            var score = 0;
            var x = 1;
            var cycles = new int[] { 20, 60, 100, 140, 180, 220 };
            var screen = new List<char>();
            foreach (var s in formatted)
            {
                var parts = s.Split(' ');

                if (parts[0] == "noop")
                {
                    cycle++;
                    screen.Add(sdf(x, cycle));
                    if (cycles.Contains(cycle))
                    {
                        score += cycle * x;
                    }
                }
                else
                {
                    cycle++;
                    screen.Add(sdf(x, cycle));
                    if (cycles.Contains(cycle))
                    {
                        score += cycle * x;
                    }
                    cycle++;
                    screen.Add(sdf(x, cycle));
                    if (cycles.Contains(cycle))
                    {
                        score += cycle * x;
                    }
                    x += Int32.Parse(parts[1]);
                }
            }
            var builder = new StringBuilder();
            for (int i = 1; i <= screen.Count; i++)
            {
                builder.Append(screen[i - 1]);
                if (i % 40 == 0)
                    builder.AppendLine();
            }

            Console.WriteLine(builder.ToString());

            return new Results(score, 0);
        }
    }
}
