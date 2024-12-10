namespace AdventOfCode2024.Days;

public class Day5
{
    public static int SumMiddlePageOfCorrectUpdates(string rulesString, string pageNumbersString)
    {
        var rules = ParseRules(rulesString);
        var pageNumbers = ParsePageNumbers(pageNumbersString);

        return pageNumbers.Where(x => IsOrderCorrect(x, rules)).Sum(x => x[(x.Count / 2)]);
    }

    public static int SumMiddlePageOfIncorrectUpdates(string rulesString, string pageNumbersString)
    {
        var rules = ParseRules(rulesString);
        var pageNumbers = ParsePageNumbers(pageNumbersString);

        return pageNumbers.Where(x => !IsOrderCorrect(x, rules))
            .Select(x => ReorderPageNumbers(x, rules))
            .Sum(x => x[(x.Count / 2)]);
    }

    private static bool IsOrderCorrect(List<int> pageNumbers, Dictionary<int, List<int>> rules)
    {
        for (var i = 0; i < pageNumbers.Count; i++)
        {
            if (!rules.ContainsKey(pageNumbers[i]))
            {
                continue;
            }

            var anyIncorrectlyBeforePage = rules[pageNumbers[i]].Any(x =>
            {
                var index = pageNumbers.IndexOf(x);
                return index != -1 && index < i;
            });
            if (anyIncorrectlyBeforePage)
            {
                return false;
            }
        }

        return true;
    }

    private static List<int> ReorderPageNumbers(List<int> pageNumbers, Dictionary<int, List<int>> rules)
    {
        // skip the first item, since it is before everything else
        var i = 1;
        while (i < pageNumbers.Count)
        {
            // skip if no rules
            if (!rules.ContainsKey(pageNumbers[i]))
            {
                i++;
                continue;
            }

            var pageRules = rules[pageNumbers[i]];
            var moved = false;
            foreach (var trailingPage in pageRules)
            {
                var index = pageNumbers.IndexOf(trailingPage);
                if (index == -1 || index >= i) continue;

                var value = pageNumbers[i];
                pageNumbers.RemoveAt(i);
                pageNumbers.Insert(index, value);
                moved = true;
                break;
            }

            // restart from beginning if moved
            i = moved ? 1 : i + 1;
        }

        return pageNumbers;
    }

    private static List<List<int>> ParsePageNumbers(string pageNumbers)
    {
        var pageNumberLists = new List<List<int>>();

        var reader = new StringReader(pageNumbers);
        while (reader.ReadLine() is { } line)
        {
            pageNumberLists.Add(line.Split(',').Select(int.Parse).ToList());
        }

        return pageNumberLists;
    }

    // Rule dictionary has key of page and list of all pages which must come after it
    private static Dictionary<int, List<int>> ParseRules(string rules)
    {
        var ruleDict = new Dictionary<int, List<int>>();

        var reader = new StringReader(rules);
        while (reader.ReadLine() is { } line)
        {
            var pages = line.Split('|').Select(int.Parse).ToList();
            if (ruleDict.ContainsKey(pages[0]))
            {
                ruleDict[pages[0]].Add(pages[1]);
            }
            else
            {
                ruleDict[pages[0]] = [pages[1]];
            }
        }

        return ruleDict;
    }
}