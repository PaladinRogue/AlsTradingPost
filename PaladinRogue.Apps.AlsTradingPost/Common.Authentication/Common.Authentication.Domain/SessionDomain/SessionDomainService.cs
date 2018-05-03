using System;
using AutoMapper;
using Common.Authentication.Domain.SessionDomain.Exceptions;
using Common.Authentication.Domain.SessionDomain.Interfaces;
using Common.Authentication.Domain.SessionDomain.Models;
using Common.Authentication.Resources.RefreshTokens;

namespace Common.Authentication.Domain.SessionDomain
{
    public class SessionDomainService : ISessionDomainService
    {
        private readonly ISessionCommandService _sessionCommandService;
        private readonly ISessionQueryService _sessionQueryService;
        private readonly IRefreshTokenProvider _refreshTokenProvider;
        private readonly IMapper _mapper;

        public SessionDomainService(ISessionCommandService sessionCommandService,
            IRefreshTokenProvider refreshTokenProvider,
            ISessionQueryService sessionQueryService,
            IMapper mapper)
        {
            _sessionCommandService = sessionCommandService;
            _refreshTokenProvider = refreshTokenProvider;
            _sessionQueryService = sessionQueryService;
            _mapper = mapper;
        }

        public RefreshSessionProjection Refresh(RefreshSessionDdto refreshSessionDdto)
        {
            if (refreshSessionDdto == null)
            {
                throw new ArgumentNullException(nameof(refreshSessionDdto));
            }

            SessionProjection sessionProjection = _sessionQueryService.GetById(refreshSessionDdto.Id);
            if (sessionProjection.IsRevoked)
            {
                throw new SessionRevokedDomainException();
            }

            if (sessionProjection.RefreshToken != refreshSessionDdto.RefreshToken)
            {
                throw new RefreshTokenInvalidDomainException();
            }
            
            return _mapper.Map<SessionProjection, RefreshSessionProjection>(_sessionCommandService.Update(
                new UpdateSessionDdto
                {
                    Id = refreshSessionDdto.Id,
                    RefreshToken = _refreshTokenProvider.GenerateRefreshToken(refreshSessionDdto.Id),
                    Revoked = false,
                    Version = sessionProjection.Version
                }));
        }

        public CreateSessionProjection Create(Guid sessionId)
        {
            SessionProjection existingSession = _sessionQueryService.GetById(sessionId);
            if (existingSession == null)
            {
                existingSession = _sessionCommandService.Create(new CreateSessionDdto
                {
                    Id = sessionId,
                    RefreshToken = _refreshTokenProvider.GenerateRefreshToken(sessionId),
                    Revoked = false
                });
            }
            else
            {
                if (existingSession.IsRevoked)
                {
                    existingSession = _sessionCommandService.Update(new UpdateSessionDdto
                    {
                        Id = existingSession.Id,
                        RefreshToken = _refreshTokenProvider.GenerateRefreshToken(existingSession.Id),
                        Revoked = false,
                        Version = existingSession.Version
                    });
                }
            }

            return _mapper.Map<SessionProjection, CreateSessionProjection>(existingSession);
        }
    }
}