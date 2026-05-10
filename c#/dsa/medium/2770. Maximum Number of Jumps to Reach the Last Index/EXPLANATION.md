# 2770. Maximum Number of Jumps to Reach the Last Index — C# Explanation

## 1. How To Think About The Problem

At first glance, this problem looks like a jumping simulation problem, but the important detail is that you are not trying to find the **fewest** jumps. You are trying to find the **maximum** number of jumps that still lets you reach the last index.

That changes the way you approach it.

If a problem asks for a maximum or minimum number of steps, it often means you should think in terms of:

- dynamic programming,
- shortest path / longest path ideas in a graph,
- state transitions,
- or greedy logic if the structure allows it.

For this problem, the cleanest solution is **dynamic programming**.

---

## 2. Understanding The Requirements Carefully

You are given:

- an array `nums`,
- an integer `target`,
- a starting position at index `0`,
- and a destination at index `n - 1`.

You may jump from index `i` to index `j` only when:

- `i < j`, so you always move forward,
- `|nums[j] - nums[i]| <= target`.

You must return:

- the maximum number of jumps that can reach the last index,
- or `-1` if the last index cannot be reached.

### Important observations

1. You can only move forward.
2. Every valid jump depends on the **current index** and the **value difference**.
3. A later jump may depend on whether an earlier jump was possible.
4. There may be multiple ways to reach the same index, and some ways may use more jumps than others.

That last point is why dynamic programming fits naturally.

---

## 3. Why This Is A Dynamic Programming Problem

Dynamic programming is useful when:

- the problem can be broken into smaller subproblems,
- the answer to a larger problem depends on answers to smaller problems,
- and the same subproblems may be reused many times.

For this problem, define:

- `dp[i]` = the maximum number of jumps needed to reach index `i`.

If index `i` is unreachable, we store `-1`.

Then:

- `dp[0] = 0` because you start at index `0` with zero jumps,
- for every reachable `i`, try jumping to every `j > i` that satisfies the constraint,
- update `dp[j]` if reaching `j` through `i` gives more jumps.

This is exactly what the solution does.

---

## 4. Approach Used In `Solution.cs`

The implementation uses a simple bottom-up DP approach.

### Step 1: Initialize the DP array

Create an array `dp` of length `n`.

- Fill all positions with `-1` to mean "unreachable".
- Set `dp[0] = 0` because the start position is already reached.

### Step 2: Try every valid forward jump

For each index `i`:

- if `dp[i] == -1`, skip it because it is unreachable,
- otherwise, try all `j > i`,
- if `Math.Abs(nums[j] - nums[i]) <= target`, then the jump is valid.

If valid:

- update `dp[j] = Math.Max(dp[j], dp[i] + 1)`.

This means:

- if reaching `j` through `i` gives a better jump count, keep it.

### Step 3: Return the answer

After checking all transitions:

- if `dp[n - 1]` is still `-1`, the end is unreachable,
- otherwise, `dp[n - 1]` is the maximum number of jumps.

---

## 5. Why This Logic Works

The logic works because every valid jump only moves forward.

That means when you are processing index `i`, all useful information about earlier positions has already been computed.

So if `dp[i]` is known:

- you can safely use it to improve future positions.

This is a classic forward DP pattern.

### Invariant idea

When the algorithm processes index `i`, `dp[i]` already represents the best known answer for reaching `i`.

From that state, all possible valid future jumps are explored.

Because we examine every reachable state and every valid transition, we do not miss any legal path.

---

## 6. Example Walkthrough

Let us take the first example:

```text
nums = [1, 3, 6, 4, 1, 2]
target = 2
```

### Initialization

```text
dp = [0, -1, -1, -1, -1, -1]
```

### From index 0

- to index 1: `|3 - 1| = 2`, valid
  - `dp[1] = 1`
- to index 2: `|6 - 1| = 5`, invalid
- to index 3: `|4 - 1| = 3`, invalid
- to index 4: `|1 - 1| = 0`, valid
  - `dp[4] = 1`
- to index 5: `|2 - 1| = 1`, valid
  - `dp[5] = 1`

Now:

```text
dp = [0, 1, -1, -1, 1, 1]
```

### From index 1

- to index 2: `|6 - 3| = 3`, invalid
- to index 3: `|4 - 3| = 1`, valid
  - `dp[3] = 2`
- to index 4: `|1 - 3| = 2`, valid
  - `dp[4] = max(1, 2) = 2`
- to index 5: `|2 - 3| = 1`, valid
  - `dp[5] = max(1, 2) = 2`

Now:

```text
dp = [0, 1, -1, 2, 2, 2]
```

### From index 3

- to index 4: `|1 - 4| = 3`, invalid
- to index 5: `|2 - 4| = 2`, valid
  - `dp[5] = max(2, 3) = 3`

Now:

```text
dp = [0, 1, -1, 2, 2, 3]
```

So the answer is `3`.

---

## 7. How To Approach Problems Like This In General

