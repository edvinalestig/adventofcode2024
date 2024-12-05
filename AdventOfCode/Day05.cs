using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly string _input;
    private readonly string rules;
    private readonly string[] updates;
    private List<List<string>> incorrect;

    public Day05()
    {
        _input    = File.ReadAllText(InputFilePath).Replace("\r","");
        rules     = _input.Split("\n\n")[0];
        updates   = _input.Split("\n\n")[1].Split("\n", StringSplitOptions.RemoveEmptyEntries);
        incorrect = [];
    }

    public override ValueTask<string> Solve_1()
    {
        int middleNumbers = 0;
        foreach (string update in updates)
        {
            bool valid = true;
            string[] pages = update.Split(",");
            for (int i = 0; i < pages.Length; i++)
            {
                // Ensure all following pages follow it in the rules
                for (int j = i+1; j < pages.Length; j++)
                {
                    Regex rx  = new($"{pages[i]}\\|{pages[j]}");
                    if (!rx.IsMatch(rules))
                    {
                        valid = false;
                        break;
                    }
                }
                if (!valid) break;
            }
            if (valid) middleNumbers += int.Parse(pages[pages.Length/2]);
            else incorrect.Add([.. pages]); // For use in part 2
        }
        return new(middleNumbers.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        if (incorrect.Count == 0) throw new ArgumentException("Part 1 must be completed first");

        int middleNumbers = 0;
        PageComparer comp = new(rules);
        foreach (List<string> update in incorrect)
        {
            // Sort using a custom comparer
            update.Sort(comp);
            middleNumbers += int.Parse(update[update.Count/2]);
        }
        return new(middleNumbers.ToString());
    }

    private class PageComparer(string rules) : Comparer<string>
    {
        public override int Compare(string x, string y)
        {
            Regex rx  = new($"{x}\\|{y}");
            if (rx.IsMatch(rules)) return -1; // Correct order
            return 1; // Incorrect order
        }
    }
}

