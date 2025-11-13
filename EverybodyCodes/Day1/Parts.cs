namespace EverybodyCodes.Day1;

public class NameIndex
{
    public int MaxIndex { get; set; }

    public int CurrentIndex
    {
        get;
        set
        {
            if (value < 0)
                field = 0;
            else if (value > MaxIndex)
                field = MaxIndex;
            else
                field = value;
        }
    }
}

public class NameCircularIndex
{
    public int MaxIndex { get; set; }

    public int CurrentIndex { get; set; }

    public void SetCurrentIndex(bool left, int amount)
    {
        if (left)
        {
            for (var i = 0; i < amount; i++)
            {
                CurrentIndex -= 1;
                if (CurrentIndex < 0)
                    CurrentIndex = MaxIndex;
            }
        }
        else
        {
            for (var i = 0; i < amount; i++)
            {
                CurrentIndex += 1;
                if (CurrentIndex > MaxIndex)
                    CurrentIndex = 0;
            }
        }
    }

    public void ResetCurrentIndex()
    {
        CurrentIndex = 0;
    }
}


public static class Parts
{
    public static void Part1()
    {
        GetNamesAndInstructions(out var names, out var instructions, "inputp1.txt");
        var name = ProcessInstructions(names, instructions);

        Console.WriteLine(name);
    }

    public static void Part2()
    {
        GetNamesAndInstructions(out var names, out var instructions, "inputp2.txt");
        var name = ProcessInstructionsCircular(names, instructions);

        Console.WriteLine(name);
    }

    public static void Part3()
    {
        GetNamesAndInstructions(out var names, out var instructions, "inputp3.txt");
        var name = ProcessInstructionsCircularSwap(names, instructions);

        Console.WriteLine(name);
    }

    private static string ProcessInstructions(string[] names, string[] instructions)
    {
        var currentNameIndex = new NameIndex { CurrentIndex = 0, MaxIndex = names.Length - 1 };
        foreach (var instruction in instructions)
        {
            var turn = instruction[0];
            var steps = int.Parse(instruction.AsSpan(1));
            
            currentNameIndex.CurrentIndex += turn == 'R' ? steps : -steps;
        }

        return names[currentNameIndex.CurrentIndex];
    }

    private static string ProcessInstructionsCircular(string[] names, string[] instructions)
    {
        var currentNameIndex = new NameCircularIndex() { CurrentIndex = 0, MaxIndex = names.Length - 1 };
        foreach (var instruction in instructions)
        {
            var turn = instruction[0];
            var steps = int.Parse(instruction.AsSpan(1));

            currentNameIndex.SetCurrentIndex(turn == 'L', steps);
        }

        return names[currentNameIndex.CurrentIndex];
    }

    private static string ProcessInstructionsCircularSwap(string[] names, string[] instructions)
    {
        var currentNameIndex = new NameCircularIndex() { CurrentIndex = 0, MaxIndex = names.Length - 1 };
        foreach (var instruction in instructions)
        {
            Console.WriteLine(string.Join(" | ", names.Select((n, i) => $"{i}:{n}")));
            Console.WriteLine($"Processing {instruction}");
            var turn = instruction[0];
            var steps = int.Parse(instruction.AsSpan(1));

            currentNameIndex.SetCurrentIndex(turn == 'L', steps);
            Console.WriteLine($"Swap {names[0]} with {names[currentNameIndex.CurrentIndex]}");
            (names[0], names[currentNameIndex.CurrentIndex]) = (names[currentNameIndex.CurrentIndex], names[0]);
            currentNameIndex.ResetCurrentIndex();
        }

        return names[currentNameIndex.CurrentIndex];
    }

    private static void GetNamesAndInstructions(out string[] names, out string[] instructions, string inputLocation)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Day1", inputLocation);
        var lines = File.ReadAllLines(path)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        var splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
        names = lines[0].Split(',', splitOptions);
        instructions = lines[1].Split(',', splitOptions);

        Console.WriteLine("Names:");
        Console.WriteLine(string.Join(" | ", names));
        Console.WriteLine("Instructions:");
        Console.WriteLine(string.Join(" , ", instructions));
    }
}