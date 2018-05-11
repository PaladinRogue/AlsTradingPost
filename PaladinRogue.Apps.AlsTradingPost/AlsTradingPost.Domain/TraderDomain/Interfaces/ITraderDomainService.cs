namespace AlsTradingPost.Domain.TraderDomain
{
    public interface ITraderDomainService
    {
        RegisteredTraderProjection Register(RegisterTraderDdto registerTraderDdto);
    }
}