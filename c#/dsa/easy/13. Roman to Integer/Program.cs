using System;
using System.Linq;
using System.Reflection;

var solutionType = Assembly.GetExecutingAssembly()
    .GetTypes()
    .First(type => type.Name == "Solution");

var solution = Activator.CreateInstance(solutionType)
    ?? throw new InvalidOperationException("Could not create Solution instance.");

var method = solutionType.GetMethod("RomanToInt")
    ?? throw new InvalidOperationException("Could not find RomanToInt method.");

var testCases = new[]
{
    (Input: "I", Expected: 1),
    (Input: "III", Expected: 3),
    (Input: "IV", Expected: 4),
    (Input: "IX", Expected: 9),
    (Input: "LVIII", Expected: 58),
    (Input: "XL", Expected: 40),
    (Input: "XC", Expected: 90),
    (Input: "CD", Expected: 400),
    (Input: "CM", Expected: 900),
    (Input: "MCMXCIV", Expected: 1994),
    (Input: "MMMDCCCLXXXVIII", Expected: 3888),
    (Input: "MMMCMXCIX", Expected: 3999)
};

foreach (var testCase in testCases)
{
    var actual = (int)method.Invoke(solution, new object[] { testCase.Input })!;

    if (actual != testCase.Expected)
    {
        throw new InvalidOperationException(
            $"Test failed for \"{testCase.Input}\": expected {testCase.Expected}, got {actual}.");
    }

    Console.WriteLine($"{testCase.Input} -> {actual}");
}

Console.WriteLine("All Roman numeral test cases passed.");
