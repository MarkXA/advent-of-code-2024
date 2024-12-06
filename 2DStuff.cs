namespace AdventOfCode2024;

public enum Direction { Up = 0, Right = 1, Down = 2, Left = 3 }
public record Movement(int X, int Y);
public record Location(int X, int Y)
{
    private static readonly Dictionary<Direction, Movement> Movements = new()
    {
        [Direction.Up] = new Movement(0, -1),
        [Direction.Down] = new Movement(0, 1),
        [Direction.Left] = new Movement(-1, 0),
        [Direction.Right] = new Movement(1, 0)
    };

    public static Location operator +(Location l, Movement m) => new Location(l.X + m.X, l.Y + m.Y);
    public static Location operator +(Location l, Direction d) => l + Movements[d];
    public bool Inside<T>(List<List<T>> map) => Y >= 0 && Y < map.Count && X >= 0 && X < map[Y].Count;
}

public record Mover(Location Location, Direction Direction);
