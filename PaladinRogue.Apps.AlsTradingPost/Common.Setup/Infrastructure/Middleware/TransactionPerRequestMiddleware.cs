using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Common.Application.Transactions;
using Common.Messaging.Message.Interfaces;
using Common.Setup.Infrastructure.Constants;
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

        private const HttpVerb TransactionalMethods = HttpVerb.Put | HttpVerb.Post | HttpVerb.Delete;

        private readonly RequestDelegate _next;

        public TransactionPerRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ITransactionManager transactionManager, IMessageDispatcher messageDispatcher)
        {
            if (TransactionalMethods.HasFlag(HttpVerbMapper.GetVerb(context.Request.Method)))
            {
                using (ITransaction transaction = transactionManager.Create())
                {
                    await _next.Invoke(context);

                    if (SuccesStatusCodes.Contains((HttpStatusCode)context.Response.StatusCode))
                    {
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

                if (SuccesStatusCodes.Contains((HttpStatusCode)context.Response.StatusCode))
                {
                    await messageDispatcher.DispatchMessagesAsync();
                }
            }
        }
    }
}
