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

        PrintResult(InvokeReverse(solution, 123));
        PrintResult(InvokeReverse(solution, -123));
        PrintResult(InvokeReverse(solution, 120));

        static int InvokeReverse(object solution, int x)
        {
            var method = solution.GetType().GetMethod("Reverse")
                         ?? throw new InvalidOperationException("Reverse method was not found.");

            return (int)method.Invoke(solution, new object[] { x })!;
        }

        static void PrintResult(int result)
        {
            Console.WriteLine(result);
        }
    }
}

#pragma warning restore
