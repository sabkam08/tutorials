# 5. Longest Palindromic Substring — C# Explanation

## 1. How To Think About The Problem

This problem asks for the longest substring that reads the same forwards and backwards.

At first, you might think about checking every substring and testing whether it is a palindrome. That would work conceptually, but it would be too slow for the full input size.

The useful insight is that every palindrome has a center.

That center is either:

- a single character, for odd-length palindromes,
- or the gap between two characters, for even-length palindromes.

That means instead of checking every substring directly, we can expand outward from every possible center.

---

## 2. Understanding The Requirements Carefully

You are given a string `s`.

You must return the **longest palindromic substring** in `s`.

### Important observations

1. The answer must be a contiguous substring.
2. The substring must read the same forward and backward.
3. There may be more than one valid answer of the same maximum length.
4. The problem only asks for one longest palindrome, not all of them.

These observations point directly to center expansion.

---

## 3. Why This Is An Expand-Around-Center Problem

A palindrome is symmetric around its center.

So if we know the center, we can expand outward as long as the characters on the left and right are equal.

There are two possible centers:

- one character in the middle,
- two characters in the middle.

That is why the solution checks both:

- `ExpandAroundCenter(s, i, i, ...)`
- `ExpandAroundCenter(s, i, i + 1, ...)`

This guarantees that both odd-length and even-length palindromes are considered.

---

## 4. Core Idea Of The Solution

The implementation keeps track of:

- `start` = the starting index of the best palindrome found so far,
- `maxLength` = the length of that palindrome.

### Step 1: Handle empty input

If the string is empty, the answer is just `string.Empty`.

### Step 2: Try every possible center

For each index `i`:

- expand around `i, i` for odd-length palindromes,
- expand around `i, i + 1` for even-length palindromes.

### Step 3: Expand while characters match

Inside `ExpandAroundCenter`, the code moves outward while:

- `left >= 0`,
- `right < s.Length`,
- and `s[left] == s[right]`.

### Step 4: Record the longest palindrome found

After expansion stops:

- compute the palindrome length,
- if it is larger than the best one so far, update `start` and `maxLength`.

### Step 5: Return the best substring

At the end, return:

```csharp
s.Substring(start, maxLength)
```

---

## 5. Why This Logic Works

The logic works because every palindrome must have a center, and every center is tested.

For a fixed center:

- if the characters match, the palindrome can grow,
- if they do not match, expansion stops.

By checking every center, we guarantee that we examine every possible palindrome shape.

### Invariant idea

At any point during expansion:

- the substring between `left + 1` and `right - 1` is a palindrome.

When expansion stops, that palindrome is maximal for that center.

Then we compare it to the best palindrome found so far.

---

## 6. Example Walkthrough

### Example 1

```text
s = "babad"
```

Possible palindromes include:

- `"bab"`
- `"aba"`

Both have length `3`, so either is a valid answer.

### How the algorithm sees it

The code checks each index as a center.

For example:

- center at `b` → expand to `"b"`
- center at `a` → expand to `"bab"` or `"aba"` depending on the center used
- center at `d` → only `"d"`

The longest palindrome found has length `3`.

So the answer can be:

```text
"bab"
```

or:

```text
"aba"
```

---

## 7. Another Example Walkthrough

### Example 2

```text
s = "cbbd"
```

The longest palindrome is:

```text
"bb"
```

### Why

The palindrome `"bb"` is even-length, so it has a center between the two `b` characters.

That is why the code checks:

```csharp
ExpandAroundCenter(s, i, i + 1, ...)
```

without that check, even-length palindromes would be missed.

---

## 8. Why The Implemented Solution Is Good

The current `Solution.cs` is strong because it:

- checks every possible center,
- handles both odd and even palindromes,
- keeps track of the best answer without storing all substrings,
- and uses only a small amount of extra memory.

It is the standard clean approach for this problem.

---

## 9. Time Complexity And Space Complexity

Let `n = s.Length`.

### Time complexity

For each character, the algorithm may expand outward.

In the worst case, each center can expand across much of the string.

So the total time complexity is:

- **O(n^2)**

### Space complexity

The solution uses only a few integer variables and no extra data structure proportional to input size:

- **O(1)** extra space

That is optimal for this approach.

---

## 10. Common Mistakes To Avoid

### Mistake 1: Checking only odd-length centers

Even-length palindromes also need to be considered.

### Mistake 2: Forgetting to update the best answer after expansion

Expansion alone is not enough; you must store the longest palindrome found so far.

### Mistake 3: Off-by-one errors

After the loop ends, the actual palindrome boundaries are `left + 1` and `right - 1`.

### Mistake 4: Trying to check every substring directly

That is much slower than expanding from centers.

### Mistake 5: Not handling the empty string

If the string is empty, the result should be `string.Empty`.

---

## 11. How To Approach Problems Like This In General

When you see a problem involving:

- substrings,
- symmetry,
- palindromes,
- or a longest valid segment,

ask yourself:

1. Does the structure have a center or pivot point?
2. Can I expand outward from that point?
3. Do I need to consider both odd and even cases?
4. Can I keep only the best answer found so far?

For this problem, the answer is yes to all four.

---

## 12. Topics To Read On To Get Better

If you want to improve at problems like this, study these topics:

### Core algorithm topics

- Expand Around Center
- String algorithms
- Two-pointer style expansion
- Palindrome checking
- Complexity analysis

### Helpful supporting topics

- Boundary handling
- Invariants
- Index arithmetic
- Symmetry in strings

### C# topics worth practicing

- String slicing and `Substring`
- Writing helper methods
- `Math.Max` and index tracking
- Efficient console runners for testing

---

## 13. Sources To Consult

Here are useful places to learn more and strengthen your understanding.

### Official / high-quality references

1. **Microsoft Learn — C# documentation**
   - Good for strings, helper methods, and general syntax.

2. **LeetCode discussions**
   - Useful for seeing different palindrome approaches.

3. **GeeksforGeeks**
   - Good for palindrome and string pattern explanations.

4. **CP-Algorithms**
   - Helpful for learning clean algorithm reasoning and invariants.

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

If you want to get better at palindrome problems, try this order:

1. Learn how palindromes expand from a center.
2. Practice odd-length and even-length cases separately.
3. Study how to track the best substring without storing everything.
4. Re-solve this problem without looking.
5. Compare your approach to the explanation here.
6. Explain the expansion logic in your own words.

That last step is especially useful.

If you can clearly explain why every palindrome has a center, then you understand the problem.

---

## 15. Final Summary

The best way to solve this problem is:

- try every possible center,
- expand outward while the characters match,
- record the longest palindrome found,
- and return it at the end.

This works because every palindrome is symmetric around a center, and checking both odd and even centers guarantees that no valid answer is missed.

The solution is efficient, clean, and uses constant extra space.

The main lessons from this problem are:

- look for symmetry,
- use center expansion for palindrome problems,
- handle odd and even cases separately,
- and always think about index boundaries carefully.

