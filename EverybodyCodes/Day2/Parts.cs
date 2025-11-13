using System.Numerics;

namespace EverybodyCodes.Day2;

public static class Parts
{
    public class ComplexNumber
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void Print()
        {
            Console.WriteLine($"[{X},{Y}]");
        }

        public ComplexNumber()
        {
        }

        public ComplexNumber(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static ComplexNumber Add(ComplexNumber one, ComplexNumber two)
        {
            // [X1,Y1] + [X2,Y2] = [X1 + X2, Y1 + Y2]
            return new ComplexNumber
            {
                X = (one.X + two.X),
                Y = (one.Y + two.Y)
            };
        }

        public static ComplexNumber Multiply(ComplexNumber one, ComplexNumber two)
        {
            // [X1,Y1] * [X2,Y2] = [X1 * X2 - Y1 * Y2, X1 * Y2 + Y1 * X2]
            return new ComplexNumber
            {
                X = (one.X * two.X) - (one.Y * two.Y),
                Y = (one.X * two.Y) + (one.Y * two.X)
            };
        }

        public static ComplexNumber Divide(ComplexNumber one, ComplexNumber two)
        {
            // [X1,Y1] / [X2,Y2] = [X1 / X2, Y1 / Y2]
            return new ComplexNumber
            {
                X = one.X / two.X,
                Y = one.Y / two.Y
            };
        }
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
}