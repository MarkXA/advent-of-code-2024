namespace AdventOfCode2024;

public partial class Day6 : IPuzzle
{
    public enum MapState { Empty, Obstacle, Visited };

    private List<List<MapState>> map = new();
    private Mover guard = null!;

    public void LoadInput(string inputPath)
    {
        foreach (var line in File.ReadAllLines(inputPath).RemoveEmpty().Index())
        {
            map.Add(line.Item.Select(c => c == '#' ? MapState.Obstacle : MapState.Empty).ToList());

            foreach (var c in line.Item.Index())
                if (c.Item == '^')
                    guard = new Mover(new Location(c.Index, line.Index), Direction.Up);
                else if (c.Item == 'v')
                    guard = new Mover(new Location(c.Index, line.Index), Direction.Down);
                else if (c.Item == '<')
                    guard = new Mover(new Location(c.Index, line.Index), Direction.Left);
                else if (c.Item == '>')
                    guard = new Mover(new Location(c.Index, line.Index), Direction.Right);
        }
    }

    public int AttemptEscape(Mover guard, Action<Location>? onVisit = null)
    {
        HashSet<Mover> visited = [];

        while (guard.Location.Inside(map))
        {
            if (!visited.Add(guard))
                return -1;

            var direction = guard.Direction;
            var next = guard.Location + direction;
            while (next.Inside(map) && map[next.Y][next.X] == MapState.Obstacle)
            {
                direction = direction.RotateRight();
                next = guard.Location + direction;
            }

            onVisit?.Invoke(next);

            guard = new Mover(next, direction);
        }

        return visited.Select(v => v.Location).Distinct().Count();
    }

    public long Part1()
    {
        return AttemptEscape(guard);
    }

    public long Part2()
    {
        var newObstacleLocations = new HashSet<Location>();

        var originalGuard = guard;

        AttemptEscape(guard, next => {
            if (next.Inside(map))
            {
                map[next.Y][next.X] = MapState.Obstacle;
                if (AttemptEscape(originalGuard) == -1)
                {
                    newObstacleLocations.Add(next);
                }
                map[next.Y][next.X] = MapState.Empty;
            }
        });

        return newObstacleLocations.Count;
    }
}
