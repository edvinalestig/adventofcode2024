using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly string _input;

    public Day04()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        Regex rx_hor  = new(@"XMA(?=S)|SAM(?=X)");
        // length in regex is dependent of OS. Windows uses \r\n instead of \n which adds 1. 
        int l         = ((int) Math.Sqrt(_input.Length)) + 1;
        string vert   = $$"""X(?=.{{{l}}}M.{{{l}}}A.{{{l}}}S)|S(?=.{{{l}}}A.{{{l}}}M.{{{l}}}X)""";
        Regex rx_vert = new(vert, RegexOptions.Singleline);
        string dia1   = $$"""X(?=.{{{l-1}}}M.{{{l-1}}}A.{{{l-1}}}S)|S(?=.{{{l-1}}}A.{{{l-1}}}M.{{{l-1}}}X)""";
        Regex rx_dia1 = new(dia1, RegexOptions.Singleline);
        string dia2   = $$"""X(?=.{{{l+1}}}M.{{{l+1}}}A.{{{l+1}}}S)|S(?=.{{{l+1}}}A.{{{l+1}}}M.{{{l+1}}}X)""";
        Regex rx_dia2 = new(dia2, RegexOptions.Singleline);

        int matches   = rx_hor.Matches(_input).Count;
        matches      += rx_vert.Matches(_input).Count;
        matches      += rx_dia1.Matches(_input).Count;
        matches      += rx_dia2.Matches(_input).Count;
        return new(matches.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int l          = ((int) Math.Sqrt(_input.Length)) + 1; // +1 because windows
        string pattern =
            $$"""(?<=M.{{{l-1}}})(?<=M.{{{l+1}}})A(?=.{{{l-1}}}S)(?=.{{{l+1}}}S)|""" +
            $$"""(?<=S.{{{l-1}}})(?<=S.{{{l+1}}})A(?=.{{{l-1}}}M)(?=.{{{l+1}}}M)|""" +
            $$"""(?<=M.{{{l-1}}})(?<=S.{{{l+1}}})A(?=.{{{l-1}}}S)(?=.{{{l+1}}}M)|""" +
            $$"""(?<=S.{{{l-1}}})(?<=M.{{{l+1}}})A(?=.{{{l-1}}}M)(?=.{{{l+1}}}S)""";
        int matches    = new Regex(pattern, RegexOptions.Singleline).Matches(_input).Count;
        return new(matches.ToString());
    }
}
