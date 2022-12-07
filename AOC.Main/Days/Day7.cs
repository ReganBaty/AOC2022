using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day7 : IDay
    {
        public const int Day = 7;

        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var formatted = input.Split('\n').Select(s => s.Trim()).Where(s => !String.IsNullOrEmpty(s));
            var currentFolder = new List<string>();
            var folderSizes = new Dictionary<string, int>();
            foreach (var s in formatted)
            {
                if (s[0] == '$')
                {
                    if (s[2] == 'c')
                    {
                        var dir = s.Split(' ')[2];

                        if (dir == "/")
                        {
                            currentFolder = new List<string>();
                        }
                        else if (dir == "..")
                        {
                            currentFolder.RemoveAt(currentFolder.Count - 1);
                        }
                        else
                        {
                            currentFolder.Add(dir);
                        }

                    }
                }
                else if (Int32.TryParse(s.Split(' ')[0], out var size))
                {
                    for (var i = 0; i <= currentFolder.Count; i++)
                    {
                        var folderName = currentFolder.Take(i).Aggregate("/", (a, b) => a + b + "/");
                        if (!folderSizes.TryAdd(folderName, size))
                        {
                            folderSizes[folderName] += size;
                        }
                    }
                }
            }

            var p1 = folderSizes.Values.Where(x => x <= 100000).Sum();

            var total = 70000000;
            var used = folderSizes.Values.Max();
            var free = total - used;
            var p2 = folderSizes.Values.Where(x => x + free >= 30000000).Min();

            return new Results(p1, p2);
        }
    }

}
