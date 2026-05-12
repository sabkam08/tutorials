using System;

public class Program
{
    public static void Main()
    {
        var solution = new Solution();

        RunCase(solution, new[] { new[] { 1, 2 }, new[] { 2, 4 }, new[] { 4, 8 } });
        RunCase(solution, new[] { new[] { 1, 3 }, new[] { 2, 4 }, new[] { 10, 11 }, new[] { 10, 12 }, new[] { 8, 9 } });
        RunCase(solution, new[] { new[] { 1, 7 }, new[] { 2, 8 }, new[] { 3, 9 }, new[] { 4, 10 }, new[] { 5, 11 }, new[] { 6, 12 } });
    }

    private static void RunCase(Solution solution, int[][] tasks)
    {
        Console.WriteLine(solution.MinimumEffort(tasks));
    }
}

