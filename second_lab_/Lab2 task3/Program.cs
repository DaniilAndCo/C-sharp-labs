using System;

namespace rangeTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Int64 a = Int64.Parse(Console.ReadLine());
            Int64 b = Int64.Parse(Console.ReadLine());
            Int64 result = Algorithm(b) - Algorithm(a - 1);
            Console.WriteLine("2 ^ {0} = {1}", result, Math.Pow(2, result));
        }

        static private Int64 Algorithm(Int64 number)
        {
            Int64 k = 2, result = 0;
            for (int i = 1; i < 64; i++) {
                result += number / k;
                k *= 2;
            }
            return result;
        }
    }
}
