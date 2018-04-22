using AlsTradingPost.Domain.Persistence;

namespace AlsTradingPost.Persistence.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public ItemRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }
    }
}