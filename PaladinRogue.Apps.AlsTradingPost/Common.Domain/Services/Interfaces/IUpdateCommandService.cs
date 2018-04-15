namespace Common.Domain.Services.Interfaces
{
    public interface IUpdateCommandService<in TIn, out TOut>
    {
        TOut Update(TIn entity);
    }
}
