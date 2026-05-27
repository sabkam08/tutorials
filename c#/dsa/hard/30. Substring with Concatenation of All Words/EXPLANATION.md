# 30. Substring with Concatenation of All Words — C# Explanation

## 1. How To Think About The Problem

At first glance, this problem looks like a simple substring search problem, but there is an important twist:

- every word in `words` has the same length,
- the substring you are looking for must contain **all** words exactly once,
- and the order of those words can be anything.

That means you are not searching for one fixed string.
You are searching for any window of length `words.Length * wordLength` that can be broken into valid word chunks.

A naive solution would be to check every possible starting index, split the substring into pieces, and verify whether the pieces match the word list.

That works conceptually, but it can be too slow.

The better approach is to use a **sliding window** combined with a **frequency map**.

---

## 2. Understanding The Requirements Carefully

You are given:

- a string `s`,
- an array of strings `words`.

All strings in `words` have the same length.

You must return every starting index where a substring of `s` is a concatenation of all the words in `words` exactly once.

### What “exactly once” means

If `words = ["foo", "bar"]`, then valid matches are:

- `"foobar"`
- `"barfoo"`

Both contain `foo` and `bar` once each.

But these are not valid:

- `"foofoo"` → missing `bar`
- `"barbar"` → missing `foo`
- `"foobarbaz"` → extra characters

### Important observations

1. Every word has the same length.
2. The total matched substring length is fixed.
3. You only need to look at chunks of word length.
4. Duplicate words matter.
5. Overlapping answers are possible.

That is why this problem is a good fit for a sliding window over word-sized chunks.

---

## 3. Why A Sliding Window Works

Suppose the word length is `k`.

Then any valid substring must be exactly:

- `k * words.Length` characters long.

Instead of checking every character one by one, we can move in steps of `k` and treat each chunk as a potential word.

This gives us two major benefits:

- we only inspect useful boundaries,
- we can keep track of how many times each word appears in the current window.

### The core idea

For each possible offset from `0` to `k - 1`:

- scan the string in word-sized jumps,
- keep a dictionary of the words currently in the window,
- shrink the window when a word appears too many times,
- record the starting index when the window contains all the required words.

This is a standard sliding-window technique adapted to fixed-size chunks.

---

## 4. Approach Used In `Solution.cs`

The implementation uses these steps:

### Step 1: Handle trivial cases

If `words` is empty or `s` is empty, there is no answer.

Also, if `s.Length` is smaller than the total required concatenation length, return an empty list immediately.

### Step 2: Build the required frequency map

Create a dictionary called `required` that stores how many times each word must appear.

For example:

- `words = ["foo", "bar", "foo"]`

then:

- `foo -> 2`
- `bar -> 1`

### Step 3: Try every possible alignment

Because all words have the same length, valid matches must line up with word boundaries.

So we try each offset from `0` to `wordLength - 1`.

For each offset, we move through the string in word-sized jumps.

### Step 4: Maintain a window of words

As we move through the string:

- extract the current chunk,
- check whether it is a required word,
- update a `window` dictionary,
- if the current word appears too many times, shrink from the left.

### Step 5: Record valid starting positions

When the window contains exactly the same number of words as `words.Length`, the current left boundary is a valid answer.

That left boundary is added to the result.

---

## 5. Why This Logic Works

The key reason this works is that every valid answer must be built from exact word-length pieces.

So instead of checking arbitrary substrings, we only check aligned windows.

### Why duplicate handling matters

Suppose:

- `words = ["good", "good", "best", "word"]`

A valid answer must contain `good` twice.

That is why a set is not enough.
You need a frequency map.

### Why we shrink the window

Sometimes the current word appears too many times.

Example:

- required: `foo -> 1`
- current window: `foo -> 2`

Then the window is invalid, and we must move the left edge rightward until the counts match again.

This guarantees the window always stays consistent with the requirements.

---

## 6. Example Walkthrough

### Example 1

```text
s = "barfoothefoobarman"
words = ["foo", "bar"]
```

Each word has length `3`.

The total window length is:

```text
3 * 2 = 6
```

We are looking for 6-character substrings that contain `foo` and `bar` exactly once.

Possible matches:

- `"barfoo"` starting at index `0`
- `"foobar"` starting at index `9`

So the answer is:

```text
[0, 9]
```

### What the sliding window sees

At offset `0`:

- read `bar` → valid
- read `foo` → valid
- window now has both required words → record `0`

Continue scanning:

- words do not match until index `9`
- then `foo` + `bar` appear again → record `9`

---

## 7. Another Example With Duplicate Words

### Example 2

```text
s = "wordgoodgoodgoodbestword"
words = ["word", "good", "best", "word"]
```

Here the required counts are:

- `word -> 2`
- `good -> 1`
- `best -> 1`

There is no substring that contains exactly those counts in one contiguous block.

So the answer is:

