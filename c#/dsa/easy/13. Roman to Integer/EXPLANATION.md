# 13. Roman to Integer — C# Explanation

## 1. How To Think About The Problem

At first glance, Roman numerals look like a simple character-to-number conversion problem. You can map `I` to `1`, `V` to `5`, `X` to `10`, and so on. That part is easy.

The real challenge is that Roman numerals do not always add values from left to right. Sometimes a smaller symbol appears before a larger one, and in that case the smaller value is subtracted instead of added.

For example:

- `III` means `1 + 1 + 1 = 3`
- `VI` means `5 + 1 = 6`
- `IV` means `5 - 1 = 4`
- `IX` means `10 - 1 = 9`

So the important part of the problem is not just knowing the symbol values, but understanding when to add and when to subtract.

The solution in `Solution.cs` uses a clean and efficient strategy:

- scan the string from right to left,
- keep track of the last seen value,
- subtract when the current value is smaller than the previous one,
- otherwise add it.

That gives a simple and correct solution.

---

## 2. Understanding The Requirements Carefully

You are given a Roman numeral string `s`, and you must convert it into its integer value.

### Roman numeral symbols

| Symbol | Value |
| --- | ---: |
| I | 1 |
| V | 5 |
| X | 10 |
| L | 50 |
| C | 100 |
| D | 500 |
| M | 1000 |

### Important rules

1. Roman numerals are usually written from largest to smallest.
2. If a smaller symbol appears before a larger symbol, it is subtracted.
3. Only certain subtractive pairs are valid:
   - `I` before `V` or `X`
   - `X` before `L` or `C`
   - `C` before `D` or `M`
4. The input is guaranteed to be a valid Roman numeral.

### What that means for the solution

You do not need to validate the Roman numeral rules yourself.
You only need to interpret the string correctly according to the subtractive pattern.

That makes the problem much simpler.

---

## 3. Why The Right-To-Left Approach Works

The key observation is this:

- if a symbol is smaller than the symbol to its right, it should be subtracted,
- otherwise it should be added.

When scanning from right to left:

- the first symbol you see is always added,
- then for each earlier symbol, you compare it with the last symbol value you processed,
- if the current value is smaller, subtract it,
- otherwise add it and update the "previous" value.

This works because Roman numerals only use subtraction when a smaller symbol comes before a larger one.

### Example

For `MCMXCIV`:

- `V` is added
- `I` comes before `V`, so subtract `I`
- `C` is greater than `I`, so add `C`
- `X` comes before `C`, so subtract `X`
- `M` is greater than `X`, so add `M`
- `C` comes before `M`, so subtract `C`
- `M` is greater than `C`, so add `M`

The final answer is `1994`.

---

## 4. Approach Used In `Solution.cs`

The implementation uses a dictionary from Roman symbols to their integer values.

### Step 1: Create the symbol map

The solution stores the values in a dictionary:

- `I -> 1`
- `V -> 5`
- `X -> 10`
- `L -> 50`
- `C -> 100`
- `D -> 500`
- `M -> 1000`

This makes each character lookup quick and readable.

### Step 2: Walk through the string from right to left

The method keeps two variables:

- `total` — the accumulated result
- `previous` — the last Roman value we saw while scanning from the right

For each character:

- if `current < previous`, subtract it from `total`
- otherwise, add it to `total` and set `previous = current`

### Step 3: Return the result

After processing all characters, `total` contains the integer value of the Roman numeral.

---

## 5. Why This Logic Is Correct

The correctness depends on the Roman numeral subtraction rule.

If a symbol is smaller than the symbol after it, then that smaller symbol is part of a subtractive pair.

Examples:

- `IV` -> `I` is smaller than `V`, so `I` is subtracted
- `XL` -> `X` is smaller than `L`, so `X` is subtracted
- `CM` -> `C` is smaller than `M`, so `C` is subtracted

Scanning from right to left makes this easy to detect because each symbol can be compared to the symbol that was processed most recently.

### Why we only need the last seen value

When scanning from the right:

- the current symbol only needs to know whether it is smaller than the nearest processed symbol to its right,
- if it is, we subtract it,
- otherwise, it contributes positively.

That is enough because the Roman numeral format guarantees valid structure.

---

## 6. Example Walkthroughs

### Example 1: `III`

Symbols and values:

- `I = 1`
- `I = 1`
- `I = 1`

Scanning from right to left:

- start with `1`
- next `1` is not smaller, so add it
- next `1` is not smaller, so add it

Result:

- `1 + 1 + 1 = 3`

### Example 2: `LVIII`

Breakdown:

- `L = 50`
- `V = 5`
- `III = 3`

Scanning from right to left:

- add `I`
- add `I`
- add `I`
- `V` is larger than `I`, so add `V`
- `L` is larger than `V`, so add `L`

Result:

- `50 + 5 + 1 + 1 + 1 = 58`

### Example 3: `MCMXCIV`

Breakdown:

- `M = 1000`
- `CM = 900`
- `XC = 90`
- `IV = 4`

Scanning from right to left:

- `V = 5` -> add
- `I = 1` is smaller than `V` -> subtract
- `C = 100` is larger than `I` -> add
- `X = 10` is smaller than `C` -> subtract
- `M = 1000` is larger than `X` -> add
- `C = 100` is smaller than `M` -> subtract
- `M = 1000` is larger than `C` -> add

