using System.Reflection;

#pragma warning disable CS8602

var tests = new[]
{
    new TestCase(
        "23",
        new[] { "ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf" }),
    new TestCase(
        "2",
        new[] { "a", "b", "c" }),
    new TestCase(
        "7",
        new[] { "p", "q", "r", "s" }),
    new TestCase(
        "79",
        new[]
        {
            "pw", "px", "py", "pz",
            "qw", "qx", "qy", "qz",
            "rw", "rx", "ry", "rz",
            "sw", "sx", "sy", "sz"
        }),
    new TestCase(
        "234",
        new[]
        {
            "adg", "adh", "adi",
            "aeg", "aeh", "aei",
            "afg", "afh", "afi",
            "bdg", "bdh", "bdi",
            "beg", "beh", "bei",
            "bfg", "bfh", "bfi",
            "cdg", "cdh", "cdi",
            "ceg", "ceh", "cei",
            "cfg", "cfh", "cfi"
        }),
    new TestCase(
        "568",
        new[]
        {
            "jmt", "jmu", "jmv",
            "jnt", "jnu", "jnv",
            "jot", "jou", "jov",
            "kmt", "kmu", "kmv",
            "knt", "knu", "knv",
            "kot", "kou", "kov",
            "lmt", "lmu", "lmv",
            "lnt", "lnu", "lnv",
            "lot", "lou", "lov"
        })
};

var totalPassed = 0;
var solutionType = Assembly.GetExecutingAssembly().GetTypes().First(type => type.Name == "Solution");
var solution = Activator.CreateInstance(solutionType) ?? throw new InvalidOperationException("Could not create Solution instance.");
var method = solutionType.GetMethod("LetterCombinations") ?? throw new InvalidOperationException("Could not find LetterCombinations method.");

foreach (var test in tests)
{
    var actual = (IList<string>)method.Invoke(solution, new object[] { test.Digits })!;
    var expected = test.Expected.OrderBy(value => value).ToArray();
    var normalizedActual = actual.OrderBy(value => value).ToArray();

    if (!expected.SequenceEqual(normalizedActual))
    {
        Console.WriteLine($"FAILED for input: \"{test.Digits}\"");
        Console.WriteLine($"Expected: [{string.Join(", ", expected.Select(value => $"\"{value}\""))}]");
        Console.WriteLine($"Actual:   [{string.Join(", ", normalizedActual.Select(value => $"\"{value}\""))}]");
        return;
    }

    Console.WriteLine($"PASSED for input: \"{test.Digits}\" => [{string.Join(", ", normalizedActual.Select(value => $"\"{value}\""))}]");
    totalPassed++;
}

Console.WriteLine($"All {totalPassed} Letter Combinations of a Phone Number test cases passed.");

record TestCase(string Digits, string[] Expected);

