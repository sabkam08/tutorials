# 12. Integer to Roman — C# Explanation

## 1. How To Think About The Problem

This problem is really about **pattern matching** and **careful construction**.

At first, it may seem like you need to "calculate" Roman numerals in a complicated way, but the input range is small and the Roman numeral system has a fixed structure.

The easiest way to think about the problem is:

- break the number into its Roman numeral components,
- handle the special subtractive cases,
- append symbols from largest to smallest.

That makes the problem much more manageable.

---

## 2. Understanding The Requirements Carefully

You are given an integer `num` and must convert it to a Roman numeral.

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

1. Roman numerals are built from largest value to smallest value.
2. Some values use subtractive notation:
   - `IV` = 4
   - `IX` = 9
   - `XL` = 40
   - `XC` = 90
   - `CD` = 400
   - `CM` = 900
3. Symbols like `I`, `X`, `C`, and `M` can repeat up to 3 times in a row.
4. Symbols like `V`, `L`, and `D` do not repeat.

The key is to follow these rules in the correct order.

---

## 3. Why This Is A Greedy Construction Problem

The solution works because Roman numerals are always formed by choosing the **largest valid symbol or symbol pair** first.

For example:

- `3749` becomes `MMM` + `DCC` + `XL` + `IX`
- `58` becomes `L` + `VIII`
- `1994` becomes `M` + `CM` + `XC` + `IV`

This means we can use a greedy approach:

- check the biggest Roman values first,
- subtract them while possible,
- append the matching Roman symbols.

That gives a simple and reliable solution.

---

## 4. Core Idea Of The Implementation

The implementation uses two parallel arrays:

- one array for integer values,
- one array for the matching Roman symbols.

### Example structure

- `1000 -> M`
- `900 -> CM`
- `500 -> D`
- `400 -> CD`
- and so on.

Then the algorithm loops from largest to smallest:

- while the current number is at least the current value,
- append the matching symbol,
- subtract that value from the number.

This continues until the number becomes zero.

---

## 5. Approach Used In `Solution.cs`

The solution in `Solution.cs` follows a straightforward greedy strategy.

### Step 1: Create the mapping

We define two arrays:

- `values` = all Roman values in descending order
- `symbols` = corresponding Roman numeral strings

### Step 2: Loop through the mappings

For each value:

- if `num` is still large enough,
- keep appending the symbol,
- subtract the value each time.

### Step 3: Build the result

We use a `StringBuilder` because string concatenation inside a loop would be less efficient.

At the end, we return the built string.

---

## 6. Why This Logic Works

Roman numerals are not arbitrary.

They are designed so that:

- the largest symbols appear first,
- special subtractive combinations appear in fixed places,
- the remainder is always smaller and easier to process.

Because of that structure, the greedy algorithm always makes the correct next choice.

### Example

If `num = 944`:

- `900 -> CM`
- `40 -> XL`
- `4 -> IV`

The algorithm automatically picks those values because they appear in the mapping before their smaller components.

That is why the solution is correct.

---

## 7. Example Walkthrough

### Example 1

```text
num = 3749
```

The algorithm works like this:

- `3749 >= 1000` → append `M` three times
- remaining value: `749`
- `749 >= 500` → append `D`
- remaining value: `249`
- `249 >= 100` → append `C` twice
- remaining value: `49`
- `49 >= 40` → append `XL`
- remaining value: `9`
- `9 >= 9` → append `IX`

Final result:

```text
MMMDCCXLIX
```

---

## 8. Another Example

### Example 2

```text
num = 58
```

The algorithm produces:

- `50 -> L`
- `8 -> VIII`

Final result:

```text
LVIII
```

---

## 9. Why The Special Subtractive Forms Matter

A common mistake is trying to build Roman numerals only from repeated symbols.

That would fail for values like:

- `4` → should be `IV`, not `IIII`
- `9` → should be `IX`, not `VIIII`
- `40` → should be `XL`, not `XXXX`

