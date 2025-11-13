using System.Numerics;

namespace EverybodyCodes.Day2;

public static class Parts
{
    public class ComplexNumber
    {
        public long X { get; set; }
        public long Y { get; set; }

        public void Print()
        {
            Console.WriteLine($"[{X},{Y}]");
        }

        public ComplexNumber() { }
        public ComplexNumber(long x, long y) { X = x; Y = y; }

        public static ComplexNumber Add(ComplexNumber one, ComplexNumber two) => new() { X = one.X + two.X, Y = one.Y + two.Y };
        public static ComplexNumber Multiply(ComplexNumber one, ComplexNumber two) => new() { X = (one.X * two.X) - (one.Y * two.Y), Y = (one.X * two.Y) + (one.Y * two.X) };
        public static ComplexNumber Divide(ComplexNumber one, ComplexNumber two) => new() { X = one.X / two.X, Y = one.Y / two.Y };

        public bool ValidateBounds() => (X is >= -1000000 and <= 1000000 && Y is >= -1000000 and <= 1000000);
    }

    public static void Part1()
    {
        var additionalValue = new ComplexNumber(162, 59);
        var complexTens = new ComplexNumber(10, 10);
        var complexNumber = new ComplexNumber(0, 0);

        for (var i = 0; i < 3; i++)
        {
            complexNumber = ComplexNumber.Multiply(complexNumber, complexNumber);
            complexNumber = ComplexNumber.Divide(complexNumber, complexTens);
            complexNumber = ComplexNumber.Add(complexNumber, additionalValue);
        }
        complexNumber.Print();
    }

    public static void Part2()
    {
        //var topLeftCorner = new ComplexNumber(35300, -64910);
        var topLeftCorner = new ComplexNumber(-79047, 14068);
        var bottomRightCorner =
            ComplexNumber.Add(topLeftCorner, new ComplexNumber(1000, 1000));

        var grid = GenerateGridPoints(topLeftCorner, bottomRightCorner, 101).ToList();
        Console.WriteLine($"Generated {grid.Count} engraving points");

        var engravingPoints = 0;

        foreach (var success in grid.Select(ShouldEngravePoint))
        {
            Console.WriteLine($"{grid[0].X},{grid[0].Y} Engrave: {success}");
            engravingPoints += success ? 1 : 0;
        }

        Console.WriteLine(engravingPoints);
    }

    private static bool ShouldEngravePoint(ComplexNumber point)
    {
        var divideByNumber = new ComplexNumber(100000, 100000);
        var r = new ComplexNumber(0, 0);
        for (var i = 1; i <= 100; i++)
        {
            r = ComplexNumber.Multiply(r, r);
            r = ComplexNumber.Divide(r, divideByNumber);
            r = ComplexNumber.Add(r, point);
            if (!r.ValidateBounds()) return false;
        }
        return true;
    }

    private static IEnumerable<ComplexNumber> GenerateGridPoints(ComplexNumber topLeft, ComplexNumber bottomRight, int size)
    {
        var xStep = (bottomRight.X - topLeft.X) / (size - 1);
        var yStep = (bottomRight.Y - topLeft.Y) / (size - 1);
        for (var yIndex = 0; yIndex < size; yIndex++)
        {
            var y = topLeft.Y + yIndex * yStep; 
            for (var xIndex = 0; xIndex < size; xIndex++)
            {
                var x = topLeft.X + xIndex * xStep;
                yield return new ComplexNumber(x, y);
            }
        }
    }
}