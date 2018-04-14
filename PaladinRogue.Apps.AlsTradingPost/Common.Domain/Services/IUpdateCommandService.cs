namespace Common.Domain.Services
{
    public interface IUpdateCommandService<in TIn, out TOut>
    {
        TOut Update(TIn entity);
    }
}
