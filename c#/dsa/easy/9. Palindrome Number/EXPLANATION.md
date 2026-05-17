# 9. Palindrome Number — C# Explanation

## 1. How To Think About The Problem

At first glance, this problem looks like it could be solved by converting the number to a string and checking whether the string reads the same forward and backward.

That would work, but the follow-up asks us to solve it **without converting the integer to a string**.

That means we need a solution that works directly on the number itself.

The main idea is to compare the number from both ends using arithmetic.

---

## 2. Understanding The Requirements Carefully

You are given an integer `x`.

You must return:

- `true` if `x` is a palindrome,
- `false` otherwise.

### What is a palindrome number?

A number is a palindrome if it reads the same from left to right and from right to left.

Examples:

- `121` → palindrome
- `123` → not a palindrome
- `10` → not a palindrome
- `-121` → not a palindrome

### Important observations

1. Negative numbers are never palindromes because of the `-` sign.
2. Numbers ending in `0` are not palindromes unless the number is exactly `0`.
3. We do not need to reverse the whole number.
4. We can solve the problem by reversing only half of the number.

That last point is the key to the optimal solution.

---

## 3. Why Reversing Only Half Is Enough

If we reverse the entire number, we might run into:

- extra work,
- possible overflow concerns,
- and unnecessary computation.

Instead, we only reverse half of the digits and compare that reversed half to the remaining half.

### Example

Suppose `x = 1221`.

We can split it conceptually into:

- left half: `12`
- right half: `21`

If we reverse the right half while extracting digits, we can compare the two halves directly.

For odd-length numbers, the middle digit does not matter.

Example:

- `12321`
- the middle digit is `3`
- left half and right half should still match after ignoring the middle digit

---

## 4. Approach Used In `Solution.cs`

The implementation uses the classic **half-reversal** technique.

### Step 1: Reject impossible cases early

We immediately return `false` if:

- `x < 0`
- `x` ends in `0` and `x != 0`

Why?

- Negative numbers are never palindromes.
- A positive number ending in `0` would need to start with `0`, which is not possible in standard integer form.

### Step 2: Reverse only half of the digits

We create a variable like `reversedHalf`.

While `x > reversedHalf`, we:

- take the last digit of `x`,
- append it to `reversedHalf`,
- remove that last digit from `x`.

This continues until the reversed half is at least as large as the remaining half.

### Step 3: Compare the two halves

When the loop ends, there are two possible cases:

- **Even number of digits**: `x == reversedHalf`
- **Odd number of digits**: `x == reversedHalf / 10`

Why divide by `10` in the odd case?

Because the middle digit does not need to match anything.

---

## 5. Example Walkthrough

### Example 1

```text
x = 121
```

Start:

- `x = 121`
- `reversedHalf = 0`

First iteration:

- take last digit `1`
- `reversedHalf = 1`
- `x = 12`

Second iteration:

- take last digit `2`
- `reversedHalf = 12`
- `x = 1`

Now stop because `x <= reversedHalf`.

Compare:

- `x == reversedHalf` → `1 == 12` → false
- `x == reversedHalf / 10` → `1 == 1` → true

So the number is a palindrome.

---

### Example 2

```text
x = -121
```

This is negative, so we return `false` immediately.

---

### Example 3

```text
x = 10
```

This ends in `0` and is not `0` itself, so it cannot be a palindrome.

We return `false` immediately.

---

## 6. Why This Works

The logic works because palindrome numbers have symmetrical digit structure.

By reversing only half of the number:

- we avoid unnecessary work,
- we avoid reversing the entire integer,
- and we can compare the two halves directly.

This gives us an efficient and elegant solution.

---

## 7. Time Complexity And Space Complexity

Let `d` be the number of digits in `x`.

### Time complexity

We process only half of the digits, so the runtime is:

- **O(log10(x))**

More informally, that is proportional to the number of digits in the number.

### Space complexity

We use only a few integer variables:

- **O(1)** space

---

## 8. Common Mistakes To Avoid

### Mistake 1: Converting the number to a string when the follow-up asks not to

That is simpler, but it does not satisfy the follow-up requirement.

### Mistake 2: Forgetting to reject negative numbers

Negative numbers are never palindromes.

### Mistake 3: Forgetting the trailing zero rule

`10` is not a palindrome because it becomes `01` when reversed.

### Mistake 4: Reversing the entire number

That is unnecessary and can be less elegant than reversing only half.

### Mistake 5: Incorrect comparison for odd-length numbers

Remember:

- even length → `x == reversedHalf`
- odd length → `x == reversedHalf / 10`

---

## 9. How To Approach Problems Like This In General

When a problem asks you to check symmetry or reversal, ask yourself:

1. Can I use arithmetic instead of strings?
2. Can I avoid processing the entire number if only half is enough?
3. Are there special cases like negative numbers or trailing zeros?
4. Can I compare parts of the number directly?

This kind of reasoning is very useful in integer and digit problems.

---

## 10. Topics To Read On To Get Better

To improve at this type of problem, study:

- Integer arithmetic
- Digit manipulation
- Palindrome patterns
- Reversal logic
- Boundary condition handling
- Overflow awareness

### Helpful supporting topics

- Modulus operator (`%`)
- Integer division (`/`)
- Loop invariants
- Conditional checks

---

## 11. Sources To Consult

Good places to learn more include:

1. **Microsoft Learn**
   - Useful for C# syntax, integer arithmetic, loops, and project setup.

2. **LeetCode Discussions**
   - Helpful for seeing different palindrome-checking strategies.

3. **GeeksforGeeks**
   - Good for integer reversal and palindrome number explanations.

4. **CP-Algorithms**
   - Great for learning algorithmic reasoning and edge-case handling.

5. **freeCodeCamp / YouTube algorithm tutorials**
   - Helpful if you prefer visual explanations.

### Good search topics

- palindrome number without string
- reverse half integer palindrome
- digit manipulation problems
- C# modulus and integer division
- palindrome edge cases

---

## 12. Final Summary

The best solution is to avoid converting the number to a string and instead reverse only half of the number.

The algorithm:

- rejects impossible cases early,
- reverses digits from the end of the number,
- compares the two halves when done.

This is efficient, clean, and satisfies the follow-up requirement.

The solution in `Solution.cs` is the standard optimal approach for this problem.

