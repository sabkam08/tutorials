public class Solution
{
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        if (nums1.Length > nums2.Length)
        {
            return FindMedianSortedArrays(nums2, nums1);
        }

        int m = nums1.Length;
        int n = nums2.Length;
        int leftSize = (m + n + 1) / 2;

        int low = 0;
        int high = m;

        while (low <= high)
        {
            int partition1 = low + (high - low) / 2;
            int partition2 = leftSize - partition1;

            int maxLeft1 = partition1 == 0 ? int.MinValue : nums1[partition1 - 1];
            int minRight1 = partition1 == m ? int.MaxValue : nums1[partition1];
            int maxLeft2 = partition2 == 0 ? int.MinValue : nums2[partition2 - 1];
            int minRight2 = partition2 == n ? int.MaxValue : nums2[partition2];

            if (maxLeft1 <= minRight2 && maxLeft2 <= minRight1)
            {
                if (((m + n) & 1) == 1)
                {
                    return Math.Max(maxLeft1, maxLeft2);
                }

                return (Math.Max(maxLeft1, maxLeft2) + Math.Min(minRight1, minRight2)) / 2.0;
            }

            if (maxLeft1 > minRight2)
            {
                high = partition1 - 1;
            }
            else
            {
                low = partition1 + 1;
            }
        }

        return 0.0;
    }
}

