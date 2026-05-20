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

		int first = (int)method.Invoke(solution, new object[] { "42" })!;
		int second = (int)method.Invoke(solution, new object[] { "   -042" })!;

		Console.WriteLine(first);
		Console.WriteLine(second);
	}
}

#pragma warning restore

