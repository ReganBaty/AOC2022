using System;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{

    public sealed class Day8 : IDay
    {
        public const int Day = 8;

        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var formatted = input.Split('\n').Select(s => s.Trim()).Where(s => !String.IsNullOrEmpty(s)).ToList();

            var width = formatted[0].Length;
            var height = formatted.Count();

            var grid = Enumerable.Range(0, width).Select(x =>
            {
                return Enumerable.Range(0, height).Select(y =>
                {
                    return ((x, y), (int)Char.GetNumericValue(formatted[x][y]));
                });

            }).SelectMany(x => x).ToDictionary(x => x.Item1, x => x.Item2);

            var p1 = grid.Where(tr =>
            {
                var x = tr.Key.x;
                var y = tr.Key.y;
                var tree = tr.Value;

                // Check left
                if (grid.Where(t => t.Key.x < x && t.Key.y == y && t.Value < tree).Count() >= x)
                    return true;

                // Check right
                if (grid.Where(t => t.Key.x > x && t.Key.y == y && t.Value < tree).Count() >= (width - x - 1))
                    return true;

                // Check above
                if (grid.Where(t => t.Key.x == x && t.Key.y < y && t.Value < tree).Count() >= y)
                    return true;

                // Check below
                if (grid.Where(t => t.Key.x == x && t.Key.y > y && t.Value < tree).Count() >= (height - y - 1))
                    return true;

                return false;
            }).Count();

            var p2 = grid.Select(tr =>
            {
                var x = tr.Key.x;
                var y = tr.Key.y;
                var tree = tr.Value;

                // Check left
                var left = grid.Where(t => t.Key.x < x && t.Key.y == y).OrderByDescending(x => x.Key.x).TakeWhileIncluding(x => x.Value < tree).Count();

                // Check right
                var right = grid.Where(t => t.Key.x > x && t.Key.y == y).OrderBy(x => x.Key.x).TakeWhileIncluding(x => x.Value < tree).Count();

                // Check above
                var up = grid.Where(t => t.Key.x == x && t.Key.y < y).OrderByDescending(x => x.Key.y).TakeWhileIncluding(x => x.Value < tree).Count();

                // Check below
                var below = grid.Where(t => t.Key.x == x && t.Key.y > y).OrderBy(x => x.Key.y).TakeWhileIncluding(x => x.Value < tree).Count();

                return left * right * up * below;
            }).Max();

            return new Results(p1, p2);
        }
    }
}

