using PalindromeNumber;

namespace PalindromeNumber;

public static class Program
{
    public static void Main()
    {
        var solution = new Solution();

        Console.WriteLine(solution.IsPalindrome(121));
        Console.WriteLine(solution.IsPalindrome(-121));
        Console.WriteLine(solution.IsPalindrome(10));
    }
}

