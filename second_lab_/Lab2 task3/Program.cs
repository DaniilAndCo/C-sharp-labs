using System;

namespace rangeTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Int64 a, b;
            while (!Int64.TryParse(Console.ReadLine(), out a))
            {
				Console.WriteLine("Enter valid data!");
            }
            while (!Int64.TryParse(Console.ReadLine(), out b))
            {
                Console.WriteLine("Enter valid data!");
            }
            Int64 result = Algorithm(b) - Algorithm(a - 1);
            Console.WriteLine("result => 2 ^ {0}", result);
        }

        static private Int64 Algorithm(Int64 number)
        {
            Int64 k = 2, result = 0;
            for (int i = 1; i < 64; ++i) {
                result += number / k;
                k *= 2;
            }
            return result;
        }
    }
}
