using System;
using MathAnalyzerSpace;

namespace TestLib
{
    internal class TestLib
    {
        private static void Main() //string[] args
        {
            //string str = "(42+1/(42+1))/(3+2*41/(42+2))"; // test string
            string str = "5+9*9-10";

            Console.WriteLine(str);
            Console.WriteLine(MathAnalyzer.Delimiter(str));
            Console.WriteLine(MathAnalyzer.Solution(str));
        }
    }
}
