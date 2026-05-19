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

        PrintResult(InvokeIsPalindrome(solution, 121));
        PrintResult(InvokeIsPalindrome(solution, -121));
        PrintResult(InvokeIsPalindrome(solution, 10));

        static bool InvokeIsPalindrome(object solution, int x)
        {
            var method = solution.GetType().GetMethod("IsPalindrome")
                         ?? throw new InvalidOperationException("IsPalindrome method was not found.");

            return (bool)method.Invoke(solution, new object[] { x })!;
        }

        static void PrintResult(bool result)
        {
            Console.WriteLine(result);
        }
    }
}
