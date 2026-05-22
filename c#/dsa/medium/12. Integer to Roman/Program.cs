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

        MethodInfo method = solutionType.GetMethod("IntToRoman")
            ?? throw new InvalidOperationException("IntToRoman method not found.");

        RunTest(method, solution, 1, "I");
        RunTest(method, solution, 3, "III");
        RunTest(method, solution, 4, "IV");
        RunTest(method, solution, 8, "VIII");
        RunTest(method, solution, 9, "IX");
        RunTest(method, solution, 14, "XIV");
        RunTest(method, solution, 19, "XIX");
        RunTest(method, solution, 40, "XL");
        RunTest(method, solution, 44, "XLIV");
        RunTest(method, solution, 49, "XLIX");
        RunTest(method, solution, 58, "LVIII");
        RunTest(method, solution, 90, "XC");
        RunTest(method, solution, 94, "XCIV");
        RunTest(method, solution, 99, "XCIX");
        RunTest(method, solution, 400, "CD");
        RunTest(method, solution, 444, "CDXLIV");
        RunTest(method, solution, 500, "D");
        RunTest(method, solution, 900, "CM");
        RunTest(method, solution, 944, "CMXLIV");
        RunTest(method, solution, 1987, "MCMLXXXVII");
        RunTest(method, solution, 1994, "MCMXCIV");
        RunTest(method, solution, 2421, "MMCDXXI");
        RunTest(method, solution, 2999, "MMCMXCIX");
        RunTest(method, solution, 3749, "MMMDCCXLIX");
        RunTest(method, solution, 3999, "MMMCMXCIX");
    }

    private static void RunTest(MethodInfo method, object solution, int num, string expected)
    {
        string actual = (string)method.Invoke(solution, new object[] { num })!;
        Console.WriteLine($"num = {num}, expected = {expected}, actual = {actual}");

        if (actual != expected)
        {
            throw new InvalidOperationException($"Test failed for num = {num}. Expected {expected}, got {actual}.");
        }
    }
}

#pragma warning restore

