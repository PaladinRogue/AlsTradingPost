namespace PaladinRogue.Library.Core.Common.Builders
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}