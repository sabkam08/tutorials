using System;
using System.Collections.Generic;

namespace ReverseNodesInKGroup;

public static class Program
{
    public static void Main()
    {
        var solution = new Solution();

        PrintResult(solution.ReverseKGroup(BuildList(new[] { 1, 2, 3, 4, 5 }), 2));
        PrintResult(solution.ReverseKGroup(BuildList(new[] { 1, 2, 3, 4, 5 }), 3));
    }

    private static ListNode? BuildList(int[] values)
    {
        var dummy = new ListNode();
        var tail = dummy;

        foreach (int value in values)
        {
            tail.next = new ListNode(value);
            tail = tail.next;
        }

        return dummy.next;
    }

    private static void PrintResult(ListNode? head)
    {
        Console.WriteLine(FormatList(head));
    }

    private static string FormatList(ListNode? head)
    {
        var values = new List<string>();

        while (head != null)
        {
            values.Add(head.val.ToString());
            head = head.next;
        }

        return "[" + string.Join(",", values) + "]";
    }
}