Result:

- `1000 + 900 + 90 + 4 = 1994`

### Example 4: `MMMCMXCIX`

This is the maximum valid value in the problem range.

Breakdown:

- `MMM = 3000`
- `CM = 900`
- `XC = 90`
- `IX = 9`

Total:

- `3000 + 900 + 90 + 9 = 3999`

This is a good boundary test because it uses multiple subtractive pairs.

### More Problem-Specific Examples

These extra examples are useful because they highlight the exact subtractive patterns that often confuse people:

- `IV` = `4`
  - `I` comes before `V`, so the value is `5 - 1`.
- `IX` = `9`
  - `I` comes before `X`, so the value is `10 - 1`.
- `XL` = `40`
  - `X` comes before `L`, so the value is `50 - 10`.
- `XC` = `90`
  - `X` comes before `C`, so the value is `100 - 10`.
- `CD` = `400`
  - `C` comes before `D`, so the value is `500 - 100`.
- `CM` = `900`
  - `C` comes before `M`, so the value is `1000 - 100`.

You can think of these as the building blocks that make up larger numerals like `MCMXCIV`.

---

## 7. Why The Implemented Solution Is Good

The current solution is strong because it is:

- **simple** to understand,
- **efficient** in time,
- **small** in code size,
- **easy to verify** with examples,
- and **perfectly suited** to the Roman numeral rules.

It avoids complicated parsing logic and works directly with the structure of the numeral.

---

## 8. Time Complexity And Space Complexity

Let `n` be the length of the Roman numeral string.

### Time complexity

The solution makes one pass through the string:

- **O(n)** time

### Space complexity

The dictionary of Roman values is fixed and very small.
Ignoring that constant-sized lookup table, the algorithm itself uses only a few variables:

- **O(1)** extra space

---

## 9. Common Mistakes To Avoid

### Mistake 1: Always adding every symbol

That fails for values like `IV`, `IX`, `XL`, `XC`, `CD`, and `CM`.

You must detect when subtraction is needed.

### Mistake 2: Scanning left to right without careful rules

Left-to-right scanning can work, but it is easier to make mistakes unless you explicitly look ahead to the next symbol.

The right-to-left approach is simpler because it only needs one comparison.

### Mistake 3: Forgetting the subtractive pairs

The valid subtraction cases are limited.
A smaller symbol does not always mean subtraction unless it comes before a larger one.

### Mistake 4: Overcomplicating validation

The problem guarantees valid input.
You do not need to detect invalid Roman numerals.

### Mistake 5: Using too much extra logic

A Roman numeral conversion problem does not need a complicated parser.
A direct mapping plus one pass is enough.

---

## 10. How To Approach Problems Like This In General

When you see a problem involving character patterns and special rules, ask:

1. Can I map each symbol to a value directly?
2. Is there a local rule that changes how the value should be interpreted?
3. Would scanning from one direction make the rule easier?
4. Can I solve it with one pass and a small amount of state?

For this problem, the answer is yes to all four.

That is a useful pattern to remember for string parsing problems.

---

## 11. Topics To Read On To Get Better

If you want to improve at problems like this, study these topics:

### Core algorithm topics

- String parsing
- Hash maps / dictionaries
- One-pass scanning
- Greedy interpretation rules
- Boundary handling

### Helpful supporting topics

- Prefix and suffix logic
- Character-to-value mapping
- Problem constraints analysis
- Time and space complexity

### C# topics worth practicing

- Dictionaries
- `foreach` and indexed loops
- Console runners for quick testing
- Clean method design
- `int` and `double` basics

---

## 12. Sources To Consult

Here are good places to learn and improve your understanding.

### Official / high-quality references

1. **Microsoft Learn — C# documentation**
   - Useful for `Dictionary<char, int>`, loops, and general C# syntax.

2. **LeetCode problem discussions**
   - Good for seeing alternate scanning strategies and edge cases.

3. **GeeksforGeeks**
   - Helpful for Roman numeral conversion explanations and string parsing patterns.

4. **MDN / general algorithm references**
   - Useful if you want to practice reading and converting structured strings.

### If you prefer video learning

- NeetCode
- Striver / takeUforward
- Abdul Bari
- freeCodeCamp algorithm playlists

### If you want structured practice

- LeetCode string problems
- HackerRank parsing problems
- Codeforces easy implementation tasks
- AtCoder beginner contests

---

## 13. A Good Study Plan For Problems Like This

If you want to improve steadily, try this order:

1. Learn the symbol-to-value mapping.
2. Understand Roman subtraction rules.
3. Practice scanning from right to left.
4. Solve a few examples by hand.
5. Re-implement the solution without looking.
6. Add your own test cases.
7. Explain why the subtraction rule works.

If you can explain the `IV` and `IX` cases clearly, you understand the problem.

---

## 14. Final Summary

This problem is best solved by mapping Roman symbols to values and scanning the string from right to left.

The key rule is:

- add the current value if it is at least as large as the previous value,
- subtract it if it is smaller.

That approach is:

- easy to implement,
- easy to test,
- and efficient.

The solution in `Solution.cs` is a clean, standard answer that correctly handles all valid Roman numerals in the range `[1, 3999]`.

