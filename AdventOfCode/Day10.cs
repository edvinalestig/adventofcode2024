namespace AdventOfCode;

public class Day10 : BaseDay
{
    private readonly string _input;
    private readonly string[] rows;
    private readonly int[,] map;
    private readonly List<(int x,int y)> zeros;



    public Day10()
    {
        _input   = File.ReadAllText(InputFilePath).Replace("\r","");
        rows     = _input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        int xdim = rows[0].Length;
        int ydim = rows.Length;
        map      = new int[xdim, ydim];
        zeros    = [];
        for (int y = 0; y < ydim; y++)
        {
            for (int x = 0; x < xdim; x++)
            {
                map[x,y] = rows[y][x] - 48; // Ascii to normal ints
                if (map[x,y] == 0) zeros.Add((x,y));
            }
        }

    }

    (int,int)[] FindTrailHeads(int x, int y) 
    {
        int height = map[x,y];
        if (height == 9) return [(x,y)];

        List<(int,int)> nexts = [];
        try {if (map[x-1,y] == height+1) nexts.Add((x-1,y));} catch (IndexOutOfRangeException) {}
        try {if (map[x+1,y] == height+1) nexts.Add((x+1,y));} catch (IndexOutOfRangeException) {}
        try {if (map[x,y+1] == height+1) nexts.Add((x,y+1));} catch (IndexOutOfRangeException) {}
        try {if (map[x,y-1] == height+1) nexts.Add((x,y-1));} catch (IndexOutOfRangeException) {}

        return nexts.Select(pos => FindTrailHeads(pos.Item1, pos.Item2)).SelectMany(x => x).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        int result = 0;
        foreach ((int x,int y) in zeros)
        {
            result += FindTrailHeads(x, y).Distinct().Count();
        }

        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int result = 0;
        foreach ((int x,int y) in zeros)
        {
            result += FindTrailHeads(x, y).Length;
        }

        return new(result.ToString());
    }
}

