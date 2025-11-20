using System.Text;

namespace EverybodyCodes.Day6;

public static class Parts
{
    public static void Part1()
    {
        GetKnights(out var knights, "inputp1.txt");
        var mentors = 0;
        var mentorPairings = 0;
        foreach (var knight in knights)
        {
            if (knight == 'A')
            {
                mentors++;
                continue;
            }
            
            if (knight != 'a') continue;
            
            mentorPairings += mentors;
        }

        Console.WriteLine(mentorPairings);
    }


    public static void Part2()
    {
        GetKnights(out var knights, "inputp2.txt");

        var mentorPairings = 0;
        var mentors = new Dictionary<char, int>();
        foreach (var knight in knights)
        {
            if (char.IsUpper(knight))
            {
                if (mentors.TryGetValue(knight, out var c))
                    mentors[knight] = c + 1;
                else
                    mentors[knight] = 1;
                continue;
            }

            mentorPairings += mentors.GetValueOrDefault(char.ToUpper(knight), 0);
        }

        Console.WriteLine(mentorPairings);
    }

    public static void Part3()
    {
        const int distanceLimit = 1000;
        const int repeatingTentPatterns = 1000;

        GetKnights(out var simplifiedKnights, "inputp3.txt");
        var knightsBuilder = new StringBuilder();
        for (var i = 0; i < repeatingTentPatterns; i++)
        {
            knightsBuilder.Append(simplifiedKnights);
        }

        var knights = knightsBuilder.ToString();
        var mentorPairings = GetMentorPairings(knights);

        var knightsReversedArray = knightsBuilder.ToString().Reverse().ToArray();
        var knightsReversed = new string(knightsReversedArray);
        mentorPairings += GetMentorPairings(knightsReversed);
        Console.WriteLine(mentorPairings);
        return;

        static int GetMentorPairings(string knightsToProcess)
        {
            var pairings = 0;
            for (var i = 0; i < knightsToProcess.Length; i++)
            {
                if (char.IsUpper(knightsToProcess[i])) continue;

                var searchLength = Math.Min(distanceLimit, knightsToProcess.Length - (i + 1));
                var nextKnights = knightsToProcess.AsSpan(i + 1, searchLength);
                pairings += nextKnights.Count(char.ToUpper(knightsToProcess[i]));
            }
            return pairings;
        }
    }

    private static void GetKnights(out char[] knights, string inputLocation)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Day6", inputLocation);
        var line = File.ReadLines(path).First().Trim();
        knights = line.ToCharArray();
    }
}