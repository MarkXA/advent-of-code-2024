namespace AdventOfCode2024;

public static class ExtensionMethods
{
    public static string[] RemoveEmpty(this IEnumerable<string> strings) =>
        strings.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
}
