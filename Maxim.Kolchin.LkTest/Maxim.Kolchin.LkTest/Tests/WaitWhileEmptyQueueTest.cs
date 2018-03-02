using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Maxim.Kolchin.LkTest
{
	[TestClass]
	public class WaitWhileEmptyQueueTest
	{
		private WaitWhileEmptyQueue<int> target;

		[TestInitialize]
		public void Init()
		{
			target = new WaitWhileEmptyQueue<int>();
		}

		[TestMethod]
		public void PushPopWithSeveralThreads_WorksCorrect()
		{
			// arrange
			int minValue = 1;
			int maxValue = 100000;
			int taskCount = 5;
			var resultQueue = new ConcurrentQueue<int>();
			var threadIdList = new ConcurrentBag<int>();
						
			var pushTasks = new List<Task>();
			for (int i=0; i<taskCount; i++)
			{
				pushTasks.Add(new Task(() =>
				{
					for (int j=minValue; j <= maxValue; j++)
					{
						threadIdList.Add(Thread.CurrentThread.ManagedThreadId);
						target.Push(j);
					}
				}));
			}

			var popTasks = new List<Task>();
			for (int i=0; i<taskCount; i++)
			{
				popTasks.Add(new Task(() =>
				{
					for (int j=minValue; j <= maxValue; j++)
					{
						threadIdList.Add(Thread.CurrentThread.ManagedThreadId);
						int item = target.Pop();
						resultQueue.Enqueue(item);
					}
				}));
			}

			// act
			pushTasks.ForEach(t => t.Start());
			popTasks.ForEach(t => t.Start());
			Task.WaitAll(pushTasks.Union(popTasks).ToArray());

			// assert
			Assert.AreEqual(maxValue * taskCount, resultQueue.Count); // all items are in result			

			var countsByItem = resultQueue.GroupBy(i => i)
				.ToDictionary(p => p.Key, p => p.Count());
			Assert.AreEqual(maxValue, countsByItem.Count); // all item values are in result

			var countOfEveryItem = countsByItem.Values.Distinct().Single();
			Assert.AreEqual(taskCount, countOfEveryItem); // 5 times for every item

			var threadIds = threadIdList.Distinct().ToList();
			Assert.IsTrue(threadIds.Count > 1); // more than 1 thread participated
		}
	}
}
