public static class Program
{
    public static void Main()
    {
        Solution solution = new();

        Console.WriteLine(solution.IsMatch("aa", "a"));
        Console.WriteLine(solution.IsMatch("aa", "a*"));
        Console.WriteLine(solution.IsMatch("ab", ".*"));
    }
}

