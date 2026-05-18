# 25. Reverse Nodes in k-Group

**Hard**

## Problem Understanding

This problem asks us to reverse a linked list in groups of size `k`.

From the examples in the folder, you can see how the first `k` nodes are reversed, then the next `k` nodes are reversed, and so on. If the remaining nodes are fewer than `k`, they stay in the same order.

The key restriction is that we may only change the node connections. We cannot change the values inside the nodes.

## How to Think About the Problem

When solving linked-list problems, it helps to separate the list into small sections and handle one section at a time.

For this problem, the natural unit is a group of `k` nodes.

The main questions are:
- How do we know whether there are enough nodes left to reverse?
- How do we reverse a group without losing the rest of the list?
- How do we connect the reversed group back to the list?

## Approach

The best approach is to process the list group by group.

### Step 1: Find the next group of `k` nodes
Starting from the node before the group, check whether there are at least `k` nodes available.

If there are fewer than `k` nodes left, we stop immediately and leave the rest unchanged.

### Step 2: Reverse the group in place
Once we know a full group exists, reverse only those `k` nodes by changing pointers.

A common way to do this is:
- keep track of the node before the group
- keep track of the first node in the group
- reverse pointers one by one until the group is reversed

### Step 3: Reconnect the list
After reversing the group:
- connect the previous part of the list to the new head of the reversed group
- connect the old group head to the next group
- move to the next group and repeat

## Why This Works

This works because each group is handled independently.

- If a full group exists, reversing it is valid.
- If a group is incomplete, it must remain unchanged.
- Reconnecting after each group ensures the entire list remains intact.

## Example Walkthrough

### Example 1

Input:
`head = [1,2,3,4,5]`, `k = 2`

The list is divided into groups:
- `[1,2]`
- `[3,4]`
- `[5]`

Now reverse each full group:
- `[1,2]` becomes `[2,1]`
- `[3,4]` becomes `[4,3]`
- `[5]` stays the same because it is smaller than `k`

Final result:
`[2,1,4,3,5]`

### Example 2

Input:
`head = [1,2,3,4,5]`, `k = 3`

The list is divided into groups:
- `[1,2,3]`
- `[4,5]`

Now reverse each full group:
- `[1,2,3]` becomes `[3,2,1]`
- `[4,5]` stays the same because it is smaller than `k`

Final result:
`[3,2,1,4,5]`

## Complexity Analysis

- **Time:** `O(n)`
  - Each node is visited a constant number of times.
- **Space:** `O(1)`
  - The list is modified in place without extra data structures.

## Common Mistakes

### 1. Losing the rest of the list
When reversing pointers, always save the next node before changing links.

### 2. Reversing an incomplete group
Only reverse if there are at least `k` nodes available.

### 3. Forgetting to reconnect groups
After reversing one group, make sure the previous group points to the new head of that group.

### 4. Not handling `k = 1`
If `k = 1`, the list should remain unchanged.

## Topics to Study

To get better at this type of problem, focus on:

- Singly linked lists
- Pointer manipulation
- Dummy nodes
- In-place reversal
- Segment-based linked list processing

## Good Resources to Review

- LeetCode discuss solutions for linked-list reversal problems
- GeeksforGeeks articles on linked list reversal
- NeetCode linked list patterns
- C# reference on classes and nullable references

## Summary

The core idea is to process the linked list in chunks of `k`, reverse each complete chunk in place, and leave any leftover nodes untouched.

This makes the solution efficient, clean, and memory-friendly.

