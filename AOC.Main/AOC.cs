using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AOC
{
    public class AOC
    {
        private readonly static List<IDay> Days = new List<IDay>() {
            new Day1(),
        };

        public static async Task Main()
        {
            Console.WriteLine("Enter day to run!");
            var day = Console.ReadLine();

            if (Int32.TryParse(day, out int dayOut))
            {
                if (dayOut <= Days.Count)
                {
                    var result = await Days[dayOut - 1].Run();
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine($"Invalid day {day}");
                }
            }
            else
            {
                Console.WriteLine($"Invalid day {day}");
            }
        }
    }
}