# 14. Longest Common Prefix

**Easy**

## Description

Write a function that finds the longest common prefix string among an array of strings.

If there is no common prefix, return an empty string `""`.

## Examples

### Example 1

**Input:** `strs = ["flower", "flow", "flight"]`

**Output:** `"fl"`

**Explanation:**
The longest prefix shared by all strings is `fl`.

### Example 2

**Input:** `strs = ["dog", "racecar", "car"]`

**Output:** `""`

**Explanation:**
There is no common prefix among the input strings.

## Constraints

- `1 <= strs.length <= 200`
- `0 <= strs[i].length <= 200`
- `strs[i]` consists of only lowercase English letters if it is non-empty.
