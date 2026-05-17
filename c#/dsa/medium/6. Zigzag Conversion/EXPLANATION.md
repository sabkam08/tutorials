# 6. Zigzag Conversion

**Medium**

## Description

This problem asks us to convert a string into a zigzag pattern across a given number of rows, then read the result row by row.

For example, the string `PAYPALISHIRING` written with `3` rows looks like this:

```text
P   A   H   N
A P L S I I G
Y   I   R
```

Reading each row from left to right gives `PAHNAPLSIIGYIR`.

The task is to implement:

```csharp
string Convert(string s, int numRows);
```

## How to Think About the Problem

The key idea is that the characters are not placed in a simple straight line. Instead, they move:

1. straight down through the rows,
2. then diagonally up,
3. then straight down again,
4. and so on.

So the process repeats in a cycle.

If `numRows = 4`, the pattern for `PAYPALISHIRING` becomes:

```text
P     I    N
A   L S  I G
Y A   H R
P     I
```

Reading row by row gives `PINALSIGYAHRPI`.

## Approach

The easiest way to solve this is to simulate the zigzag writing process.

### Step 1: Handle edge cases

If `numRows == 1`, the string stays the same because there is only one row.

If `s.Length <= numRows`, then each character can already fit into its own row, so the string also stays the same.

### Step 2: Use one builder per row

Create a `StringBuilder` for every row.

As you iterate through the string, append each character to the current row.

### Step 3: Track direction

We need to know whether we are moving:

- downward, or
- upward diagonally.

Start at row `0` and move down. When we reach the bottom row, reverse direction and move upward. When we reach the top row again, reverse direction back downward.

### Step 4: Combine the rows

Once all characters are placed into their rows, concatenate the rows in order.

That final joined string is the answer.

## Example Walkthrough

Let’s use `s = "PAYPALISHIRING"` and `numRows = 3`.

We build three rows:

- Row 0
- Row 1
- Row 2

Then place each character:

- `P` -> Row 0
- `A` -> Row 1
- `Y` -> Row 2
- `P` -> Row 1
- `A` -> Row 0
- `L` -> Row 1
- `I` -> Row 2
- `S` -> Row 1
- `H` -> Row 0
- `I` -> Row 1
- `R` -> Row 2
- `I` -> Row 1
- `N` -> Row 0
- `G` -> Row 1

So the rows become:

- Row 0: `PAHN`
- Row 1: `APLSIIG`
- Row 2: `YIR`

Joining them gives `PAHNAPLSIIGYIR`.

## Why This Works

Every character belongs to exactly one row in the zigzag pattern.

By simulating the movement and storing characters in the correct row as we go, we reproduce the same layout the problem describes.

Since the final answer is just the rows read from top to bottom, joining the row builders gives the correct converted string.

## Complexity Analysis

- **Time:** `O(n)` where `n` is the length of the string.
- **Space:** `O(n)` for storing the characters in the row builders.

## Common Mistakes

- Forgetting to handle `numRows == 1`.
- Reversing direction at the wrong row.
- Appending characters to the wrong row while moving upward.
- Trying to build the zigzag grid manually instead of using row simulation.

## Topics to Study

To get better at this type of problem, study:

- String manipulation
- Simulation problems
- Two-direction traversal
- Array or list of builders
- Pattern construction problems

## Where to Learn More

Good places to review similar ideas include:

- [Microsoft Learn](https://learn.microsoft.com/)
- [LeetCode Discussion](https://leetcode.com/discuss/)
- [GeeksforGeeks](https://www.geeksforgeeks.org/)
- [CP-Algorithms](https://cp-algorithms.com/)

Practicing pattern-based string problems will help you recognize when simulation is the right approach.

