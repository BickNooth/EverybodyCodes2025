namespace EverybodyCodes.Day3;

public static class Parts
{
    public static void Part1()
    {
        GetCrates(out var crates, "inputp1.txt");
        var orderedCrates = crates.Distinct().OrderBy(x => x).Reverse().ToArray();

        var crateSet = new List<int>();
        foreach (var orderedCrate in orderedCrates)
        {
            if (crateSet.Count == 0)
            {
                crateSet.Add(orderedCrate);
                continue;
            }

            var highestCrateIndex = crateSet.Count - 1;
            if (crateSet.ElementAt(highestCrateIndex) > orderedCrate)
            {
                crateSet.Add(orderedCrate);
            }
            PrintCrates(crateSet);
        }
        PrintCrates(crateSet);
        Console.WriteLine($"Sum of Crates: {crateSet.Sum()}");
    }

    public static void Part2()
    {
        GetCrates(out var crates, "inputp2.txt");
        var orderedCrates = crates.Distinct().OrderBy(x => x).ToArray();

        var crateStack = new Stack<int>();
        foreach (var orderedCrate in orderedCrates)
        {
            if (crateStack.Count == 20)
            {
                break;
            }
            if (crateStack.Count == 0)
            {
                crateStack.Push(orderedCrate);
                PrintCrates(crateStack);
            }

            if (orderedCrate <= crateStack.Peek()) continue;
            
            crateStack.Push(orderedCrate);
            PrintCrates(crateStack);
        }

        PrintCrates(crateStack);
        Console.WriteLine($"Sum of Crates: {crateStack.Sum()}");
    }

    public static void Part3()
    {
        GetCrates(out var crates, "inputp3.txt");
        var orderedCrates = crates.OrderBy(x => x).ToArray();
        PrintCrates(orderedCrates);

        var crateStacks = new List<Stack<int>>();
        foreach (var orderedCrate in orderedCrates)
        {
            if (crateStacks.Count == 0)
            {
                NewCrateStack(orderedCrate);
                continue;
            }

            var addedCrate = false;
            foreach (var crateStack in crateStacks)
            {
                if (crateStack.Peek() < orderedCrate)
                {
                    crateStack.Push(orderedCrate);
                    addedCrate = true;
                    break;
                }
            }
            if (!addedCrate)
            {
                NewCrateStack(orderedCrate);
            }
        }
        PrintCrates(crateStacks);
        Console.WriteLine($"Number of stacks: {crateStacks.Count}");
        return;

        void NewCrateStack(int orderedCrate)
        {
            var crateStack = new Stack<int>();
            crateStack.Push(orderedCrate);
            crateStacks.Add(crateStack);
        }
    }

    private static void GetCrates(out int[] crates, string inputLocation)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Day3", inputLocation);
        var lines = File.ReadAllLines(path)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        const StringSplitOptions splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
        crates = lines[0]
            .Split(',', splitOptions)
            .Select(int.Parse)
            .ToArray();
    }

    private static void PrintCrates(int[] crates) => Console.WriteLine(string.Join(" > ", crates));
    private static void PrintCrates(List<int> crates) => Console.WriteLine(string.Join(" > ", crates));
    private static void PrintCrates(Stack<int> crates) => Console.WriteLine(string.Join(" > ", crates));
    private static void PrintCrates(List<Stack<int>> crateStacks)
    {
        for (var i = 0; i < crateStacks.Count; i++)
        {
            var crateStack = crateStacks[i];
            Console.WriteLine($"Stack {i + 1}: {string.Join(" > ", crateStack)}");
        }
    }
}