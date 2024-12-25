namespace AdventOfCode;

public class Day11 : BaseDay
{
    private readonly string _input;
    private readonly string[] stones;
    private Dictionary<(string,int),long> results; // Cached intermediate results


    public Day11()
    {
        _input = File.ReadAllText(InputFilePath).Trim();
        results = [];
        stones = _input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    }

    long Recurse(string stone, int n) 
    {
        // Cache intermediate results
        if (results.ContainsKey((stone,n))) return results[(stone,n)];
        if (n == 0)       return 1;
        if (stone == "0") return Recurse("1", n-1);
        
        long res;
        if (stone == "1") res = Recurse("2024", n-1);
        else if (stone.Length % 2 == 0)
             res = Recurse(stone[..(stone.Length/2)], n-1) +
                   Recurse(long.Parse(stone[(stone.Length/2)..]).ToString(), n-1);
        else res = Recurse((long.Parse(stone) * 2024).ToString(), n-1);
        results.Add((stone,n), res);
        return res;
    }

    public override ValueTask<string> Solve_1()
    {
        long result = stones.Select(s => Recurse(s, 25)).Sum();
        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        long result = stones.Select(s => Recurse(s, 75)).Sum();
        return new(result.ToString());
    }
}

