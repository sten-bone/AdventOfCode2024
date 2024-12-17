namespace AdventOfCode2024.Days;

public class Day8
{
    public static int CountAntiNodes(string input)
    {
        return PopulateAntiNodes(ParseInput(input)).Sum(x => x.Count(y => y.IsAntiNode));
    }

    public static int CountAntiNodesWithResonance(string input)
    {
        return PopulateAntiNodesWithResonance(ParseInput(input)).Sum(x => x.Count(y => y.IsAntiNode));
    }

    private static List<List<Location>> PopulateAntiNodes(List<List<Location>> grid)
    {
        var antennas = grid.SelectMany(x => x).Where(x => x.IsAntenna).ToList();
        var antennasLookup = antennas.GroupBy(x => x.Frequency).ToDictionary(x => x.Key, x => x.ToList());
        foreach (var antenna in antennas)
        {
            var otherAntennas = antennasLookup[antenna.Frequency].Where(x => !x.Point.Equals(antenna.Point)).ToList();
            foreach (var otherAntenna in otherAntennas)
            {
                var distance = antenna.GetDistance(otherAntenna);

                var nearAntennaPoint = antenna.Point.Add(distance);
                if (grid.IsInBounds(nearAntennaPoint.YPosition, nearAntennaPoint.XPosition))
                {
                    grid[nearAntennaPoint.YPosition][nearAntennaPoint.XPosition].IsAntiNode = true;
                }

                var nearOtherAntennaPoint = otherAntenna.Point.Subtract(distance);
                if (grid.IsInBounds(nearOtherAntennaPoint.YPosition, nearOtherAntennaPoint.XPosition))
                {
                    grid[nearOtherAntennaPoint.YPosition][nearOtherAntennaPoint.XPosition].IsAntiNode = true;
                }
            }
        }

        return grid;
    }

    private static List<List<Location>> PopulateAntiNodesWithResonance(List<List<Location>> grid)
    {
        var antennas = grid.SelectMany(x => x).Where(x => x.IsAntenna).ToList();
        var antennasLookup = antennas.GroupBy(x => x.Frequency).ToDictionary(x => x.Key, x => x.ToList());
        foreach (var antenna in antennas)
        {
            var otherAntennas = antennasLookup[antenna.Frequency].Where(x => !x.Point.Equals(antenna.Point)).ToList();
            if (otherAntennas.Count == 0)
            {
                continue;
            }

            antenna.IsAntiNode = true;
            foreach (var otherAntenna in otherAntennas)
            {
                var distance = antenna.GetDistance(otherAntenna);

                var nearAntennaPoint = antenna.Point.Add(distance);
                while (grid.IsInBounds(nearAntennaPoint.YPosition, nearAntennaPoint.XPosition))
                {
                    grid[nearAntennaPoint.YPosition][nearAntennaPoint.XPosition].IsAntiNode = true;
                    nearAntennaPoint = nearAntennaPoint.Add(distance);
                }

                var nearOtherAntennaPoint = otherAntenna.Point.Subtract(distance);
                while (grid.IsInBounds(nearOtherAntennaPoint.YPosition, nearOtherAntennaPoint.XPosition))
                {
                    grid[nearOtherAntennaPoint.YPosition][nearOtherAntennaPoint.XPosition].IsAntiNode = true;
                    nearOtherAntennaPoint = nearOtherAntennaPoint.Subtract(distance);
                }
            }
        }

        return grid;
    }

    private static List<List<Location>> ParseInput(string input)
    {
        var grid = new List<List<Location>>();

        var reader = new StringReader(input);
        var lineNumber = 0;
        while (reader.ReadLine() is { } line)
        {
            grid.Add(line.ToCharArray().Select((x, i) => new Location(x, i, lineNumber)).ToList());
            lineNumber++;
        }

        return grid;
    }
}

public class Location(char value, int x, int y)
{
    public Point Point { get; } = new(x, y);
    public bool IsAntenna { get; } = char.IsLetterOrDigit(value);
    public char Frequency { get; } = value;
    public bool IsAntiNode { get; set; }

    public Point GetDistance(Location other)
    {
        return Point.Subtract(other.Point);
    }
}

public class Point(int x, int y)
{
    public int XPosition { get; } = x;
    public int YPosition { get; } = y;

    public Point Add(Point other)
    {
        return new Point(XPosition + other.XPosition, YPosition + other.YPosition);
    }

    public Point Subtract(Point other)
    {
        return new Point(XPosition - other.XPosition, YPosition - other.YPosition);
    }

    public bool Equals(Point other)
    {
        return XPosition == other.XPosition && YPosition == other.YPosition;
    }
}