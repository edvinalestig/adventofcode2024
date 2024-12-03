using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string _input;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        Regex rx = new(@"mul\((\d+),(\d+)\)");
        MatchCollection matches = rx.Matches(_input);
        int sum = 0;
        foreach (Match m in matches) {
            sum += int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value);
        }
        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        Regex rx = new(@"mul\((\d+),(\d+)\)|don't\(\)|do\(\)");
        MatchCollection matches = rx.Matches(_input);
        int sum = 0;
        bool enabled = true;
        foreach (Match m in matches) {
            switch (m.Value) {
                case "don't()":
                    enabled = false;
                    break;
                case "do()":
                    enabled = true;
                    break;
                default:
                    if (enabled) sum += int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value);
                    break;
            }
        }
        return new(sum.ToString());
    }
}
