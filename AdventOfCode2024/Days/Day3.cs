using System.Text.RegularExpressions;

namespace AdventOfCode2024.Days;

public class Day3
{
    public static int SumMultiplicationsInCorruption(string input)
    {
        return ParseInput(input).Sum(x => x.Item1 * x.Item2);
    }

    public static int SumConditionalMultiplicationsInCorruption(string input)
    {
        var inputs = input.Split("don't()", StringSplitOptions.RemoveEmptyEntries).ToList();
        if (inputs.Count == 0) return 0;

        var groups = new List<(int, int)>();
        groups.AddRange(ParseInput(inputs[0]));
        foreach (var i in inputs.Skip(1))
        {
            groups.AddRange(ParseInput(string.Join(string.Empty,
                i.Split("do()", StringSplitOptions.RemoveEmptyEntries).Skip(1))));
        }

        return groups.Sum(x => x.Item1 * x.Item2);
    }

    private static List<(int, int)> ParseInput(string input)
    {
        var matches = Regex.Matches(input, @"mul\((\d{1,3}),(\d{1,3})\)");
        return matches.Select(x => (int.Parse(x.Groups[1].Value), int.Parse(x.Groups[2].Value))).ToList();
    }
}