That is why the mapping must include the subtractive forms explicitly.

The solution handles this by placing those subtractive pairs directly into the value-symbol list.

---

## 10. Time Complexity And Space Complexity

Let the integer be converted using a fixed Roman numeral mapping.

### Time complexity

The algorithm checks a fixed number of Roman values, so the runtime is effectively:

- **O(1)**

Even though there is a loop inside the loop, the number of Roman symbols is fixed and tiny.

### Space complexity

The algorithm only uses:

- the output builder,
- a few variables,
- the fixed mapping arrays.

So the auxiliary space is:

- **O(1)**

If you count the output itself, that is necessary for the answer.

---

## 11. Common Mistakes To Avoid

### Mistake 1: Forgetting subtractive notation

Roman numerals require special forms like `IV`, `IX`, `XL`, `XC`, `CD`, and `CM`.

### Mistake 2: Building from smallest to largest

Roman numerals should be constructed from largest to smallest.

### Mistake 3: Using repeated subtraction without a mapping

That can work, but it is easier to make mistakes with special cases if the mapping is incomplete.

### Mistake 4: Repeated string concatenation

Using `+` in a loop is less efficient than using `StringBuilder`.

### Mistake 5: Missing boundary values

Numbers like `4`, `9`, `40`, `90`, `400`, and `900` must be handled exactly right.

---

## 12. How To Approach Problems Like This In General

When a problem involves:

- a fixed output system,
- known special cases,
- ordered values,
- or conversion between two representations,

ask yourself:

1. Can I store the known patterns in a mapping?
2. Can I process the input from largest to smallest?
3. Are there special cases I should include directly?
4. Can a greedy approach work because the structure is fixed?

For this problem, the answer is yes.

---

## 13. Topics To Read On To Get Better

If you want to strengthen the skills used here, study these topics:

### Core algorithm topics

- Greedy algorithms
- String construction
- Pattern matching
- Number conversion problems
- Ordered mappings

### Helpful supporting topics

- Arrays
- Loops
- StringBuilder usage
- Complexity analysis
- Edge case handling

### C# topics worth practicing

- `StringBuilder`
- Arrays
- `while` loops
- Clean method design
- Console-based test runners

---

## 14. Sources To Consult

Here are good references for learning more.

### Official / high-quality references

1. **Microsoft Learn — C# documentation**
   - Helpful for `StringBuilder`, arrays, and general syntax.

2. **LeetCode problem discussions**
   - Useful for seeing multiple conversion approaches.

3. **GeeksforGeeks**
   - Good for Roman numeral conversion patterns and greedy thinking.

4. **CP-Algorithms**
   - Helpful for broader algorithmic reasoning.

### If you prefer video learning

- NeetCode
- Striver / takeUforward
- Abdul Bari
- freeCodeCamp algorithm playlists

### If you want structured practice

- LeetCode problem sets by topic
- HackerRank implementation problems
- Codeforces practice on greedy logic
- AtCoder beginner practice

---

## 15. A Good Study Plan For Problems Like This

A practical way to learn this kind of problem is:

1. Memorize the Roman numeral symbols and subtractive forms.
2. Practice writing the mapping from largest to smallest.
3. Solve the conversion manually for a few numbers.
4. Implement the greedy solution.
5. Test edge cases like `4`, `9`, `40`, `90`, `400`, and `900`.
6. Re-implement it from memory without looking.

That last step is especially helpful.

If you can explain why the mapping order matters, you understand the problem well.

---

## 16. Final Summary

The best way to solve Integer to Roman is with a greedy mapping approach:

- store all important Roman values in descending order,
- repeatedly subtract the largest possible value,
- append the matching Roman symbol,
- continue until the number becomes zero.

The solution is simple, efficient, and naturally handles the subtractive forms because they are included directly in the mapping.

That makes `Solution.cs` clean, correct, and easy to test.

