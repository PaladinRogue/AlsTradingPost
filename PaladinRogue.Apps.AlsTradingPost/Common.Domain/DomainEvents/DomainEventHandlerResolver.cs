using Common.Domain.DomainEvents.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.Domain.DomainEvents
{
	public class DomainEventHandlerResolver : IDomainEventHandlerResolver
	{
		public DomainEventHandlerResolver()
		{
			Assembly assembly = Assembly.GetEntryAssembly();
			assembly.GetReferencedAssemblies();

			foreach (TypeInfo ti in assembly.DefinedTypes)
			{
				if (ti.ImplementedInterfaces.Contains(typeof(IDomainEventHandler<>)))
				{
					DomainEventHandlers.Add(assembly.CreateInstance(ti.FullName) as IDomainEventHandler<IDomainEvent>);
				}
			}
		}

		public IList<IDomainEventHandler<IDomainEvent>> DomainEventHandlers { get; set; }
		public IEnumerable<IDomainEventHandler<IDomainEvent>> ResolveAllOfType(Type type)
		{
			return DomainEventHandlers.Where(handler => handler.GetType().GetGenericTypeDefinition() == type);
		}
	}
}
