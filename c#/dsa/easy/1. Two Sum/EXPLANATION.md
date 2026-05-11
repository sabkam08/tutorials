# 1. Two Sum — C# Explanation

## 1. How To Think About The Problem

At first glance, this problem looks like a simple search problem: you want two numbers that add up to a target. The main challenge is that you need to return the **indices**, not the values themselves, and you must do it efficiently.

A naive solution would be:

- check every pair of numbers,
- see whether they add up to the target,
- return the matching indices.

That works conceptually, but it is not the best solution because it takes quadratic time.

The key idea is to remember what you have already seen while scanning the array.

That is exactly what a hash map / dictionary is good at.

---

## 2. Understanding The Requirements Carefully

You are given:

- an integer array `nums`
- an integer `target`

You must return the indices of the two numbers such that:

- `nums[i] + nums[j] == target`
- `i != j`

### Important details

1. You may not use the same element twice.
2. The problem guarantees exactly one valid answer.
3. You can return the indices in any order.
4. The answer must be based on **indices**, not just the values.

That means the solution must remember both:

- the value,
- and where it appeared.

---

## 3. Why This Is A Dictionary Problem

When you are looking at a number `x`, the number you need is:

- `target - x`

That value is called the **complement**.

So instead of asking:

- “Which two numbers form the target?”

we ask:

- “Have I already seen the complement of the current number?”

That is a perfect use case for a dictionary because:

- lookup is fast,
- insertion is fast,
- and you only need to scan the array once.

---

## 4. Core Idea Of The Solution

As we move from left to right through `nums`:

- compute the complement of the current number,
- check whether that complement has already been seen,
- if yes, return the stored index and the current index,
- if not, store the current number with its index.

This ensures that when a valid pair appears, we detect it immediately.

### Why this avoids using the same element twice

We only look for the complement among **previously seen** numbers.

That means the current index is never matched with itself.

---

## 5. Approach Used In `Solution.cs`

The implementation in `Solution.cs` uses a `Dictionary<int, int>`.

### Step 1: Create a dictionary

The dictionary stores:

- key = number from the array
- value = index where that number appears

Example:

```text
value -> index
```

### Step 2: Scan the array once

For each element `nums[i]`:

- compute `complement = target - nums[i]`
- check whether `complement` already exists in the dictionary

### Step 3: Return the answer when found

If the complement is already in the dictionary:

- the stored index is one answer,
- the current index is the other answer.

### Step 4: Store the current value if needed

If the complement is not found:

- store `nums[i]` and its index.

### Why the code checks before storing

This is important.

If you store first and then check, you could accidentally match a number with itself when duplicates are involved.

By checking first, the algorithm preserves the rule that one element cannot be used twice.

---

## 6. How The Current Code Works

The current solution does the following:

```csharp
var seen = new Dictionary<int, int>();
```

This dictionary remembers numbers that have already been processed.

Then for each `nums[i]`:

```csharp
int complement = target - nums[i];
```

This gives the value we need to find.

Then:

```csharp
if (seen.TryGetValue(complement, out int index))
```

If the complement exists, the answer is found.

Otherwise:

```csharp
if (!seen.ContainsKey(nums[i]))
{
    seen[nums[i]] = i;
}
```

The current number is saved for future comparisons.

### Why keep only the first index for a number

If a number appears multiple times, the first index is enough because the problem guarantees one valid solution. Keeping the first occurrence is simple and safe.

---

## 7. Example Walkthrough

### Example 1

```text
nums = [2, 7, 11, 15]
target = 9
```

We scan from left to right.

#### Step 1

- current number = `2`
- complement = `9 - 2 = 7`
- `7` is not in the dictionary yet
- store `2 -> 0`

Dictionary:

```text
{ 2: 0 }
```

#### Step 2

- current number = `7`
- complement = `9 - 7 = 2`
- `2` is already in the dictionary
- stored index = `0`
- current index = `1`

So the answer is:

```text
[0, 1]
```

---

## 8. Another Example Walkthrough

### Example 2

```text
nums = [3, 2, 4]
target = 6
```

#### Step 1

- current number = `3`
- complement = `3`
- not found
- store `3 -> 0`

#### Step 2

- current number = `2`
- complement = `4`
- not found
- store `2 -> 1`

#### Step 3

- current number = `4`
- complement = `2`
- `2` is already stored at index `1`

Answer:

```text
[1, 2]
```

---

## 9. Duplicate Values Example

### Example 3

```text
nums = [3, 3]
target = 6
```

#### Step 1

- current number = `3`
- complement = `3`
- not found
- store `3 -> 0`

