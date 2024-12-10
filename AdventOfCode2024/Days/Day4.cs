namespace AdventOfCode2024.Days;

public class Day4
{
    public static int CountXmas(string input)
    {
        var search = ParseInput(input);
        return search.SelectMany((x, r) => x.Select((_, c) => CheckCharacterForXmas(search, r, c))).Sum();
    }

    public static int CountMasX(string input)
    {
        var search = ParseInput(input);
        return search.SelectMany((x, r) => x.Select((_, c) => CheckCharacterForMasX(search, r, c))).Sum();
    }

    private static int CheckCharacterForXmas(List<List<char>> search, int row, int col)
    {
        if (search[row][col] != 'X') return 0;

        var count = 0;
        // N
        if (row >= 3)
        {
            count += search[row - 1][col] == 'M' && search[row - 2][col] == 'A' && search[row - 3][col] == 'S' ? 1 : 0;
        }

        // NE
        if (row >= 3 && col <= search[row].Count - 4)
        {
            count += search[row - 1][col + 1] == 'M' && search[row - 2][col + 2] == 'A' && search[row - 3][col + 3] == 'S' ? 1 : 0;

        }

        // E
        if (col <= search[row].Count - 4)
        {
            count += search[row][col + 1] == 'M' && search[row][col + 2] == 'A' && search[row][col + 3] == 'S' ? 1 : 0;
        }

        // SE
        if (row <= search.Count - 4 && col <= search[row].Count - 4)
        {
            count += search[row + 1][col + 1] == 'M' && search[row + 2][col + 2] == 'A' && search[row + 3][col + 3] == 'S' ? 1 : 0;
        }

        // S
        if (row <= search.Count - 4)
        {
            count += search[row + 1][col] == 'M' && search[row + 2][col] == 'A' && search[row + 3][col] == 'S' ? 1 : 0;
        }

        // SW
        if (row <= search.Count - 4 && col >= 3)
        {
            count += search[row + 1][col - 1] == 'M' && search[row + 2][col - 2] == 'A' && search[row + 3][col - 3] == 'S' ? 1 : 0;
        }

        // W
        if (col >= 3)
        {
            count += search[row][col - 1] == 'M' && search[row][col - 2] == 'A' && search[row][col - 3] == 'S' ? 1 : 0;
        }

        // NW
        if (row >= 3 && col >= 3)
        {
            count += search[row - 1][col - 1] == 'M' && search[row - 2][col - 2] == 'A' && search[row - 3][col - 3] == 'S' ? 1 : 0;
        }


        return count;
    }

    private static int CheckCharacterForMasX(List<List<char>> search, int row, int col)
    {
        return search[row][col] == 'A' && row >= 1 && row <= search.Count - 2 && col >= 1 &&
               col <= search[row].Count - 2 &&
               ((search[row - 1][col - 1] == 'M' && search[row + 1][col + 1] == 'S') ||
                (search[row - 1][col - 1] == 'S' && search[row + 1][col + 1] == 'M')) &&
               ((search[row - 1][col + 1] == 'M' && search[row + 1][col - 1] == 'S') ||
                (search[row - 1][col + 1] == 'S' && search[row + 1][col - 1] == 'M'))
            ? 1
            : 0;
    }

    private static List<List<char>> ParseInput(string input)
    {
        var search = new List<List<char>>();

        var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            search.Add(line.ToCharArray().ToList());
        }

        return search;
    }
}