using System.Numerics;

namespace AdventOfCode;

public class Day08 : BaseDay
{
    private readonly string[] _input;
    private readonly int xdim;
    private readonly int ydim;
    private readonly Dictionary<char,List<(int x,int y)>> antennas;


    public Day08()
    {
        _input = File.ReadAllText(InputFilePath).Replace("\r","")
                .Split("\n", StringSplitOptions.RemoveEmptyEntries);
        ydim   = _input.Length;
        xdim   = _input[0].Length;

        // Fill dictionary with antenna coordinates
        antennas = [];
        for (int y = 0; y < ydim; y++)
        {
            for (int x = 0; x < xdim; x++)
            {
                char c = _input[y][x];
                if (c == '.') continue;
                if (antennas.ContainsKey(c)) antennas[c].Add((x,y));
                else antennas.Add(c, [(x,y)]);
            }
        }
    }

    public override ValueTask<string> Solve_1()
    {
        List<(int,int)> antinodes = [];
        foreach (List<(int x,int y)> ants in antennas.Values)
        {
            for (int i = 0; i < ants.Count; i++)
            {
                for (int j = i+1; j < ants.Count; j++)
                {
                    int dx = ants[i].x - ants[j].x;
                    int dy = ants[i].y - ants[j].y;
                    (int x,int y) c1 = (ants[i].x + dx, ants[i].y + dy);
                    (int x,int y) c2 = (ants[j].x - dx, ants[j].y - dy);

                    if (c1.x >= 0 && c1.x < xdim && c1.y >= 0 && c1.y < ydim)
                        antinodes.Add(c1);
                    if (c2.x >= 0 && c2.x < xdim && c2.y >= 0 && c2.y < ydim)
                        antinodes.Add(c2);
                }
            }
        }
        return new(antinodes.Distinct().Count().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        List<(int,int)> antinodes = [];
        foreach (List<(int x,int y)> ants in antennas.Values)
        {
            for (int i = 0; i < ants.Count; i++)
            {
                for (int j = i+1; j < ants.Count; j++)
                {
                    int dx = ants[i].x - ants[j].x;
                    int dy = ants[i].y - ants[j].y;
                    // Get all coords on the line by dividing by gcd
                    int gcd = (int) BigInteger.GreatestCommonDivisor(dx, dy);
                    dx /= gcd;
                    dy /= gcd;

                    int ii = 0;
                    while (true)
                    {
                        (int x,int y) c = (ants[i].x + ii*dx, ants[i].y + ii*dy);
                        if (c.x >= 0 && c.x < xdim && c.y >= 0 && c.y < ydim)
                            antinodes.Add(c);
                        else break;
                        ii++;
                    }
                    ii = -1;
                    while (true)
                    {
                        (int x,int y) c = (ants[i].x + ii*dx, ants[i].y + ii*dy);
                        if (c.x >= 0 && c.x < xdim && c.y >= 0 && c.y < ydim)
                            antinodes.Add(c);
                        else break;
                        ii--;
                    }
                }
            }
        }
        return new(antinodes.Distinct().Count().ToString());
    }
}

