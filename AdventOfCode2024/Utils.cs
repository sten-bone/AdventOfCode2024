namespace AdventOfCode2024;

public static class Utils
{
    public static bool IsInBounds<T>(this List<List<T>> grid, int row, int col) => row >= 0 &&
        row < grid.Count && col >= 0 && col < grid[row].Count;
}