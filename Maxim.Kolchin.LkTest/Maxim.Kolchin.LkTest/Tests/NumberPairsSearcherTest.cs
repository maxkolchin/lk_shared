using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxim.Kolchin.LkTest.Tests
{
	[TestClass]
	public class NumberPairsSearcherTest
	{
		private NumberPairsSearcher target;

		[TestInitialize]
		public void Init()
		{
			target = new NumberPairsSearcher();
		}

		[TestMethod]
		public void SearchPairs_OnlyPositive_SomePairsFound()
		{
			// arrange
			int sum = 2;
			var list = new List<int>
			{
				1, 2, 1, 0, 1, 4, 5, 10
			};

			// act
			var actual = target.SearchPairs(list, sum);

			// assert
			Assert.AreEqual(2, actual.Count); // correct pairs count
			Assert.AreEqual(sum, actual.Select(a => a.Item1 + a.Item2).Distinct().Single()); // all pair sums are correct
		}

		[TestMethod]
		public void SearchPairs_OnlyPositive_NoPairsFound()
		{
			// arrange
			int sum = 16;
			var list = new List<int>
			{
				1, 2, 1, 0, 1, 4, 5, 10
			};

			// act
			var actual = target.SearchPairs(list, sum);

			// assert
			Assert.AreEqual(0, actual.Count); // no pairs found
		}

		[TestMethod]
		public void SearchPairs_WithNegative_SomePairsFound()
		{
			// arrange
			int sum = 2;
			var list = new List<int>
			{
				1, 2, 1, 0, 1, 4, -2, 10
			};

			// act
			var actual = target.SearchPairs(list, sum);

			// assert
			Assert.AreEqual(3, actual.Count); // correct pairs count
			Assert.AreEqual(sum, actual.Select(a => a.Item1 + a.Item2).Distinct().Single()); // all pair sums are correct
		}

		[TestMethod]
		public void SearchPairs_NoItems_NoPairsFound()
		{
			// arrange
			int sum = 2;
			var list = new List<int>();

			// act
			var actual = target.SearchPairs(list, sum);

			// assert
			Assert.AreEqual(0, actual.Count); // no pairs found
		}
	}
}