```text
[]
```

This example is useful because it shows that duplicates must be matched exactly.

---

## 8. Example With Multiple Answers

### Example 3

```text
s = "barfoofoobarthefoobarman"
words = ["bar", "foo", "the"]
```

Each word has length `3`, and the window length is:

```text
3 * 3 = 9
```

Valid substrings are:

- `"foobarthe"` starting at `6`
- `"barthefoo"` starting at `9`
- `"thefoobar"` starting at `12`

So the answer is:

```text
[6, 9, 12]
```

This example shows that answers can overlap and appear in any order.

---

## 9. Why The Implemented Solution Is Good

The current `Solution.cs` is strong because it is:

- **efficient**
- **handles duplicates correctly**
- **works with overlapping answers**
- **uses only a small amount of extra memory**
- **avoids checking every possible substring character by character**

This is the standard sliding-window solution for this problem.

---

## 10. Time Complexity And Space Complexity

Let:

- `n = s.Length`
- `m = words.Length`
- `k = words[0].Length`

### Time complexity

The algorithm scans the string in `k` different offsets.

Each scan moves in word-sized steps.

Overall, the runtime is typically:

- **O(n)** for the scanning work, with dictionary operations per chunk

More precisely, it is efficient enough for the given constraints and is the standard optimal-style approach for this problem class.

### Space complexity

The dictionaries store at most the words from `words`:

- **O(m)** space

---

## 11. Common Mistakes To Avoid

### Mistake 1: Checking every character position with a full rebuild each time

That can become unnecessarily slow.

### Mistake 2: Using a set instead of a frequency map

A set cannot handle duplicates.

### Mistake 3: Ignoring alignment

You must step through the string by word length, not by single characters only.

### Mistake 4: Forgetting to shrink the window

If a word count becomes too large, the window must be adjusted immediately.

### Mistake 5: Forgetting to reset after invalid chunks

If you hit a word that is not in `words`, the current window must be cleared.

### Mistake 6: Not handling overlapping answers

A valid answer at one position does not prevent another answer nearby.

---

## 12. How To Approach Problems Like This In General

When you see a problem involving:

- substrings,
- repeated chunks of equal size,
- exact counts,
- and multiple valid start positions,

ask yourself:

1. Can I break the string into fixed-size pieces?
2. Do I need counts instead of just presence?
3. Can I slide a window instead of restarting from scratch?
4. Can I use multiple offsets to cover all alignments?

For this problem, the answer is yes to all four.

---

## 13. Topics To Read On To Get Better

If you want to improve at problems like this, study these topics:

### Core algorithm topics

- Sliding Window
- Hash Maps / Dictionaries
- String Chunking
- Frequency counting
- Duplicate handling
- Fixed-size window techniques

### Helpful supporting topics

- Two pointers
- Window shrinking logic
- Overlapping substring problems
- Complexity analysis

### C# topics worth practicing

- `Dictionary<TKey, TValue>`
- `StringComparison.Ordinal`
- `Substring`
- Lists and arrays
- Writing clean console test runners

---

## 14. Sources To Consult

Here are useful places to learn more about this pattern.

### Official / high-quality references

1. **Microsoft Learn — C# documentation**
   - Good for dictionaries, strings, and project setup.

2. **LeetCode problem discussions**
   - Helpful for different sliding-window variants and optimizations.

3. **GeeksforGeeks**
   - Good for frequency-map and substring-window explanations.

4. **CP-Algorithms**
   - Useful for learning algorithmic thinking and invariants.

5. **MIT OpenCourseWare / free algorithm lectures**
   - Great for building a deeper understanding of search and window-based reasoning.

### If you prefer video learning

- NeetCode
- Striver / takeUforward
- Abdul Bari
- freeCodeCamp algorithm playlists

### If you want structured practice

- LeetCode problem sets by topic
- HackerRank string tracks
- Codeforces educational rounds
- AtCoder beginner contests

---

## 15. A Good Study Plan For Problems Like This

If you want to improve steadily, try this order:

1. Learn frequency maps deeply.
2. Practice fixed-size sliding window problems.
3. Study duplicate handling carefully.
4. Re-solve substring problems with offsets.
5. Compare your answer to an optimal implementation.
6. Explain the window-shrinking logic in your own words.

That last step is especially useful.

If you can explain why the window must shrink when a count is too high, you understand the heart of this problem.

---

## 16. Final Summary

This problem is best solved with a sliding window and a frequency map:

- count how many times each word is required,
- scan the string in word-sized chunks,
- keep a window of the current chunks,
- shrink the window when counts are too large,
- record the left index when the full set of words is matched.

The method is efficient because it avoids rebuilding and rechecking everything from scratch.

The main lessons from this problem are:

- use the equal word lengths to your advantage,
- count words carefully,
- handle duplicates exactly,
- and always think in fixed-size chunks when the problem gives them to you.

