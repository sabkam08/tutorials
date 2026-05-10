# 4. Median of Two Sorted Arrays — C# Explanation

## 1. How To Think About The Problem

This problem looks simple at first because the input arrays are already sorted, but the real challenge is that you are asked to find the median of the **combined** data without fully merging the arrays in a slow way.

A naive solution would be:

- merge both arrays,
- sort or combine them,
- then compute the median.

That works conceptually, but it is not the best answer because the problem asks for an efficient solution.

The key idea is to use the fact that both arrays are already sorted.

That allows a much faster approach using **binary search**.

---

## 2. Understanding The Requirements Carefully

You are given two sorted integer arrays:

- `nums1`
- `nums2`

You must return the median of the combined values.

### Median reminder

For a sorted sequence:

- if the total length is odd, the median is the middle element,
- if the total length is even, the median is the average of the two middle elements.

### Important observations

1. The arrays are already sorted.
2. You do not need the full merged array.
3. The median depends on the elements around the middle.
4. Because the arrays are sorted, the correct partition can be found efficiently.

That last point is what makes the binary search approach possible.

---

## 3. Why This Is A Binary Search Problem

The problem can be transformed into finding a split between the two arrays such that:

- everything on the left side is less than or equal to everything on the right side,
- and the left side contains half of the combined elements.

If we can find such a partition, then the median is easy to compute from the partition boundaries.

This is a classic application of binary search on the smaller array.

### Why search the smaller array

We always binary search the smaller array because:

- it keeps the search space small,
- it avoids invalid partition indices more easily,
- and it gives the optimal logarithmic runtime.

---

## 4. Core Idea Of The Partition

Suppose we split the two arrays into left and right parts.

We want:

- left part size = half of the total elements,
- all values in the left part <= all values in the right part.

If that condition is true:

- for an odd total length, the median is the largest value on the left side,
- for an even total length, the median is the average of:
  - the largest value on the left side,
  - the smallest value on the right side.

### The four boundary values

When we try a partition, we look at:

- `maxLeft1` = biggest value on the left side of `nums1`
- `minRight1` = smallest value on the right side of `nums1`
- `maxLeft2` = biggest value on the left side of `nums2`
- `minRight2` = smallest value on the right side of `nums2`

The partition is correct when:

- `maxLeft1 <= minRight2`
- `maxLeft2 <= minRight1`

If both are true, we found the correct split.

---

## 5. Approach Used In `Solution.cs`

The implementation uses a standard binary search partition method.

### Step 1: Make sure `nums1` is the smaller array

If `nums1` is longer than `nums2`, the method swaps them.

This is important because the binary search is performed on `nums1`.

### Step 2: Compute the size of the left partition

Let:

- `m = nums1.Length`
- `n = nums2.Length`

The number of elements that must be on the left side is:

- `(m + n + 1) / 2`

The `+1` helps handle odd total lengths cleanly.

### Step 3: Binary search over `nums1`

We choose a partition in `nums1` and compute the matching partition in `nums2`.

Then we check whether the partition is valid.

### Step 4: Evaluate the partition boundaries

For each partition:

- if the left side of `nums1` is too large, move the partition left,
- otherwise, move it right.

Eventually we find the correct split.

### Step 5: Compute the median

Once the correct partition is found:

- if total length is odd, return the maximum of the two left boundary values,
- if total length is even, return the average of:
  - the maximum left boundary,
  - the minimum right boundary.

---

## 6. Why This Logic Works

The arrays are sorted, so if a partition is valid, then the median must lie at the boundary between left and right halves.

The binary search works because:

- increasing the partition in one array changes the matching partition in the other array,
- the validity of the partition gives a clear direction:
  - move left if the left side is too big,
  - move right if the left side is too small.

That gives a monotonic decision path, which is exactly what binary search needs.

### Invariant idea

When the algorithm tests a partition:

- it checks whether the left side contains the correct elements,
- and whether both sides are ordered properly relative to each other.

Because the arrays are sorted, these boundary checks are enough to determine correctness.

---

## 7. Example Walkthrough

### Example 1

```text
nums1 = [1, 3]
nums2 = [2]
```

The combined sorted sequence would be:

```text
[1, 2, 3]
```

The median is `2`.

### How the partition method sees it

We make sure the smaller array is used for binary search.

Let:

- `nums1 = [2]`
- `nums2 = [1, 3]`

Total length = 3, so the left side must contain 2 elements.

Try partitioning `nums1` at index 0:

- left of `nums1` = empty
- right of `nums1` = `[2]`
- left of `nums2` = `[1, 3]` would be too many on the left, so the partition is not correct.

Try partitioning `nums1` at index 1:

