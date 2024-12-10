namespace AdventOfCode2024.UnitTests;

[TestClass]
public class Day7Tests
{
    private const string Input = """
                                 190: 10 19
                                 3267: 81 40 27
                                 83: 17 5
                                 156: 15 6
                                 7290: 6 8 6 15
                                 161011: 16 10 13
                                 192: 17 8 14
                                 21037: 9 7 18 13
                                 292: 11 6 16 20
                                 """;

    [TestMethod]
    public void Part1()
    {
        Assert.AreEqual(3749, Day7.GetTotalCalibration(Input));
    }

    [TestMethod]
    public void Part2()
    {
        Assert.AreEqual(11387, Day7.GetTotalCalibrationWithConcatenation(Input));
    }
}
