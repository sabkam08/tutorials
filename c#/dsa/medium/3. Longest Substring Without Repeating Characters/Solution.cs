using System;
using System.Collections.Generic;

public class Solution
{
    public int LengthOfLongestSubstring(string s)
    {
        var lastSeen = new Dictionary<char, int>();
        int left = 0;
        int best = 0;

        for (int right = 0; right < s.Length; right++)
        {
            char current = s[right];

            if (lastSeen.TryGetValue(current, out int previousIndex) && previousIndex >= left)
            {
                left = previousIndex + 1;
            }

            lastSeen[current] = right;
            best = Math.Max(best, right - left + 1);
        }

        return best;
    }
}

