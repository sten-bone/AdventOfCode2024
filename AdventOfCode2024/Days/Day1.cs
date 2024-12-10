namespace AdventOfCode2024.Days;

public class Day1
{

    public static int FindTotalDistance(string input)
    {
        var (list1, list2) = ParseInput(input);
        return list1.OrderBy(x => x).Zip(list2.OrderBy(x => x)).Sum(x => Math.Abs(x.First - x.Second));
    }

    public static int FindSimilarityScore(string input)
    {
        var (list1, list2) = ParseInput(input);
        var groupedList2 = list2.GroupBy(x => x).ToDictionary(x => x.Key, x => x.ToList().Count);
        return list1.Sum(x => x * groupedList2.GetValueOrDefault(x, 0));
    }
    private static (List<int>, List<int>) ParseInput(string input)
    {
        var list1 = new List<int>();
        var list2 = new List<int>();

        var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            var x = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            list1.Add(int.Parse(x[0]));
            list2.Add(int.Parse(x[1]));
        }

        return (list1, list2);
    }
}