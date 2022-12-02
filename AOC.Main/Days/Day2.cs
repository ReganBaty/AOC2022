using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day2 : IDay
    {
        public const int Day = 2;
        public async Task<Results> Run(string? input)
        {

            var pointLookup = new Dictionary<string, int>()
            {
                {"X",1},
                {"Y",2},
                {"Z",3},
                {"A",1},
                {"B",2},
                {"C",3},
            };

            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var result = input.Split("\n").Where(s => !String.IsNullOrEmpty(s));

            var total = result.Select(s =>
            {
                var split = s.Split(" ");

                var opp = pointLookup[split[0]];
                var me = pointLookup[split[1]];

                return me + WinLoss(me, opp);
            }).Sum();

            var total2 = result.Select(s =>
            {
                var split = s.Split(" ");

                var opp = split[0];
                var me = split[1];

                if (me == "X")
                    return pointLookup[opp] == 1 ? 3 : pointLookup[opp] - 1;

                if (me == "Y")
                    return pointLookup[opp] + 3;

                if (me == "Z")
                    return (pointLookup[opp] == 3 ? 1 : pointLookup[opp] + 1) + 6;

                return 0;
            }).Sum();


            return new Results(total, total2);
        }

        public int WinLoss(int me, int them)
        {
            if (me == them)
                return 3;

            if (me == them + 1 || me == them - 2)
                return 6;

            return 0;
        }
    }
}
