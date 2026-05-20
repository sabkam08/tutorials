public class Solution
{
    public int MyAtoi(string s)
    {
        int index = 0;
        int length = s.Length;

        while (index < length && s[index] == ' ')
        {
            index++;
        }

        int sign = 1;
        if (index < length && (s[index] == '+' || s[index] == '-'))
        {
            sign = s[index] == '-' ? -1 : 1;
            index++;
        }

        long result = 0;

        while (index < length && char.IsDigit(s[index]))
        {
            int digit = s[index] - '0';
            result = result * 10 + digit;

            long signedResult = sign * result;
            if (signedResult > int.MaxValue)
            {
                return int.MaxValue;
            }

            if (signedResult < int.MinValue)
            {
                return int.MinValue;
            }

            index++;
        }

        return (int)(sign * result);
    }
}

