# LONGEST COMMON PREFIX — C# EXPLANATION

## 1. Setting Up C# IntelliSense In This Folder

To get good C# IntelliSense and code analysis in Rider or VS Code, the folder should be part of a .NET project.

### Recommended setup
1. Install the **.NET SDK**.
2. Open the problem folder, or open the parent `c#` workspace that contains the project.
3. Keep a `.csproj` file in the same folder as the source code.
4. Let the IDE restore and index the project.

The `LongestCommonPrefix.csproj` file in this folder is enough for the IDE to understand the project structure, infer references, and provide IntelliSense.

### Why this matters
Without a `.csproj` file, the IDE may still open the file, but it will not always understand:
- target framework
- nullable analysis
- compiler references
- project-wide symbols
- better autocomplete and diagnostics

## 2. Understanding The Problem Requirements

Before coding, make sure the task is clear:

- You are given an array of strings: `strs`.
- You must find the longest prefix shared by every string.
- If no common prefix exists, return an empty string `""`.

### What the requirement is really asking
This is a **prefix comparison** problem:
- start with one candidate prefix
- compare it against every string
- shorten it until it matches all strings

## 3. Approaching The Solution

A simple and efficient approach is:

1. If the array is empty, return `""`.
2. Take the first string as the initial prefix.
3. Compare that prefix with every other string.
4. While a string does not start with the current prefix, remove the last character from the prefix.
5. If the prefix becomes empty, return `""`.
6. Return the final prefix after all comparisons.

### Why this works
Any common prefix must also be a prefix of the first string. By shrinking the prefix only when needed, you avoid extra work and keep the solution easy to understand.

## 4. Time Complexity And Why It Is Good

Let `n = strs.Length` and let `m` be the length of the common prefix candidate as it shrinks.

### Time complexity
The algorithm compares characters only as needed, so it is efficient in practice and commonly described as:

- **O(n × k)** in the worst case, where `k` is the length of the prefix being checked

If the strings are similar, the comparisons are still linear in the amount of prefix work performed.

### Space complexity
The algorithm uses only a few variables:

- **O(1)** extra space

This is optimal because you do not need any additional data structures.

## 5. Common Mistakes To Avoid

### Mistake 1: Comparing only adjacent strings
The prefix must be common to **all** strings, not just two neighboring ones.

**Better:** compare against every string.

### Mistake 2: Forgetting the empty-prefix case
If no shared prefix exists, you must return `""`.

**Better:** stop and return early when the prefix becomes empty.

### Mistake 3: Not handling strings of different lengths
A string can be shorter than the current prefix.

**Better:** always test whether the string starts with the current prefix before continuing.

### Mistake 4: Writing a solution that is harder than needed
You do not need sorting, trie structures, or complex parsing for this problem.

**Better:** use a direct prefix-shrinking approach.

## 6. How To Review Your Solution

Before finalizing, ask these questions:

- Does the code return `""` when there is no common prefix?
- Does it check every string in the array?
- Does it shrink the candidate prefix correctly?
- Is the runtime reasonable for the constraints?
- Is the code easy to read and maintain?

If the answer is yes to all of these, the solution is strong.

## 7. Final Notes

This is a good beginner-friendly problem because it teaches:
- string comparison
- prefix logic
- early exits
- writing clean linear-style solutions

The implementation in `Solution.cs` follows these principles directly.

