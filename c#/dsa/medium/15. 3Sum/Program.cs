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

        MethodInfo method = solutionType.GetMethod("ThreeSum")
            ?? throw new InvalidOperationException("ThreeSum method not found.");

        RunTest(method, solution, new[] { -1, 0, 1, 2, -1, -4 }, new[]
        {
            new[] { -1, -1, 2 },
            new[] { -1, 0, 1 }
        });

        RunTest(method, solution, new[] { 0, 1, 1 }, Array.Empty<int[]>());
        RunTest(method, solution, new[] { 0, 0, 0 }, new[]
        {
            new[] { 0, 0, 0 }
        });

        RunTest(method, solution, new[] { -2, 0, 1, 1, 2 }, new[]
        {
            new[] { -2, 0, 2 },
            new[] { -2, 1, 1 }
        });

        RunTest(method, solution, new[] { -2, 0, 0, 2, 2 }, new[]
        {
            new[] { -2, 0, 2 }
        });

        RunTest(method, solution, new[] { 1, 2, -2, -1 }, Array.Empty<int[]>());
        RunTest(method, solution, new[] { -1, -1, -1, 2, 2 }, new[]
        {
            new[] { -1, -1, 2 }
        });

        Console.WriteLine("All 3Sum test cases passed.");
    }

    private static void RunTest(MethodInfo method, object solution, int[] nums, int[][] expected)
    {
        var actual = (IList<IList<int>>)method.Invoke(solution, new object[] { nums })!;
        string actualNormalized = Normalize(actual);
        string expectedNormalized = Normalize(expected.Select(triplet => (IList<int>)triplet.ToList()).ToList());

        Console.WriteLine($"nums = [{string.Join(", ", nums)}]");
        Console.WriteLine($"expected = {expectedNormalized}");
        Console.WriteLine($"actual   = {actualNormalized}");
        Console.WriteLine();

        if (actualNormalized != expectedNormalized)
        {
            throw new InvalidOperationException(
                $"Test failed for nums = [{string.Join(", ", nums)}]. Expected {expectedNormalized}, got {actualNormalized}.");
        }
    }

    private static string Normalize(IList<IList<int>> triplets)
    {
        List<string> serialized = new();

        foreach (IList<int> triplet in triplets)
        {
            int[] sorted = triplet.OrderBy(value => value).ToArray();
            serialized.Add($"[{string.Join(",", sorted)}]");
        }

        serialized.Sort(StringComparer.Ordinal);
        return $"[{string.Join(", ", serialized)}]";
    }
}

#pragma warning restore

