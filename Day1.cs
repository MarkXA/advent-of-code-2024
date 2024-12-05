namespace AdventOfCode2024;

public class Day1 : IPuzzle
{
    private IEnumerable<int> list1 = null!;
    private IEnumerable<int> list2 = null!;

    public void LoadInput(string inputPath)
    {
        var numbers =
            File.ReadAllLines(inputPath).RemoveEmpty()
            .Select(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());

        list1 = numbers.Select(n => n[0]).OrderBy(n => n);
        list2 = numbers.Select(n => n[1]).OrderBy(n => n);
    }

    public int Part1()
    {
        return list1
            .Zip(list2, (n1, n2) => Math.Abs(n1 - n2))
            .Sum();
    }

    public int Part2()
    {
        return list1
            .Select(n1 => n1 * list2.Count(n2 => n2 == n1))
            .Sum();
    }
}