#### Step 2

- current number = `3`
- complement = `3`
- now `3` is in the dictionary
- answer = `[0, 1]`

This shows why it is useful to store what we have already seen.

---

## 10. Why This Logic Works

The solution works because for every index `i`:

- we check whether a valid partner for `nums[i]` has already appeared,
- and if so, we immediately have the answer.

This is correct because:

- the complement formula is exact,
- the dictionary lookup is exact,
- and the array is scanned in a way that guarantees no element is reused.

### Invariant idea

Before processing `nums[i]`, the dictionary contains exactly the values seen in earlier positions.

That means any complement found in the dictionary corresponds to a valid earlier index.

---

## 11. Time Complexity And Space Complexity

Let `n = nums.Length`.

### Time complexity

The array is scanned once.

Each dictionary lookup and insertion is average `O(1)`.

So the total time complexity is:

- **O(n)**

### Space complexity

In the worst case, the dictionary may store almost all values.

So the space complexity is:

- **O(n)**

This is the standard optimal approach for this problem.

---

## 12. Common Mistakes To Avoid

### Mistake 1: Using a nested loop

This gives a correct but slow solution:

- **O(n^2)** time

That may work on small examples, but it is not efficient.

### Mistake 2: Storing values after checking incorrectly

If you do the wrong order, you may accidentally match a number with itself.

The safe order is:

1. check whether the complement already exists,
2. then store the current number.

### Mistake 3: Forgetting to return indices

The problem asks for **indices**, not values.

### Mistake 4: Overwriting useful earlier indices

If the same number appears multiple times, overwriting may not be necessary. In this solution, keeping the first index is enough.

### Mistake 5: Ignoring the guarantee of one answer

The problem says exactly one solution exists. That means once you find a match, you can return immediately.

---

## 13. How To Approach Problems Like This In General

When you see a problem that asks for:

- a pair of values,
- a target sum,
- or a complement relationship,

think about whether you can remember previous elements while scanning once.

Ask yourself:

1. What value do I need to complete the target?
2. Have I seen it already?
3. Can I store the information in a hash map?
4. Can I solve this in one pass instead of comparing everything?

For Two Sum, the answer is yes.

---

## 14. Topics To Read On To Get Better

If you want to get better at problems like this, study these topics:

### Core algorithm topics

- Hash maps / dictionaries
- One-pass array scanning
- Complement-based search
- Pair sum problems
- Complexity analysis

### Helpful supporting topics

- Arrays and indexing
- Loop invariants
- Greedy thinking
- Memory vs speed tradeoffs

### C# topics worth practicing

- `Dictionary<TKey, TValue>`
- `TryGetValue`
- Array iteration
- Returning arrays from methods
- Console runner setup for local testing

---

## 15. Sources To Consult

Here are good places to learn more and improve your understanding.

### Official / high-quality references

1. **Microsoft Learn — C# documentation**
   - Great for learning `Dictionary`, arrays, and C# syntax.

2. **LeetCode discussions**
   - Useful for seeing multiple ways to solve Two Sum.

3. **GeeksforGeeks**
   - Good for hash map explanations and pair-sum problems.

4. **CP-Algorithms**
   - Helpful for building algorithmic thinking and understanding complexity.

5. **MIT OpenCourseWare / algorithm lectures**
   - Useful if you want a deeper foundation in problem solving.

### If you prefer video learning

- NeetCode
- Striver / takeUforward
- Abdul Bari
- freeCodeCamp algorithm playlists

### If you want structured practice

- LeetCode problem sets by topic
- HackerRank array and hashing tracks
- Codeforces educational rounds
- AtCoder beginner contests

---

## 16. A Good Study Plan For Problems Like This

If you want to improve steadily, try this order:

1. Learn hash maps well.
2. Practice one-pass array problems.
3. Solve Two Sum without looking at the answer.
4. Explain why the dictionary solution works.
5. Practice similar problems like Three Sum, Subarray Sum, and Anagram checks.
6. Compare your solution to the optimal solution.
7. Rewrite the solution from memory.

That last step is especially important.

If you can explain the complement logic clearly, you understand the problem well.

---

## 17. Final Summary

This problem is best solved by using a dictionary to remember previously seen numbers and their indices.

For each number:

- compute the complement,
- check whether it has been seen before,
- if yes, return the two indices,
- if not, store the current number.

The method in `Solution.cs` is efficient because it runs in linear time and uses linear space.

The main lessons from this problem are:

- think in terms of complements,
- use a dictionary for fast lookup,
- and scan the array once instead of comparing every pair.

