namespace AdventOfCode2024;

public class Program
{
    public static void Main(string[] args)
    {
        var days = args.Length < 1 ? Enumerable.Range(1, 25) : [int.Parse(args[0])];
        var parts = args.Length < 2 ? Enumerable.Range(1, 2) : [int.Parse(args[1])];

        foreach (var day in days)
            foreach (var part in parts)
                SolvePuzzle(day, part);
    }

    private static void SolvePuzzle(int day, int part)
    {
        var type = Type.GetType($"AdventOfCode2024.Day{day}");
        if (type is null || Activator.CreateInstance(type) is not IPuzzle puzzle)
            return;

        var answer = puzzle.Solve($"input/day{day}.txt", part);
        if (answer == int.MinValue)
            return;

        Console.WriteLine($"Day {day} part {part}: {answer}");
    }
}