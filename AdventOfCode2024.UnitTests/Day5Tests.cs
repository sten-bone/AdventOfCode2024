namespace AdventOfCode2024.UnitTests;

[TestClass]
public class Day5Tests
{
    private const string Rules = """
                                  47|53
                                  97|13
                                  97|61
                                  97|47
                                  75|29
                                  61|13
                                  75|53
                                  29|13
                                  97|29
                                  53|29
                                  61|53
                                  97|53
                                  61|29
                                  47|13
                                  75|47
                                  97|75
                                  47|61
                                  75|61
                                  47|29
                                  75|13
                                  53|13
                                  """;

    private const string PageNumbers = """
                                       75,47,61,53,29
                                       97,61,53,29,13
                                       75,29,13
                                       75,97,47,61,53
                                       61,13,29
                                       97,13,75,29,47
                                       """;

    [TestMethod]
    public void Part1()
    {
        Assert.AreEqual(143, Day5.SumMiddlePageOfCorrectUpdates(Rules, PageNumbers));
    }

    [TestMethod]
    public void Part2()
    {
        Assert.AreEqual(123, Day5.SumMiddlePageOfIncorrectUpdates(Rules, PageNumbers));
    }
}