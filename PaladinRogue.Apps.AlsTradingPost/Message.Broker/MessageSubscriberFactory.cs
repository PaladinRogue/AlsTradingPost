using System;
using System.Collections.Generic;

namespace Message.Broker
{
	public static class MessageSubscriberFactory
	{
		private static IDictionary<Type, IList<Delegate>> _messageTypeHandlers;

		public static IEnumerable<Delegate> GetAllOfType(Type type)
		{
			return _messageTypeHandlers.ContainsKey(type) ? _messageTypeHandlers[type] : new List<Delegate>();
		}

		public static void Register<T>(Action<T> handler)
		{
			if (_messageTypeHandlers == null)
			{
				_messageTypeHandlers = new Dictionary<Type, IList<Delegate>>();
			}

			if (!_messageTypeHandlers.ContainsKey(typeof(T)))
			{
				_messageTypeHandlers.Add(typeof(T), new List<Delegate> {handler});
			}
			else
			{
				_messageTypeHandlers[typeof(T)].Add(handler);
			}
		}
	}
}
