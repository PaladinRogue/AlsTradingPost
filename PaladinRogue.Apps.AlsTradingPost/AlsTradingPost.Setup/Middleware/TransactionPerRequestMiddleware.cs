using System.Threading.Tasks;
using AlsTradingPost.Persistence;
using Microsoft.AspNetCore.Http;

namespace AlsTradingPost.Setup.Middleware
{
    public class TransactionPerRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public TransactionPerRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, AlsTradingPostDbContext dbContext)
        {
            var transaction = dbContext.Database.BeginTransaction();

            await _next.Invoke(context);

            if (context.Response.StatusCode == 200)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }
        }
    }
}
