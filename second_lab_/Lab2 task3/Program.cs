using System.Globalization;
using System;
using System.Linq;
using System.Collections.Generic;
namespace StrTask
{
	class Program
	{
		static void Main(string[] args)
		{
		
			ulong a, b, amount = 0;
			a = ulong.Parse(Console.ReadLine());
			b = ulong.Parse(Console.ReadLine());
			List<ulong> sieve = new List<ulong>();
			for (ulong i = a; i <= b; ++i)
			{
				sieve.Add(i);
			}
			//	List<ulong> sieve = Enumerable.Range((int)a, (int)b - (int)a + 1).ToList();		
			for (ulong k = 2; k <= b; k *= 2)
			{
					amount += (ulong)(sieve.Count - sieve.RemoveAll(i => i % k != 0));				
			}
			Console.WriteLine("answer is 2 ^ {0} = {1}", amount,  Math.Pow(2, amount));
		}
		static private bool isDivided(ulong number, ulong k)
		{
			return (number % k == 0) ? true : false;
		}
	}
}