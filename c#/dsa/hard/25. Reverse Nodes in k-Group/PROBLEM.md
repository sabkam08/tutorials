# 25. Reverse Nodes in k-Group

**Hard**

## Description

Given the head of a linked list, reverse the nodes of the list `k` at a time, and return the modified list.

`k` is a positive integer and is less than or equal to the length of the linked list. If the number of nodes is not a multiple of `k`, then the left-out nodes at the end should remain as they are.

You may not alter the values in the list's nodes; only nodes themselves may be changed.

## Examples

### Example 1

![Example 1](images/reverse_ex1.jpg){width="400" height="200"}

**Input:** `head = [1,2,3,4,5]`, `k = 2`  
**Output:** `[2,1,4,3,5]`

### Example 2

![Example 2](images/reverse_ex2.jpg){width="400" height="200"}

**Input:** `head = [1,2,3,4,5]`, `k = 3`  
**Output:** `[3,2,1,4,5]`

## Constraints

- The number of nodes in the list is `n`.
- `1 <= k <= n <= 5000`
- `0 <= Node.val <= 1000`

## Follow-up

Can you solve the problem in `O(1)` extra memory space?
