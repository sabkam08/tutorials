#pragma warning disable

public static class Program
{
    public static void Main(string[] args)
    {
        var solutionType = System.Reflection.Assembly.GetExecutingAssembly()
            .GetTypes()
            .First(type => type.Name == "Solution");
        var solution = Activator.CreateInstance(solutionType) ??
                       throw new InvalidOperationException("Could not create Solution instance.");

        PrintResult(InvokeConvert(solution, "PAYPALISHIRING", 3));
        PrintResult(InvokeConvert(solution, "PAYPALISHIRING", 4));
        PrintResult(InvokeConvert(solution, "A", 1));

        static string InvokeConvert(object solution, string s, int numRows)
        {
            var method = solution.GetType().GetMethod("Convert")
                         ?? throw new InvalidOperationException("Convert method was not found.");

            return (string)method.Invoke(solution, new object[] { s, numRows })!;
        }

        static void PrintResult(string result)
        {
            Console.WriteLine(result);
        }
    }
}

#pragma warning restore
