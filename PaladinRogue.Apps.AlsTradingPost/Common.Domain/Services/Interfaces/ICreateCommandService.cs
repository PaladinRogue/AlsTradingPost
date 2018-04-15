namespace Common.Domain.Services.Interfaces
{
    public interface ICreateCommandService<in TIn, out TOut>
    {
        TOut Create(TIn entity);
    }
}
