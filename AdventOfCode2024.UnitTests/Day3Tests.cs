namespace AdventOfCode2024.UnitTests;

[TestClass]
public class Day3Tests
{
    private const string Input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

    [TestMethod]
    public void Part1()
    {
        Assert.AreEqual(161, Day3.SumMultiplicationsInCorruption(Input));
    }

    [TestMethod]
    public void Part2()
    {
        Assert.AreEqual(48, Day3.SumConditionalMultiplicationsInCorruption(Input));
    }
}