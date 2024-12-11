namespace AdventOfCode2024;

public partial class Day11 : IPuzzle
{
    record StoneToExpand(long StartValue, long Blinks);

    List<long> stones = [];
    readonly Dictionary<StoneToExpand, long> expandedStones = [];

    public void LoadInput(string inputPath)
    {
        stones = File.ReadAllLines(inputPath)[0].Split(' ').Select(long.Parse).ToList();
    }

    private long ExpandedLength(StoneToExpand stone)
    {
        if (stone.Blinks == 0) return 1;

        if (expandedStones.TryGetValue(stone, out var length)) return length;

        List<long> afterBlink;

        var stoneVal = stone.StartValue;
        var stoneString = stoneVal.ToString();
        if (stoneVal == 0) {
            afterBlink = [1];
        } else if (stoneString.Length % 2 == 0) {
            afterBlink = [long.Parse(stoneString[..(stoneString.Length / 2)]), long.Parse(stoneString[(stoneString.Length / 2)..])];
        } else {
            afterBlink = [stoneVal * 2024];
        }

        expandedStones[new StoneToExpand(stoneVal, 1)] = afterBlink.Count;

        var result = afterBlink.Select(val => ExpandedLength(new StoneToExpand(val, stone.Blinks - 1))).Sum();
        expandedStones[stone] = result;
        return result;
    }

    public long Part1()
    {
        return stones.Select(s => ExpandedLength(new StoneToExpand(s, 25))).Sum();
    }

    public long Part2()
    {
        return stones.Select(s => ExpandedLength(new StoneToExpand(s, 75))).Sum();
    }
}
