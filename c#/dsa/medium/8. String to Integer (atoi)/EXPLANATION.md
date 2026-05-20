# 8. String to Integer (atoi) — C# Explanation

## 1. How To Think About The Problem

At first glance, `myAtoi` looks like a simple string parsing task, but it is really a careful **state-by-state conversion** problem.

You are not just reading digits.
You must also handle:

- leading whitespace,
- an optional sign,
- stopping at the first invalid character,
- leading zeros,
- and overflow / underflow.

That means the problem is less about heavy algorithms and more about **careful control flow**.

The best way to solve it is to scan the string from left to right once, keeping track of what part of the number you are currently parsing.

---

## 2. Understanding The Requirements Carefully

The function must convert a string into a 32-bit signed integer.

### Parsing rules

The conversion follows these steps:

1. Ignore leading whitespace.
2. Read an optional `+` or `-` sign.
3. Read digits until the first non-digit character.
4. If no digits were read, return `0`.
5. Clamp the result to the range of a 32-bit signed integer.

### Important bounds

The valid range is:

- `int.MinValue = -2147483648`
- `int.MaxValue = 2147483647`

### Important observations

- The input string may contain letters or symbols after the number.
- Parsing must stop as soon as a non-digit appears.
- The sign applies only once, before the digits.
- A string like `"   +0 123"` should return `0`, because parsing stops at the first space after the digits.
- A string like `"words and 987"` should return `0`, because parsing never reaches a valid number.

---

## 3. Why This Is A Straight Scan Problem

This problem does not need sorting, dynamic programming, or binary search.

You only need a single pass through the string because:

- every character has a clear role,
- the parsing order is fixed,
- and once invalid input appears after the digits, you stop immediately.

That makes the solution efficient and easy to reason about.

The main challenge is not the number of steps.
It is making sure the steps happen in the correct order.

---

## 4. Core Idea Of The Parsing State

A clean way to think about the solution is as a small parser with three stages:

### Stage 1: Skip whitespace

Move past all leading spaces.

### Stage 2: Read the sign

If the next character is:

- `'-'`, the result is negative,
- `'+'`, the result is positive,
- anything else, assume positive.

### Stage 3: Read digits

Keep reading digits until:

- the string ends, or
- the first non-digit character appears.

While reading digits, build the numeric result carefully and check overflow.

---

## 5. Approach Used In `Solution.cs`

The implementation uses a left-to-right scan with overflow protection.

### Step 1: Skip spaces

The code advances an index while the current character is a space.

This handles inputs like:

- `"   42"`
- `"      -17"`

### Step 2: Read the sign

If the next character is `+` or `-`, the code stores the sign and moves forward.

If there is no sign, the default sign is positive.

### Step 3: Read digits into a larger numeric type

The code uses a `long` accumulator instead of `int` while parsing.

That makes it easier to detect overflow before the final cast.

### Step 4: Check clamping while parsing

After each digit is added, the code checks whether the signed value has moved past the 32-bit range.

If so, it returns:

- `int.MaxValue` for positive overflow,
- `int.MinValue` for negative overflow.

### Step 5: Return the final integer

If parsing completes without overflow, the code returns the final signed result.

---

## 6. Why This Logic Works

The rules of `atoi` are strict and sequential.

That means the parser must behave like a machine that only moves forward.

The solution works because it obeys the exact order of the problem statement:

- first skip whitespace,
- then read the sign,
- then read digits,
- then stop at the first invalid character.

### Why the overflow check is correct

If the parsed value goes beyond the valid `int` range, the answer must be clamped.

By checking after each digit, the code avoids building a number that is too large to safely store in an `int`.

Using `long` gives extra room, but the solution still clamps immediately once the result is out of bounds.

---

## 7. Example Walkthrough

### Example 1

```text
s = "42"
```

- no leading whitespace,
- no sign,
- digits read: `42`,
- result: `42`.

### Example 2

```text
s = "   -042"
```

- leading whitespace is skipped,
- sign is `-`,
- digits read are `042`,
- leading zeros do not change the value,
- result is `-42`.

### Example 3

```text
s = "1337c0d3"
```

- digits `1337` are read,
- parsing stops when `c` is reached,
- result is `1337`.

### Example 4

```text
s = "0-1"
```

- digit `0` is read,
- parsing stops at `-`,
- result is `0`.