- left of `nums1` = `[2]`
- right of `nums1` = empty
- left of `nums2` = `[1]`
- right of `nums2` = `[3]`

Now:

- left max = `max(2, 1) = 2`
- right min is not needed for odd length

So the median is `2`.

---

## 8. Even-Length Example

### Example 2

```text
nums1 = [1, 2]
nums2 = [3, 4]
```

Combined sorted sequence:

```text
[1, 2, 3, 4]
```

The middle two values are `2` and `3`, so the median is:

```text
(2 + 3) / 2 = 2.5
```

The partition method finds exactly those boundary values.

---

## 9. Why The Implemented Solution Is Good

The current `Solution.cs` is strong because it is:

- **efficient**
- **correct**
- **uses only O(1) extra space**
- **works in logarithmic time**
- **handles odd and even lengths cleanly**

This is the standard optimal solution for this problem.

---

## 10. Time Complexity And Space Complexity

Let:

- `m = nums1.Length`
- `n = nums2.Length`

### Time complexity

Binary search is performed on the smaller array, so the runtime is:

- **O(log(min(m, n)))**

### Space complexity

The solution uses only a few variables:

- **O(1)** space

That is optimal.

---

## 11. Common Mistakes To Avoid

### Mistake 1: Merging both arrays first

That is simpler to think about, but it wastes extra time and space.

### Mistake 2: Binary searching the larger array

You should always search the smaller array to keep the logic safe and efficient.

### Mistake 3: Using the wrong left partition size

The left partition must contain:

- `(m + n + 1) / 2`

That formula is important for both odd and even totals.

### Mistake 4: Forgetting boundary cases

When a partition lands at the start or end of an array, use sentinel values:

- `int.MinValue`
- `int.MaxValue`

This avoids index errors.

### Mistake 5: Mixing up odd and even cases

For odd totals:

- median = `max(left side)`

For even totals:

- median = average of `max(left side)` and `min(right side)`

---

## 12. How To Approach Problems Like This In General

When you see a problem involving:

- sorted arrays,
- finding a middle point,
- minimizing or maximizing a boundary condition,
- or needing a result without full merging,

ask yourself:

1. Can I use the sorted property directly?
2. Can I search for a partition rather than build the whole output?
3. Can binary search narrow down the answer?
4. Can I use boundary values to simplify edge cases?

For this problem, the answer is yes to all four.

---

## 13. Topics To Read On To Get Better

If you want to improve at problems like this, study these topics:

### Core algorithm topics

- Binary Search
- Partition-based algorithms
- Sorted array problems
- Array merging logic
- Median and percentile concepts
- Complexity analysis

### Helpful supporting topics

- Two pointers
- Divide and conquer
- Invariants
- Boundary handling
- Sentinel values

### C# topics worth practicing

- Arrays and slicing concepts
- `Math.Max` and `Math.Min`
- Integer division behavior
- `double` arithmetic
- Writing clean console runners for testing

---

## 14. Sources To Consult

Here are good places to learn and improve your understanding.

### Official / high-quality references

1. **Microsoft Learn — C# documentation**
   - Useful for syntax, arrays, `double`, `Math`, and project setup.

2. **LeetCode problem discussions**
   - Great for seeing alternate partition-based explanations.

3. **GeeksforGeeks**
   - Good for binary search and median-related algorithm patterns.

4. **CP-Algorithms**
   - Excellent for learning binary search reasoning and correctness thinking.

5. **MIT OpenCourseWare / free algorithm lectures**
   - Helpful if you want deeper understanding of algorithm design.

### If you prefer video learning

- NeetCode
- Striver / takeUforward
- Abdul Bari
- freeCodeCamp algorithm playlists

### If you want structured practice

- LeetCode problem sets by topic
- HackerRank binary search tracks
- Codeforces educational rounds
- AtCoder beginner contests

---

## 15. A Good Study Plan For Problems Like This

If you want to improve steadily, try this order:

1. Learn binary search deeply.
2. Practice sorted array problems.
3. Learn partition-based reasoning.
4. Study edge case handling with sentinels.
5. Re-solve the problem without looking.
6. Compare your solution to an optimal one.
7. Explain the solution in your own words.

That last step is especially useful.

If you can explain the partition logic clearly, you understand the problem.

---

## 16. Final Summary

This problem is best solved by binary search on the smaller array:

- search for a valid partition,
- ensure the left side and right side are properly ordered,
- compute the median from the partition boundaries.

The method in `Solution.cs` is the optimal approach because it runs in logarithmic time and constant space.

The main lessons from this problem are:

- use the sorted property,
- search for the partition instead of merging,
- handle odd and even lengths carefully,
- and always think about boundary values in partition problems.

