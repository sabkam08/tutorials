# 16. 3Sum Closest — C# Explanation

## 1. How To Think About The Problem

At first glance, this problem looks very similar to `3Sum`, but it is a little easier in one important way: you do **not** need to list all triplets.

Instead, you only need the triplet whose sum is **closest** to the target.

That changes the goal from:

- “find every valid answer”

to:

- “find the best possible answer.”

A brute-force solution would try every triplet and compare its sum with the target. That would work conceptually, but it would be too slow.

Because the array can be sorted, we can use a **two-pointer** strategy to search efficiently.

---

## 2. Understanding The Requirements Carefully

You are given:

- an integer array `nums`
- an integer `target`

You must choose three integers at distinct indices such that the sum is closest to `target`.

Then return that sum.

### Important details

- The three numbers must come from **distinct indices**.
- You are not returning the triplet itself.
- You are returning only the **sum** of the closest triplet.
- The problem guarantees that there is exactly one solution.

### What “closest” means

If one sum has a smaller absolute difference from the target than another sum, it is better.

For example, if the target is `1`:

- sum `0` is distance `1`
- sum `2` is distance `1`

Both are equally close.

In practice, the algorithm keeps the first or latest best value it finds depending on how the search moves, but the final answer is still valid as long as it is one of the closest sums.

---

## 3. Why Sorting Helps

Sorting the array is the key step that makes the problem efficient.

Once the array is sorted:

- if the current sum is too small, moving the left pointer right increases the sum,
- if the current sum is too large, moving the right pointer left decreases the sum.

That gives us a reliable way to move toward the target without checking every triplet manually.

---

## 4. Core Idea Of The Algorithm

The algorithm is built from two ideas:

1. fix one number at a time,
2. use two pointers to search for the best remaining pair.

### The plan

For each index `i`:

- treat `nums[i]` as the first number of the triplet,
- use `left` and `right` to find the other two numbers,
- compare each triplet sum with the target,
- keep the closest sum seen so far.

### Why this works

The sorted order lets us move the pointers in the right direction:

- if the sum is too small, increase it by moving `left` rightward,
- if the sum is too large, decrease it by moving `right` leftward.

This makes the search efficient and controlled.

---

## 5. Approach Used In `Solution.cs`

The implementation follows the standard sorted-array two-pointer pattern.

### Step 1: Sort the array

The input array is sorted first.

This allows the pointer movement rules to work correctly.

### Step 2: Initialize the best sum

Before searching, the algorithm sets `closestSum` to the sum of the first three numbers.

That gives it a valid starting point to compare against.

### Step 3: Fix one number at a time

For each index `i`:

- `nums[i]` is the first number in the triplet,
- `left = i + 1`,
- `right = nums.Length - 1`.

### Step 4: Compute the current sum

At each step:

```text
currentSum = nums[i] + nums[left] + nums[right]
```

Then compare it with the target.

### Step 5: Update the closest sum if needed

If the current sum is closer to the target than the previous best, store it.

### Step 6: Move the pointers

- If `currentSum < target`, move `left` rightward.
- If `currentSum > target`, move `right` leftward.
- If `currentSum == target`, return immediately because you cannot do better than an exact match.

---

## 6. Why This Logic Works

The sorted order gives the algorithm a clear direction.

### If the sum is too small

Increasing the left pointer moves to a larger number, which increases the sum.

### If the sum is too large

Decreasing the right pointer moves to a smaller number, which decreases the sum.

### Why no backtracking is needed

Because the array is sorted, each pointer movement changes the sum in a predictable direction.

That makes it unnecessary to try every possible pair explicitly.

This is why the two-pointer approach is much faster than brute force.

---

## 7. Example Walkthrough

### Example 1

```text
nums = [-1, 2, 1, -4]
target = 1
```

After sorting:

```text
[-4, -1, 1, 2]
```

### Start with the first triplet

The first three numbers give:

```text
-4 + -1 + 1 = -4
```

