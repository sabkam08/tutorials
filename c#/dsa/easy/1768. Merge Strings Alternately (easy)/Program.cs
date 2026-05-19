#pragma warning disable

public static class Program
{
    public static void Main(string[] args)
    {
        // Search every loaded assembly for a type named "Solution".
        // Create an instance of that type at runtime.
        // Throw a helpful error if the instance cannot be created.
        var solutionType = System.Reflection.Assembly.GetExecutingAssembly()
            .GetTypes()
            .First(type => type.Name == "Solution");
        var solution = Activator.CreateInstance(solutionType) ??
                       throw new InvalidOperationException("Could not create Solution instance.");

        PrintResult(InvokeMergeAlternately(solution, "abc", "pqr"));
        PrintResult(InvokeMergeAlternately(solution, "ab", "pqrs"));
        PrintResult(InvokeMergeAlternately(solution, "abcd", "pq"));

        static string InvokeMergeAlternately(object solution, string word1, string word2)
        {
            var method = solution.GetType().GetMethod("MergeAlternately")
                         ?? throw new InvalidOperationException("MergeAlternately method was not found.");

            return (string)method.Invoke(solution, new object[] { word1, word2 })!;
        }

        static void PrintResult(string result)
        {
            Console.WriteLine(result);
        }
    }
}
#pragma warning restore
