# 3. Longest Substring Without Repeating Characters — C# Explanation

## 1. How To Think About The Problem

This problem asks for the length of the longest substring without repeating characters.

At first, it may seem like we should try every substring and check whether all characters are unique. That would work conceptually, but it would be too slow.

The important observation is that we are dealing with a **contiguous substring**, not just any subsequence. That means we should focus on a moving window of characters and keep track of which characters are currently inside that window.

This naturally leads to the **sliding window** technique.

---

## 2. Understanding The Requirements Carefully

You are given a string `s`.

You must return the length of the longest substring that contains no duplicate characters.

### Important distinction

- A **substring** is contiguous.
- A **subsequence** is not necessarily contiguous.

This matters because the answer must be a substring, not a subsequence.

### What the problem is really asking

You need to find:

- the longest segment of the string,
- where every character appears at most once inside that segment.

---

## 3. Why This Is A Sliding Window Problem

A sliding window is useful when:

- you want to examine a contiguous range,
- the range can grow or shrink,
- and you want to maintain some property efficiently.

Here, the property is:

- all characters in the current window are unique.

Instead of restarting from scratch for every possible substring, we move two pointers:

- `left` = the start of the window,
- `right` = the end of the window.

As `right` expands, if we see a duplicate, we move `left` forward until the window becomes valid again.

That gives us an efficient solution.

---

## 4. Core Idea Of The Window

The current implementation keeps track of:

- `lastSeen` = a dictionary from character to its most recent index,
- `left` = the start index of the current valid window,
- `best` = the best window length seen so far.

### Why we store the last seen index

If we see a repeated character, we want to know:

- where did we last see it?
- is that occurrence still inside the current window?

If yes, then the current window is no longer valid, and we must move `left` past the previous occurrence.

---

## 5. Approach Used In `Solution.cs`

The code works as follows:

```csharp
var lastSeen = new Dictionary<char, int>();
int left = 0;
int best = 0;
```

### Step 1: Create a dictionary

We use a dictionary to remember the last index where each character appeared.

### Step 2: Expand the window with `right`

We loop over the string with `right`.

For each character:

- check whether it has been seen before,
- if the previous occurrence is still inside the current window, move `left`.

### Step 3: Update the last seen position

After handling the duplicate logic:

- store the current index as the latest occurrence of this character.

### Step 4: Update the best answer

At every step:

- compute the current window length as `right - left + 1`,
- compare it to `best`.

---

## 6. Why The Duplicate Check Works

The code does this:

```csharp
if (lastSeen.TryGetValue(current, out int previousIndex) && previousIndex >= left)
{
    left = previousIndex + 1;
}
```

This means:

- if `current` was seen before,
- and that previous occurrence is still inside the current window,
- then the window is invalid.

So we move `left` to one position after the previous occurrence.

### Why `previousIndex >= left` matters

A character might have been seen before, but that older occurrence may already be outside the current window.

If that happens, it does **not** create a duplicate inside the current substring.

So we only move `left` when the duplicate is actually inside the active window.

---

## 7. Example Walkthrough

### Example 1

```text
s = "abcabcbb"
```

We will track the window step by step.

### Start

- `left = 0`
- `best = 0`
- `lastSeen = {}`

### `right = 0`, character = `a`

- `a` has not been seen before.
- window = `"a"`
- current length = `1`
- `best = 1`

### `right = 1`, character = `b`

- `b` has not been seen before.
- window = `"ab"`
- current length = `2`
- `best = 2`

### `right = 2`, character = `c`

- `c` has not been seen before.
- window = `"abc"`
- current length = `3`
- `best = 3`

### `right = 3`, character = `a`

- `a` was last seen at index `0`.
- `0 >= left(0)`, so it is inside the current window.
- move `left` to `0 + 1 = 1`
- window becomes `"bca"`
- current length = `3`
- `best = 3`

### `right = 4`, character = `b`

- `b` was last seen at index `1`.
- `1 >= left(1)`, so move `left` to `2`
- window becomes `"cab"`
- current length = `3`
- `best = 3`

### `right = 5`, character = `c`

- `c` was last seen at index `2`.
- `2 >= left(2)`, so move `left` to `3`
- window becomes `"abc"`
- current length = `3`
- `best = 3`

### `right = 6`, character = `b`

