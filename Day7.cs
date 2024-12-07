namespace AdventOfCode2024;

public class Day7 : IPuzzle
{
    private record Equation(long Solution, long[] Numbers);

    private Equation[] equations = null!;

    public void LoadInput(string inputPath)
    {
        equations = File.ReadAllLines(inputPath).RemoveEmpty()
            .Select(s => new Equation(
                long.Parse(s.Split(':')[0]),
                s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse).ToArray()))
            .ToArray();
    }

    public bool HasSolution1(long target, IList<long> numbers) {
        if (numbers.Count == 1)
            return numbers[0] == target;
        if (numbers[0] > target)
            return false;
        return
            HasSolution1(target, [numbers[0] * numbers[1], ..numbers.Skip(2)]) ||
            HasSolution1(target, [numbers[0] + numbers[1], ..numbers.Skip(2)]);
    }

    public bool HasSolution2(long target, IList<long> numbers) {
        if (numbers.Count == 1)
            return numbers[0] == target;
        if (numbers[0] > target)
            return false;
        return
            HasSolution2(target, [long.Parse($"{numbers[0]}{numbers[1]}"), ..numbers.Skip(2)]) ||
            HasSolution2(target, [numbers[0] * numbers[1], ..numbers.Skip(2)]) ||
            HasSolution2(target, [numbers[0] + numbers[1], ..numbers.Skip(2)]);
    }

    public long Part1()
    {
        return equations.Where(e => HasSolution1(e.Solution, e.Numbers)).Sum(e => e.Solution);
    }

    public long Part2()
    {
        return equations.Where(e => HasSolution2(e.Solution, e.Numbers)).Sum(e => e.Solution);
    }
}
