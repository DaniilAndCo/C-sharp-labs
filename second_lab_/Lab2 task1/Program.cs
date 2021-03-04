using System;

namespace StrTask
{
	class Program
	{
		static void Main(string[] args)
		{
			string strNumber = Console.ReadLine();
			string beforeDot = "", afterDot = "";
			int indexOfDot = strNumber.IndexOf('.');
			if (indexOfDot == -1) indexOfDot = strNumber.Length;
			int startIndex = 0;
			bool isNegative = (strNumber[0] == '-');
			if (isNegative) ++startIndex;
			for (int i = startIndex; i < indexOfDot; ++i) {
				beforeDot += strNumber[i];
			}
			for (int i = indexOfDot + 1; i < strNumber.Length; ++i)
			{
				afterDot += strNumber[i];
			}
			double answer = FromStringToInt(beforeDot) + FromStringToInt(afterDot) / Math.Pow(10, afterDot.Length);
			if (isNegative) answer *= -1;
			Console.WriteLine(answer);
		}
		static private int FromStringToInt(string strNumber) {
			int result = 0;
			int k = (int)Math.Pow(10, strNumber.Length - 1);
			for (int i = 0; i < strNumber.Length; ++i){
				result += k * ((int)strNumber[i] - (int)'0');
				k /= 10;
			}
			return result;	
		}
	}
}
