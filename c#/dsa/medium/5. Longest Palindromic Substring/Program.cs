#pragma warning disable

public static class Program
{
    public static void Main(string[] args)
    {
        var solutionType = System.Reflection.Assembly.GetExecutingAssembly()
            .GetTypes()
            .First(type => type.Name == "Solution");
        var solution = Activator.CreateInstance(solutionType) ??
                       throw new InvalidOperationException("Could not create Solution instance.");

        PrintResult(InvokeLongestPalindrome(solution, "babad"));
        PrintResult(InvokeLongestPalindrome(solution, "cbbd"));
        PrintResult(InvokeLongestPalindrome(solution, "a"));

        static string InvokeLongestPalindrome(object solution, string s)
        {
            var method = solution.GetType().GetMethod("LongestPalindrome")
                         ?? throw new InvalidOperationException("LongestPalindrome method was not found.");

            return (string)method.Invoke(solution, new object[] { s })!;
        }

        static void PrintResult(string result)
        {
            Console.WriteLine(result);
        }
    }
}

#pragma warning restore
