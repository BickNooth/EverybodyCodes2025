using System.Numerics;

namespace EverybodyCodes.Day4;

public static class Parts
{
    public static void Part1()
    {
        GetGears(out var gears, "inputp1.txt");
        var firstTurns = 2025;

        var gear1 = (decimal)gears[0];
        var gear2 = (decimal)gears[^1];

        var ratio = gear1 / gear2;
        var wholeTurns = (int)Math.Floor(ratio * firstTurns);

        Console.WriteLine(wholeTurns);
    }

    public static void Part2()
    {
        GetGears(out var gears, "inputp2.txt");
        BigInteger finalTurns = 10000000000000;

        var gear1 = (BigInteger)gears[0];
        var gear2 = (BigInteger)gears[^1];

        var firstTurns = (finalTurns * gear2 / gear1) + 1;

        Console.WriteLine(firstTurns);
    }

    public static void Part3(){}


    private static void GetGears(out int[] gears, string inputLocation)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Day4", inputLocation);
        gears = File.ReadAllLines(path)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(int.Parse)
                .ToArray();
    }

}