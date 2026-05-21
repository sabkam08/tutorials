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

        MethodInfo method = solutionType.GetMethod("MaxArea")
            ?? throw new InvalidOperationException("MaxArea method not found.");

        PrintResult(method, solution, new[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 });
        PrintResult(method, solution, new[] { 1, 1 });
        PrintResult(method, solution, new[] { 1, 2, 1 });
        PrintResult(method, solution, new[] { 4, 3, 2, 1, 4 });
        PrintResult(method, solution, new[] { 1, 2, 4, 3 });
    }

    private static void PrintResult(MethodInfo method, object solution, int[] height)
    {
        int result = (int)method.Invoke(solution, new object[] { height })!;
        Console.WriteLine(result);
    }
}

#pragma warning restore

