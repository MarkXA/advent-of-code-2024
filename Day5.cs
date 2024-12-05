namespace AdventOfCode2024;

public partial class Day5 : IPuzzle
{
    private List<List<int>> ordering = null!;
    private List<List<int>> updates = null!;

    public void LoadInput(string inputPath)
    {
        var input = File.ReadAllLines(inputPath);

        ordering = input
            .TakeWhile(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s.Split('|').Select(int.Parse).ToList())
            .ToList();

        updates = input
            .SkipWhile(s => !string.IsNullOrWhiteSpace(s))
            .SkipWhile(string.IsNullOrWhiteSpace)
            .Select(s => s.Split(',').Select(int.Parse).ToList())
            .ToList();
    }

    public bool IsValid(List<int> update)
    {
        return ordering.All(order => UpdateMatchesOrder(update, order));
    }

    private static bool UpdateMatchesOrder(IEnumerable<int> update, List<int> order)
    {
        return !update.Contains(order[0]) || !update.Contains(order[1])
            || update.SkipWhile(n => n != order[0]).Contains(order[1]);
    }

    public List<int> Reorder(List<int> update)
    {
        update.Sort((a, b) => IsValid([a, b]) ? -1 : 1);
        return update;
    }

    public int Part1()
    {
        return updates
            .Where(IsValid)
            .Select(u => u[u.Count / 2])
            .Sum();
    }

    public int Part2()
    {
        return updates
            .Where(u => !IsValid(u))
            .Select(Reorder)
            .Select(u => u[u.Count / 2])
            .Sum();
    }
}
