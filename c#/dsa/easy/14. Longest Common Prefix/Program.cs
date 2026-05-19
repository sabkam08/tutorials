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

        PrintResult(InvokeLongestCommonPrefix(solution, new[] { "flower", "flow", "flight" }));
        PrintResult(InvokeLongestCommonPrefix(solution, new[] { "dog", "racecar", "car" }));

        static string InvokeLongestCommonPrefix(object solution, string[] strs)
        {
            var method = solution.GetType().GetMethod("LongestCommonPrefix")
                         ?? throw new InvalidOperationException("LongestCommonPrefix method was not found.");

            return (string)method.Invoke(solution, new object[] { strs })!;
        }

        static void PrintResult(string result)
        {
            Console.WriteLine(result);
        }
    }
}
#pragma warning restore