- `b` was last seen at index `4`.
- `4 >= left(3)`, so move `left` to `5`
- window becomes `"cb"`
- current length = `2`
- `best = 3`

### `right = 7`, character = `b`

- `b` was last seen at index `6`.
- `6 >= left(5)`, so move `left` to `7`
- window becomes `"b"`
- current length = `1`
- `best = 3`

Final answer:

```text
3
```

---

## 8. Another Example Walkthrough

### Example 2

```text
s = "bbbbb"
```

Every time we see another `b`, the previous `b` is still inside the window.

So the window is constantly adjusted to size `1`.

The longest unique substring is:

```text
"b"
```

So the answer is:

```text
1
```

---

## 9. Why The Implemented Solution Is Good

The current `Solution.cs` is strong because it:

- uses a sliding window,
- keeps track of the last position of each character,
- updates the window in constant time per character,
- and avoids rechecking substrings from scratch.

This is the standard optimal approach for this problem.

---

## 10. Time Complexity And Space Complexity

Let `n = s.Length`.

### Time complexity

Each character is processed once with O(1) average dictionary operations.

So the total time complexity is:

```text
O(n)
```

### Space complexity

The dictionary stores at most one entry per distinct character in the current alphabet.

So the space complexity is:

```text
O(k)
```

where `k` is the number of distinct characters that can appear.

In practice, this is bounded by the character set, so it is effectively constant relative to the input length.

---

## 11. Common Mistakes To Avoid

### Mistake 1: Using brute force

Checking every substring is too slow.

### Mistake 2: Moving `left` too far

When a duplicate appears, you should only move `left` to one position after the previous occurrence, not always reset it to zero.

### Mistake 3: Forgetting to check whether the duplicate is inside the current window

A previous character outside the window does not matter.

### Mistake 4: Updating `best` at the wrong time

You should update `best` after the window is made valid again.

### Mistake 5: Confusing substring with subsequence

The problem is about contiguous substrings only.

---

## 12. How To Approach Problems Like This In General

When you see a problem involving:

- a contiguous range,
- a longest or shortest valid segment,
- repeated values,
- and a need for efficient updates,

ask yourself:

1. Can I use a sliding window?
2. What makes the window invalid?
3. What information do I need to restore validity quickly?
4. Can I store the last seen position of each item?

For this problem, the answer is yes to all four.

---

## 13. Topics To Read On To Get Better

If you want to improve at problems like this, study these topics:

### Core algorithm topics

- Sliding Window
- Hash Maps / Dictionaries
- Two Pointers
- String processing
- Complexity analysis

### Helpful supporting topics

- Invariants
- Window maintenance
- Index tracking
- Greedy movement of pointers

### C# topics worth practicing

- `Dictionary<TKey, TValue>`
- `TryGetValue`
- String indexing
- `Math.Max`
- Console testing runners

---

## 14. Sources To Consult

Here are useful places to learn more and get stronger at this pattern.

### Official / high-quality references

1. **Microsoft Learn — C# documentation**
   - Helpful for dictionaries, strings, and general C# syntax.

2. **LeetCode discussions**
   - Good for seeing alternate sliding-window explanations.

3. **GeeksforGeeks**
   - Useful for sliding window and hash map-based string problems.

4. **CP-Algorithms**
   - Good for learning algorithm reasoning and invariant thinking.

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

## 15. A Good Study Plan For Problems Like This

If you want to get better at sliding-window problems, try this order:

1. Learn the basic sliding window idea.
2. Practice tracking last seen positions.
3. Study how to move `left` only when needed.
4. Re-solve this problem without looking.
5. Compare your reasoning to the explanation here.
6. Explain the algorithm in your own words.

That last step is very important.

If you can clearly explain why `left` moves the way it does, then you understand the problem.

---

## 16. Final Summary

This problem is best solved with a sliding window and a dictionary of last seen positions.

The algorithm:

- expands the window one character at a time,
- moves the left boundary when a duplicate appears inside the window,
- and keeps track of the best window length seen so far.

The recurrence is simple:

- if the current character was seen inside the window, move `left` past the previous occurrence,
- then update the best answer.

The solution is efficient, clean, and runs in linear time.

The main lessons from this problem are:

- use a sliding window for contiguous substring problems,
- track last seen positions to handle duplicates,
- and always maintain a valid window invariant.

