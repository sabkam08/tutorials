# 15. 3Sum — C# Explanation

## 1. How To Think About The Problem

At first glance, `3Sum` looks like a simple combination problem: find three numbers whose sum is `0`.

The hard part is not just finding a valid triplet — it is finding **all unique triplets** without returning duplicates.

A brute-force approach would try every group of three numbers. That works conceptually, but it is far too slow for the input size in this problem.

The important observation is that once the array is sorted, we can use a **two-pointer** strategy to search for matching pairs efficiently.

That gives us a much better solution.

---

## 2. Understanding The Requirements Carefully

You are given an integer array `nums`.

You must return all triplets `[nums[i], nums[j], nums[k]]` such that:

- `i != j`, `i != k`, and `j != k`
- `nums[i] + nums[j] + nums[k] == 0`

### Important requirement

The solution set must **not contain duplicate triplets**.

That means:

- `[-1, 0, 1]` should appear only once,
- `[-1, -1, 2]` should appear only once,
- even if those values can be formed using different indices.

### Why this matters

Many different index combinations can produce the same numeric triplet.

So the main challenge is not only finding valid triplets, but also skipping repeated values in a clean and reliable way.

---

## 3. Why Sorting Helps

The array is not sorted initially, but sorting it gives us two major advantages:

1. We can use two pointers to search efficiently.
2. We can easily skip duplicates.

After sorting, if we fix one number `nums[i]`, we can search for two other numbers whose sum is `-nums[i]`.

That turns the problem into a smaller version of the classic **two-sum** problem on a sorted array.

---

## 4. Core Idea Of The Algorithm

The algorithm works like this:

1. Sort the array.
2. Pick one number at a time as the first element of the triplet.
3. Use two pointers:
   - one pointer just after the fixed element,
   - one pointer at the end of the array.
4. Move the pointers inward until you find all valid pairs.
5. Skip duplicates at every step.

### Why this works

If the array is sorted:

- increasing the left pointer increases the sum,
- decreasing the right pointer decreases the sum.

That makes it possible to move in the correct direction without checking every pair blindly.

---

## 5. Approach Used In `Solution.cs`

The implementation follows the standard sorted-array two-pointer pattern.

### Step 1: Sort the array

Sorting puts equal values next to each other and makes pointer movement predictable.

### Step 2: Loop through each possible first value

For each index `i`:

- treat `nums[i]` as the first number in the triplet,
- search for two numbers after it that sum to `-nums[i]`.

### Step 3: Skip duplicate first values

If `nums[i] == nums[i - 1]`, then the same triplets would already have been found earlier.

So we continue to the next index.

### Step 4: Use two pointers

Set:

- `left = i + 1`
- `right = nums.Length - 1`

Then repeatedly check the sum:

- if the sum is too small, move `left` rightward,
- if the sum is too large, move `right` leftward,
- if the sum is `0`, record the triplet.

### Step 5: Skip duplicate pair values

After finding a valid triplet, move both pointers inward and skip over repeated values so the same triplet is not added again.

---

## 6. Why The Duplicate Handling Is Correct

Duplicate handling is the most important part of this problem.

There are two places where duplicates must be skipped:

### 1. Duplicate first elements

If `nums[i]` is the same as the previous number, then starting a new search from this value would generate the same triplets again.

So we skip it.

### 2. Duplicate pair elements

After a valid triplet is found, both pointers move inward.

If the new `left` value is the same as the previous `left` value, we skip it.

If the new `right` value is the same as the previous `right` value, we skip it.

This ensures each numeric triplet is added only once.

### Why this is safe

Because the array is sorted, equal values appear in blocks.

Skipping repeated values does not remove any new unique triplets; it only removes repeated copies of the same triplet.

---

## 7. Example Walkthrough

### Example 1

```text
nums = [-1, 0, 1, 2, -1, -4]
```

After sorting:

```text
[-4, -1, -1, 0, 1, 2]
```

### First useful fixed value: `-1`

Use `-1` as the first value.

We want two more values that sum to `1`.

- `left = -1`
- `right = 2`

Current sum:

```text
-1 + (-1) + 2 = 0
```

So we record:

```text
[-1, -1, 2]
```

Then we move both pointers and continue.

Next valid pair:

```text
-1 + 0 + 1 = 0
```

So we record:

```text
[-1, 0, 1]
```

These are the two unique triplets for the example.

### Why duplicates are avoided

The sorted array contains two `-1` values.

Without duplicate skipping, the same triplets would be produced more than once.

The algorithm prevents that.

---

## 8. More Problem-Specific Examples

### Example 2

```text
nums = [0, 1, 1]
```

There is no way to choose three numbers that sum to `0`.

So the answer is:

```text
[]
```

### Example 3

```text
nums = [0, 0, 0]
```

After sorting, the array is still:

```text
[0, 0, 0]
```

The only valid triplet is:

```text
[0, 0, 0]
```

Even though there are multiple ways to pick the indices, the result must contain this triplet only once.

### Example 4

```text
nums = [-2, 0, 1, 1, 2]
```

Valid triplets are:

```text
[-2, 0, 2]
[-2, 1, 1]
```

This example is useful because it shows:

- one fixed number can produce more than one valid triplet,
- duplicate values like `1` must be handled carefully.

### Example 5

```text
nums = [-2, 0, 0, 2, 2]
```

The only unique triplet is:

```text
[-2, 0, 2]
```

Even though there are repeated `0` and `2` values, the triplet should appear only once.

---

## 9. Why This Solution Is Good

The current `Solution.cs` is strong because it is:

- **correct**
- **efficient**
- **easy to reason about after sorting**
- **able to remove duplicates cleanly**
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

- The algorithm uses a small amount of extra memory for pointers and variables
- The returned list is part of the output

Overall extra space:

```text
O(1)
```

---

## 11. Common Mistakes

### 1. Forgetting to sort

Without sorting, the two-pointer strategy does not work correctly.

### 2. Not skipping duplicate first values

This causes repeated triplets in the result.

### 3. Not skipping duplicate `left` and `right` values after a match

This also causes duplicates.

### 4. Moving only one pointer after finding a triplet

Both pointers must move inward after a valid triplet is recorded.

### 5. Using brute force without considering efficiency

A triple nested loop may be too slow for larger arrays.

---

## 12. Study Topics And Learning Resources

### Topics to review

- Arrays
- Sorting
- Two pointers
- Duplicate handling
- Brute-force vs optimized solutions
- Combination search problems

### Useful learning resources

- C# arrays and `List<T>` documentation
- Two-pointer technique tutorials
- Sorting and duplicate skipping patterns
- LeetCode discussions on `3Sum`

---

## 13. Final Takeaway

The main idea behind `3Sum` is:

- sort the array,
- fix one number,
- use two pointers to find the other two numbers,
- skip duplicates carefully.

Once you understand how duplicate skipping works, the problem becomes much easier to solve reliably.

