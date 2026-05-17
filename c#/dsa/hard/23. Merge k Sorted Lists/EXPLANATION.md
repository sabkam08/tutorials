# 23. Merge k Sorted Lists — C# Explanation

## 1. How To Think About The Problem

This problem asks you to merge multiple already-sorted linked lists into one sorted linked list.

At first, you might think about flattening everything into one large list, sorting it, and then rebuilding a linked list.

That would work conceptually, but it would waste the fact that each individual list is already sorted.

The key insight is that the lists are already sorted, so we should merge them efficiently instead of re-sorting all the values from scratch.

---

## 2. Understanding The Requirements Carefully

You are given an array of linked lists:

- `lists`

Each linked list is already sorted in ascending order.

Your job is to merge all of them into one sorted linked list and return the result.

### Important observations

1. Each list is already sorted.
2. Some lists may be empty.
3. The input array itself may be empty.
4. The final merged list must also be sorted.

This is very similar to merging sorted arrays, except here the data structure is a linked list.

---

## 3. Why A Priority Queue Is A Good Fit

Since every list is sorted, the smallest remaining node among all lists is always the next node that should appear in the output.

A priority queue (min-heap) is perfect for this because it lets us:

- quickly find the smallest current node,
- remove it,
- and then insert the next node from that same list.

That way, we never need to compare every node with every other node manually.

### Why not sort everything directly?

You could collect all nodes and sort them, but that would ignore the sorted structure of the input lists.

Using a min-heap gives a much better algorithmic approach.

---

## 4. Approach Used In `Solution.cs`

The implementation uses a **priority queue** to always pick the smallest available node.

### Step 1: Add the head of each non-empty list to the queue

For every list in `lists`:

- if it is not empty,
- push its head node into the priority queue.

These heads represent the current smallest candidates from each list.

### Step 2: Repeatedly remove the smallest node

Each time we remove the smallest node from the queue:

- append it to the result linked list,
- then if that node has a `next` node,
- push that next node into the queue.

This ensures the queue always contains the current front node from each list.

### Step 3: Build the merged linked list

We use:

- a dummy head node,
- and a tail pointer.

Each time we pop the smallest node, we attach it to the tail and move the tail forward.

At the end, the dummy head’s next pointer is the head of the merged list.

---

## 5. Example Walkthrough

### Example 1

```text
lists = [[1,4,5],[1,3,4],[2,6]]
```

The individual lists are:

```text
1->4->5
1->3->4
2->6
```

### Initial queue

We push the heads:

- `1` from the first list
- `1` from the second list
- `2` from the third list

So the smallest values available are `1`, `1`, and `2`.

### Process

1. Pop `1` → output: `1`
   - push its next node `4`

2. Pop `1` → output: `1->1`
   - push its next node `3`

3. Pop `2` → output: `1->1->2`
   - push its next node `6`

4. Pop `3` → output: `1->1->2->3`
   - push its next node `4`

5. Pop `4` → output: `1->1->2->3->4`
   - push its next node `5`

6. Pop `4` → output: `1->1->2->3->4->4`

7. Pop `5` → output: `1->1->2->3->4->4->5`

8. Pop `6` → output: `1->1->2->3->4->4->5->6`

That is the final merged list.

---

## 6. Why This Works

The correctness comes from the fact that each list is sorted.

At any point:

- the head node of each list is the smallest not-yet-used node from that list,
- so the overall smallest remaining node must be among those heads.

A min-heap keeps exactly those candidate nodes available.

Every time we remove the smallest node, the next candidate from that same list becomes relevant, so we insert its `next` node.

This preserves the invariant that the queue always contains the smallest remaining candidate from each list.

---

## 7. Why The Implemented Solution Is Good

The current `Solution.cs` is strong because it is:

- **efficient**
- **clean**
- **easy to reason about**
- **works for empty lists**
- **handles all `k` lists naturally**

It is the standard optimal approach for this problem.

