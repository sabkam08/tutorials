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

		MethodInfo method = solutionType.GetMethod("MyAtoi")
			?? throw new InvalidOperationException("MyAtoi method not found.");

		PrintResult(method, solution, "42");
		PrintResult(method, solution, "   -042");
		PrintResult(method, solution, "1337c0d3");
		PrintResult(method, solution, "0-1");
		PrintResult(method, solution, "words and 987");
		PrintResult(method, solution, "91283472332");
		PrintResult(method, solution, "-91283472332");
		PrintResult(method, solution, "   +0 123");
		PrintResult(method, solution, "+-12");
		PrintResult(method, solution, "");
	}

	private static void PrintResult(MethodInfo method, object solution, string input)
	{
		int result = (int)method.Invoke(solution, new object[] { input })!;
		Console.WriteLine(result);
	}
}

#pragma warning restore

