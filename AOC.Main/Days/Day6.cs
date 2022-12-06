using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day6 : IDay
    {
        public const int Day = 6;

        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var length1 = 4;
            var p1 = Enumerable.Range(0, input.Length - 4).
                First(i => input.Skip(i).Take(length1).Distinct().Count() == length1);

            var length2 = 14;
            var p2 = Enumerable.Range(0, input.Length - 4).
                First(i => input.Skip(i).Take(length2).Distinct().Count() == length2);

            return new Results(p1 + length1, p2 + length2);
        }

    }
}


