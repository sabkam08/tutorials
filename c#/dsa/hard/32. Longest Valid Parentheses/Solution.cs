using System.Collections.Generic;

public class Solution
{
    public int LongestValidParentheses(string s)
    {
        Stack<int> stack = new();
        stack.Push(-1);

        int maxLength = 0;

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                stack.Push(i);
            }
            else
            {
                stack.Pop();

                if (stack.Count == 0)
                {
                    stack.Push(i);
                }
                else
                {
                    maxLength = Math.Max(maxLength, i - stack.Peek());
                }
            }
        }

        return maxLength;
    }
}