---

## 8. Time Complexity And Space Complexity

Let:

- `k` = number of linked lists
- `N` = total number of nodes across all lists

### Time complexity

Each node is pushed and popped at most once from the priority queue.

Each heap operation costs `O(log k)`.

So the total time complexity is:

- **O(N log k)**

### Space complexity

The priority queue stores at most one node from each list at a time:

- **O(k)**

The output list reuses the existing nodes, so no extra list storage is needed beyond the queue.

---

## 9. Common Mistakes To Avoid

### Mistake 1: Forgetting to handle empty lists

Some lists may be `null` or empty, and the algorithm must skip them.

### Mistake 2: Re-sorting all values manually

That ignores the sorted nature of the input and is less efficient.

### Mistake 3: Not advancing the list after removing a node

After popping a node from the queue, you must push its `next` node if it exists.

### Mistake 4: Breaking the merged list links

When building the result, make sure the tail pointer advances properly.

### Mistake 5: Forgetting the dummy head

A dummy node makes the linked-list construction much simpler and avoids special cases.

---

## 10. How To Approach Problems Like This In General

When you see a problem involving:

- multiple sorted structures,
- merging sorted data,
- or needing the smallest current element repeatedly,

ask yourself:

1. Can I preserve the sorted structure instead of re-sorting everything?
2. Can I use a heap or priority queue to track the next smallest item?
3. Can I process one item at a time while maintaining an invariant?
4. Can I build the output incrementally with a dummy head or similar technique?

For this problem, the answer is yes to all of them.

---

## 11. Topics To Read On To Get Better

If you want to improve at problems like this, study these topics:

### Core algorithm topics

- Priority queues / min-heaps
- Merge algorithms
- Linked lists
- Sorted data structures
- Divide and conquer
- Complexity analysis

### Helpful supporting topics

- Dummy head nodes
- Pointer manipulation
- Recursion vs iteration
- Invariants
- Greedy selection

### C# topics worth practicing

- Linked-list classes
- Generics
- `PriorityQueue<TElement, TPriority>`
- Nullability in C#
- Building console runners for list-based problems

---

## 12. Sources To Consult

Here are good places to learn and improve your understanding.

### Official / high-quality references

1. **Microsoft Learn — C# documentation**
   - Useful for `PriorityQueue`, nullable reference types, and class design.

2. **LeetCode problem discussions**
   - Great for seeing different merging strategies and heap-based solutions.

3. **GeeksforGeeks**
   - Good for linked-list merging and heap explanations.

4. **CP-Algorithms**
   - Helpful for understanding heap-based selection and algorithm reasoning.

5. **MIT OpenCourseWare / free algorithm lectures**
   - Good for learning the broader algorithmic ideas behind merging and priority queues.

### If you prefer video learning

- NeetCode
- Striver / takeUforward
- Abdul Bari
- freeCodeCamp algorithm playlists

### If you want structured practice

- LeetCode problem sets by topic
- HackerRank linked list tracks
- Codeforces educational rounds
- AtCoder beginner contests

---

## 13. A Good Study Plan For Problems Like This

If you want to improve steadily, try this order:

1. Learn linked list basics.
2. Practice merging two sorted lists.
3. Learn min-heaps / priority queues.
4. Practice heap-based selection problems.
5. Re-solve the problem without looking.
6. Compare your solution to an optimal one.
7. Explain the algorithm in your own words.

That last step is especially useful.

If you can explain why the heap always contains the current best candidates, you understand the problem.

---

## 14. Final Summary

This problem is best solved by using a priority queue:

- add the head of each non-empty list,
- repeatedly remove the smallest node,
- append it to the result,
- push the next node from that list.

The method in `Solution.cs` is the optimal approach because it is efficient and easy to reason about.

The main lessons from this problem are:

- use the sorted property,
- avoid re-sorting everything,
- maintain a heap of current candidates,
- and build the output list incrementally.

