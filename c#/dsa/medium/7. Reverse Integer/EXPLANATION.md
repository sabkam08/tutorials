# 7. Reverse Integer — C# Explanation

## 1. How To Think About The Problem

This problem asks you to reverse the digits of a signed 32-bit integer.

At first, it may seem easiest to convert the number to a string, reverse the string, and convert it back.

That would work in many cases, but the problem explicitly says the environment does not allow you to store 64-bit integers, so we need to be careful about overflow.

The important part of the problem is not just reversing digits — it is reversing them safely while staying inside the 32-bit integer range.

---

## 2. Understanding The Requirements Carefully

You are given a signed 32-bit integer `x`.

You must return:

- the integer with its digits reversed,
- or `0` if reversing it would overflow the signed 32-bit range.

### Important observations

1. The sign of the number should stay negative if the input is negative.
2. Leading zeros should disappear naturally after reversal.
   - Example: `120` becomes `21`.
3. Overflow matters.
4. You should not rely on a 64-bit integer to solve it.

This means the algorithm must check bounds as it builds the reversed number.

---

## 3. Why Overflow Handling Is The Main Challenge

Reversing digits is easy if the number is small.

The challenge is that the reversed value might exceed the 32-bit signed integer range:

- minimum: `-2^31`
- maximum: `2^31 - 1`

If we build the reversed integer step by step, we must ensure that each step is safe before adding the next digit.

That is exactly what the solution does.

---

## 4. Approach Used In `Solution.cs`

The implementation uses a digit-by-digit reversal loop.

### Step 1: Start with an empty result

Create a variable such as `result = 0`.

This will hold the reversed number.

### Step 2: Extract digits one by one

While `x != 0`:

- take the last digit with `x % 10`,
- remove that digit using `x /= 10`.

This works for both positive and negative numbers in C#.

### Step 3: Check overflow before updating the result

Before doing:

```csharp
result = result * 10 + digit;
```

we check whether that operation would overflow.

If it would overflow, return `0` immediately.

### Step 4: Update the reversed number

If the new value is safe, append the digit to `result`.

Continue until all digits have been processed.

### Step 5: Return the final result

If no overflow occurred, return the reversed integer.

---

## 5. Example Walkthrough

### Example 1

```text
x = 123
```

Start:

- `result = 0`

First iteration:

- digit = `3`
- `result = 0 * 10 + 3 = 3`
- `x = 12`

Second iteration:

- digit = `2`
- `result = 3 * 10 + 2 = 32`
- `x = 1`

Third iteration:

- digit = `1`
- `result = 32 * 10 + 1 = 321`
- `x = 0`

Return `321`.

---

### Example 2

```text
x = -123
```

Start:

- `result = 0`

First iteration:

- digit = `-3`
- `result = -3`
- `x = -12`

Second iteration:

- digit = `-2`
- `result = -32`
- `x = -1`

Third iteration:

- digit = `-1`
- `result = -321`
- `x = 0`

Return `-321`.

---

### Example 3

```text
x = 120
```

Start:

- `result = 0`

First iteration:

- digit = `0`
- `result = 0`
- `x = 12`

Second iteration:

- digit = `2`
- `result = 2`
- `x = 1`

Third iteration:

- digit = `1`
- `result = 21`
- `x = 0`

Return `21`.

---

## 6. Why This Works

The algorithm works because each step moves one decimal digit from the original number into the reversed number.

The loop builds the reversed value exactly as we would do manually.

The overflow check ensures we never produce a number outside the valid 32-bit signed range.

That makes the approach both correct and safe.

---

## 7. How The Overflow Check Works

Before multiplying `result` by `10` and adding the new digit, we need to make sure the new value will still fit in an `int`.

### For positive numbers

We check whether:

- `result > int.MaxValue / 10`, or
- `result == int.MaxValue / 10` and the next digit is greater than `7`

Why `7`?

Because `int.MaxValue` is `2147483647`, and the last digit is `7`.

### For negative numbers

We check whether:

