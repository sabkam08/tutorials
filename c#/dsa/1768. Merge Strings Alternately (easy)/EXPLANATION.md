# MERGE STRINGS ALTERNATELY — C# EXPLANATION

## 1. Setting Up C# IntelliSense In This Folder

To get good C# IntelliSense and code analysis in VS Code, the folder should be part of a .NET project.

### Recommended setup
1. Install the **.NET SDK**.
2. Install the **C#** or **C# Dev Kit** extension in VS Code.
3. Open the problem folder in VS Code, not just the single `.cs` file.
4. Keep a `.csproj` file in the same folder as the source code.

The `MergeStringsAlternately.csproj` file in this folder is enough for VS Code and the C# extension to understand the project structure, infer references, and provide IntelliSense.

### Why this matters
Without a `.csproj` file, VS Code may still open the file, but it will not always understand:
- target framework
- nullable analysis
- compiler references
- project-wide symbols
- better autocomplete and diagnostics

## 2. Understanding The Problem Requirements

Before coding, make sure the task is clear:

- You are given two strings: `word1` and `word2`.
- You must merge them by alternating characters.
- Start with `word1`.
- If one string becomes shorter, append the remaining characters from the longer string.
- Return the final merged string.

### What the requirement is really asking
This is not a complex transformation problem. It is a **two-pointer string merge** problem:
- one pointer walks through `word1`
- one pointer walks through `word2`
- each step appends at most one character from each string

## 3. Approaching The Solution

A simple and efficient approach is:

1. Create a `StringBuilder` to store the output.
2. Keep two indexes, one for each string.
3. Loop until both strings are fully consumed.
4. If `word1` still has characters left, append the next one.
5. If `word2` still has characters left, append the next one.
6. Convert the builder to a string and return it.

### Why `StringBuilder`
Strings in C# are immutable. Repeated string concatenation can create many temporary strings and slow the solution down. `StringBuilder` is the best fit here because it efficiently appends characters.

## 4. Time Complexity And Why It Is Optimal

Let `n = word1.Length` and `m = word2.Length`.

### Time complexity
The algorithm visits each character exactly once, so the time complexity is:

- **O(n + m)**

### Space complexity
The output string itself requires space for all characters, so the extra space used by the builder is:

- **O(n + m)**

This is optimal because you must at least store the final merged string.

## 5. Common Mistakes To Avoid

### Mistake 1: Using repeated string concatenation
Doing this inside a loop can be inefficient.

**Better:** use `StringBuilder`.

### Mistake 2: Forgetting the leftover characters
If one word is longer, you still need to append the remaining part.

**Better:** keep looping until both indexes reach the end.

### Mistake 3: Off-by-one errors
It is easy to accidentally stop too early or read past the end of a string.

**Better:** check bounds before reading each character.

### Mistake 4: Not thinking about edge behavior
Even though the constraints usually guarantee non-empty strings, it is still good to write code that behaves cleanly for different lengths.

## 6. How To Review Your Solution

Before finalizing, ask these questions:

- Does the code always start with `word1`?
- Does it alternate correctly?
- Does it append leftovers when one string ends first?
- Is the runtime linear?
- Is the code easy to read and maintain?

If the answer is yes to all of these, the solution is strong.

## 7. Final Notes

This is a good beginner-friendly problem because it teaches:
- two-pointer traversal
- string building in C#
- writing linear-time solutions
- handling leftover elements cleanly

The implementation in `Solution.cs` follows these principles directly.

