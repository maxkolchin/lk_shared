using System;
using System.Collections.Generic;
using System.Threading;

namespace Maxim.Kolchin.LkTest
{
	/// <summary> Очередь заставляет ждать поток, который извлекает элемент, если очередь пуста	</summary>
	/// <remarks> 
	/// В задании сказано "Операция pop ждет пока не появится новый элемент". Буквально это означает,
	/// что Pop должен независимо от длины очереди остановиться и ждать, пока не поступит кто-то новый.
	/// Поскольку это нелогичное поведение, я посчитал это неточностью постановки и сделал как сказано выше.
	/// </remarks>
	class WaitWhileEmptyQueue<T>
	{
		private readonly Queue<T> _queue = new Queue<T>();
		private readonly AutoResetEvent _autoevent = new AutoResetEvent(false);

		public void Push(T item)
		{
			lock(_queue)
			{
				_queue.Enqueue(item);
				_autoevent.Set();
			}
		}

		public T Pop()
		{
			_autoevent.WaitOne();
			lock(_queue)
			{
				T item = _queue.Dequeue();
				if (_queue.Count > 0)
					_autoevent.Set();
				return item;
			}
		}
	}
}
