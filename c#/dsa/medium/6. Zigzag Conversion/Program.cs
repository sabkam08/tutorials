using ZigzagConversion;

namespace ZigzagConversion;

public static class Program
{
    public static void Main()
    {
        var solution = new Solution();

        Console.WriteLine(solution.Convert("PAYPALISHIRING", 3));
        Console.WriteLine(solution.Convert("PAYPALISHIRING", 4));
        Console.WriteLine(solution.Convert("A", 1));
    }
}

