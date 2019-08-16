using PaladinRogue.Library.Core.Domain.Entities;

namespace PaladinRogue.Library.Core.Domain.Persistence
{
    public interface IQueryRepository<T> : IGetByIdQuery<T>, IGetPageQuery<T>, IGetQuery<T>, IGetSingleQuery<T>, IAreAnyQuery<T> where T : IEntity
    {
    }
}
