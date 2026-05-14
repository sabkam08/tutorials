# 10. Regular Expression Matching — C# Explanation

## 1. How To Think About The Problem

This problem looks like string matching, but it is more subtle than ordinary substring search because the pattern contains two special symbols:

- `.` which can match any single character,
- `*` which means zero or more of the previous element.

That makes the problem a lot more than a simple character-by-character comparison.

A greedy approach is tempting, but it is easy to get stuck because `*` can represent many different possibilities. The same pattern can match a string in multiple ways, so we need a method that can systematically explore the valid choices.

That is why this problem is a classic **dynamic programming** problem.

The main idea is:

- compare the string and pattern from the current positions,
- decide whether the current characters match,
- and if there is a `*`, consider both:
  - using zero copies of the preceding element,
  - or using one more copy and staying on the same pattern position.

That choice structure is exactly what DP handles well.

---

## 2. Understanding The Requirements Carefully

You are given:

- a string `s`,
- a pattern `p`.

You must return `true` if the **entire** string matches the **entire** pattern.

### Important details

This is **not** partial matching.

For example:

- `s = "aa"`, `p = "a"` should return `false` because the pattern only matches the first character.
- `s = "ab"`, `p = ".*"` should return `true` because `.*` can match the whole string.

### Meaning of the special symbols

- `.` matches exactly one character.
- `*` applies to the character before it.
  - `a*` means zero or more `a` characters.
  - `.*` means zero or more of any character.

### Important restriction

Every `*` has a valid previous character, so you do not need to handle invalid patterns like `*a`.

---

## 3. Why This Is A Dynamic Programming Problem

The tricky part is that `*` creates branching behavior.

For example, if the pattern is `a*`, it could match:

- nothing,
- `a`,
- `aa`,
- `aaa`,
- and so on.

So when you see a `*`, you do not have just one obvious path.

That means the question is really:

> Can the suffix of `s` starting at position `i` match the suffix of `p` starting at position `j`?

That is a perfect DP subproblem.

### Why recursion alone is not enough

A pure recursive solution would repeatedly recheck the same suffix pairs many times.

For example:

- `IsMatch(i, j)` may call `IsMatch(i + 1, j)`,
- and another branch may reach the same state later.

That repeated work can explode exponentially.

DP removes that repeated work by storing results for each suffix pair.

---

## 4. The Core DP State

The implementation uses a 2D bottom-up DP table:

- `dp[i, j]` means:
  - does `s[i..]` match `p[j..]`?

Here:

- `i` ranges from `0` to `s.Length`,
- `j` ranges from `0` to `p.Length`.

### Why suffix-based DP works well

When we decide whether two current positions match, the answer depends only on:

- the current character(s),
- and the result of smaller suffixes.

That makes the problem naturally suitable for suffix DP.

---

## 5. Base Case

The base case is:

- `dp[m, n] = true`

where:

- `m = s.Length`,
- `n = p.Length`.

This means:

- an empty string matches an empty pattern.

That is the foundation of the entire DP table.

### Why this is important

When both suffixes are empty, the match is complete.

All other states build from this result.

---

## 6. How The Transition Works

The solution fills the table from the end of the string and pattern toward the beginning.

For each state `(i, j)`, it first checks whether the current characters match:

```csharp
bool firstMatch = i < m && (p[j] == s[i] || p[j] == '.');
```

This means:

- there is still a character left in `s`, and
- either:
  - the pattern character equals the string character, or
  - the pattern character is `.`.

### Case 1: The next pattern character is `*`

If `p[j + 1] == '*'`, then the current character in the pattern can be repeated zero or more times.

That gives two possibilities:

#### Option A: Use zero copies of the preceding element

Skip the character and the `*`:

```csharp
dp[i, j] = dp[i, j + 2]
```

This means:

- ignore `x*` completely,
- and see whether the rest of the pattern matches the same string suffix.

#### Option B: Use one matching character

If the current characters match, consume one character from `s` but keep the pattern at the same position:

```csharp
firstMatch && dp[i + 1, j]
```

This works because `x*` can still match more characters.

#### Combined rule

The final rule is:

```csharp
dp[i, j] = dp[i, j + 2] || (firstMatch && dp[i + 1, j]);
```

---

### Case 2: The next pattern character is not `*`

Then the current character must match exactly one pattern character.

So:

- if `firstMatch` is true, move both indices forward by one,
- otherwise the match fails.

That becomes:

```csharp
dp[i, j] = firstMatch && dp[i + 1, j + 1];
```

---

## 7. Why The Bottom-Up Order Works

The DP table is filled from the end toward the beginning:

- `i` goes from `m` down to `0`,
- `j` goes from `n - 1` down to `0`.

This order works because each state depends only on states that are already known:

- `dp[i, j + 2]`
- `dp[i + 1, j]`
- `dp[i + 1, j + 1]`

Those are all to the right or below the current position in the table.

### Why the last row and last column matter

When `i == m`, the string suffix is empty.

When `j == n`, the pattern suffix is empty.

The base case `dp[m, n] = true` anchors the solution.

---

## 8. Understanding The Implemented Solution

The implementation in `Solution.cs` follows this exact logic:

### Step 1: Initialize the table

```csharp
bool[,] dp = new bool[m + 1, n + 1];
dp[m, n] = true;
```

This creates a table with one extra row and column for the empty suffix.

### Step 2: Fill the table backward

The loops go backward so that future states are already computed when needed.

### Step 3: Compute `firstMatch`

```csharp
bool firstMatch = i < m && (p[j] == s[i] || p[j] == '.');
```

This checks whether the current positions can match a single character.

### Step 4: Handle `*`

If the next pattern character is `*`, the code uses the two-branch DP recurrence.

