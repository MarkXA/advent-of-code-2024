namespace AdventOfCode2024;

public static class ExtensionMethods
{
    public static string[] RemoveEmpty(this IEnumerable<string> strings) =>
        strings.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

    public static Direction RotateLeft(this Direction direction) =>
        (Direction)(((int)direction - 1) % 4);
        
    public static Direction RotateRight(this Direction direction) =>
        (Direction)(((int)direction + 1) % 4);
}
