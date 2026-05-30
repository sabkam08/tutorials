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

        MethodInfo method = solutionType.GetMethod("LongestValidParentheses")
            ?? throw new InvalidOperationException("LongestValidParentheses method not found.");

        RunTest(method, solution, "", 0);
        RunTest(method, solution, "(", 0);
        RunTest(method, solution, ")", 0);
        RunTest(method, solution, "()", 2);
        RunTest(method, solution, "(()", 2);
        RunTest(method, solution, ")()())", 4);
        RunTest(method, solution, "()(()", 2);
        RunTest(method, solution, "()(())", 6);
        RunTest(method, solution, "((()))", 6);
        RunTest(method, solution, "()()", 4);
        RunTest(method, solution, "(()())", 6);
        RunTest(method, solution, "(()()(()", 4);
        RunTest(method, solution, ")()()()()(", 8);
        RunTest(method, solution, "(((((", 0);
        RunTest(method, solution, "))))))", 0);
        RunTest(method, solution, "()((())", 4);
        RunTest(method, solution, "()(())()", 8);
        RunTest(method, solution, "(()(((()", 2);
        RunTest(method, solution, "(()())())", 8);
        RunTest(method, solution, "()(())(()(()))", 14);
        RunTest(method, solution, "(()((()))", 6);
        RunTest(method, solution, "((()()()))", 10);
        RunTest(method, solution, "())(())", 4);
        RunTest(method, solution, "()()(()())", 10);
        RunTest(method, solution, "())(()())", 6);
        RunTest(method, solution, "()(()()))", 8);
        RunTest(method, solution, "((())())())", 10);
        RunTest(method, solution, "(()())((()))", 12);

        Console.WriteLine("All test cases passed.");
    }

    private static void RunTest(MethodInfo method, object solution, string s, int expected)
    {
        int actual = (int)method.Invoke(solution, new object[] { s })!;

        if (actual != expected)
        {
            throw new InvalidOperationException($"Input: \"{s}\". Expected {expected}, but got {actual}.");
        }

        Console.WriteLine($"Input: \"{s}\"");
        Console.WriteLine($"Output: {actual}");
        Console.WriteLine();
    }
}

#pragma warning restore