using System;
using System.Collections.Generic;
using System.Linq;

namespace Maxim.Kolchin.LkTest
{
	class Program
	{
		static void Main(string[] args)
		{
			int sum = 2;
			var list = new List<int>
			{
				-31, 1, 2, 1, 0, 1, 4, -2, 10, -8, 33, 0, 33, -31
			};

			var pairs = new NumberPairsSearcher().SearchPairs(list, sum);

			string sourceStr = string.Join(",", list);
			string resultStr = string.Join(",", pairs.Select(p => $"({p.Item1},{p.Item2})"));
			Console.Write($"({sourceStr}) => {resultStr}");

			Console.ReadKey();
		}
	}
}
