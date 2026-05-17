using ReverseInteger;

namespace ReverseInteger;

public static class Program
{
    public static void Main()
    {
        var solution = new Solution();

        Console.WriteLine(solution.Reverse(123));
        Console.WriteLine(solution.Reverse(-123));
        Console.WriteLine(solution.Reverse(120));
    }
}

