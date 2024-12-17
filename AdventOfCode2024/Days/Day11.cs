namespace AdventOfCode2024.Days;

public class Day11
{
    public static long CountStonesAfterNBlinks(string input, int n)
    {
        return new Stones(input).BlinkNTimes(n).GetCount();
    }

    private static List<Stone> ParseInput(string input)
    {
        return input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => new Stone(long.Parse(x))).ToList();
    }
}

public class Stones
{
    public Dictionary<long, long> StoneCounts { get; set; }

    public Stones(string input)
    {
        StoneCounts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).GroupBy(x => x)
            .ToDictionary(x => x.Key, x => (long)x.Count());
    }

    public Stones BlinkNTimes(int n)
    {
        for (var i = 0; i < n; i++)
        {
            Blink();
        }

        return this;
    }

    public long GetCount()
    {
        return StoneCounts.Values.Sum();
    }

    private void Blink()
    {
        var newStoneCounts = new Dictionary<long, long>();
        foreach (var (value, count) in StoneCounts)
        {
            if (value == 0)
            {
                newStoneCounts.UpdateStoneCount(1, count);
                continue;
            }

            var valueString = value.ToString();
            if (valueString.Length % 2 == 0)
            {
                newStoneCounts.UpdateStoneCount(long.Parse(valueString[..(valueString.Length / 2)]), count);
                newStoneCounts.UpdateStoneCount(long.Parse(valueString[(valueString.Length / 2)..]), count);
                continue;
            }

            newStoneCounts.UpdateStoneCount(value * 2024, count);
        }

        StoneCounts = newStoneCounts;
    }
}

public class Stone(long value)
{
    public long Value { get; private set; } = value;

    public List<Stone> Blink()
    {
        if (Value == 0)
        {
            Value = 1;
            return [this];
        }

        var valueString = Value.ToString();
        if (valueString.Length % 2 == 0)
        {
            return
            [
                new Stone(long.Parse(valueString[..(valueString.Length / 2)])),
                new Stone(long.Parse(valueString[(valueString.Length / 2)..]))
            ];
        }

        Value *= 2024;
        return [this];
    }
}

public static class Day11Utils
{
    public static void UpdateStoneCount(this Dictionary<long, long> dict, long value, long count)
    {
        if (!dict.TryAdd(value, count))
        {
            dict[value] += count;
        }
    }
}