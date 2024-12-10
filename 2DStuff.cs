namespace AdventOfCode2024;

public enum Direction { Up = 0, Right = 1, Down = 2, Left = 3 }

public static class Directions {
    public static Direction[] AllStraight() => [ Direction.Up, Direction.Right, Direction.Down, Direction.Left ]; 
}

public record Delta(int X, int Y)
{
    public static Delta operator -(Delta d) => new(-d.X, -d.Y);
}

public record Location(int X, int Y)
{
    private static readonly Dictionary<Direction, Delta> DirectionDeltas = new()
    {
        [Direction.Up] = new Delta(0, -1),
        [Direction.Down] = new Delta(0, 1),
        [Direction.Left] = new Delta(-1, 0),
        [Direction.Right] = new Delta(1, 0)
    };

    public static Location operator +(Location l, Delta d) => new(l.X + d.X, l.Y + d.Y);
    public static Location operator -(Location l, Delta d) => new(l.X - d.X, l.Y - d.Y);
    public static Location operator +(Location l, Direction d) => l + DirectionDeltas[d];
    public static Delta operator -(Location l1, Location l2) => new(l1.X - l2.X, l1.Y - l2.Y);

    public bool Inside<T>(List<List<T>> map) => Y >= 0 && Y < map.Count && X >= 0 && X < map[Y].Count;
}

public record Mover(Location Location, Direction Direction);
