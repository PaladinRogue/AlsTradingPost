namespace Common.Api.Builders
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}