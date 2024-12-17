namespace AdventOfCode2024.UnitTests;

[TestClass]
public class Day12Tests
{
    private const string Input = """
                                 RRRRIICCFF
                                 RRRRIICCCF
                                 VVRRRCCFFF
                                 VVRCCCJFFF
                                 VVVVCJJCFE
                                 VVIVCCJJEE
                                 VVIIICJJEE
                                 MIIIIIJJEE
                                 MIIISIJEEE
                                 MMMISSJEEE
                                 """;

    [TestMethod]
    public void Part1()
    {
        Assert.AreEqual(1930, Day12.CalculateTotalFencePrice(Input));
    }

    [TestMethod]
    public void Part2()
    {
        Assert.AreEqual(1206, Day12.CalculateTotalFencePriceWithBulk(Input));
    }
}
