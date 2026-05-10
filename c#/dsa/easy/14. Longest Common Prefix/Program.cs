#pragma warning disable

public static class Program
{
	public static void Main()
	{
		Type solutionType = typeof(Program).Assembly.GetType("Solution")
			?? throw new InvalidOperationException("Solution type not found.");

		object solution = Activator.CreateInstance(solutionType)
			?? throw new InvalidOperationException("Could not create Solution instance.");

		var method = solutionType.GetMethod("LongestCommonPrefix")
			?? throw new InvalidOperationException("LongestCommonPrefix method not found.");

		string first = (string)method.Invoke(solution, new object[] { new[] { "flower", "flow", "flight" } })!;
		string second = (string)method.Invoke(solution, new object[] { new[] { "dog", "racecar", "car" } })!;

		Console.WriteLine(first);
		Console.WriteLine(second);
	}
}

#pragma warning restore

