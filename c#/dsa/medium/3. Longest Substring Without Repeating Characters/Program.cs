using System;

public static class Program
{
    public static void Main()
    {
        var solution = new Solution();

        Console.WriteLine(solution.LengthOfLongestSubstring("abcabcbb"));
        Console.WriteLine(solution.LengthOfLongestSubstring("bbbbb"));
        Console.WriteLine(solution.LengthOfLongestSubstring("pwwkew"));
    }
}

