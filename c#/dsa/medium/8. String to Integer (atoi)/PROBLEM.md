# 8. String to Integer (atoi)

**Medium**

## Description

Implement `myAtoi(string s)`, which converts a string to a 32-bit signed integer.

The conversion rules are:

- Ignore leading whitespace.
- Check for an optional `+` or `-` sign.
- Read digits until a non-digit character is encountered or the string ends.
- If no digits are read, return `0`.
- Clamp the result to the 32-bit signed integer range `[-2^31, 2^31 - 1]`.

## Examples

### Example 1

**Input:** `s = "42"`

**Output:** `42`

**Explanation:**
The string contains only digits, so the value is parsed directly as `42`.

### Example 2

**Input:** `s = "   -042"`

**Output:** `-42`

**Explanation:**
Leading whitespace is ignored, the sign is negative, and leading zeros do not affect the result.

### Example 3

**Input:** `s = "1337c0d3"`

**Output:** `1337`

**Explanation:**
Parsing stops when the first non-digit character is reached.

### Example 4

**Input:** `s = "0-1"`

**Output:** `0`

**Explanation:**
Parsing stops after the first digit because the next character is not a digit.

### Example 5

**Input:** `s = "words and 987"`

**Output:** `0`

**Explanation:**
The first character is not part of a valid number, so the result is `0`.

## Constraints

- `0 <= s.length <= 200`
- `s` consists of English letters, digits, spaces, `+`, `-`, and `.`.
