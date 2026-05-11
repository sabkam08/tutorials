#pragma warning disable
var solutionType = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(assembly => assembly.GetTypes())
    .First(type => type.Name == "Solution");
var solution = Activator.CreateInstance(solutionType) ?? throw new InvalidOperationException("Could not create Solution instance.");

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

#pragma warning restore

