namespace EverybodyCodes.Day8;

public static class Parts
{
    public static void Part1()
    {
        const int nailCount = 32;
        GetNamesAndRules(out var nailNumbers, "inputp1.txt");
        var currentNail = nailNumbers[0];
        var centrePasses = 0;
        foreach (var nailNumber in nailNumbers)
        {
            if (nailNumber == currentNail) continue;
            
            var nailOpposite = (currentNail + nailCount / 2) % nailCount;
            if (nailOpposite == 0) nailOpposite = nailCount;
            
            if (nailNumber == nailOpposite) centrePasses++;
            currentNail = nailNumber;
        }

        Console.WriteLine(centrePasses);
    }

    public static void Part2()
    {
    }

    public static void Part3()
    {
    }
    
    private static int NailPosition(int value, int nailCount, int delta)
    {
        var wrapped = (value + delta) % nailCount;
        if (wrapped < 1) wrapped += nailCount; 
        return wrapped;
    }

    private static void GetNamesAndRules(out int[] nailNumbers, string inputLocation)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Day8", inputLocation);
        nailNumbers = File.ReadLines(path).First().Split(',').Select(int.Parse).ToArray();
    }
}