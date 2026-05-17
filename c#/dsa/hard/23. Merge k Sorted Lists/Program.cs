using MergeKSortedLists;

namespace MergeKSortedLists;

public static class Program
{
    public static void Main()
    {
        var solution = new Solution();

        PrintList(solution.MergeKLists([
            BuildList([1, 4, 5]),
            BuildList([1, 3, 4]),
            BuildList([2, 6])
        ]));

        PrintList(solution.MergeKLists([]));
        PrintList(solution.MergeKLists([null]));
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

    private static void PrintList(ListNode? head)
    {
        if (head == null)
        {
            Console.WriteLine("[]");
            return;
        }

        var values = new List<int>();
        var current = head;

        while (current != null)
        {
            values.Add(current.val);
            current = current.next;
        }

        Console.WriteLine($"[{string.Join(",", values)}]");
    }
}
