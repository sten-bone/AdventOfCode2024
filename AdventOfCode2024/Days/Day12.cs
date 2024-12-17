namespace AdventOfCode2024.Days;

public class Day12
{
    public static long CalculateTotalFencePrice(string input)
    {
        return new Garden(input).CalculateFencePrice();
    }

    public static long CalculateTotalFencePriceWithBulk(string input)
    {
        return new Garden(input).CalculateFencePriceWithBulk();
    }
}

public class Garden
{
    public List<List<Plant>> Plants { get; } = [];
    public List<Region> Regions { get; } = [];

    public Garden(string input)
    {
        var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            Plants.Add(line.Select(x => new Plant(x)).ToList());
        }

        CalculateNeighbors();
        CalculateRegions();
    }

    public long CalculateFencePrice()
    {
        return Regions.Sum(x => (long)x.Plants.Count * x.Circumference);
    }

    public long CalculateFencePriceWithBulk()
    {
        return Regions.Sum(x => (long)x.Plants.Count * x.Sides);
    }

    private void CalculateNeighbors()
    {
        for (var r = 0; r < Plants.Count; r++)
        {
            for (var c = 0; c < Plants[r].Count; c++)
            {
                var plant = Plants[r][c];
                foreach (var (rPlus, cPlus) in new List<(int, int)> { (-1, 0), (0, 1), (1, 0), (0, -1) })
                {
                    plant.Neighbors.Add(Plants.IsInBounds(r + rPlus, c + cPlus) ? Plants[r + rPlus][c + cPlus] : null);
                }
            }
        }
    }

    private void CalculateRegions()
    {
        foreach (var t in Plants)
        {
            foreach (var plant in t)
            {
                if (plant.RegionProcessed) continue;
                Regions.Add(new Region(plant));
            }
        }
    }
}

public class Region(Plant source)
{
    public List<Plant> Plants { get; } = source.GetRegion();
    public int Circumference => Plants.Sum(x => x.Neighbors.Count(n => n is null || n.Value != x.Value));
    public int Sides => Plants.Sum(x => x.CountCorners());
}

public class Plant(char value)
{
    public char Value { get; } = value;
    public List<Plant?> Neighbors { get; } = [];
    public Plant? North => Neighbors[0];
    public Plant? East => Neighbors[1];
    public Plant? South => Neighbors[2];
    public Plant? West => Neighbors[3];
    public bool RegionProcessed { get; set; }

    public List<Plant> GetRegion()
    {
        RegionProcessed = true;
        return [this, .. Neighbors.Where(x => x is not null && x.Value == Value && !x.RegionProcessed).SelectMany(p => p!.GetRegion())];
    }

    public int CountCorners()
    {
        var ne = (North?.Value != Value && East?.Value != Value) || (North?.Value == Value && East?.Value == Value && North.East?.Value != Value) ? 1 : 0;
        var se = (South?.Value != Value && East?.Value != Value) || (South?.Value == Value && East?.Value == Value && South.East?.Value != Value) ? 1 : 0;
        var sw = (South?.Value != Value && West?.Value != Value) || (South?.Value == Value && West?.Value == Value && South.West?.Value != Value) ? 1 : 0;
        var nw = (North?.Value != Value && West?.Value != Value) || (North?.Value == Value && West?.Value == Value && North.West?.Value != Value) ? 1 : 0;
        return ne + se + sw + nw;
    }
}