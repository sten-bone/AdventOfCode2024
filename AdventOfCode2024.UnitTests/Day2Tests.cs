namespace AdventOfCode2024.UnitTests;

[TestClass]
public class Day2Tests
{
    private const string Input = """
                                 7 6 4 2 1
                                 1 2 7 8 9
                                 9 7 6 2 1
                                 1 3 2 4 5
                                 8 6 4 4 1
                                 1 3 6 7 9
                                 15 8 7 6 4
                                 4 6 7 8 15
                                 81 83 84 81 83
                                 """;

    [TestMethod]
    public void Part1()
    {
        Assert.AreEqual(2, Day2.CountSafe(Input));
    }

    [TestMethod]
    public void Part2()
    {
        Assert.AreEqual(6, Day2.CountSafeWithDampener(Input));
    }
}