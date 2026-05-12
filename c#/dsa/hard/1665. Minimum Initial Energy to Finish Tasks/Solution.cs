using System;

public class Solution
{
    public int MinimumEffort(int[][] tasks)
    {
        Array.Sort(tasks, (a, b) => (b[1] - b[0]).CompareTo(a[1] - a[0]));

        long requiredEnergy = 0;

        for (int i = tasks.Length - 1; i >= 0; i--)
        {
            requiredEnergy = Math.Max(requiredEnergy + tasks[i][0], tasks[i][1]);
        }

        return (int)requiredEnergy;
    }
}

