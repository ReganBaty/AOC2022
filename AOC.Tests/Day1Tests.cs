using Xunit;

namespace AOC.Tests;

public class Day1Tests
{
    [Fact]
    public void Test1()
    {
        var day1 = new Day1();
        var result = day1.Run(@"WOW").Result;
        Assert.Equal(1, result.Part1);
        Assert.Equal(2, result.Part2);
    }
}