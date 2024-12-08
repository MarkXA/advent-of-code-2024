namespace AdventOfCode2024;

public partial class Day8 : IPuzzle
{
    private List<List<char>> map = [];
    private IList<(Location loc1, Location loc2)> antennaPairs = [];

    public void LoadInput(string inputPath)
    {
        map = File.ReadAllLines(inputPath).RemoveEmpty().Select(s => s.ToList()).ToList();

        antennaPairs = map.Index()
            .SelectMany(
                row => row.Item.Index().Select(
                    col => (Code: col.Item, Loc: new Location(col.Index, row.Index))))
            .Where(a => a.Code != '.')
            .GroupBy(a => a.Code)
            .Select(g => g
                .Index()
                .SelectMany(g1 =>
                    g.Skip(g1.Index + 1).Select(g2 => (loc1: g1.Item.Loc, loc2: g2.Loc))))
            .SelectMany(p => p)
            .ToList();
    }

    public long Part1()
    {
        HashSet<Location> antinodes = [];

        foreach (var (loc1, loc2) in antennaPairs)
        {
            var delta = loc2 - loc1;
            antinodes.Add(loc1 - delta);
            antinodes.Add(loc2 + delta);
        }

        return antinodes.Where(l => l.Inside(map)).Count();
    }

    public long Part2()
    {
        HashSet<Location> antinodes = [];

        foreach (var (loc1, loc2) in antennaPairs)
        {
            var delta = loc2 - loc1;
            var antinode = loc1;
            while (antinode.Inside(map))
            {
                antinodes.Add(antinode);
                antinode -= delta;
            }
            antinode = loc2;
            while (antinode.Inside(map))
            {
                antinodes.Add(antinode);
                antinode += delta;
            }
        }

        return antinodes.Count;
    }
}
