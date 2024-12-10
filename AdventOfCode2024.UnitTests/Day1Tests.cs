namespace AdventOfCode2024.UnitTests;

[TestClass]
public class Day1Tests
{
    private const string Input = """
                                 3   4
                                 4   3
                                 2   5
                                 1   3
                                 3   9
                                 3   3
                                 """;

    [TestMethod]
    public void Part1()
    {
        Assert.AreEqual(11, Day1.FindTotalDistance(Input));
    }

    [TestMethod]
    public void Part2()
    {
        Assert.AreEqual(31, Day1.FindSimilarityScore(Input));
    }
}