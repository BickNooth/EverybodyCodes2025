namespace EverybodyCodes.Day3;

public static class Parts
{
    public static void Part1()
    {
        GetCrates(out var crates, "inputp1.txt");
        var orderedCrates = crates.Distinct().OrderBy(x => x).Reverse().ToArray();
        var crateSet = new HashSet<int>();
        foreach (var orderedCrate in orderedCrates)
        {
            if (crateSet.Count == 0)
            {
                crateSet.Add(orderedCrate);
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
        var orderedCrates = crates.Distinct().OrderBy(x => x).Reverse().ToArray();
    }
    public static void Part3() { }

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

        Console.WriteLine("Crates:");
        PrintCrates(crates);
    }

    private static void PrintCrates(int[] crates) => Console.WriteLine(string.Join(" > ", crates));
    private static void PrintCrates(HashSet<int> crates) => Console.WriteLine(string.Join(" > ", crates));
}