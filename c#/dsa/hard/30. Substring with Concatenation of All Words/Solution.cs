using System.Collections.Generic;

public class Solution
{
    public IList<int> FindSubstring(string s, string[] words)
    {
        List<int> result = new();

        if (words.Length == 0 || s.Length == 0)
        {
            return result;
        }

        int wordLength = words[0].Length;
        int wordCount = words.Length;
        int totalLength = wordLength * wordCount;

        if (s.Length < totalLength)
        {
            return result;
        }

        Dictionary<string, int> required = new(StringComparer.Ordinal);
        foreach (string word in words)
        {
            required.TryGetValue(word, out int count);
            required[word] = count + 1;
        }

        for (int offset = 0; offset < wordLength; offset++)
        {
            int left = offset;
            int matchedWords = 0;
            Dictionary<string, int> window = new(StringComparer.Ordinal);

            for (int right = offset; right + wordLength <= s.Length; right += wordLength)
            {
                string word = s.Substring(right, wordLength);

                if (!required.ContainsKey(word))
                {
                    window.Clear();
                    matchedWords = 0;
                    left = right + wordLength;
                    continue;
                }

                window.TryGetValue(word, out int count);
                window[word] = count + 1;
                matchedWords++;

                while (window[word] > required[word])
                {
                    string leftWord = s.Substring(left, wordLength);
                    window[leftWord]--;
                    matchedWords--;
                    left += wordLength;
                }

                if (matchedWords == wordCount)
                {
                    result.Add(left);
                }
            }
        }

        result.Sort();
        return result;
    }
}

