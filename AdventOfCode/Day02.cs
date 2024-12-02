namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string _input;

    public Day02()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    private static bool Increasing(List<int> levels) {
        if (levels.Count < 2) return true;
        return levels[0] < levels[1] && Math.Abs(levels[0]-levels[1]) <= 3 && Increasing(levels[1..]);
    }

    public override ValueTask<string> Solve_1()
    {
        int numberSafe = 0;
        foreach (string line in _input.Split("\n", StringSplitOptions.RemoveEmptyEntries))
        {
            List<int> levels = (from level in line.Split(" ") select int.Parse(level)).ToList();
            if (Increasing(levels) || Increasing(levels.Reverse<int>().ToList())) numberSafe++;
        }
        return new(numberSafe.ToString());
    }

    private static bool Increasing2(List<int> levels) {
        bool skipped = false;
        for (int i = 0; i < levels.Count-1; i++) //  Loop until penultimate element
        {
            // OK
            if (levels[i] < levels[i+1] && Math.Abs(levels[i]-levels[i+1]) <= 3) continue;
            // Second fault in report
            else if (skipped) return false;
            // Last element faulty in report
            else if (i >= levels.Count - 2) return true;
            // Middle element faulty, works if next element is skipped
            else if (levels[i] < levels[i+2] && Math.Abs(levels[i]-levels[i+2]) <= 3) { skipped = true; i++; }
            // Middle element faulty, works if current element is skipped
            else if (i >= 1 && levels[i-1] < levels[i+1] && Math.Abs(levels[i-1]-levels[i+1]) <= 3) skipped = true;
            // First element faulty in report
            else if (i == 0) skipped = true;
            else return false;
        }
        return true;
    }

    public override ValueTask<string> Solve_2()
    {
        int numberSafe = 0;
        foreach (string line in _input.Split("\n", StringSplitOptions.RemoveEmptyEntries))
        {
            List<int> levels = (from level in line.Split(" ") select int.Parse(level)).ToList();
            if (Increasing2(levels) || Increasing2(levels.Reverse<int>().ToList())) numberSafe++;
        }
        return new(numberSafe.ToString());
    }
}
