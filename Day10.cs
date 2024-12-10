namespace AdventOfCode2024;

public partial class Day10 : IPuzzle
{
    private record Node(int Height, IList<Node> NextSteps);

    private List<List<Node>> mapNodes = [];

    public void LoadInput(string inputPath)
    {
        var map = File.ReadAllLines(inputPath).RemoveEmpty()
            .Select(x => x.Select(y => y - '0').ToList()).ToList();

        mapNodes = map.Select(row => row.Select(col => new Node(col, [])).ToList()).ToList();
        foreach (var row in map.Index())
            foreach (var col in row.Item.Index())
            {
                var loc = new Location(col.Index, row.Index);
                foreach (var direction in Directions.AllStraight())
                {
                    var adjacent = loc + direction;
                    if (adjacent.Inside(map) && map[adjacent.Y][adjacent.X] == map[row.Index][col.Index] + 1)
                    {
                        mapNodes[row.Index][col.Index].NextSteps.Add(mapNodes[adjacent.Y][adjacent.X]);
                    }
                }
            }

    }

    private HashSet<Node> NodesAtOrAbove(Node n)
    {
        return n.NextSteps.SelectMany(NodesAtOrAbove).Append(n).ToHashSet();
    }


    private int BranchesAbove(Node n)
    {
        return n.NextSteps.Count == 0
            ? n.Height == 9 ? 0 : -1
            : n.NextSteps.Count - 1 + n.NextSteps.Sum(BranchesAbove);
    }

    public long Part1()
    {
        return mapNodes.SelectMany(x => x).Where(x => x.Height == 0).Sum(node => NodesAtOrAbove(node).Count(n => n.Height == 9));
    }

    public long Part2()
    {
        return mapNodes.SelectMany(x => x).Where(x => x.Height == 0).Sum(n => BranchesAbove(n) + 1);
    }
}