- `result < int.MinValue / 10`, or
- `result == int.MinValue / 10` and the next digit is less than `-8`

Why `-8`?

Because `int.MinValue` is `-2147483648`, and the last digit is `-8`.

These checks let us safely build the reversed number without using a larger numeric type.

---

## 8. Why The Implemented Solution Is Good

The current `Solution.cs` is strong because it is:

- **simple**
- **efficient**
- **safe from overflow**
- **does not use 64-bit integers**
- **handles positive, negative, and trailing-zero cases correctly**

This is the standard optimal approach for this problem.

---

## 9. Time Complexity And Space Complexity

Let `d` be the number of digits in `x`.

### Time complexity

We process each digit once, so the runtime is:

- **O(d)**

Since `d` is small for 32-bit integers, this is very efficient.

### Space complexity

We use only a few integer variables:

- **O(1)** space

---

## 10. Common Mistakes To Avoid

### Mistake 1: Using a string conversion

That may be easy, but the problem is meant to be solved with arithmetic.

### Mistake 2: Forgetting overflow checks

If you reverse a large number without checking bounds, the result may exceed the 32-bit range.

### Mistake 3: Ignoring negative numbers

Negative inputs must remain negative after reversal.

### Mistake 4: Keeping trailing zeros

`120` should become `21`, not `021`.

### Mistake 5: Using a 64-bit type when the problem says not to

The follow-up explicitly says not to rely on 64-bit integers.

---

## 11. How To Approach Problems Like This In General

When a problem involves:

- digits,
- integer manipulation,
- reversal,
- or overflow concerns,

ask yourself:

1. Can I process the number one digit at a time?
2. Do I need to check bounds before each update?
3. What happens with negative inputs?
4. Do leading or trailing zeros matter?

This kind of reasoning helps a lot in integer simulation problems.

---

## 12. Topics To Read On To Get Better

If you want to improve at this type of problem, study these topics:

### Core algorithm topics

- Digit manipulation
- Integer reversal
- Overflow detection
- Simulation problems
- Boundary conditions

### Helpful supporting topics

- Modulus operator (`%`)
- Integer division (`/`)
- Sentinel checks
- Loop invariants

### C# topics worth practicing

- `int.MaxValue` and `int.MinValue`
- Integer arithmetic
- Signed remainder behavior
- Writing clean console runners for numeric problems

---

## 13. Sources To Consult

Here are good places to learn and improve your understanding.

### Official / high-quality references

1. **Microsoft Learn — C# documentation**
   - Useful for integer arithmetic, operators, and project setup.

2. **LeetCode problem discussions**
   - Great for seeing different overflow-safe reversal strategies.

3. **GeeksforGeeks**
   - Good for number reversal and overflow explanation.

4. **CP-Algorithms**
   - Helpful for learning problem-solving patterns and careful reasoning.

5. **MIT OpenCourseWare / free algorithm lectures**
   - Helpful if you want a deeper understanding of algorithm design.

### If you prefer video learning

- NeetCode
- Striver / takeUforward
- Abdul Bari
- freeCodeCamp algorithm playlists

### If you want structured practice

- LeetCode problem sets by topic
- HackerRank math and simulation tracks
- Codeforces educational rounds
- AtCoder beginner contests

---

## 14. A Good Study Plan For Problems Like This

If you want to improve steadily, try this order:

1. Practice digit-based problems.
2. Learn how integer division and modulus work.
3. Study overflow detection carefully.
4. Re-solve the problem without looking.
5. Compare your solution to an optimal one.
6. Explain the overflow logic in your own words.

That last step is especially useful.

If you can explain why the overflow checks are necessary and sufficient, you understand the problem.

---

## 15. Final Summary

This problem is best solved by reversing the integer one digit at a time.

The algorithm:

- extracts digits with `% 10`,
- removes digits with `/ 10`,
- checks for overflow before appending each digit,
- and returns `0` if the reversed value would exceed the 32-bit range.

The method in `Solution.cs` is the optimal approach because it is safe, simple, and works without using 64-bit integers.

