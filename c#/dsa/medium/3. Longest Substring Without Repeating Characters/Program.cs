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

        PrintResult(InvokeLengthOfLongestSubstring(solution, "abcabcbb"));
        PrintResult(InvokeLengthOfLongestSubstring(solution, "bbbbb"));
        PrintResult(InvokeLengthOfLongestSubstring(solution, "pwwkew"));

        static int InvokeLengthOfLongestSubstring(object solution, string s)
        {
            var method = solution.GetType().GetMethod("LengthOfLongestSubstring")
                         ?? throw new InvalidOperationException("LengthOfLongestSubstring method was not found.");

            return (int)method.Invoke(solution, new object[] { s })!;
        }

        static void PrintResult(int result)
        {
            Console.WriteLine(result);
        }
    }
}

#pragma warning restore
