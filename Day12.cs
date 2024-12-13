namespace AdventOfCode2024;

public partial class Day12 : IPuzzle
{
    class Region
    {
        public List<Plot> Plots = [];
        public long Perimiter = 0;
    }

    class Plot
    {
        public char Plant;
        public Location Loc = null!;
        public Region? Region;
    }

    List<List<Plot>> map = [];
    List<Region> regions = [];

    public void LoadInput(string inputPath)
    {
        map = File.ReadAllLines(inputPath).Index().
            Select(row => row.Item.Index()
            .Select(col => new Plot { Plant = col.Item, Loc = new Location(col.Index, row.Index) })
            .ToList()).ToList();
    }

    private void AddToRegion(Region region, Plot plot)
    {
        if (plot.Region != null) return;

        plot.Region = region;
        region.Plots.Add(plot);

        var loc = plot.Loc;
        var plant = plot.Plant;
        foreach (var direction in Directions.AllStraight())
        {
            var newLoc = loc + direction;
            if (newLoc.Inside(map) && map[newLoc.Y][newLoc.X].Plant == plant)
            {
                AddToRegion(region, map[newLoc.Y][newLoc.X]);
            }
            else
            {
                region.Perimiter++;
            }
        }
    }

    private void FindRegions()
    {
        while (true)
        {
            var next = map.SelectMany(r => r).FirstOrDefault(p => p.Region is null);
            if (next == null) break;
            var region = new Region();
            regions.Add(region);
            AddToRegion(region, next);
        }
    }

    private int CountSides(Region r)
    {
        var plant = r.Plots[0].Plant;

        var xMin = r.Plots.Min(p => p.Loc.X) - 1;
        var yMin = r.Plots.Min(p => p.Loc.Y) - 1;
        var xMax = r.Plots.Max(p => p.Loc.X) + 1;
        var yMax = r.Plots.Max(p => p.Loc.Y) + 1;

        var subMap = Enumerable.Range(yMin, yMax - yMin + 1)
            .Select(y => Enumerable.Range(xMin, xMax - xMin + 1)
            .Select(x => '.').ToList()).ToList();
        foreach (var plot in r.Plots)
        {
            subMap[plot.Loc.Y - yMin][plot.Loc.X - xMin] = plant;
        }

        int sides = 0;

        foreach (var x in Enumerable.Range(0, xMax - xMin))
        {
            var prevState = 0;
            foreach (var y in Enumerable.Range(0, yMax - yMin + 1))
            {
                var plant1 = subMap[y][x];
                var plant2 = subMap[y][x + 1];
                var state = (plant1 == plant2) ? 0 : (plant1 == plant) ? 1 : 2;
                if (state > 0 && state != prevState) sides++;
                prevState = state;
            }
        }

        foreach (var y in Enumerable.Range(0, yMax - yMin))
        {
            var prevState = 0;
            foreach (var x in Enumerable.Range(0, xMax - xMin + 1))
            {
                var plant1 = subMap[y][x];
                var plant2 = subMap[y + 1][x];
                var state = (plant1 == plant2) ? 0 : (plant1 == plant) ? 1 : 2;
                if (state > 0 && state != prevState) sides++;
                prevState = state;
            }
        }

        return sides;
    }

    public long Part1()
    {
        FindRegions();
        return regions.Sum(r => r.Plots.Count * r.Perimiter);
    }

    public long Part2()
    {
        FindRegions();
        return regions.Sum(r => r.Plots.Count * CountSides(r));
    }
}
