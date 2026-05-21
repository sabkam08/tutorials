# 11. Container With Most Water — C# Explanation

## 1. How To Think About The Problem

At first glance, this problem looks like it might require checking every possible pair of lines.

That would mean:

- choose one line,
- choose another line,
- compute the water container between them,
- keep the best result.

That brute-force idea is easy to understand, but it is not efficient enough for a strong solution.

The key observation is that the amount of water depends on two things:

- the **width** between the two lines,
- the **shorter** of the two heights.

So the real challenge is figuring out how to move the two boundaries in a smart way without checking every pair.

That is what makes the **two-pointer technique** the natural solution here.

---

## 2. Understanding The Requirements Carefully

You are given an array `height`, where:

- each value represents the height of a vertical line,
- the x-axis acts as the base of the container,
- two lines together with the x-axis form a container,
- the container can hold water only up to the height of the shorter line.

### Important rules

- You may not slant the container.
- You need the **maximum** amount of water.
- The answer depends on both height and distance.

### What the container area means

If one line is at index `left` and another is at index `right`, then:

- width = `right - left`
- height = `Math.Min(height[left], height[right])`
- area = `width * height`

That formula is the core of the problem.

---

## 3. Why The Brute Force Idea Is Not Ideal

A brute-force solution tries all pairs.

If there are `n` lines, then there are about:

- `n * (n - 1) / 2` pairs

That gives a time complexity of **O(n²)**.

For large inputs, that is too slow.

We want a better way.

---

## 4. Core Insight Behind The Two-Pointer Solution

The area is limited by the **shorter** line.

Suppose you have two lines:

- left height = `h1`
- right height = `h2`

If `h1 < h2`, then the area is limited by `h1`.

Now ask:

- if we keep the shorter line and move the taller one inward, can the area improve?

The width becomes smaller, and the limiting height stays the same.

So the area cannot get better in that case.

That means:

- the only useful move is to move the pointer at the **shorter** line.

This is the key logic.

---

## 5. Approach Used In `Solution.cs`

The solution uses two pointers:

- `left` starts at the beginning of the array,
- `right` starts at the end of the array.

### Step 1: Start with the widest possible container

The initial pair uses the outermost lines.

That gives the maximum possible width.

### Step 2: Compute the area

At each step:

- calculate the current width,
- calculate the height using the smaller of the two lines,
- update the best area seen so far.

### Step 3: Move the shorter line

If `height[left] < height[right]`, move `left` one step to the right.

Otherwise, move `right` one step to the left.

This is because only the shorter line can potentially improve the limiting height.

### Step 4: Repeat until the pointers meet

Continue until `left >= right`.

At that point, all useful pairs have been considered through the pointer strategy.

---

## 6. Why This Logic Works

This approach works because the area is controlled by the shorter boundary.

When you move the taller side inward:

- the width decreases,
- the limiting height does not improve,
- so the area cannot increase in a meaningful way.

When you move the shorter side inward:

- the width decreases,
- but the new shorter side might be taller,
- so there is a chance to get a larger area.

That is why the algorithm always moves the shorter pointer.

### Intuition in one sentence

You are trying to trade width for a potentially better height.

---

## 7. Example Walkthrough

### Example 1

```text
height = [1,8,6,2,5,4,8,3,7]
```

Start with:

- `left = 0`
- `right = 8`

Area:

- width = `8`
- height = `min(1, 7) = 1`
- area = `8 * 1 = 8`

Since `1 < 7`, move `left`.

Now:

- `left = 1`
- `right = 8`

Area:

- width = `7`
- height = `min(8, 7) = 7`
- area = `49`

This is currently the best answer.

Since `8 > 7`, move `right`.

The algorithm continues checking better possibilities until the pointers meet.

The final answer is `49`.

---

## 8. Another Small Example

### Example 2

```text
height = [1,1]
```

There is only one possible container:

- width = `1`
- height = `1`
- area = `1`

So the answer is `1`.

---

## 9. Why The Implemented Solution Is Good

The current `Solution.cs` is strong because it is:

- **simple**
- **correct**
- **fast**
- **uses only O(1) extra space**
- **easy to reason about once you understand the pointer rule**

This is the standard optimal solution for this problem.

---

## 10. Time Complexity And Space Complexity

### Time complexity

The two pointers each move inward at most `n - 1` times.

So the runtime is:

- **O(n)**

### Space complexity

The solution uses only a few variables:

- `left`
- `right`
- `maxArea`
- temporary width/area values

So the space usage is:

- **O(1)**

---

## 11. Common Mistakes To Avoid

### Mistake 1: Checking every pair

That works, but it is too slow.

### Mistake 2: Moving the taller pointer

That usually does not help, because the shorter side is the bottleneck.

### Mistake 3: Forgetting that width matters

A very tall pair that is too close together may hold less water than a wider pair with slightly smaller heights.

### Mistake 4: Using the wrong formula

Remember:

- area = `min(left height, right height) * distance`

### Mistake 5: Stopping too early

You must continue until the pointers cross or meet.

---

## 12. How To Approach Problems Like This In General

When you see a problem involving:

- maximizing or minimizing something between two ends,
- a sorted or ordered scan not necessarily required,
- a decision based on which side is the bottleneck,
- or a pair of pointers that can move inward,

ask yourself:

1. What limits the answer?
2. Which pointer should move to possibly improve that limit?
3. Can I discard one side safely after each step?
4. Can I avoid checking every pair?

For this problem, the answer is yes.

---

## 13. Topics To Read On To Get Better

If you want to improve at problems like this, study these topics:

### Core algorithm topics

- Two pointers
- Greedy reasoning
- Array scanning
- Optimization by eliminating impossible choices
- Complexity analysis

### Helpful supporting topics

- Brute force vs optimized solutions
- Invariants
- Boundary reasoning
- Problem reduction

### C# topics worth practicing

- Arrays and indexing
- `Math.Min`
- Writing clean loop logic
- Small runner programs for testing solutions

---

## 14. Sources To Consult

Here are good places to learn and improve your understanding.

### Official / high-quality references

1. **Microsoft Learn — C# documentation**
   - Useful for syntax, arrays, and `Math` usage.

2. **LeetCode problem discussions**
   - Good for seeing alternative two-pointer explanations.

3. **GeeksforGeeks**
   - Helpful for two-pointer patterns and array optimization problems.

4. **CP-Algorithms**
   - Good for developing algorithmic thinking and invariants.

5. **MIT OpenCourseWare / free algorithm lectures**
   - Helpful for understanding how greedy choices are justified.

### If you prefer video learning

- NeetCode
- Striver / takeUforward
- Abdul Bari
- freeCodeCamp algorithm playlists

### If you want structured practice

- LeetCode problem sets by topic
- HackerRank array tracks
- Codeforces educational rounds
- AtCoder beginner contests

---

## 15. A Good Study Plan For Problems Like This

If you want to improve steadily, try this order:

1. Learn the two-pointer technique.
2. Practice problems where one side is a bottleneck.
3. Study how to justify greedy moves.
4. Re-solve the problem without looking.
5. Compare your logic to the optimal solution.
6. Explain the reasoning in your own words.

That last step is especially useful.

If you can explain why moving the shorter pointer is correct, you understand the problem.

---

## 16. Final Summary

This problem is best solved with two pointers:

- start from both ends,
- compute the area,
- move the shorter pointer inward,
- keep track of the maximum area.

The method in `Solution.cs` is the optimal approach because it runs in linear time and constant space.

The main lessons from this problem are:

- the shorter line controls the water level,
- width and height must both be considered,
- and moving the shorter pointer is the only move that can potentially improve the answer.

