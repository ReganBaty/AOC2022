using System;
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


            return new Results(1, 2);
        }
    }
}
