# 1665. Minimum Initial Energy to Finish Tasks — C# Explanation

## 1. How To Think About The Problem

This problem looks like a simulation problem at first: each task consumes energy, and each task also requires a minimum amount of energy before you can start it.

But the important part is not just whether a task can be done.

The real challenge is finding:

- the best order to do the tasks,
- and the smallest possible amount of energy needed at the very beginning.

A brute-force approach would be to try every order, simulate the energy usage, and keep the smallest answer.

That would be far too slow.

So the solution must use a greedy idea.

---

## 2. Understanding The Requirements Carefully

Each task is written as:

```text
[actualEnergy, minimumEnergy]
```

Where:

- `actualEnergy` = energy spent while completing the task,
- `minimumEnergy` = energy required before starting the task.

You may perform the tasks in any order.

You must return the minimum initial energy needed so that:

- every task can be started,
- and the energy never becomes too low to continue.

### Important observations

1. The order matters.
2. A task can be blocked by a large minimum requirement.
3. A task with a large energy cost can reduce what remains for later tasks.
4. The answer is about the smallest starting energy, not the final remaining energy.

---

## 3. Why This Is A Greedy Problem

To minimize the starting energy, we need a task order that is as favorable as possible.

One useful way to measure how demanding a task is:

```text
minimumEnergy - actualEnergy
```

The larger this value is, the more important it is to handle that task earlier in the execution order.

That is the greedy insight behind the solution.

---

## 4. Core Idea Of The Sorting Rule

The code sorts the tasks by:

```text
minimumEnergy - actualEnergy
```

in **descending** order.

### Why this helps

Tasks with a large gap between the minimum energy they require and the energy they consume should come earlier.

If we leave them too late, we may already have spent too much energy on other tasks, and then the starting energy requirement becomes larger.

This sort order is the key greedy choice.

---

## 5. How The Solution Computes The Answer

The implementation does this:

```csharp
long requiredEnergy = 0;

for (int i = tasks.Length - 1; i >= 0; i--)
{
	requiredEnergy = Math.Max(requiredEnergy + tasks[i][0], tasks[i][1]);
}
```

This is a backward computation.

### What `requiredEnergy` means

At any point, `requiredEnergy` means:

- the minimum energy needed **before** the tasks to the right of the current position are completed.

We process the sorted tasks from right to left so we can build the answer from the end back to the beginning.

### Why the formula is correct

Suppose the current task is `[actualEnergy, minimumEnergy]`.

To place this task before the tasks already accounted for, we need enough energy for two things:

1. We must have at least `minimumEnergy` before starting the task.
2. After spending `actualEnergy`, we must still have enough energy left for the later tasks.

So the starting energy must be at least:

```text
requiredEnergy + actualEnergy
```

Therefore the minimum valid energy before this task is:

```text
max(requiredEnergy + actualEnergy, minimumEnergy)
```

That is exactly what the code computes.

---

## 6. Why The Sorting Rule Is Correct

Take two tasks:

- `A = [aA, mA]`
- `B = [aB, mB]`

If:

```text
mA - aA >= mB - aB
```

then doing `A` before `B` is never worse than doing `B` before `A`.

### Pairwise comparison

If we do `A` then `B`, the required starting energy is:

```text
needAB = max(mA, aA + mB)
```

If we do `B` then `A`, the required starting energy is:

```text
needBA = max(mB, aB + mA)
```

Because `mA - aA >= mB - aB`, we get:

```text
mA + aB >= mB + aA
```

which is the same as:

```text
aA + mB <= aB + mA
```

That means the order `A` then `B` does not require more starting energy than `B` then `A`.

So sorting by `minimumEnergy - actualEnergy` descending is the correct greedy rule.

---

## 7. Example Walkthrough

### Example

```text
tasks = [[1, 3], [2, 5], [4, 8]]
```

Compute the gap for each task:

- `[1, 3]` → `3 - 1 = 2`
- `[2, 5]` → `5 - 2 = 3`
- `[4, 8]` → `8 - 4 = 4`

Sorted descending by gap:

```text
[4, 8], [2, 5], [1, 3]
```

Now process from right to left.

### Start

```text
requiredEnergy = 0
```

### Process `[1, 3]`

```text
requiredEnergy = max(0 + 1, 3) = 3
```

