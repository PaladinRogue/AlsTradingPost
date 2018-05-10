using AlsTradingPost.Domain.Persistence;

namespace AlsTradingPost.Persistence.Repositories
{
    public class MagicItemRepository : IMagicItemRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public MagicItemRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }
    }
}