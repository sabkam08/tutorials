using System;
using System.Collections.Generic;

public class Solution
{
    public int RomanToInt(string s)
    {
        var values = new Dictionary<char, int>
        {
            ['I'] = 1,
            ['V'] = 5,
            ['X'] = 10,
            ['L'] = 50,
            ['C'] = 100,
            ['D'] = 500,
            ['M'] = 1000
        };

        int total = 0;
        int previous = 0;

        for (int i = s.Length - 1; i >= 0; i--)
        {
            int current = values[s[i]];

            if (current < previous)
            {
                total -= current;
            }
            else
            {
                total += current;
                previous = current;
            }
        }

        return total;
    }
}
