using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{

    public sealed class Day9 : IDay
    {
        public const int Day = 9;
        Dictionary<(int x, int y), int> parts = new Dictionary<(int x, int y), int>();
        Dictionary<(int x, int y), int> parts2 = new Dictionary<(int x, int y), int>();

        public (int x, int y) Catchup((int x, int y) head, (int x, int y) tail)
        {
            var x = Math.Abs(head.x - tail.x);
            var y = Math.Abs(head.y - tail.y);


            if (x == 1 && y == 1)
            {
                return (tail);
            }

            if (x <= 1 && y == 0)
            {
                return (tail);
            }

            if (x == 0 && y <= 1)
            {
                return (tail);
            }

            if (x >= 1 && y >= 1)
            {
                x = tail.x + (head.x > tail.x ? 1 : -1);
                y = tail.y + (head.y > tail.y ? 1 : -1);
            }

            else if (x > 1)
            {
                x = tail.x + (head.x > tail.x ? 1 : -1);
                y = tail.y;
            }

            else if (y > 1)
            {
                x = tail.x;
                y = tail.y + (head.y > tail.y ? 1 : -1);
            }

            return (x, y);
        }

        public void MoveHead(char dir, int dist, ref (int x, int y) head, ref (int x, int y) tail, ref List<(int x, int y)> tails)
        {
            for (int i = 1; i <= dist; i++)
            {
                var headCopy = head;
                switch (dir)
                {
                    case 'R':
                        head = (head.x + 1, head.y);
                        tail = Catchup(head, tail);
                        break;
                    case 'L':
                        head = (head.x - 1, head.y);
                        tail = Catchup(head, tail);
                        break;
                    case 'U':
                        head = (head.x, head.y + 1);
                        tail = Catchup(head, tail);
                        break;
                    case 'D':
                        head = (head.x, head.y - 1);
                        tail = Catchup(head, tail);
                        break;
                    default:
                        break;
                }

                for (int j = 0; j < tails.Count; j++)
                {
                    var prev = j == 0 ? head : tails[j - 1];
                    tails[j] = Catchup(prev, tails[j]);

                    if (j == tails.Count - 1)
                    {
                        parts2.TryAdd(tails[j], 0);
                    }
                }

                parts.TryAdd(tail, 0);
            }
        }

        public async Task<Results> Run(string? input)
        {


            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var formatted = input.Split('\n').Select(s => s.Trim()).Where(s => !String.IsNullOrEmpty(s)).ToList();

            var head = (x: 0, y: 0);
            var tail = head;
            parts.Add((0, 0), 0);
            parts2.Add((0, 0), 0);

            var tails = Enumerable.Range(0, 9).Select(s => (x: 0, y: 0)).ToList();

            foreach (var s in formatted)
            {
                var parts = s.Split(' ');
                var dir = s[0];
                var dist = Int32.Parse(s.Substring(1));

                MoveHead(dir, dist, ref head, ref tail, ref tails);
            }

            return new Results(parts.Count(), parts2.Count());
        }
    }
}

