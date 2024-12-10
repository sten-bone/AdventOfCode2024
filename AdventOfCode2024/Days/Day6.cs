namespace AdventOfCode2024.Days;

public class Day6
{
    public static int CountVisitedPositions(string input)
    {
        return new Grid(input).PatrolGrid().VisitedCount;
    }

    public static int CountLoopPositions(string input)
    {
        return new Grid(input).CountLoopPositions();
    }
}

internal class Grid
{
    public List<List<Tile>> Tiles { get; }
    public List<Tile> AllTiles => Tiles.SelectMany(x => x).ToList();
    public int VisitedCount => AllTiles.Count(x => x.Visited);

    public Grid(string input)
    {
        var lines = new List<string>();
        var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            lines.Add(line);
        }

        Tiles = lines.Select(x => x.ToCharArray().Select(c => new Tile(c)).ToList()).ToList();
        PopulateAllNeighbors();
    }

    public Grid PatrolGrid()
    {
        new Guard(this).Patrol();
        return this;
    }

    public int CountLoopPositions()
    {
        var loopPositionCount = 0;
        for (var r = 0; r < Tiles.Count; r++)
        {
            for (var c = 0; c < Tiles[r].Count; c++)
            {
                if (Tiles[r][c].IsObstacle || Tiles[r][c].IsStart)
                {
                    continue;
                }

                // create new obstacle, save previous to restore later
                var actualTile = Tiles[r][c];
                Tiles[r][c] = new Tile('#');
                PopulateAllNeighbors(); // could be more efficient by only recalculating surrounding, but meh

                loopPositionCount += new Guard(this).Patrol();

                // reset grid
                Tiles[r][c] = actualTile;
                Reset();
            }
        }

        return loopPositionCount;
    }

    private void Reset()
    {
        AllTiles.Where(x => x is { IsStart: false, IsObstacle: false }).ToList().ForEach(x => x.Visited = false);
        PopulateAllNeighbors();
    }

    private void PopulateAllNeighbors()
    {
        for (var r = 0; r < Tiles.Count; r++)
        {
            for (var c = 0; c < Tiles[r].Count; c++)
            {
                var tile = Tiles[r][c];
                if (r - 1 >= 0) tile.North = Tiles[r - 1][c];
                if (c + 1 < Tiles[r].Count) tile.East = Tiles[r][c + 1];
                if (r + 1 < Tiles.Count) tile.South = Tiles[r + 1][c];
                if (c - 1 >= 0) tile.West = Tiles[r][c - 1];
            }
        }
    }
}

internal class Tile(char value)
{
    public bool IsObstacle { get; } = value == '#';
    public bool IsStart { get; } = value == '^';
    public bool Visited { get; set; } = value == '^';
    public Tile? North { get; set; }
    public Tile? East { get; set; }
    public Tile? South { get; set; }
    public Tile? West { get; set; }
}

internal class Guard
{
    public Tile? StartingTile { get; }
    public Tile? CurrentTile { get; private set; }
    public Direction CurrentDirection { get; private set; } = Direction.North;
    private int RepeatVisits { get; set; } = 0;

    public Guard(Grid grid)
    {
        StartingTile = grid.Tiles.SelectMany(x => x).First(x => x.IsStart);
        CurrentTile = StartingTile;
    }

    public int Patrol()
    {
        while (GetNextTile() is { } nextTile)
        {
            if (nextTile.IsObstacle)
            {
                Rotate();
                continue;
            }

            CurrentTile = nextTile;
            if (CurrentTile.Visited)
            {
                RepeatVisits++;
            }
            else
            {
                CurrentTile.Visited = true;
                RepeatVisits = 0;
            }

            // arbitrarily pick number to define a loop, high to make sure we get it all
            if (RepeatVisits > 200)
            {
                return 1;
            }
        }

        return 0;
    }

    public Tile? GetNextTile()
    {
        return CurrentDirection switch

        {
            Direction.North => CurrentTile?.North,
            Direction.East => CurrentTile?.East,
            Direction.South => CurrentTile?.South,
            Direction.West => CurrentTile?.West,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void Rotate()
    {
        CurrentDirection = (Direction)(((int)CurrentDirection + 1) % 4);
    }
}

internal enum Direction
{
    North,
    East,
    South,
    West
}