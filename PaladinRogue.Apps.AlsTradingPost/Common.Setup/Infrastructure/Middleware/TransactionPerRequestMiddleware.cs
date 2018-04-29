using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Domain.DomainEvents.Interfaces;
using Common.Messaging.Message.Interfaces;
using Common.Setup.Infrastructure.Constants;
using Common.Setup.Infrastructure.Transactions;
using Microsoft.AspNetCore.Http;

namespace Common.Setup.Infrastructure.Middleware
{
	public class TransactionPerRequestMiddleware
	{
		private static readonly IList<HttpStatusCode> SuccesStatusCodes = new List<HttpStatusCode>
		{
			HttpStatusCode.OK,
			HttpStatusCode.Created,
			HttpStatusCode.NoContent
		};

		private readonly string[] _transactionalMethods =
		{
			HttpVerbs.Put, HttpVerbs.Post, HttpVerbs.Delete
		};

		private readonly RequestDelegate _next;

		public TransactionPerRequestMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context, ITransactionFactory transactionFactory, IDomainEventDispatcher domainEventDispatcher, IMessageDispatcher messageDispatcher)
		{
			if (_transactionalMethods.Contains(context.Request.Method))
			{
				using (ITransaction transaction = transactionFactory.Create())
				{
					await _next.Invoke(context);
		
					if (SuccesStatusCodes.Contains((HttpStatusCode)context.Response.StatusCode))
					{
						await domainEventDispatcher.DispatchEventsAsync();
						transaction.Commit();
						
						await messageDispatcher.DispatchMessagesAsync();
					}
					else
					{
						transaction.Rollback();
					}
				}
			}
			else
			{
				await _next.Invoke(context);

				if (SuccesStatusCodes.Contains((HttpStatusCode) context.Response.StatusCode))
				{
					await domainEventDispatcher.DispatchEventsAsync();
					await messageDispatcher.DispatchMessagesAsync();
				}
			}
		}
	}
}