### Example 5

```text
s = "words and 987"
```

- the first character is not whitespace, sign, or digit,
- no valid number is read,
- result is `0`.

---

## 8. Overflow And Underflow Example

Suppose the input is something like:

```text
s = "91283472332"
```

This value is larger than `int.MaxValue`.

The parser keeps multiplying by 10 and adding digits, but once the value exceeds `2147483647`, it returns `int.MaxValue`.

Similarly, for a very large negative number, the code returns `int.MinValue`.

This is important because the problem explicitly requires clamping to the 32-bit range.

---

## 9. Why The Implemented Solution Is Good

The current `Solution.cs` is a strong solution because it is:

- **correct**
- **easy to follow**
- **efficient**
- **safe against overflow**
- **faithful to the problem rules**

It handles all the tricky cases that usually cause bugs in `atoi` implementations.

---

## 10. Time Complexity And Space Complexity

### Time complexity

The algorithm scans the string once, so the runtime is:

- **O(n)**

where `n` is the length of the string.

### Space complexity

The solution uses only a few variables:

- **O(1)** space

That is optimal for this problem.

---

## 11. Common Mistakes To Avoid

### Mistake 1: Forgetting to skip leading spaces

If you do not skip whitespace first, inputs like `"   -42"` will fail.

### Mistake 2: Ignoring the sign

The sign must be handled before reading digits.

### Mistake 3: Not stopping at the first non-digit

Parsing must stop immediately when the first invalid character appears.

### Mistake 4: Parsing with `int` directly

If you build the number directly in an `int`, overflow can happen before you detect it.

### Mistake 5: Returning the wrong value when no digits are found

If the string does not contain a valid number, the answer must be `0`.

### Mistake 6: Mishandling overflow

The returned value must be clamped exactly to the 32-bit signed integer range.

---

## 12. How To Approach Problems Like This In General

When you see a parsing problem, ask yourself:

1. What exact order must the characters be processed in?
2. Are there optional prefixes like whitespace or sign?
3. What should happen when invalid input appears?
4. What are the numeric limits?
5. Do I need to clamp or reject out-of-range values?

For this problem, the answer to all of those questions matters.

That is why `atoi` is a good exercise in careful implementation.

---

## 13. Topics To Read On To Get Better

If you want to improve at problems like this, study these topics:

### Core algorithm topics

- String parsing
- State machines
- Overflow handling
- Input validation
- One-pass scanning

### Helpful supporting topics

- Character classification
- Sentinel values
- Boundary checking
- Signed integer ranges
- Defensive programming

### C# topics worth practicing

- `char.IsDigit`
- `int.MinValue` and `int.MaxValue`
- `long` arithmetic
- `Console.WriteLine`
- Writing small test runners

---

## 14. Sources To Consult

Here are useful places to continue learning.

### Official / high-quality references

1. **Microsoft Learn — C# documentation**
   - Useful for `char`, `long`, integer limits, and general language behavior.

2. **LeetCode problem discussions**
   - Helpful for seeing alternative parsing strategies.

3. **GeeksforGeeks**
   - Good for string-to-integer conversion patterns and edge cases.

4. **CP-Algorithms**
   - Less directly related here, but still useful for strengthening algorithmic thinking.

### If you prefer video learning

- NeetCode
- Striver / takeUforward
- Abdul Bari
- freeCodeCamp algorithm playlists

### If you want structured practice

- LeetCode string problems
- HackerRank parsing exercises
- Codeforces implementation problems
- AtCoder beginner tasks

---

## 15. A Good Study Plan For Problems Like This

If you want to get better at parsing problems, try this order:

1. Solve a few simple string scanning problems.
2. Practice handling signs and whitespace.
3. Add overflow checks to your solutions.
4. Test cases with invalid trailing characters.
5. Re-implement `atoi` from memory.
6. Compare your version with the official behavior.

The best way to learn this problem is to practice writing the parser from scratch.

---

## 16. Final Summary

This problem is solved by scanning the string once and applying the rules in order:

- skip whitespace,
- read the sign,
- read digits,
- stop at the first invalid character,
- clamp the result to the 32-bit range.

The implementation in `Solution.cs` is a clean and reliable way to do that.

The main lessons from this problem are:

- parsing is a step-by-step process,
- edge cases matter a lot,
- and overflow protection should be built in early.

