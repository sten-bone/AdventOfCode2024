namespace AdventOfCode2024.Days;

public class Day2
{
    public static int CountSafe(string input)
    {
        return ParseInput(input).Count(IsSafe);
    }

    public static int CountSafeWithDampener(string input)
    {
        return ParseInput(input).Count(IsSafeWithDampener);
    }

    private static bool IsSafe(List<int> list)
    {
        if (!(list.OrderBy(x => x).SequenceEqual(list) || list.OrderByDescending(x => x).SequenceEqual(list)))
        {
            return false;
        }

        return list.Zip(list.Skip(1), (a, b) => Math.Abs(a - b) >= 1 && Math.Abs(a - b) <= 3).All(x => x);
    }

    private static bool IsSafeWithDampener(List<int> list)
    {
        if (IsSafe(list))
        {
            return true;
        }

        for (var i = 0; i < list.Count; i++)
        {
            List<int> newList = [.. list];
            newList.RemoveAt(i);

            if (IsSafe(newList)) return true;
        }

        return false;
    }

    private static List<List<int>> ParseInput(string input)
    {
        var list = new List<List<int>>();

        var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            list.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList());
        }

        return list;
    }
}