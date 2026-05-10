# 4. Median of Two Sorted Arrays

**Hard**

## Description

Given two sorted integer arrays `nums1` and `nums2`, find the median of the combined values.

The solution should run in logarithmic time with respect to the total number of elements.

## Examples

### Example 1

**Input:** `nums1 = [1, 3]`, `nums2 = [2]`

**Output:** `2.0`

**Explanation:**
After merging the two arrays, the combined sequence is `[1, 2, 3]`, and the middle value is `2`.

### Example 2

**Input:** `nums1 = [1, 2]`, `nums2 = [3, 4]`

**Output:** `2.5`

**Explanation:**
After merging the two arrays, the combined sequence is `[1, 2, 3, 4]`. The median is the average of the two middle values: `(2 + 3) / 2 = 2.5`.

## Constraints

- `0 <= nums1.length, nums2.length <= 1000`
- `1 <= nums1.length + nums2.length <= 2000`
- `-10^6 <= nums1[i], nums2[i] <= 10^6`
- `nums1` and `nums2` are sorted in non-decreasing order.