### Step 5: Handle normal characters

If there is no `*`, then the current characters must match once, and both indices advance.

### Step 6: Return the final answer

```csharp
return dp[0, 0];
```

This means:

- does the entire string match the entire pattern?

---

## 9. Example Walkthrough

### Example 1

```text
s = "aa"
p = "a"
```

The pattern has only one `a`, so it can only match one character.

The first character matches, but one `a` remains in the string.

So the answer is `false`.

### How the DP sees it

- `a` matches the first `a`
- then the pattern ends too early
- the string still has another `a`
- therefore the full match fails

---

### Example 2

```text
s = "aa"
p = "a*"
```

The `*` means the `a` can be repeated zero or more times.

So `a*` can match:

- `""`
- `"a"`
- `"aa"`
- `"aaa"`
- and so on

For `"aa"`, the best choice is to use two `a` characters.

So the answer is `true`.

### DP interpretation

At the `a*` state, the algorithm checks:

- skip `a*` entirely, or
- consume one `a` and stay on the same pattern state.

That flexibility is what makes the match succeed.

---

### Example 3

```text
s = "ab"
p = ".*"
```

The `.` matches any character, and `*` allows repetition.

So `.*` can match any string, including `"ab"`.

The answer is `true`.

### DP interpretation

At `.`:

- `firstMatch` is true for both `a` and `b`.

At `*`:

- the algorithm can keep consuming characters until the whole string is covered.

---

## 10. Why The Implemented Solution Is Correct

This solution is correct because it handles every possible pattern structure using the exact rules of the problem.

### Correct handling of `.`

The solution treats `.` as a wildcard for exactly one character.

### Correct handling of `*`

The solution considers both valid interpretations:

- zero occurrences,
- one or more occurrences.

### Correct handling of full-string matching

The final answer comes from `dp[0, 0]`, which represents the entire string and the entire pattern.

That ensures partial matches do not count.

### Correct handling of boundaries

The code checks `i < m` before reading `s[i]`, which prevents out-of-range access.

---

## 11. Time Complexity And Space Complexity

Let:

- `m = s.Length`
- `n = p.Length`

### Time complexity

Each DP state is computed once, and there are `(m + 1) * (n + 1)` states.

So the time complexity is:

- **O(m * n)**

### Space complexity

The DP table stores all states.

So the space complexity is:

- **O(m * n)**

This is a standard and acceptable solution for the given constraints.

---

## 12. Common Mistakes To Avoid

### Mistake 1: Treating `*` as matching the character itself

`*` does not stand alone.

It always modifies the character before it.

### Mistake 2: Forgetting the zero-occurrence case

For `a*`, you must consider the possibility that it matches nothing.

Without that branch, many valid matches fail.

### Mistake 3: Only trying greedy matching

A greedy approach can get stuck because the correct match may require backtracking.

### Mistake 4: Forgetting full-string matching

The problem is not asking whether `p` appears anywhere in `s`.

The entire string must be matched.

### Mistake 5: Reading outside array bounds

Always check whether `i < m` before using `s[i]`.

---

## 13. How To Approach Problems Like This In General

When you see a string problem with special operators like `.` or `*`, ask yourself:

1. Does the current choice affect future choices?
2. Can the same subproblem occur multiple times?
3. Do I need to consider multiple interpretations of a symbol?
4. Would recursion naturally describe the problem?
5. If recursion works, can I memoize or convert it to DP?

For this problem, the answer is yes to all of those.

That is a strong signal that DP is the right direction.

---

## 14. Topics To Read On To Get Better

If you want to improve at problems like this, study these topics:

### Core algorithm topics

- Dynamic Programming
- Memoization vs tabulation
- String matching
- Recurrence relations
- State transitions
- Backtracking and pruning

### Helpful supporting topics

- Recursion
- Regular expression basics
- Boundary handling
- Boolean DP tables
- Problem decomposition

### C# topics worth practicing

- Multi-dimensional arrays
- Boolean expressions
- `string` indexing
- Writing clean console runners
- Reading and understanding recursive DP code

---

## 15. Sources To Consult

Here are some good places to study this type of problem.

### Official / high-quality references

1. **Microsoft Learn — C# documentation**
   - Useful for arrays, strings, booleans, and general C# syntax.

2. **LeetCode discussions**
   - Helpful for seeing how other people derive the DP recurrence.

3. **GeeksforGeeks**
   - Good for learning dynamic programming and pattern-matching style problems.

4. **CP-Algorithms**
   - Helpful for general DP and algorithmic reasoning.

5. **MIT OpenCourseWare / free algorithm lectures**
   - Great for learning how to build correct recurrence relations.

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

## 16. A Good Study Plan For Problems Like This

If you want to get better at regex-style DP, try this order:

1. Learn basic recursion on strings.
2. Practice simple DP on suffixes or prefixes.
3. Understand how `*` creates branching.
4. Solve the problem once with recursion + memoization.
5. Convert that solution to bottom-up DP.
6. Re-solve it without looking at the answer.
7. Explain the recurrence in your own words.

If you can explain why `dp[i, j]` works, then you really understand the problem.

---

## 17. Final Summary

This problem is best solved with dynamic programming because the `*` operator creates multiple valid ways to match the same substring.

The implemented solution works by:

- defining `dp[i, j]` as whether `s[i..]` matches `p[j..]`,
- handling `.` as a single-character wildcard,
- handling `*` with two choices:
  - skip the `x*` pair,
  - or consume one matching character and stay on the same pattern position,
- and filling the table from the end toward the beginning.

The final result is `dp[0, 0]`, which tells whether the full string matches the full pattern.

This is the standard optimal style of solution for the problem.

