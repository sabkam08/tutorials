using System.Text;

namespace ZigzagConversion;

public class Solution
{
    public string Convert(string s, int numRows)
    {
        if (numRows == 1 || s.Length <= numRows)
        {
            return s;
        }

        var rows = new StringBuilder[numRows];
        for (int i = 0; i < numRows; i++)
        {
            rows[i] = new StringBuilder();
        }

        int currentRow = 0;
        int direction = 1;

        foreach (char ch in s)
        {
            rows[currentRow].Append(ch);

            if (currentRow == 0)
            {
                direction = 1;
            }
            else if (currentRow == numRows - 1)
            {
                direction = -1;
            }

            currentRow += direction;
        }

        var result = new StringBuilder(s.Length);
        for (int i = 0; i < numRows; i++)
        {
            result.Append(rows[i]);
        }

        return result.ToString();
    }
}
