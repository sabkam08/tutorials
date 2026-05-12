# 14. Longest Common Prefix — C# Explanation

## 1. How To Think About The Problem

This problem looks simple at first, but the real challenge is finding the **longest shared starting segment** among all strings without doing unnecessary work.

A naive solution would be:

- compare every possible prefix,
- check whether it appears in every string,
- then keep the longest one that works.

That is conceptually correct, but it is more work than needed.

The key idea is to use the fact that any common prefix must also be a prefix of the first string.

That allows a clean shrinking approach.

---

## 2. Understanding The Requirements Carefully

You are given an array of strings:

- `strs`

You must return the longest prefix shared by **every** string in the array.

If no common prefix exists, return the empty string `""`.

### Important observations

1. The prefix must be common to all strings.
2. The answer must start at the beginning of each string.
3. If one string is shorter than the candidate prefix, the candidate is invalid.
4. If the array is empty, the answer should be the empty string.

These observations point toward a prefix-shrinking strategy.

---

## 3. Why This Is A Prefix-Shrinking Problem

If a prefix is too long for one string, then it is also too long for the final answer.

So instead of building a prefix from scratch, we can:

- start with the first string,
- compare it to every other string,
- shorten it until it matches all of them.

This is efficient because the prefix only ever gets smaller.

---

## 4. Core Idea Of The Solution

The current implementation follows this pattern:

```csharp
if (strs.Length == 0)
{
	return string.Empty;
}

string prefix = strs[0];
```

### Step 1: Handle the empty array

If there are no strings, there is no common prefix.

### Step 2: Use the first string as the candidate prefix

Any common prefix must be a prefix of the first string, so it is a natural starting point.

### Step 3: Compare against every other string

For each string, we check whether it starts with the current prefix.

If it does not, we remove the last character from the prefix and check again.

### Step 4: Stop early if the prefix becomes empty

If the prefix shrinks to `""`, then no common prefix exists.

---

## 5. Why This Logic Works

The logic works because the answer must satisfy two conditions:

- it must be a prefix of the first string,
- and it must also be a prefix of every other string.

If a string does not start with the current prefix, then that prefix is too long.

By shrinking the prefix one character at a time, we move toward the largest prefix that still works for all strings.

### Invariant idea

At every step, the current `prefix` is still a valid prefix of all strings we have already checked.

If a later string rejects it, we shrink it and try again.

Because the prefix only moves downward in length, we never miss the correct answer.

---

## 6. Example Walkthrough

### Example 1

```text
strs = ["flower", "flow", "flight"]
```

Start with:

```text
prefix = "flower"
```

Check against `"flow"`:

- `"flow"` does not start with `"flower"`
- shrink prefix to `"flowe"`
- still no match
- shrink to `"flow"`
- now it matches

Check against `"flight"`:

- `"flight"` does not start with `"flow"`
- shrink prefix to `"flo"`
- still no match
- shrink to `"fl"`
- now it matches

Final answer:

```text
"fl"
```

---

## 7. Another Example Walkthrough

### Example 2

```text
strs = ["dog", "racecar", "car"]
```

Start with:

```text
prefix = "dog"
```

Check against `"racecar"`:

- `"racecar"` does not start with `"dog"`
- shrink to `"do"`
- still no match
- shrink to `"d"`
- still no match
- shrink to `""`

Once the prefix becomes empty, the answer is immediately:

```text
""
```

---

## 8. Why The Implemented Solution Is Good

The current `Solution.cs` is strong because it is:

- easy to understand,
- efficient enough for the constraints,
- simple to implement,
- and uses only constant extra space.

It avoids unnecessary data structures and uses the sorted-like structure of prefixes directly.

---

## 9. Time Complexity And Space Complexity

Let:

- `n = strs.Length`
- `k = length of the prefix being checked`

### Time complexity

In the worst case, the algorithm checks many prefix characters across all strings:

- **O(n × k)**

This is the standard way to describe prefix shrinking.

### Space complexity

The algorithm uses only a few variables:

- **O(1)** extra space

That is optimal.

---

## 10. Common Mistakes To Avoid

### Mistake 1: Comparing only adjacent strings

The prefix must work for **all** strings, not just neighbors.

### Mistake 2: Forgetting the empty-array case

If the input array is empty, return `""` immediately.

### Mistake 3: Forgetting to stop when the prefix becomes empty

If the prefix is empty, there is no common prefix left.

### Mistake 4: Using a more complicated structure than needed

You do not need a trie, sorting, or heavy preprocessing for this problem.

### Mistake 5: Not handling strings of different lengths

A shorter string can force the prefix to shrink.

---

## 11. How To Approach Problems Like This In General

When you see a problem involving:

- prefixes,
- shared structure across multiple strings,
- and a need to find the longest valid common part,

ask yourself:

1. Can I start with one candidate and shrink it?
2. Can I use the first item as the initial guess?
3. Can I stop early when the candidate becomes invalid?
4. Can I avoid building unnecessary extra structures?

For this problem, the answer is yes to all four.

---

## 12. Topics To Read On To Get Better

If you want to improve at problems like this, study these topics:

### Core algorithm topics

- String processing
- Prefix logic
- Two-pointer style reasoning
- Early exit strategies
- Complexity analysis

### Helpful supporting topics

- Invariants
- Boundary handling
- Iterative shrinking methods
- Comparing strings efficiently

### C# topics worth practicing

- `string.StartsWith`
- Range slicing like `prefix[..^1]`
- `string.Empty`
- String comparison behavior

---

## 13. Sources To Consult

Here are useful places to learn more and strengthen your understanding.

### Official / high-quality references

1. **Microsoft Learn — C# documentation**
   - Helpful for strings, slicing, and general language behavior.

2. **LeetCode discussions**
   - Good for alternate prefix-based explanations.

3. **GeeksforGeeks**
   - Useful for string prefix problems and related patterns.

4. **CP-Algorithms**
   - Good for learning algorithm reasoning and clean proof structure.

### If you prefer video learning

- NeetCode
- Striver / takeUforward
- Abdul Bari
- freeCodeCamp algorithm playlists

### If you want structured practice

- LeetCode topic-wise practice
- HackerRank string problems
- Codeforces educational rounds
- AtCoder beginner contests

---

## 14. A Good Study Plan For Problems Like This

If you want to get better at prefix/string problems, try this order:

1. Learn the difference between prefix, substring, and subsequence.
2. Practice string comparison methods.
3. Study how to shrink a candidate solution safely.
4. Re-solve this problem without looking.
5. Compare your answer to the explanation here.
6. Explain the shrinking logic in your own words.

That last step is especially useful.

If you can clearly explain why the prefix shrinks and why it eventually stops, then you understand the problem.

---

## 15. Final Summary

The best way to solve this problem is:

- start with the first string as the candidate prefix,
- compare it with every other string,
- shrink it whenever it does not match,
- and return the final prefix when all strings agree.

This works because any common prefix must also be a prefix of the first string, and shrinking from there guarantees correctness.

The solution is simple, efficient, and uses only constant extra space.

The main lessons from this problem are:

- use the structure of the data directly,
- shrink the candidate instead of exploring everything,
- handle edge cases early,
- and always think about what must remain true while the algorithm runs.

