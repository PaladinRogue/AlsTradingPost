using Common.Authentication.Resources.RefreshTokens;
using Common.Setup.Infrastructure.Hashing;

namespace Common.Authentication.Setup.Infrastructure.RefreshTokens
{
    public class RefreshTokenProvider : IRefreshTokenProvider
    {
        private readonly IHashFactory _hashFactory;
        
        public RefreshTokenProvider(IHashFactory hashFactory)
        {
            _hashFactory = hashFactory;
        }
        
        public string GenerateRefreshToken<T>(T data)
        {
            return _hashFactory.GenerateHash(data).Hash;
        }
    }
}