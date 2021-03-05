using System.Globalization;
using System;
namespace MonthsTask
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Write("Here the list of all languages you can use\nEnter needed language by typing the name from the list: \n");
			foreach (CultureInfo i in CultureInfo.GetCultures(CultureTypes.NeutralCultures))
			{
				Console.WriteLine(i + " - " + i.EnglishName);
			}
			string culture = Console.ReadLine(); ;
			int indexOfMonth = 1;
			DateTime Months = new DateTime(2021, indexOfMonth, 1);
			while (indexOfMonth <= 12) {
				Console.WriteLine(Months.ToString("MMM", new CultureInfo(culture)));
				Months = Months.AddMonths(1);
				++indexOfMonth;
			}
		}
	
	}
}
