using System;
using System.Collections.Generic;
namespace Common.Domain.DomainEvents
{
	public static class DomainEventHandlerFactory
	{
		public static IDictionary<Type, IList<Delegate>> DomainEventTypeHandlers { get; set; }

		public static void Register<T>(Action<T> handler)
		{
			if (DomainEventTypeHandlers == null)
			{
				DomainEventTypeHandlers = new Dictionary<Type, IList<Delegate>>();
			}

			if (!DomainEventTypeHandlers.ContainsKey(typeof(T)))
			{
				DomainEventTypeHandlers.Add(typeof(T), new List<Delegate> {handler});
			}
			else
			{
				DomainEventTypeHandlers[typeof(T)].Add(handler);
			}
		}
	}
}
