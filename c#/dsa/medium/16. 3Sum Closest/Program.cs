#pragma warning disable

using System.Reflection;

public static class Program
{
    public static void Main()
    {
        Assembly assembly = typeof(Program).Assembly;
        Type solutionType = assembly.GetType("Solution")
            ?? throw new InvalidOperationException("Solution type not found.");

        object solution = Activator.CreateInstance(solutionType)
            ?? throw new InvalidOperationException("Could not create Solution instance.");

        MethodInfo method = solutionType.GetMethod("ThreeSumClosest")
            ?? throw new InvalidOperationException("ThreeSumClosest method not found.");

        RunTest(method, solution, new[] { -1, 2, 1, -4 }, 1, 2);
        RunTest(method, solution, new[] { 0, 0, 0 }, 1, 0);
        RunTest(method, solution, new[] { 1, 1, 1, 0 }, -100, 2);
        RunTest(method, solution, new[] { 1, 1, -1, -1, 3 }, -1, -1);
        RunTest(method, solution, new[] { -8, -6, -5, -3, 0, 2, 4, 7 }, 3, 3);
        RunTest(method, solution, new[] { -4, -1, 1, 2 }, 1, 2);
        RunTest(method, solution, new[] { -1000, 0, 1, 2, 1000 }, 3, 3);
        RunTest(method, solution, new[] { 5, 2, 7, 1, 3, 9 }, 10, 10);

        Console.WriteLine("All 3Sum Closest test cases passed.");
    }

    private static void RunTest(MethodInfo method, object solution, int[] nums, int target, int expected)
    {
        int actual = (int)method.Invoke(solution, new object[] { nums, target })!;
        Console.WriteLine($"nums = [{string.Join(", ", nums)}], target = {target}, expected = {expected}, actual = {actual}");

        if (actual != expected)
        {
            throw new InvalidOperationException(
                $"Test failed for nums = [{string.Join(", ", nums)}], target = {target}. Expected {expected}, got {actual}.");
        }
    }
}

#pragma warning restore

