using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day1 : IDay
    {
        public const int Day = 1;
        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var result = input.Split("\n\n").Select(s => s.Split('\n').Where(s => !String.IsNullOrWhiteSpace(s)).Sum(x => Convert.ToInt32(x)));
            return new Results(result.Max(), result.OrderByDescending(x => x).Take(3).Sum());
        }
    }
}
