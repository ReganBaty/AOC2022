using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    public class Monkey
    {
        public Monkey()
        {
            Items = new List<long>();
        }
        public List<long> Items { get; set; }
        public int Operation { get; set; }
        public bool Add { get; set; }
        public int Test { get; set; }
        public int IfTrue { get; set; }
        public int IfFalse { get; set; }
        public int Inspected { get; set; }

    }
    public sealed class Day11 : IDay
    {
        public const int Day = 11;

        public Monkey MakeMonkey(string m)
        {
            var tests = m.Split('\n');
            var start = tests[1].Split(":")[1].Split(',').Select(s => Convert.ToInt64(s.Trim()));
            var ops = tests[2].Split('=')[1].Split(' ');
            var parse = Int32.TryParse(ops[3], out int op2);
            var add = ops[2].Trim() == "+";
            var test = Convert.ToInt32(tests[3].Split(' ').Last().Trim());
            var ifTrue = Convert.ToInt32(tests[4].Split(' ').Last().Trim());
            var ifFalse = Convert.ToInt32(tests[5].Split(' ').Last().Trim());

            return new Monkey()
            {
                Items = start.ToList(),
                Operation = op2,
                Add = add,
                Test = test,
                IfTrue = ifTrue,
                IfFalse = ifFalse,
            };
        }

        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var formatted = input.Split("\n\n").Select(s => s.Trim()).Where(s => !String.IsNullOrEmpty(s)).ToList();

            var monkeys = formatted.Select(m => MakeMonkey(m)).ToList();
            var monkeys2 = formatted.Select(m => MakeMonkey(m)).ToList();

            //Part 1
            for (var round = 1; round <= 20; round++)
            {
                foreach (var m in monkeys)
                {
                    m.Inspected += m.Items.Count;
                    foreach (var i in m.Items)
                    {
                        var newI = i;
                        if (m.Operation == 0)
                            newI = m.Add ? i + i : i * i;
                        else
                            newI = m.Add ? i + m.Operation : i * m.Operation;


                        newI /= 3;

                        var success = newI % m.Test == 0;

                        monkeys[success ? m.IfTrue : m.IfFalse].Items.Add(newI);
                    }

                    m.Items.Clear();
                }
            }

            var cycleLength = monkeys2.Select(m => m.Test).Aggregate(1, (s, d) => s * d);
            // Part 2
            for (var round = 1; round <= 10000; round++)
            {
                foreach (var m in monkeys2)
                {
                    m.Inspected += m.Items.Count;
                    foreach (var i in m.Items)
                    {
                        var newI = i;
                        if (m.Operation == 0)
                            newI = m.Add ? i + i : i * i;
                        else
                            newI = m.Add ? i + m.Operation : i * m.Operation;

                        newI %= cycleLength;
                        var success = newI % m.Test == 0;

                        monkeys2[success ? m.IfTrue : m.IfFalse].Items.Add(newI);
                    }

                    m.Items.Clear();
                }
            }

            return new Results(monkeys.OrderByDescending(s => s.Inspected).Take(2).Aggregate(1L, (s, d) => s * d.Inspected), monkeys2.OrderByDescending(s => s.Inspected).Take(2).Aggregate(1L, (s, d) => s * d.Inspected));
        }
    }
}
