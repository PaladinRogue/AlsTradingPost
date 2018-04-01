using System;
using System.Collections.Generic;
namespace Common.Domain.DomainEvents
{
	public static class DomainEventHandlerFactory
	{
		private static IDictionary<Type, IList<Delegate>> _domainEventTypeHandlers;

		public static IEnumerable<Delegate> GetAllOfType(Type type)
		{
			return _domainEventTypeHandlers.ContainsKey(type) ? _domainEventTypeHandlers[type] : new List<Delegate>();
		}

		public static void Register<T>(Action<T> handler)
		{
			if (_domainEventTypeHandlers == null)
			{
				_domainEventTypeHandlers = new Dictionary<Type, IList<Delegate>>();
			}

			if (!_domainEventTypeHandlers.ContainsKey(typeof(T)))
			{
				_domainEventTypeHandlers.Add(typeof(T), new List<Delegate> {handler});
			}
			else
			{
				_domainEventTypeHandlers[typeof(T)].Add(handler);
			}
		}
	}
}
