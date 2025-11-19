using System.Linq;
using System.Linq.Expressions;
using System.Numerics;

namespace EverybodyCodes.Day5;

public static class Parts
{
    private class FishboneSegment
    {
        public int? LeftNumber { get; set; }
        public int SpineNumber { get; set; }
        public int? RightNumber { get; set; }

        public bool TryInsertNumber(int number)
        {
            if (number < SpineNumber && LeftNumber == null)
            {
                LeftNumber = number;
                return true;
            }
            else if (number > SpineNumber && RightNumber == null)
            {
                RightNumber = number;
                return true;
            }
            return false;
        }
    }

    private class Fishbone
    {
        public int Identifier { get; set; }
        public IList<int> FishboneNumbers { get; set; } = [];
        public List<FishboneSegment> Segments { get; set; } = [];
        public long Quality { get; set; }

        public void ConstructFishbone()
        {
            foreach (var number in FishboneNumbers)
            {
                if (Segments.Count == 0)
                {
                    Segments.Add(new FishboneSegment { SpineNumber = number });
                    continue;
                }

                var inserted = Segments.Any(segment => segment.TryInsertNumber(number));
                if (inserted) continue;

                var newSegment = new FishboneSegment { SpineNumber = number };
                Segments.Add(newSegment);
            }

            Quality = long.Parse(Segments.Select(x => x.SpineNumber.ToString())
                .Aggregate((x, y) => x + y));
        }

        public void PrintFishbone()
        {
            foreach (var fishboneSegment in Segments)
            {
                var left = fishboneSegment.LeftNumber.HasValue ? (fishboneSegment.LeftNumber.Value + "-") : "  ";
                var right = fishboneSegment.RightNumber.HasValue ? ("-" + fishboneSegment.RightNumber.Value) : string.Empty;
                Console.WriteLine($"{left}{fishboneSegment.SpineNumber}{right}");
            }
        }

        public IEnumerable<BigInteger> GetJoinedSegments()
        {
            var segments = Segments.Select(segment => (segment.LeftNumber.HasValue ? segment.LeftNumber.Value.ToString() : string.Empty)
                                              + segment.SpineNumber
                                              + (segment.RightNumber.HasValue ? segment.RightNumber.Value.ToString() : string.Empty));

            return segments.Select(BigInteger.Parse);
        }
    }

    public static void Part1()
    {
        GetFishbones(out var fishbones, "inputp1.txt");
        
        var fishbone = fishbones.First();
        fishbone.PrintFishbone();
        Console.WriteLine(fishbone.Quality);
    }


    public static void Part2()
    {
        GetFishbones(out var fishbones, "inputp2.txt");
        var orderedFishbones = fishbones.OrderByDescending(f => f.Quality).ToList();
        var qualityDifference = orderedFishbones.First().Quality - orderedFishbones.Last().Quality;
        Console.WriteLine(qualityDifference);
    }

    public static void Part3()
    {
        GetFishbones(out var fishbones, "inputp3.txt");

        var segmentComparer = new SegmentComparer();

        var ordered = fishbones
            .Select(f => new { Fishbone = f, Levels = f.GetJoinedSegments().ToArray() })
            .GroupBy(x => x.Fishbone.Quality)
            .OrderByDescending(g => g.Key) 
            .SelectMany(g => g
                .OrderBy(x => x.Levels, segmentComparer)
                .ThenByDescending(x => x.Fishbone.Identifier)) 
            .Select(x => x.Fishbone) 
            .ToList();

        foreach (var fishbone in ordered)
        {
            Console.WriteLine($"Id: {fishbone.Identifier} Quality:{fishbone.Quality}");
            fishbone.PrintFishbone();
        }

        var checksum = ordered.Select((fishbone, i) => (i + 1) * fishbone.Identifier).Sum();
        Console.WriteLine(checksum);
    }

    private sealed class SegmentComparer : IComparer<BigInteger[]>
    {
        public int Compare(BigInteger[]? segment1, BigInteger[]? segment2)
        {
            var smallestSegmentCount = Math.Min(segment1!.Length, segment2!.Length);
            for (var i = 0; i < smallestSegmentCount; i++)
            {
                var segmentNumber = segment1[i];
                var secondSegmentNumber = segment2[i];

                if (segmentNumber == secondSegmentNumber) continue;

                return secondSegmentNumber.CompareTo(segmentNumber); 
            }

            if (segment1.Length == segment2.Length) return 0;
            return segment1.Length > segment2.Length ? -1 : 1;
        }
    }

    private static void GetFishbones(out IEnumerable<Fishbone> fishbones, string inputLocation)
    {
        var fishboneList = new List<Fishbone>();
        var path = Path.Combine(AppContext.BaseDirectory, "Day5", inputLocation);
        var lines = File.ReadLines(path);
        foreach (var line in lines)
        {
            var fishbone = new Fishbone();
            var split = line.Split(':', 2, StringSplitOptions.TrimEntries);
        
            fishbone.Identifier = int.Parse(split[0]);
            fishbone.FishboneNumbers = split[1]
                .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            fishbone.ConstructFishbone();

            fishboneList.Add(fishbone);
        }
        fishbones = fishboneList;
    }
}