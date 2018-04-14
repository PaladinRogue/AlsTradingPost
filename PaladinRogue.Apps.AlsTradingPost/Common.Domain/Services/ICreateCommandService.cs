namespace Common.Domain.Services
{
    public interface ICreateCommandService<in TIn, out TOut>
    {
        TOut Create(TIn entity);
    }
}
