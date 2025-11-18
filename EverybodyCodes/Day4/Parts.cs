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

    public static void Part3()
    {
        GetPipedGears(out var gears, "inputp3.txt");
        decimal ratio = 0;
        for (var i = 0; i < gears.Length - 1; i++)
        {
            (decimal firstGear, decimal secondGear) = gears[i];
            (decimal firstNextGear, decimal secondNextGear) = gears[i + 1];
            if (secondGear == 0)
            {
                if (ratio == 0)
                {
                    var innerRatio = decimal.Divide(firstGear, firstNextGear);
                    ratio = firstNextGear * innerRatio;
                }
                else
                {
                    var innerRatio = decimal.Divide(ratio, firstNextGear);
                    ratio = firstNextGear * innerRatio;
                }
            }
            else
            {
                var pipedRatio = decimal.Divide(ratio, firstNextGear);
                if (secondNextGear == 0)
                {
                    ratio = firstNextGear * pipedRatio;
                    break;
                }
                ratio = secondNextGear * pipedRatio;
            }
        }
    }

    private static void GetGears(out int[] gears, string inputLocation)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Day4", inputLocation);
        gears = File.ReadAllLines(path)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(int.Parse)
                .ToArray();
    }

    private static void GetPipedGears(out (int,int)[] gears, string inputLocation)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Day4", inputLocation);
        var lines = File.ReadAllLines(path)
            .Where(x => !string.IsNullOrWhiteSpace(x));
        var gearList = new List<(int,int)>();
        foreach (var line in lines)
        {
            var parts = line.Split('|');
            var firstGear = int.Parse(parts[0].Trim());
            var secondGear = parts.Length > 1 && !string.IsNullOrWhiteSpace(parts[1]) 
                ? int.Parse(parts[1].Trim()) 
                : 0;
            gearList.Add((firstGear, secondGear));
        }
        gears = gearList.ToArray();
    }
}