So the current best sum is `-4`.

### Fix `-4`

Use `left = -1` and `right = 2`:

```text
-4 + -1 + 2 = -3
```

This is closer to `1` than `-4`, so update the best sum to `-3`.

The sum is still too small, so move `left` rightward.

Now `left = 1`:

```text
-4 + 1 + 2 = -1
```

Update best sum to `-1`.

Still too small, so move `left` rightward again, but now `left >= right`, so this pass ends.

### Fix `-1`

Now use `-1` as the first value.

With `left = 1` and `right = 2`:

```text
-1 + 1 + 2 = 2
```

This is closer to `1` than the previous best `-1`.

So update the best sum to `2`.

This is the closest sum found, and in this case it is the correct answer.

---

## 8. More Problem-Specific Examples

### Example 2

```text
nums = [0, 0, 0]
target = 1
```

There is only one possible triplet:

```text
0 + 0 + 0 = 0
```

So the answer is:

```text
0
```

This example is useful because it shows that the closest sum may be below the target.

### Example 3

```text
nums = [1, 1, 1, 0]
target = -100
```

After sorting:

```text
[0, 1, 1, 1]
```

The smallest possible triplet sum is still much larger than `-100`, so the algorithm still returns the smallest achievable sum.

This shows that the target may be far outside the range of possible triplet sums.

### Example 4

```text
nums = [1, 1, -1, -1, 3]
target = -1
```

Sorted:

```text
[-1, -1, 1, 1, 3]
```

A good triplet is:

```text
-1 + -1 + 1 = -1
```

That is an exact match, so the algorithm can return immediately.

### Example 5

```text
nums = [-8, -6, -5, -3, 0, 2, 4, 7]
target = 3
```

One closest sum is:

```text
-3 + 0 + 7 = 4
```

Another very close candidate is:

```text
-5 + 2 + 4 = 1
```

The algorithm checks combinations in a structured way and keeps the best one.

### Example 6

```text
nums = [-1000, 0, 1, 2, 1000]
target = 3
```

A perfect match exists:

```text
0 + 1 + 2 = 3
```

So the algorithm returns `3` immediately once it finds that sum.

---

## 9. Why This Solution Is Good

The current `Solution.cs` is strong because it is:

- **efficient**
- **simple after sorting**
- **easy to reason about**
- **able to stop early on an exact match**
- **better than brute force by a large margin**

It follows the standard optimal approach for this problem.

---

## 10. Time And Space Complexity

### Time complexity

- Sorting takes `O(n log n)`
- The outer loop runs `O(n)` times
- The two-pointer scan inside runs `O(n)` total for each fixed value

Overall complexity:

```text
O(n^2)
```

### Space complexity

- The algorithm uses only a small amount of extra memory
- The sorting is done in place for the array

Overall extra space:

```text
O(1)
```

---

## 11. Common Mistakes

### 1. Forgetting to sort

Without sorting, the two-pointer movement rules do not work.

### 2. Not updating the best sum at every step

You should compare every current triplet sum with the best answer seen so far.

### 3. Returning only when the sum is too large or too small

You must also check the case where the current sum is the closest seen so far.

### 4. Not handling exact matches early

If the current sum equals the target, that is the best possible answer.

### 5. Using brute force only

A triple nested loop is much slower than the sorted two-pointer approach.

---

## 12. Study Topics And Learning Resources

### Topics to review

- Arrays
- Sorting
- Two pointers
- Absolute difference comparison
- Brute-force vs optimized solutions
- Combination search problems

### Useful learning resources

- C# arrays and `Array.Sort`
- Two-pointer technique tutorials
- Sorting-based search patterns
- LeetCode discussions on `3Sum Closest`

---

## 13. Final Takeaway

The main idea behind `3Sum Closest` is:

- sort the array,
- fix one number,
- use two pointers to search the remaining pair,
- keep track of the closest sum seen so far.

Once you understand how sorting and pointer movement work together, the problem becomes very manageable.

