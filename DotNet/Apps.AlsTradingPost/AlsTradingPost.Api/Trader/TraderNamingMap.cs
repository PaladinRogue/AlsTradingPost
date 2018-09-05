using Common.Api.NamingMap;

namespace AlsTradingPost.Api.Trader
{
    public class TraderNamingMap : INamingMap
    {
        private const string Trader = "Trader";

        public void Register(INamingMapProvider namingMapProvider)
        {
            namingMapProvider.AddNamingMap<TraderResource>(Trader);
        }
    }
}
