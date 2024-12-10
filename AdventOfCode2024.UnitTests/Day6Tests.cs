namespace AdventOfCode2024.UnitTests;

[TestClass]
public class Day6Tests
{
    private const string Input = """
                                 ....#.....
                                 .........#
                                 ..........
                                 ..#.......
                                 .......#..
                                 ..........
                                 .#..^.....
                                 ........#.
                                 #.........
                                 ......#...
                                 """;

    [TestMethod]
    public void Part1()
    {
        Assert.AreEqual(41, Day6.CountVisitedPositions(Input));
    }

    [TestMethod]
    public void Part2()
    {
        Assert.AreEqual(6, Day6.CountLoopPositions(Input));
    }
}
