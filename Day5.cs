namespace AdventOfCode2024;

public partial class Day5 : IPuzzle
{
    private int[][] ordering = null!;
    private int[][] updates = null!;

    public void LoadInput(string inputPath)
    {
        var input = File.ReadAllLines(inputPath);

        ordering = input.TakeWhile(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s.Split('|').Select(int.Parse).ToArray())
            .ToArray();

        updates = input.SkipWhile(s => !string.IsNullOrWhiteSpace(s)).SkipWhile(s => string.IsNullOrWhiteSpace(s))
            .Select(s => s.Split(',').Select(int.Parse).ToArray())
            .ToArray();
    }

    public bool IsValid(int[] update)
    {
        return ordering.All(order => UpdateMatchesOrder(update, order));
    }

    private static bool UpdateMatchesOrder(IEnumerable<int> update, int[] order)
    {
        return !update.Contains(order[0]) || !update.Contains(order[1]) || update.SkipWhile(n => n != order[0]).Contains(order[1]);
    }

    public int[] Reorder(int[] update)
    {
        var reordered = new List<int>(update);
        while (true)
        {
            var order = ordering.FirstOrDefault(order => !UpdateMatchesOrder(reordered, order));
            if (order == null)
            {
                return reordered.ToArray();
            }
            reordered.Remove(order[0]);
            reordered.Insert(reordered.IndexOf(order[1]), order[0]);
        }
    }

    public int Part1()
    {
        return updates
            .Where(IsValid)
            .Select(u => u[u.Length / 2])
            .Sum();
    }

    public int Part2()
    {
        return updates
            .Where(u => !IsValid(u))
            .Select(Reorder)
            .Select(u => u[u.Length / 2])
            .Sum();
    }
}
