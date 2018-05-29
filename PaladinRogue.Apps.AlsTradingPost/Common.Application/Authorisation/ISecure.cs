namespace Common.Application.Authorisation
{
    public interface ISecure<out T>
    {
        T Service { get; }
    }
}
