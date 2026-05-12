using System;

public static class Program
{
    public static void Main()
    {
        var solution = new Solution();

        Console.WriteLine(solution.LongestPalindrome("babad"));
        Console.WriteLine(solution.LongestPalindrome("cbbd"));
        Console.WriteLine(solution.LongestPalindrome("a"));
    }
}

