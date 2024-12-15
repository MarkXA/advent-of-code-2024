using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public partial class Day14 : IPuzzle
{
    private readonly Regex matchInt = new(@"-?\d+");

    record Robot(int X, int Y, int VX, int VY);

    int width, height;
    List<Robot> robots = [];

    public void LoadInput(string inputPath)
    {
        var test = inputPath.Contains("test");

        width = test ? 11 : 101;
        height = test ? 7 : 103;

        robots = File.ReadAllLines(inputPath).RemoveEmpty().Select(l =>
        {
            var ints = matchInt.Matches(l).Select(m => int.Parse(m.Value)).ToArray();
            return new Robot(ints[0], ints[1], ints[2], ints[3]);
        }).ToList();
    }

    int FinalPos(int pos, int vel, int wrap, int iterations)
    {
        var n = (pos + vel * iterations) % wrap;
        return n < 0 ? n + wrap : n;
    }

    public long Part1()
    {
        var finalRobots = robots.Select(r => (X: FinalPos(r.X, r.VX, width, 100), Y: FinalPos(r.Y, r.VY, height, 100))).ToList();
        var xMid = width / 2;
        var yMid = height / 2;
        var quadrants = finalRobots.Where(r => r.X != xMid && r.Y != yMid).GroupBy(r => (r.X < xMid, r.Y < yMid));

        return quadrants.Aggregate(1L, (acc, q) => acc * q.Count());
    }

    public long Part2()
    {
        for (var n = 1;; n++)
        {
            robots = robots.Select(r => new Robot(FinalPos(r.X, r.VX, width, 1), Y: FinalPos(r.Y, r.VY, height, 1), r.VX, r.VY)).ToList();
            if (!robots.GroupBy(r => r.X).Any(g => g.Count() >= 15) || !robots.GroupBy(r => r.Y).Any(g => g.Count() >= 15)) continue;

            //TODO: AI Christmas tree recognition
            
            var room = Enumerable.Range(0, height).Select(y => Enumerable.Range(0, width).Select(x => '.').ToArray()).ToArray();
            foreach (var robot in robots) room[robot.Y][robot.X] = '#';
            foreach (var row in room) Console.WriteLine(new string(row));

            Console.WriteLine(n);
            Console.ReadLine();
        }
    }
}
