namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly string _input;
    private readonly string[] rows;
    private readonly int xdim;
    private readonly int ydim;
    private readonly bool[,] obstacles;
    private readonly (int,int) startPos;

    public Day06()
    {
        _input = File.ReadAllText(InputFilePath).Replace("\r","");
        rows   = _input.Split("\n");
        xdim   = rows[0].Length;
        ydim   = rows.Length;
        obstacles = new bool[xdim, ydim];

        // Find all obstacles
        for (int i = 0; i < rows.Length; i++) // y
        {
            for (int j = 0; j < rows[i].Length; j++) // x
            {
                if (rows[i][j] == '#') obstacles[j,i] = true;
                else if (rows[i][j] == '^') startPos = (j, i);
            }
        }
    }

    public override ValueTask<string> Solve_1()
    {
        bool[,] visited      = new bool[xdim, ydim];
        (int x, int y) pos   = startPos;
        (int x, int y) delta = (0,-1);
        int visits           = 1;

        try {
            while (true)
            {
                (int x, int y) next = (pos.x + delta.x, pos.y + delta.y);
                if (obstacles[next.x, next.y]) // Throws exception when it leaves the map
                {
                    // Turn right
                    delta = (delta.x == 0 ? delta.x - delta.y : 0,
                             delta.y == 0 ? delta.y + delta.x : 0);
                }
                else
                {
                    if (!visited[pos.x, pos.y])
                    {
                        visits++;
                        visited[pos.x, pos.y] = true;
                    }
                    pos = next;
                }
            }
        } catch (IndexOutOfRangeException) {} // Leaves the map

        return new(visits.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        // Brute force -> slooooooooow
        int loops = 0;
        for (int y = 0; y < ydim; y++)
        {
            for (int x = 0; x < xdim; x++)
            {
                if (obstacles[x,y]) continue;
                // Array with visited positions and directions
                List<(int,int)>[,] visited = new List<(int, int)>[xdim, ydim];
                (int x, int y) pos         = startPos;
                (int x, int y) delta       = (0,-1);

                try {
                    while (true)
                    {
                        (int x, int y) next = (pos.x + delta.x, pos.y + delta.y);
                        // Normal or added obstacle
                        if (obstacles[next.x, next.y] || (next.x == x && next.y == y))
                        {
                            // Turn right
                            delta = (delta.x == 0 ? delta.x - delta.y : 0,
                                     delta.y == 0 ? delta.y + delta.x : 0);
                        }
                        else
                        {
                            if (visited[pos.x, pos.y] == null)
                                visited[pos.x, pos.y] = [delta];
                            else if (visited[pos.x, pos.y].Contains(delta))
                                {loops++; break;} // Found loop
                            else 
                                visited[pos.x, pos.y].Add(delta);
                            pos = next;
                        }
                    }
                } catch (IndexOutOfRangeException) {} // Leaves the map, no loop
            }
        }

        return new(loops.ToString());
    }
}

