namespace AdventOfCode;

public class Day07 : BaseDay
{
    private readonly string[] _input;


    public Day07()
    {
        _input = File.ReadAllText(InputFilePath).Replace("\r","")
                .Split("\n", StringSplitOptions.RemoveEmptyEntries);
    }

    public override ValueTask<string> Solve_1()
    {
        long solvableSum = 0;
        foreach (string s in _input)
        {
            string[] tmp = s.Split(": ");
            long target = long.Parse(tmp[0]);
            long[] numbers = tmp[1].Split(" ").Select(long.Parse).ToArray();
            if (HitsTarget(target, numbers[0], numbers[1..])) solvableSum += target;
        }
        return new(solvableSum.ToString());
    }

    private static bool HitsTarget(long target, long acc, long[] numbers)
    {
        if (acc > target) return false;
        if (numbers.Length == 0 && acc == target) return true;
        if (numbers.Length == 0) return false;
        return HitsTarget(target, acc + numbers[0], numbers[1..])
            || HitsTarget(target, acc * numbers[0], numbers[1..]);
    }

    public override ValueTask<string> Solve_2()
    {
        long solvableSum = 0;
        foreach (string s in _input)
        {
            string[] tmp = s.Split(": ");
            long target = long.Parse(tmp[0]);
            long[] numbers = tmp[1].Split(" ").Select(long.Parse).ToArray();
            if (HitsTarget2(target, numbers[0], numbers[1..])) solvableSum += target;
        }
        return new(solvableSum.ToString());
    }

    private static bool HitsTarget2(long target, long acc, long[] numbers)
    {
        if (acc > target) return false;
        if (numbers.Length == 0 && acc == target) return true;
        if (numbers.Length == 0) return false;
        return HitsTarget2(target, acc + numbers[0]                , numbers[1..])
            || HitsTarget2(target, acc * numbers[0]                , numbers[1..])
            || HitsTarget2(target, long.Parse($"{acc}{numbers[0]}"), numbers[1..]);
    }
}

