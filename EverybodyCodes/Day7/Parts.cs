using System.Linq;

namespace EverybodyCodes.Day7;

public static class Parts
{
    public static void Part1()
    {
       GetNamesAndRules(out var names, out var rules, "inputp1.txt");
       foreach (var name in names)
       {
           if (!CheckName(name, rules)) continue;
           Console.WriteLine($"Valid name: {name}");
       }
    }

    public static void Part2()
    {
        GetNamesAndRules(out var names, out var rules, "inputp2.txt");
        var nameSum = 0;
        for (var i = 0; i < names.Length; i++)
        {
            var name = names[i];
            if (!CheckName(name, rules)) continue;
            Console.WriteLine($"Valid name: {name}");
            nameSum += i + 1;
        }

        Console.WriteLine($"Name sum: {nameSum}");
    }

    public static void Part3()
    {
        GetNamesAndRules(out var names, out var rules, "inputp3.txt");
        var validPrefixes = names.Where(name => name.Length <= 11)
            .Distinct()
            .Where(name => CheckName(name, rules))
            .ToArray();

        var validNames = new HashSet<string>();
        foreach (var validPrefix in validPrefixes)
        {
            var namesToProcess = new Queue<string>();
            namesToProcess.Enqueue(validPrefix);
            while (namesToProcess.Count > 0)
            {
                //Console.WriteLine($"validNames = {validNames.Count} namesToProcess = {namesToProcess.Count}");
                var currentName = namesToProcess.Dequeue();
                if (!rules.TryGetValue(currentName.Last(), out var nextChars)) continue;
                foreach (var nextChar in nextChars)
                {
                    var newName = currentName + nextChar;
                    if (!CheckName(newName, rules)) continue;

                    if (validNames.Contains(newName) || newName.Length > 11) continue;
                    
                    if (!namesToProcess.Contains(newName)) namesToProcess.Enqueue(newName);

                    if (newName.Length is >= 7 and <= 11)
                    {
                        validNames.Add(newName);
                    }
                }
            }
        }

        var finishedNames = validNames.ToList().Where(name => name.Length >= 7).ToList();
        Console.WriteLine($"Count: {validNames.Count}");
    }

    private static bool CheckName(string name, Dictionary<char, char[]> rules)
    {
        return !name.Where(
                (t, i) => rules.ContainsKey(t) && 
                  i != name.Length - 1 && 
                  !rules[t].Contains(name[i + 1]))
            .Any();
    }


    private static void GetNamesAndRules(out string[] names, out Dictionary<char, char[]> rules, string inputLocation)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Day7", inputLocation);
        var lines = File.ReadLines(path).ToArray();

        names = lines[0].Split(",");
        rules = new Dictionary<char, char[]>();

        for (var i = 2; i < lines.Length; i++)
        {
            var parts = lines[i].Split(" > ");
            var key = parts[0][0];
            var values = parts[1].Split(",").Select(c => c[0]).ToArray();
            rules[key] = values;
        }
    }
}