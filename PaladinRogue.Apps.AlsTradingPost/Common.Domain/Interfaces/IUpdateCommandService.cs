namespace Common.Domain.Interfaces
{
    public interface IUpdateCommandService<in TIn, out TOut>
    {
        TOut Update(TIn entity);
    }
}
