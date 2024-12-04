namespace AdventOfCode2024;

using System.Text.RegularExpressions;

public partial class Day4 : IPuzzle
{
    private string[] input = null!;

    public void LoadInput(string inputPath)
    {
        input = File.ReadAllLines(inputPath)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToArray();
    }

    public int CountOccurrences(IEnumerable<string> strings, string target)
    {
        var match = new Regex(target);
        var matchReverse = new Regex(new string(target.Reverse().ToArray()));
        return strings.Sum(s => match.Matches(s).Count + matchReverse.Matches(s).Count);
    }

    public int Part1()
    {
        var width = input[0].Length;
        var height = input.Length;
        var padding = new string(' ', height - 1);
        var padded = input.Select(s => padding + s + padding).ToArray();

        var horizontal = input;

        var vertical = Enumerable.Range(0, width)
            .Select(n => string.Concat(input.Select(s => s[n]).ToArray()))
            .ToArray();

        var diagonal1 = Enumerable.Range(0, padded[0].Length - padding.Length)
            .Select(n1 => String.Concat(padded.Select((s, n2) => s.Skip(n1 + n2).Take(1)).SelectMany(c => c)).Trim())
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToArray();

        var diagonal2 = Enumerable.Range(padding.Length, padded[0].Length - padding.Length)
            .Select(n1 => String.Concat(padded.Select((s, n2) => s.Skip(n1 - n2).Take(1)).SelectMany(c => c)).Trim())
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToArray();

        return new[] { horizontal, vertical, diagonal1, diagonal2 }.Sum(strings => CountOccurrences(strings, "XMAS"));
    }

    public int Part2()
    {
        var width = input[0].Length;
        var height = input.Length;

        var stars = Enumerable.Range(0, width - 2)
            .SelectMany(x => Enumerable.Range(0, height - 2)
                .Select(y => new string([
                    input[y][x],
                    input[y][x + 2],
                    input[y + 1][x + 1],
                    input[y + 2][x],
                    input[y + 2][x + 2]
                ]))).ToArray();

        return new[] {"MMASS", "MSAMS"}.Sum(s => CountOccurrences(stars, s));
    }
}
