using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day4 : IDay
    {
        public const int Day = 4;


        public (int low, int high) lowHigh(string input)
        {
            var sd = input.Split('-');
            var low = Convert.ToInt32(sd[0]);
            var high = Convert.ToInt32(sd[1]);

            return (low, high);
        }

        public bool contains((int low, int high) first, (int low, int high) second)
        {
            if (first.low <= second.low && first.high >= second.high)
                return true;

            if (second.low <= first.low && second.high >= first.high)
                return true;

            return false;
        }

        public bool overlaps((int low, int high) first, (int low, int high) second)
        {
            if (first.low >= second.low && first.low <= second.high)
                return true;

            if (second.low >= first.low && second.low <= first.high)
                return true;

            return false;
        }


        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var filteredInput = input.Split("\n").Where(s => !String.IsNullOrEmpty(s)).Select(s =>
            {
                var t = s.Split(',');
                return (t[0], t[1]);
            });

            var count = filteredInput.Where(s => contains(lowHigh(s.Item1), lowHigh(s.Item2))).Count();
            var count2 = filteredInput.Where(s => overlaps(lowHigh(s.Item1), lowHigh(s.Item2))).Count();

            return new Results(count, count2);
        }

    }
}


