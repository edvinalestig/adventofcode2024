namespace AdventOfCode;

public class Day09 : BaseDay
{
    private readonly string _input;


    public Day09()
    {
        _input = File.ReadAllText(InputFilePath).Replace("\r","").Replace("\n", "");

    }

    public override ValueTask<string> Solve_1()
    {
        // Create array with ids/empty spaces
        int length = 0;
        foreach (char c in _input)
        {
            length += c-48;
        }
        int[] arr = new int[length];

        int id = 0;
        int i = 0;
        int j = 0;
        bool empty = false;
        foreach (char c in _input)
        {
            while (j < i + c - 48)
            {
                if (empty) arr[j] = -1;
                else arr[j] = id;
                j++;
            }
            i = j;

            if (!empty) id++;
            empty = !empty;
        }

        // Compress
        int first = 0;
        int last = length-1;
        while (first <= last)
        {
            if (arr[first] != -1) { first++; continue; }
            if (arr[last]  == -1) { last--;  continue; }

            arr[first] = arr[last];
            arr[last]  = -1;
            first++;
            last--;
        }

        // Calculate result
        long result = 0;
        for (int k = 0; k < arr.Length; k++)
        {
            if (arr[k] == -1) break;
            result += arr[k] * k;
        }

        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        // Create array with ids/empty spaces
        int length = 0;
        foreach (char c in _input)
        {
            length += c-48;
        }
        int[] arr = new int[length];
        Dictionary<int,int> lengths = []; // Block lengths of each id

        int id = 0;
        int i = 0;
        int j = 0;
        bool empty = false;
        foreach (char c in _input)
        {
            while (j < i + c - 48)
            {
                if (empty) arr[j] = -1;
                else arr[j] = id;
                j++;
            }

            if (!empty)
            {
                lengths.Add(id, c-48);
                id++;
            }
            i = j;
            empty = !empty;
        }

        // Compress
        int last = length-1;
        while (last > 0)
        {
            if (arr[last]  == -1) { last--;  continue; }

            int id1 = arr[last];
            int len = lengths[id1];

            int ix = 0;
            while (ix < last)
            {
                if (arr[ix] == -1) {
                    // Found an empty block, check if long enough
                    bool allEmpty = arr.Take(new Range(ix, ix+len)).All(i => i == -1);
                    if (allEmpty)
                    {
                        for (int k = 0; k < len; k++)
                        {
                            arr[ix+k]   = id1;
                            arr[last-k] = -1;
                        }
                        break;
                    }
                }
                ix++;
            }
            last -= len;
        }

        // Calculate result
        long result = 0;
        for (int k = 0; k < arr.Length; k++)
        {
            if (arr[k] == -1) continue;
            result += arr[k] * k;
        }

        return new(result.ToString());
    }
}

