#pragma warning disable

public static class Program
{
	public static void Main(string[] args)
	{
		// Search every loaded assembly for a type named "Solution".
		// Create an instance of that type at runtime.
		// Throw a helpful error if the instance cannot be created.
		var solutionType = System.Reflection.Assembly.GetExecutingAssembly()
			.GetTypes()
			.First(type => type.Name == "Solution");
		var solution = Activator.CreateInstance(solutionType) ??
					   throw new InvalidOperationException("Could not create Solution instance.");

		PrintResult(InvokeMaximumJumps(solution, new[] { 1, 3, 6, 4, 1, 2 }, 2));
		PrintResult(InvokeMaximumJumps(solution, new[] { 1, 3, 6, 4, 1, 2 }, 3));
		PrintResult(InvokeMaximumJumps(solution, new[] { 1, 3, 6, 4, 1, 2 }, 0));

		static int InvokeMaximumJumps(object solution, int[] nums, int target)
		{
			var method = solution.GetType().GetMethod("MaximumJumps")
					     ?? throw new InvalidOperationException("MaximumJumps method was not found.");

			return (int)method.Invoke(solution, new object[] { nums, target })!;
		}

		static void PrintResult(int result)
		{
			Console.WriteLine(result);
		}
	}
}

#pragma warning restore