### Process `[2, 5]`

```text
requiredEnergy = max(3 + 2, 5) = 5
```

### Process `[4, 8]`

```text
requiredEnergy = max(5 + 4, 8) = 9
```

Final answer:

```text
9
```

That means the smallest energy you need at the beginning is `9`.

---

## 8. Why The Implemented Solution Is Good

The current `Solution.cs` is strong because it is:

- efficient,
- greedy,
- easy to compute,
- and uses only a few variables.

It avoids brute force completely.

It also uses `long` for the running requirement, which is safer than using `int` during intermediate calculations.

---

## 9. Time Complexity And Space Complexity

Let `n = tasks.Length`.

### Time complexity

- sorting: `O(n log n)`
- backward pass: `O(n)`

Total:

```text
O(n log n)
```

### Space complexity

The algorithm uses only a few variables:

```text
O(1)
```

---

## 10. Common Mistakes To Avoid

### Mistake 1: Trying every order

That would be too slow.

### Mistake 2: Sorting by the wrong value

The important key is:

```text
minimumEnergy - actualEnergy
```

### Mistake 3: Forgetting that order matters

Two tasks with the same numbers can still produce different answers depending on the order.

### Mistake 4: Using `int` for the running energy

The running value can grow, so `long` is safer.

### Mistake 5: Mixing up the two numbers in each task

Remember:

- `tasks[i][0]` = actual energy spent,
- `tasks[i][1]` = minimum energy required before starting.

---

## 11. How To Approach Problems Like This In General

When you see a problem involving:

- tasks,
- energy or resource usage,
- order-dependent constraints,
- and a choice of execution order,

ask yourself:

1. Can I sort the items by a meaningful priority?
2. Can I prove one order is better than another for two items?
3. Can I build the answer backward instead of forward?
4. Can I track only the minimum required state?

For this problem, the answer is yes to all four.

---

## 12. Topics To Read On To Get Better

### Core algorithm topics

- Greedy algorithms
- Custom sorting keys
- Scheduling problems
- Pairwise swap arguments
- Backward recurrence building
- Complexity analysis

### Helpful supporting topics

- Invariants
- Comparator functions
- Proof by contradiction
- Array processing

### C# topics worth practicing

- `Array.Sort` with a custom comparer
- `Math.Max`
- `long` vs `int`
- Writing small console runners

---

## 13. Sources To Consult

Here are useful places to learn more.

### Official / high-quality references

1. **Microsoft Learn — C# documentation**
   - Good for arrays, `Array.Sort`, `Math.Max`, and numeric types.

2. **LeetCode discussions**
   - Helpful for alternate greedy proofs and order-based reasoning.

3. **GeeksforGeeks**
   - Good for greedy and scheduling-style algorithm patterns.

4. **CP-Algorithms**
   - Excellent for algorithm thinking and proof techniques.

### If you prefer video learning

- NeetCode
- Striver / takeUforward
- Abdul Bari
- freeCodeCamp algorithm playlists

### If you want structured practice

- LeetCode topic-wise practice
- HackerRank greedy problems
- Codeforces educational rounds
- AtCoder beginner contests

---

## 14. A Good Study Plan For Problems Like This

If you want to get better at greedy problems, try this order:

1. Learn the idea of greedy choice.
2. Practice sorting by custom keys.
3. Learn how to prove an ordering with pairwise comparisons.
4. Study backward dynamic reasoning.
5. Re-solve this problem without looking.
6. Compare your proof to the solution here.
7. Explain the solution in your own words.

That last step is the most important.

If you can clearly explain why the sort key works, then you really understand the problem.

---

## 15. Final Summary

The best way to solve this problem is:

- sort tasks by `minimumEnergy - actualEnergy` in descending order,
- then compute the minimum required starting energy from right to left using:

```text
requiredEnergy = max(requiredEnergy + actualEnergy, minimumEnergy)
```

This works because the sorting rule places more demanding tasks earlier in the execution order, and the backward recurrence tells us the minimum energy needed to support the remaining tasks.

The solution is efficient, clean, and uses only constant extra space.

The main lessons from this problem are:

- use a greedy ordering,
- prove the order with a pairwise comparison,
- build the answer backward,
- and always think carefully about what the running state means.

