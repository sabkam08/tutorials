using System.Collections.Generic;

namespace MergeKSortedLists;

public class ListNode
{
    public int val;
    public ListNode? next;

    public ListNode(int val = 0, ListNode? next = null)
    {
        this.val = val;
        this.next = next;
    }
}

public class Solution
{
    public ListNode? MergeKLists(ListNode?[] lists)
    {
        var queue = new PriorityQueue<ListNode, int>();

        foreach (var node in lists)
        {
            if (node != null)
            {
                queue.Enqueue(node, node.val);
            }
        }

        var dummy = new ListNode();
        var tail = dummy;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            tail.next = current;
            tail = tail.next;

            if (current.next != null)
            {
                queue.Enqueue(current.next, current.next.val);
            }
        }

        return dummy.next;
    }
}
