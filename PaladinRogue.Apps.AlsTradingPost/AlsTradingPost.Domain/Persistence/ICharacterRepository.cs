using AlsTradingPost.Domain.Models;
using Common.Domain.Persistence;

namespace AlsTradingPost.Domain.Persistence
{
    public interface ICharacterRepository : IGet<Character>, IGetPage<Character>, IGetById<Character>, IGetSingle<Character>, IAdd<Character>, IUpdate<Character>, IDelete
    {
    }
}
