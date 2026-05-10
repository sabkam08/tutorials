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

		MethodInfo method = solutionType.GetMethod("MaximumJumps")
			?? throw new InvalidOperationException("MaximumJumps method not found.");

		int first = (int)method.Invoke(solution, new object[] { new[] { 1, 3, 6, 4, 1, 2 }, 2 })!;
		int second = (int)method.Invoke(solution, new object[] { new[] { 1, 3, 6, 4, 1, 2 }, 3 })!;
		int third = (int)method.Invoke(solution, new object[] { new[] { 1, 3, 6, 4, 1, 2 }, 0 })!;

		Console.WriteLine(first);
		Console.WriteLine(second);
		Console.WriteLine(third);
	}
}

#pragma warning restore

