namespace AdventOfCode2024.Days;

public class Day10
{
    public static int CountTrailheadScores(string input)
    {
        var grid = ParseInput(input);
        return grid.SelectMany((x, r) => x.Select((y, c) => y == 0 ? new Node(grid, r, c).CountDistinctPathsTo9() : 0))
            .Sum();
    }

    public static int CountTrailheadRatings(string input)
    {
        var grid = ParseInput(input);
        return grid.SelectMany((x, r) => x.Select((y, c) => y == 0 ? new Node(grid, r, c).GetPathsTo9().Count : 0))
            .Sum();
    }

    private static List<List<int>> ParseInput(string input)
    {
        var grid = new List<List<int>>();

        var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            grid.Add(line.Select(c => int.Parse(c.ToString())).ToList());
        }

        return grid;
    }
}

public class Node
{
    public int Value { get; }
    public List<Node> Neighbors { get; } = [];
    public int Row { get; }
    public int Col { get; }

    public Node(List<List<int>> grid, int row, int col)
    {
        Value = grid[row][col];
        Row = row;
        Col = col;

        if (row - 1 >= 0 && grid[row - 1][col] == Value + 1)
        {
            Neighbors.Add(new Node(grid, row - 1, col));
        }

        if (col + 1 < grid[row].Count && grid[row][col + 1] == Value + 1)
        {
            Neighbors.Add(new Node(grid, row, col + 1));
        }

        if (row + 1 < grid.Count && grid[row + 1][col] == Value + 1)
        {
            Neighbors.Add(new Node(grid, row + 1, col));
        }

        if (col - 1 >= 0 && grid[row][col - 1] == Value + 1)
        {
            Neighbors.Add(new Node(grid, row, col - 1));
        }
    }

    public int CountDistinctPathsTo9()
    {
        return GetPathsTo9().DistinctBy(x => new { x.Row, x.Col }).Count();
    }

    public List<Node> GetPathsTo9()
    {
        return Value == 9 ? [this] : Neighbors.SelectMany(x => x.GetPathsTo9()).ToList();
    }
}
