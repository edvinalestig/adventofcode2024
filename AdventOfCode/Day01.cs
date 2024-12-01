namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string _input;

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        List<int> leftList = [];
        List<int> rightList = [];
        foreach (string line in _input.Split("\n", StringSplitOptions.RemoveEmptyEntries))
        {
            // Parse into lists
            string[] splat = line.Split("   ");
            leftList.Add(int.Parse(splat[0]));
            rightList.Add(int.Parse(splat[1]));
        }

        leftList.Sort();
        rightList.Sort();
        int distance = 0;

        for (int i = 0; i < leftList.Count; i++)
        {
            distance += Math.Abs(leftList[i] - rightList[i]);
        }

        return new(distance.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        List<int> leftList = [];
        List<int> rightList = [];
        foreach (string line in _input.Split("\n", StringSplitOptions.RemoveEmptyEntries))
        {
            string[] splat = line.Split("   ");
            leftList.Add(int.Parse(splat[0]));
            rightList.Add(int.Parse(splat[1]));
        }

        // Get dictionary of number in right list: occurences in right list 
        Dictionary<int, int> occurences = rightList.GroupBy(i => i).ToDictionary(g => g.Key, g => g.Count());
        // Iterate over left list and sum number in left list * occurences in right list
        int score = leftList.Aggregate(0, (total, left) => total + left * occurences.GetValueOrDefault(left, 0));

        return new(score.ToString());
    }
}
