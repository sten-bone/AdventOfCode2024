﻿namespace AdventOfCode2024.UnitTests;

[TestClass]
public class Day9Tests
{
    private const string Input = """
                                 2333133121414131402
                                 """;

    [TestMethod]
    public void Part1()
    {
        Assert.AreEqual(1928, Day9.CalculateChecksum(Input));
    }

    [TestMethod]
    public void Part2()
    {
        Assert.AreEqual(2858, Day9.CalculateChecksumViaFileMove(Input));
    }
}
