using System.Text;

public class Solution
{
    private static readonly string[] DigitToLetters =
    {
        "",
        "",
        "abc",
        "def",
        "ghi",
        "jkl",
        "mno",
        "pqrs",
        "tuv",
        "wxyz"
    };

    public IList<string> LetterCombinations(string digits)
    {
        var result = new List<string>();

        if (string.IsNullOrEmpty(digits))
        {
            return result;
        }

        var current = new StringBuilder(digits.Length);
        Backtrack(digits, 0, current, result);
        return result;
    }

    private void Backtrack(string digits, int index, StringBuilder current, IList<string> result)
    {
        if (index == digits.Length)
        {
            result.Add(current.ToString());
            return;
        }

        int digit = digits[index] - '0';
        string letters = DigitToLetters[digit];

        foreach (char letter in letters)
        {
            current.Append(letter);
            Backtrack(digits, index + 1, current, result);
            current.Length--;
        }
    }
}

