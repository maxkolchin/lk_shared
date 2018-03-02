using System;
using System.Collections.Generic;
using System.Linq;

namespace Maxim.Kolchin.LkTest
{
	class NumberPairsSearcher
	{
		public IReadOnlyList<Tuple<int, int>> SearchPairs(
			IReadOnlyList<int> list, int pairSum)
		{
			var pairs = new List<Tuple<int, int>>();
			var countsByItem = list.GroupBy(i => i).ToDictionary(p => p.Key, p => p.Count());

			while (countsByItem.Any())
			{
				int first = countsByItem.Keys.First();
				DecreaseValueCount(countsByItem, first);

				int second = pairSum - first;
				if (countsByItem.ContainsKey(second))
				{
					pairs.Add(new Tuple<int, int>(first, second));
					DecreaseValueCount(countsByItem, second);
				}
			}

			return pairs;
		}

		private void DecreaseValueCount(Dictionary<int, int> countsByItem, int value)
		{
			int valueCount = countsByItem[value];
			if (valueCount == 1)
				countsByItem.Remove(value);
			else
				countsByItem[value] = valueCount - 1;
		}
	}
}
