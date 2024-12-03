namespace AdventOfCode2024;

public interface IPuzzle
{
    void LoadInput(string inputPath);
    int Solve(string inputPath, int part) { LoadInput(inputPath); return SolvePart(part); }
    int SolvePart(int part) => part == 1 ? Part1() : Part2();
    int Part1() => int.MinValue;
    int Part2() => int.MinValue;
}
