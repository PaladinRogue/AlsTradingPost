namespace PaladinRogue.Libray.Core.Common.Builders
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}