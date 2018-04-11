using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Common.Domain.DomainEvents.Interfaces;
using Common.Messaging.Message.Interfaces;
using Common.Resources.Transactions;
using Microsoft.AspNetCore.Http;

namespace Common.Setup.Middleware
{
	public class TransactionPerRequestMiddleware
	{
		private static readonly IList<HttpStatusCode> SuccesStatusCodes = new List<HttpStatusCode>
		{
			HttpStatusCode.OK,
			HttpStatusCode.Created,
			HttpStatusCode.NoContent
		};

		private readonly RequestDelegate _next;

		public TransactionPerRequestMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context, ITransactionFactory transactionFactory, IDomainEventDispatcher domainEventDispatcher, IMessageDispatcher messageDispatcher)
		{
			ITransaction transaction = transactionFactory.Create();

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
}
