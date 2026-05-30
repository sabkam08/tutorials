# 32. Longest Valid Parentheses — C# Explanation

## 1. How To Think About The Problem

This problem asks for the length of the longest substring made of valid, well-formed parentheses.

At first, it may look like you can simply scan the string and count matching pairs, but that is not enough. A valid substring must be continuous, and the longest one may appear inside a larger string that also contains invalid parts.

That means we need a way to track:

- where valid groups begin,
- where invalid breaks happen,
- and how long each valid stretch is.

A very effective way to solve this is with a **stack of indices**.

---

## 2. Understanding The Requirements Carefully

You are given a string `s` containing only:

- `(`
- `)`

You must return the length of the longest valid parentheses substring.

### Important observations

1. A valid parentheses substring must be continuous.
2. A substring is valid only if every opening parenthesis has a matching closing parenthesis.
3. An invalid character position can break a valid sequence.
4. The answer is the **length**, not the substring itself.

### Examples

- `(()` → longest valid substring is `()`, so the answer is `2`
- `)()())` → longest valid substring is `()()`, so the answer is `4`
- `""` → there is no valid substring, so the answer is `0`

---

## 3. Why This Is A Stack Problem

Parentheses matching is naturally tied to the idea of opening and closing pairs.

A stack helps because:

- when we see `(`, we remember where it appeared,
- when we see `)`, we try to match it with the most recent unmatched `(`,
- if a match cannot be made, that position becomes a new boundary.

Instead of storing the parentheses themselves, we store their **indices**.

That is important because the answer depends on the **distance** between positions.

---

## 4. Core Idea Of The Index Stack

The solution uses a stack of indices, not characters.

### Why store indices?

Because once a valid pair is found, the length of the current valid substring is:

- current index - boundary index

### The sentinel value

The implementation starts the stack with:

- `-1`

This acts as a starting boundary.

It helps handle cases where the first valid substring begins at index `0`.

### How the stack works

- If we see `(`, we push its index.
- If we see `)`, we pop one index from the stack.
- After popping:
   - if the stack becomes empty, we push the current index as a new boundary,
   - otherwise, we compute the length of the valid substring ending at the current index.

---

## 5. Approach Used In `Solution.cs`

The implementation follows the standard stack-based approach.

### Step 1: Initialize the stack

The stack begins with `-1`.

This gives us a starting reference point for valid substring lengths.

### Step 2: Scan the string from left to right

For each character:

- if it is `(`, push its index,
- if it is `)`, pop the stack.

### Step 3: Handle unmatched closing parentheses

If we pop and the stack becomes empty, that means there is no available opening parenthesis to match the current `)`.

So we push the current index as a new base boundary.

### Step 4: Measure valid substrings

If the stack is not empty after popping, then a valid substring exists ending at the current position.

Its length is:

- `currentIndex - stack.Peek()`

That value is compared with the current maximum length.

---

## 6. Why This Logic Works

The stack always stores the most recent unmatched opening parentheses and the latest invalid boundary.

That means:

- the top of the stack represents the position just before the current valid substring starts,
- any valid substring ending at the current index must begin after that position.

So the difference:

- `currentIndex - stack.Peek()`

gives the length of the longest valid substring ending at `currentIndex`.

Because we examine every position once, we find the maximum over the whole string.

---

## 7. Example Walkthrough

### Example 1

```text
s = "(()"