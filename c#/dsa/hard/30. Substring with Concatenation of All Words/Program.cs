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

        MethodInfo method = solutionType.GetMethod("FindSubstring")
            ?? throw new InvalidOperationException("FindSubstring method not found.");

        RunTest(method, solution, "barfoothefoobarman", new[] { "foo", "bar" }, new[] { 0, 9 });
        RunTest(method, solution, "wordgoodgoodgoodbestword", new[] { "word", "good", "best", "word" }, Array.Empty<int>());
        RunTest(method, solution, "barfoofoobarthefoobarman", new[] { "bar", "foo", "the" }, new[] { 6, 9, 12 });
        RunTest(method, solution, "wordgoodgoodgoodbestword", new[] { "word", "good", "best", "good" }, new[] { 8 });
        RunTest(method, solution, "aaaaaa", new[] { "aa", "aa" }, new[] { 0, 1, 2 });
        RunTest(method, solution, "abab", new[] { "a", "b" }, new[] { 0, 1, 2 });
        RunTest(method, solution, "a", new[] { "a", "a" }, Array.Empty<int>());
        RunTest(method, solution, "lingmindraboofooowingdingbarrwingmonkeypoundcake", new[] { "fooo", "barr", "wing", "ding", "wing" }, new[] { 13 });
        RunTest(method, solution, "aaaaaaaa", new[] { "aa", "aa", "aa" }, new[] { 0, 1, 2 });

        Console.WriteLine("All test cases passed.");
    }

    private static void RunTest(MethodInfo method, object solution, string s, string[] words, int[] expected)
    {
        IList<int> actual = (IList<int>)method.Invoke(solution, new object[] { s, words })!;
        List<int> actualSorted = new(actual);
        actualSorted.Sort();

        List<int> expectedSorted = new(expected);
        expectedSorted.Sort();

        if (actualSorted.Count != expectedSorted.Count)
        {
            throw new InvalidOperationException($"Expected [{string.Join(", ", expectedSorted)}], but got [{string.Join(", ", actualSorted)}].");
        }

        for (int i = 0; i < actualSorted.Count; i++)
        {
            if (actualSorted[i] != expectedSorted[i])
            {
                throw new InvalidOperationException($"Expected [{string.Join(", ", expectedSorted)}], but got [{string.Join(", ", actualSorted)}].");
            }
        }

        Console.WriteLine($"Input: s = \"{s}\", words = [\"{string.Join("\", \"", words)}\"]");
        Console.WriteLine($"Output: [{string.Join(", ", actualSorted)}]");
        Console.WriteLine();
    }
}

#pragma warning restore

