namespace AdventOfCode2024.UnitTests;

[TestClass]
public class Day10Tests
{
    private const string Input = """
                                 89010123
                                 78121874
                                 87430965
                                 96549874
                                 45678903
                                 32019012
                                 01329801
                                 10456732
                                 """;

    [TestMethod]
    public void Part1()
    {
        Assert.AreEqual(36, Day10.CountTrailheadScores(Input));
    }

    [TestMethod]
    public void Part2()
    {
        Assert.AreEqual(81, Day10.CountTrailheadRatings(Input));
    }
}
