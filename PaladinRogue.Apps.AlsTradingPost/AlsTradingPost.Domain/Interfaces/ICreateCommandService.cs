namespace AlsTradingPost.Domain.Interfaces
{
    public interface ICreateCommandService<in TIn, out TOut>
    {
        TOut Create(TIn entity);
    }
}