Whenever you see a problem with:

- a list or array,
- movement from one position to another,
- conditions for legal moves,
- and a request for maximum or minimum steps,

ask yourself:

1. Can this be modeled as a graph?
2. Can I define a state like `dp[i]`?
3. Do I only move in one direction?
4. Can I build the answer from smaller positions?

For this specific problem, the answer to those questions is yes.

That makes DP the natural approach.

---

## 8. Why The Implemented Solution Is Good

The current `Solution.cs` is good because it is:

- **simple**
- **correct**
- **easy to read**
- **easy to debug**
- **fast enough for the given constraints**

The constraints are only up to `1000` elements, so an `O(n^2)` solution is completely acceptable.

---

## 9. Time Complexity And Space Complexity

Let `n = nums.Length`.

### Time complexity

The algorithm checks all pairs `(i, j)` with `i < j`.

That gives:

- **O(n^2)** time

### Space complexity

The DP array uses:

- **O(n)** space

This is efficient and appropriate for the constraints.

---

## 10. Common Mistakes To Avoid

### Mistake 1: Thinking greedily

A greedy choice may look tempting, but the problem asks for the **maximum number of jumps**, not just any valid path.

A greedy jump can easily miss the optimal answer.

### Mistake 2: Forgetting unreachable states

If `dp[i] == -1`, you must skip index `i`.

Otherwise, you may try to build jumps from an impossible position.

### Mistake 3: Using the wrong comparison

The rule is:

- `|nums[j] - nums[i]| <= target`

Do not accidentally use only one direction like `nums[j] - nums[i] <= target` without the absolute value.

### Mistake 4: Updating the DP array incorrectly

Use:

- `dp[j] = Math.Max(dp[j], dp[i] + 1)`

That keeps the best answer for each index.

### Mistake 5: Returning the wrong result for unreachable end state

If `dp[n - 1]` is still `-1`, the answer must be `-1`.

---

## 11. How To Test Your Understanding

Before you say your solution is finished, check these points:

- Can index `0` always be reached? Yes, with `0` jumps.
- Do all transitions move forward? Yes.
- Do you test both reachable and unreachable cases? You should.
- Do you keep the best answer for every index? Yes.
- Do you handle equal values correctly when `target = 0`? Yes, because the absolute difference condition still works.

---

## 12. Topics To Read On To Get Better

If you want to improve at this type of problem, study these topics:

### Core algorithm topics

- Dynamic Programming
- Array traversal
- Graph modeling of array problems
- State transitions
- Complexity analysis
- Brute force vs optimized DP

### Helpful supporting topics

- Recursion and memoization
- Iterative bottom-up DP
- Prefix-style thinking
- Reachability problems
- Longest path in DAG-like structures

### C# topics worth practicing

- Arrays and loops
- `Math.Abs`
- `Math.Max`
- Default values in arrays
- Writing clear method-based solutions
- Using `Nullable` and clean project setup for LeetCode-style practice

---

## 13. Sources To Consult

Here are good places to learn and improve your understanding.

### Official / high-quality references

1. **Microsoft Learn — C# documentation**
   - Great for language syntax, arrays, loops, `Math`, and project setup.
   - Useful when you want to write cleaner C# code.

2. **LeetCode problem discussions**
   - Helpful for seeing different ways people model the same problem.
   - Good for comparing DP, greedy, and graph-based thinking.

3. **GeeksforGeeks**
   - Useful for learning dynamic programming patterns and common array techniques.

4. **CP-Algorithms**
   - Excellent for learning algorithmic thinking, graph concepts, and complexity analysis.

5. **MIT OpenCourseWare / free algorithm lectures**
   - Very helpful if you want deeper understanding of DP and problem solving.

### If you prefer video learning

- NeetCode
- Striver / takeUforward
- Abdul Bari
- freeCodeCamp algorithm playlists

### If you want structured practice

- LeetCode problem sets by topic
- HackerRank DP tracks
- Codeforces educational rounds
- AtCoder beginner contests

---

## 14. A Good Study Plan For Problems Like This

If you want to improve steadily, try this order:

1. Learn arrays and loops well.
2. Learn brute force problem solving.
3. Learn dynamic programming basics.
4. Practice reachability problems.
5. Practice graph thinking on arrays.
6. Review solutions and compare approaches.
7. Rewrite solved problems from memory.

That last step is especially helpful.

If you can explain the solution without looking at it, you really understand it.

---

## 15. Final Summary

This problem is best solved by dynamic programming:

- define `dp[i]` as the maximum number of jumps to reach index `i`,
- start with `dp[0] = 0`,
- try every valid forward jump,
- update future states with the best jump count,
- return `dp[n - 1]` or `-1` if unreachable.

The solution is simple, correct, and efficient enough for the problem constraints.

The main lessons from this problem are:

- model the state clearly,
- check reachability carefully,
- use the right transition rule,
- and think in terms of dynamic programming when a path depends on earlier choices.

