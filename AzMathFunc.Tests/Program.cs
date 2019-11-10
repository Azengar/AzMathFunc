using System;

namespace AzMathFunc.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            SampledFunc normal = DiceProbFuncs.GetDiceProbFunc("2d6");
            normal.PlotToConsole();

            WriteSeparator("normal");

            SampledFunc cumulative = normal.GetCumulative();
            cumulative.PlotToConsole();

            WriteSeparator("cumulative");

            SampledFunc inverse = cumulative.GetInverse();
            inverse.PlotToConsole(5f);

            WriteSeparator("inverse");
        }

        static void WriteSeparator(string header = null)
        {
            Console.WriteLine();
            if (header != null) Console.WriteLine(header);
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine();
        }
    }
}
