#pragma warning disable

using System.Globalization;
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

		MethodInfo method = solutionType.GetMethod("FindMedianSortedArrays")
			?? throw new InvalidOperationException("FindMedianSortedArrays method not found.");

		double first = (double)method.Invoke(solution, new object[] { new[] { 1, 3 }, new[] { 2 } })!;
		double second = (double)method.Invoke(solution, new object[] { new[] { 1, 2 }, new[] { 3, 4 } })!;

		Console.WriteLine(first.ToString(CultureInfo.InvariantCulture));
		Console.WriteLine(second.ToString(CultureInfo.InvariantCulture));
	}
}

#pragma warning restore

