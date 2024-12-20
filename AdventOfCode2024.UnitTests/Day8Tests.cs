﻿namespace AdventOfCode2024.UnitTests;

[TestClass]
public class Day8Tests
{
    private const string Input = """
                                 ............
                                 ........0...
                                 .....0......
                                 .......0....
                                 ....0.......
                                 ......A.....
                                 ............
                                 ............
                                 ........A...
                                 .........A..
                                 ............
                                 ............
                                 """;

    [TestMethod]
    public void Part1()
    {
        Assert.AreEqual(14, Day8.CountAntiNodes(Input));
    }

    [TestMethod]
    public void Part2()
    {
        Assert.AreEqual(34, Day8.CountAntiNodesWithResonance(Input));
    }
}
