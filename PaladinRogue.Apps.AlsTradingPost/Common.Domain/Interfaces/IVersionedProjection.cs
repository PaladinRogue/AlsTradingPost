namespace Common.Domain.Interfaces
{
    public interface IVersionedProjection : IProjection, IVersion<IConcurrencyVersion>
    {
    }
}
