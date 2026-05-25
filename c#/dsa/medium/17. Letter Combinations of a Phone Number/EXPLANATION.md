# 17. Letter Combinations of a Phone Number

**Medium**

## Problem Summary

Given a string of digits from `2` to `9`, generate every possible letter combination that the phone number could represent.

Each digit maps to a set of letters, like the old telephone keypad:

- `2 -> abc`
- `3 -> def`
- `4 -> ghi`
- `5 -> jkl`
- `6 -> mno`
- `7 -> pqrs`
- `8 -> tuv`
- `9 -> wxyz`

The task is to return all possible strings formed by choosing one letter for each digit in the input.

## Intuition

This problem is a classic **backtracking** problem.

For each digit, we try every possible letter that digit can represent. After choosing one letter, we move to the next digit and repeat the process. When we have chosen letters for all digits, we record the completed string.

This works because the answer is a **combinatorial search** problem:

- the first digit gives multiple choices,
- each choice branches into more choices for the next digit,
- and so on until the full combination is formed.

## How to Think About the Search Tree

If the input is `23`:

- `2` gives `a`, `b`, `c`
- `3` gives `d`, `e`, `f`

So the combinations are built like this:

- start with `a`, then combine it with `d`, `e`, `f`
- start with `b`, then combine it with `d`, `e`, `f`
- start with `c`, then combine it with `d`, `e`, `f`

That produces:

- `ad`, `ae`, `af`
- `bd`, `be`, `bf`
- `cd`, `ce`, `cf`

## Step-by-Step Approach

1. Create a mapping from each digit to its corresponding letters.
2. If the input is empty, return an empty list.
3. Use a recursive helper to build the current combination one character at a time.
4. At each position, try all letters for the current digit.
5. Append a letter, recurse to the next digit, and then remove the letter before trying the next one.
6. When the current combination reaches the same length as the input digits, add it to the result list.

## Why Backtracking Works Well

Backtracking is ideal here because:

- each digit creates multiple possible branches,
- the problem asks for all valid combinations,
- and we can build each answer incrementally without storing unnecessary intermediate results.

The `append -> recurse -> remove` pattern ensures that each branch is explored correctly before moving on to the next one.

## Example 1 Walkthrough

### Input

`digits = "23"`

### Mapping

- `2 -> abc`
- `3 -> def`

### Process

Start with an empty current string:

- choose `a`
  - choose `d` -> `ad`
  - choose `e` -> `ae`
  - choose `f` -> `af`
- choose `b`
  - choose `d` -> `bd`
  - choose `e` -> `be`
  - choose `f` -> `bf`
- choose `c`
  - choose `d` -> `cd`
  - choose `e` -> `ce`
  - choose `f` -> `cf`

### Output

`["ad","ae","af","bd","be","bf","cd","ce","cf"]`

## Example 2 Walkthrough

### Input

`digits = "2"`

### Mapping

- `2 -> abc`

### Process

The possible outputs are simply:

- `a`
- `b`
- `c`

### Output

`["a","b","c"]`

## Additional Problem-Specific Examples

### Example 3

`digits = "7"`

Since `7` maps to `pqrs`, the output is:

`["p","q","r","s"]`

### Example 4

`digits = "79"`

- `7 -> pqrs`
- `9 -> wxyz`

This creates `4 x 4 = 16` combinations:

`["pw","px","py","pz","qw","qx","qy","qz","rw","rx","ry","rz","sw","sx","sy","sz"]`

### Example 5

`digits = "234"`

This produces `3 x 3 x 3 = 27` combinations.

It is a good example of how the search tree grows as more digits are added.

## Time Complexity

Let:

- `n` be the length of the input digits
- `k` be the average number of letters per digit

The total number of combinations is approximately `k^n`.

For each combination, we build a string of length `n`, so the overall time complexity is:

- **Time:** `O(n * k^n)`

## Space Complexity

The space used comes mainly from:

- the recursion depth, which is at most `n`
- the temporary current string being built
- the output list, which stores all combinations

Ignoring the output list, the extra working space is:

- **Space:** `O(n)`

## Common Mistakes

### 1. Forgetting to handle an empty input

If the input is empty, the answer should be an empty list.

### 2. Not backtracking correctly

After trying one letter, you must remove it before trying the next one.

If you forget to remove the last character, later combinations will be incorrect.

### 3. Misreading the digit-to-letter mapping

Remember that:

- `7` maps to `pqrs`
- `9` maps to `wxyz`

These have 4 letters, while most other digits have 3.

### 4. Building strings inefficiently

Repeated string concatenation can be slower than using a mutable structure like `StringBuilder`.

## How to Recognize This Type of Problem

This is a backtracking problem when you see phrases like:

- "return all possible combinations"
- "generate every possible string"
- "try each choice for each position"

That usually means you should explore a decision tree recursively.

## Recommended Study Topics

- Backtracking
- Recursion
- Decision trees
- Cartesian products
- String building with `StringBuilder`

## Learning Resources

- [Backtracking - GeeksforGeeks](https://www.geeksforgeeks.org/backtracking-algorithms/)
- [Recursion - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/recursion)
- [StringBuilder - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder)

