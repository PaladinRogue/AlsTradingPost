namespace Common.Resources.Builders
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}