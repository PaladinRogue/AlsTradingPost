using System.Threading.Tasks;
using Authentication.Persistence;
using Common.Domain.DomainEvents.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Authentication.Setup.Middleware
{
    public class TransactionPerRequestMiddleware
    {
		private readonly RequestDelegate _next;

        public TransactionPerRequestMiddleware(RequestDelegate next)
        {
	        _next = next;
        }

        public async Task Invoke(HttpContext context, AuthenticationDbContext dbContext, IDomainEventDispatcher domainEventDispatcher)
        {
			var transaction = dbContext.Database.BeginTransaction();

            await _next.Invoke(context);

            if (context.Response.StatusCode == 200)
            {
                transaction.Commit();
	            domainEventDispatcher.DispatchEvents();
			}
			else
            {
                transaction.Rollback();
            }
        }
    }
}
