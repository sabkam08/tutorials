#pragma warning disable

public static class Program
{
    public static void Main(string[] args)
    {
        // Lines 7-11:
        // 1) Search every loaded assembly for a type named "Solution".
        // 2) Create an instance of that type at runtime.
        // 3) Throw a helpful error if the instance cannot be created.
        //
        // This avoids a compile-time reference to Solution, which is useful
        // when Rider is still having trouble resolving the symbol in the editor.
        var solutionType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .First(type => type.Name == "Solution");
        var solution = Activator.CreateInstance(solutionType) ??
                       throw new InvalidOperationException("Could not create Solution instance.");

        PrintResult(InvokeTwoSum(solution, new[] { 2, 7, 11, 15 }, 9));
        PrintResult(InvokeTwoSum(solution, new[] { 3, 2, 4 }, 6));
        PrintResult(InvokeTwoSum(solution, new[] { 3, 3 }, 6));

        static int[] InvokeTwoSum(object solution, int[] nums, int target)
        {
            var method = solution.GetType().GetMethod("TwoSum")
                         ?? throw new InvalidOperationException("TwoSum method was not found.");

            return (int[])method.Invoke(solution, new object[] { nums, target })!;
        }

        static void PrintResult(int[] result)
        {
            Console.WriteLine($"[{string.Join(",", result)}]");
        }
    }
}
#pragma warning restore
