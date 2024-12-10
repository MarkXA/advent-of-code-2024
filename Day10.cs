namespace AdventOfCode2024;

public partial class Day10 : IPuzzle
{
    private record Node(int Height, List<Node> NextSteps);

    private List<Node> nodes = [];

    public void LoadInput(string inputPath)
    {
        var map = File.ReadAllLines(inputPath).RemoveEmpty()
            .Select(row => row.Select(col => new Node(col - '0', [])).ToList())
            .ToList();

        foreach (var row in map.Index())
            foreach (var col in row.Item.Index())
            {
                var node = col.Item;
                var loc = new Location(col.Index, row.Index);
                node.NextSteps.AddRange(Directions.AllStraight()
                    .Select(dir => loc + dir)
                    .Where(l => l.Inside(map))
                    .Select(l => map[l.Y][l.X])
                    .Where(n => n.Height == node.Height + 1));
            }

        nodes = map.SelectMany(x => x).ToList();
    }

    private IEnumerable<Node> NodesAtOrAbove(Node n)
    {
        return n.NextSteps.SelectMany(NodesAtOrAbove).Append(n);
    }

    public long Part1()
    {
        return nodes.Where(x => x.Height == 0).Sum(node => NodesAtOrAbove(node).Where(n => n.Height == 9).Distinct().Count());
    }

    public long Part2()
    {
        return nodes.Where(x => x.Height == 0).Sum(node => NodesAtOrAbove(node).Where(n => n.Height == 9).Count());
    }
}
