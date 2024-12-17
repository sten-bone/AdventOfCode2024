namespace AdventOfCode2024.UnitTests;

[TestClass]
public class Day11Tests
{
    private const string Input = """
                                 125 17
                                 """;

    [TestMethod]
    public void Part1()
    {
        Assert.AreEqual(55312, Day11.CountStonesAfterNBlinks(Input, 25));
    }

    [TestMethod]
    public void Part2()
    {
    }
}
