namespace AdventOfCode2024;

using System.Text.RegularExpressions;

public partial class Day3 : IPuzzle
{
    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex matchMul();

    [GeneratedRegex(@"mul\((\d+),(\d+)\)|do\(\)|don't\(\)")]
    private static partial Regex matchAll();

    private string input = null!;

    public void LoadInput(string inputPath)
    {
        input = File.ReadAllText(inputPath);
    }

    public int Part1()
    {
        var matches = matchMul().Matches(input);
        return matches.Sum(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
    }

    public int Part2()
    {
        var matches = matchAll().Matches(input);
        var answer = 0;
        var enabled = true;

        foreach (Match match in matches)
        {
            if (match.Value.StartsWith("don't"))
                enabled = false;
            else if (match.Value.StartsWith("do"))
                enabled = true;
            else if (enabled)
                answer += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }
        
        return answer;
    }
}
