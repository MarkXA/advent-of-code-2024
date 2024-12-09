namespace AdventOfCode2024;

public partial class Day9 : IPuzzle
{
    private const int empty = -1;
    private List<(int FileNum, int Len)> map = [];

    public void LoadInput(string inputPath)
    {
        map = File.ReadAllText(inputPath).Trim()
            .Index()
            .Select(entry => (
                FileNum: entry.Index % 2 == 0 ? entry.Index / 2 : empty,
                Len: entry.Item - '0'))
            .ToList();
    }

    private List<int> MakeDisk(IEnumerable<(int FileNum, int Len)> map)
    {
        return map
            .Select(entry => Enumerable.Repeat(entry.FileNum, entry.Len))
            .SelectMany(s => s)
            .ToList();
    }

    public long Part1()
    {
        var disk = MakeDisk(map);

        var index1 = 0;
        var index2 = disk.Count - 1;
        while (true)
        {
            while (disk[index1] != empty) index1++;
            while (disk[index2] == empty) index2--;
            if (index1 >= index2) break;
            disk[index1] = disk[index2];
            disk[index2] = empty;
        }

        return disk.Index().Where(i => i.Item != empty).Sum(i => (long)i.Index * i.Item);
    }

    public long Part2()
    {
        for (var fileNum = map.Last(m => m.FileNum != empty).FileNum; fileNum > 0; fileNum--)
        {
            var (index, fileToMove) = map.Index().First(m => m.Item.FileNum == fileNum);

            if (fileToMove.FileNum == empty) continue;

            var newIndex = map.Index().FirstOrDefault(i => i.Index < index && i.Item.FileNum == empty && i.Item.Len >= map[index].Len).Index;
            if (newIndex > 0)
            {
                map.RemoveAt(index);
                map.Insert(index, new(empty, fileToMove.Len));
                if (map[newIndex].Len > fileToMove.Len)
                {
                    map[newIndex] = (map[newIndex].FileNum, map[newIndex].Len - fileToMove.Len);
                }
                else
                {
                    map.RemoveAt(newIndex);
                }
                map.Insert(newIndex, fileToMove);
            }
        }

        return MakeDisk(map).Index().Where(i => i.Item != empty).Sum(i => (long)i.Index * i.Item);
    }
}
