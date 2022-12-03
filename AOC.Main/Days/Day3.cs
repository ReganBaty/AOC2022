using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day3 : IDay
    {
        public const int Day = 3;

        public async Task<Results> Run(string? input)
        {
            int getPrio(char x) => Char.IsUpper(x) ? (int)x - 38 : (int)x - 96;

            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var result = input.Split("\n").Where(s => !String.IsNullOrEmpty(s));
            var totalCount = 0;
            foreach (var s in result)
            {
                var part1Dict = new Dictionary<char, int>();
                for (var i = 0; i < s.Length / 2; i++)
                {
                    part1Dict.TryAdd(s[i], 0);
                }

                for (var i = s.Length - 1; i > 0; i--)
                {
                    if (part1Dict.TryGetValue(s[i], out int _))
                    {
                        totalCount += getPrio(s[i]);
                        break;
                    }
                }
            }

            var totalCount2 = 0;
            var elfRunner = 0;
            var part2Dict = new Dictionary<char, int>();


            foreach (var s in result)
            {
                var tester = s.Distinct();
                foreach (var c in tester)
                {
                    if (!part2Dict.TryAdd(c, 1))
                        part2Dict[c]++;
                }

                if (elfRunner == 2)
                {
                    elfRunner = 0;
                    var shared = part2Dict.MaxBy(kvp => kvp.Value).Key;
                    totalCount2 += getPrio(shared);
                    part2Dict.Clear();
                }
                else
                {
                    elfRunner++;
                }
            }

            return new Results(totalCount, totalCount2);
        }

    }
}


