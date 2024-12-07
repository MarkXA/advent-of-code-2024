namespace AdventOfCode2024;

public interface IPuzzle
{
    void LoadInput(string inputPath);
    long Solve(string inputPath, int part) { LoadInput(inputPath); return SolvePart(part); }
    long SolvePart(int part) => part == 1 ? Part1() : Part2();
    long Part1() => long.MinValue;
    long Part2() => long.MinValue;
}
