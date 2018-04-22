using AlsTradingPost.Domain.Persistence;

namespace AlsTradingPost.Persistence.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public PlayerRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }
    }
}