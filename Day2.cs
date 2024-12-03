namespace AdventOfCode2024;

using System.Linq;

public class Day2 : IPuzzle
{
    private int[][] reports = null!;

    public void LoadInput(string inputPath)
    {
        reports =
            File.ReadAllLines(inputPath)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
            .ToArray();
    }

    private static bool IsSafe(int[] report)
    {
        var diffs = Enumerable.Range(0, report.Length - 1).Select(i => report[i + 1] - report[i]);
        return diffs.All(d => d >= 1 && d <= 3) || diffs.All(d => d <= -1 && d >= -3);
    }

    public int Part1()
    {
        return reports.Count(IsSafe);
    }

    public int Part2()
    {
        return reports.Count(report => 
            Enumerable.Range(0, report.Length).Any(n =>
                IsSafe(report.Take(n).Concat(report.Skip(n + 1)).ToArray()
            ))
        );
    }
}
