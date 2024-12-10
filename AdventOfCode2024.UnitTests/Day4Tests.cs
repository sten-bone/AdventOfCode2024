namespace AdventOfCode2024.UnitTests;

[TestClass]
public class Day4Tests
{
    private const string Input = """
                                 MMMSXXMASM
                                 MSAMXMSMSA
                                 AMXSXMAAMM
                                 MSAMASMSMX
                                 XMASAMXAMM
                                 XXAMMXXAMA
                                 SMSMSASXSS
                                 SAXAMASAAA
                                 MAMMMXMMMM
                                 MXMXAXMASX
                                 """;

    [TestMethod]
    public void Part1()
    {
        Assert.AreEqual(18, Day4.CountXmas(Input));
    }

    [TestMethod]
    public void Part2()
    {
        Assert.AreEqual(9, Day4.CountMasX(Input));
    }
}