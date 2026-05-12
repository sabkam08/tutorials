# 1768. Merge Strings Alternately — C# Explanation

## 1. How To Think About The Problem

This problem looks simple, but the important part is not just combining two strings.

You must combine them in a very specific pattern:

- take one character from `word1`,
- then one character from `word2`,
- keep alternating,
- and if one string ends first, append the rest of the other string.

A naive approach would be to build the answer character by character using repeated string concatenation.

That would work conceptually, but it is not the best approach in C# because strings are immutable.

The clean solution is to build the result efficiently while walking through both strings.

---

## 2. Understanding The Requirements Carefully

You are given two strings:

- `word1`
- `word2`

You must return a string formed by alternating characters from the two strings, starting with `word1`.

If one string is longer, the leftover characters should be appended to the end.

### Important observations

1. The order of characters must be preserved.
2. The merge must start with `word1`.
3. The result should include every character from both strings.
4. The solution must handle different lengths cleanly.

These observations point toward a two-pointer traversal.

---

## 3. Why This Is A Two-Pointer String Problem

We are combining two ordered sequences.

That means we can keep:

- one pointer for `word1`,
- one pointer for `word2`.

Each step we decide whether to append the next character from each string.

This is a natural use case for a simple linear scan.

---

## 4. Core Idea Of The Solution

The current implementation uses:

```csharp
var result = new StringBuilder(word1.Length + word2.Length);
int i = 0;
int j = 0;
```

### Step 1: Create a `StringBuilder`

We need a way to build the result efficiently.

### Step 2: Keep two indexes

The variables `i` and `j` track our position in each string.

### Step 3: Loop until both strings are consumed

The loop continues while either string still has characters left.

### Step 4: Append from `word1` if possible

If `i < word1.Length`, append `word1[i]` and move `i` forward.

### Step 5: Append from `word2` if possible

If `j < word2.Length`, append `word2[j]` and move `j` forward.

### Step 6: Return the built string

Once both strings are exhausted, convert the builder to a string.

---

## 5. Why This Logic Works

The loop is designed so that every iteration tries to contribute at most one character from each string.

That guarantees the alternating pattern.

When one string runs out:

- the first `if` or second `if` simply stops appending from that string,
- but the loop continues until the other string is also fully processed.

That means no character is lost.

### Invariant idea

At any point in the loop:

- the result already contains the correctly merged prefix,
- `i` tells us how much of `word1` has been used,
- `j` tells us how much of `word2` has been used.

Because both pointers only move forward, we never revisit a character.

---

## 6. Example Walkthrough

### Example 1

```text
word1 = "abc"
word2 = "pqr"
```

Start with:

```text
result = ""
i = 0
j = 0
```

### First iteration

- append `word1[0] = 'a'`
- append `word2[0] = 'p'`

Result:

```text
"ap"
```

### Second iteration

- append `word1[1] = 'b'`
- append `word2[1] = 'q'`

Result:

```text
"apbq"
```

### Third iteration

- append `word1[2] = 'c'`
- append `word2[2] = 'r'`

Result:

```text
"apbqcr"
```

Final answer:

```text
"apbqcr"
```

---

## 7. Another Example Walkthrough

### Example 2

```text
word1 = "ab"
word2 = "pqrs"
```

Merge step by step:

- append `a`, then `p`
- append `b`, then `q`
- `word1` is now finished
- continue appending the rest of `word2`: `r`, `s`

Final answer:

```text
"apbqrs"
```

This shows why the leftover portion must still be added at the end.

---

## 8. Why The Implemented Solution Is Good

The current `Solution.cs` is strong because it is:

- simple,
- efficient,
- easy to read,
- and linear in the size of the input.

It avoids repeated string concatenation and uses `StringBuilder`, which is the right tool for this job.

---

## 9. Time Complexity And Space Complexity

Let:

- `n = word1.Length`
- `m = word2.Length`

### Time complexity

Each character is appended exactly once, so the runtime is:

- **O(n + m)**

### Space complexity

The output itself needs space for all characters, so the extra storage used by the builder is:

- **O(n + m)**

This is optimal because the final string must store all of those characters anyway.

---

## 10. Common Mistakes To Avoid

### Mistake 1: Using repeated string concatenation

Doing this inside a loop can create many temporary strings.

**Better:** use `StringBuilder`.

### Mistake 2: Forgetting leftover characters

If one string is longer, the rest still needs to be appended.

### Mistake 3: Off-by-one errors

It is easy to read past the end of a string if you do not check bounds first.

### Mistake 4: Starting with the wrong string

The alternation must begin with `word1`, not `word2`.

---

## 11. How To Approach Problems Like This In General

When you see a problem involving:

- two ordered inputs,
- building a combined output,
- preserving the original order,
- and handling leftover elements,

ask yourself:

1. Can I use two pointers?
2. Can I build the answer incrementally?
3. Can I avoid repeated string concatenation?
4. Can I stop only when both inputs are fully consumed?

For this problem, the answer is yes to all four.

---

## 12. Topics To Read On To Get Better

If you want to improve at problems like this, study these topics:

### Core algorithm topics

- Two pointers
- String traversal
- String building
- Linear scanning
- Complexity analysis

### Helpful supporting topics

- Boundary handling
- Index tracking
- Invariants
- Efficient output building

### C# topics worth practicing

- `StringBuilder`
- String immutability
- Indexing with `[]`
- Looping with bounds checks

---

## 13. Sources To Consult

Here are useful places to learn more and strengthen your understanding.

### Official / high-quality references

1. **Microsoft Learn — C# documentation**
   - Helpful for strings, `StringBuilder`, indexing, and language basics.

2. **LeetCode discussions**
   - Good for seeing alternate two-pointer and string-building explanations.

3. **GeeksforGeeks**
   - Useful for string merging and two-pointer style problems.

4. **CP-Algorithms**
   - Good for learning algorithm reasoning and efficient traversal patterns.

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

If you want to get better at string construction problems, try this order:

1. Learn when to use `StringBuilder`.
2. Practice two-pointer string problems.
3. Study how to handle leftover characters.
4. Re-solve this problem without looking.
5. Compare your answer to the explanation here.
6. Explain the merge process in your own words.

That last step is especially useful.

If you can clearly explain why the two pointers alternate correctly, then you understand the problem.

---

## 15. Final Summary

The best way to solve this problem is:

- keep one pointer for each string,
- append characters alternately,
- append leftover characters when one string ends,
- and return the built result.

This works because both strings are already ordered and we only need to interleave them while preserving their internal order.

The solution is efficient, clean, and uses `StringBuilder` to avoid unnecessary string allocations.

The main lessons from this problem are:

- use two pointers for ordered inputs,
- build strings efficiently in C#,
- check bounds before reading characters,
- and always think about leftover data when one input ends first.

