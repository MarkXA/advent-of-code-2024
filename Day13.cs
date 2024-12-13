using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public partial class Day13 : IPuzzle
{
    private readonly Regex matchInt = new(@"\d+");

    record Equation(long XA, long YA, long XB, long YB, long XT, long YT);

    IEnumerable<Equation> equations = [];

    public void LoadInput(string inputPath)
    {
        var input = File.ReadAllLines(inputPath);

        equations = input.Chunk(4).Select(lines =>
        {
            var xys = lines.Select(l => matchInt.Matches(l).Select(m => long.Parse(m.Value))).SelectMany(x => x).ToArray();
            return new Equation(xys[0], xys[1], xys[2], xys[3], xys[4], xys[5]);
        });
    }

    private (long a, long b) Solve(Equation eq)
    {
        var aNum = eq.YT * eq.XB - eq.XT * eq.YB;
        var aDen = eq.XB * eq.YA - eq.XA * eq.YB;
        var bNum = eq.YT * eq.XA - eq.XT * eq.YA;
        var bDen = eq.XA * eq.YB - eq.XB * eq.YA;

        if ((aNum % aDen != 0) || (bNum % bDen != 0)) return (0, 0);
        return (aNum / aDen, bNum / bDen);
    }

    public long Part1()
    {
        return equations.Select(Solve).Sum(s => s.a * 3 + s.b);
    }

    public long Part2()
    {
        equations = equations.Select(eq => new Equation(eq.XA, eq.YA, eq.XB, eq.YB, eq.XT + 10000000000000, eq.YT + 10000000000000)).ToList();
        return equations.Select(Solve).Sum(s => s.a * 3 + s.b);
    }
}
