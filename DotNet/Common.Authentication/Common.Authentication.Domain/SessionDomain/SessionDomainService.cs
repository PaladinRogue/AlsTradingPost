using System;
using AutoMapper;
using Common.Authentication.Domain.Models;
using Common.Authentication.Domain.SessionDomain.Exceptions;
using Common.Authentication.Domain.SessionDomain.Interfaces;
using Common.Authentication.Domain.SessionDomain.Models;
using Common.Authentication.Resources.RefreshTokens;
using Common.Domain.Models;
using Common.Domain.Services.Command;
using Common.Domain.Services.Query;

namespace Common.Authentication.Domain.SessionDomain
{
    public class SessionDomainService : ISessionDomainService
    {
        private readonly ICommandService<Session> _sessionCommandService;
        private readonly IQueryService<Session> _sessionQueryService;
        private readonly IRefreshTokenProvider _refreshTokenProvider;
        private readonly IMapper _mapper;

        public SessionDomainService(ICommandService<Session> sessionCommandService,
            IRefreshTokenProvider refreshTokenProvider,
            IQueryService<Session> sessionQueryService,
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

            Session existingSession = _sessionQueryService.GetById(refreshSessionDdto.Id);
            if (existingSession.IsRevoked)
            {
                throw new SessionRevokedDomainException();
            }

            if (existingSession.RefreshToken != refreshSessionDdto.RefreshToken)
            {
                throw new RefreshTokenInvalidDomainException();
            }

            existingSession.RefreshToken = _refreshTokenProvider.GenerateRefreshToken(refreshSessionDdto.Id);
            existingSession.IsRevoked = false;

            _sessionCommandService.Update(existingSession);

            return _mapper.Map<Session, RefreshSessionProjection>(_sessionQueryService.GetById(existingSession.Id));
        }

        public SessionProjection Create(Guid sessionId)
        {
            Session existingSession = _sessionQueryService.GetById(sessionId);
            if (existingSession == null)
            {
                Session newSession = AggregateFactory.CreateRoot<Session>(sessionId);

                newSession.RefreshToken = _refreshTokenProvider.GenerateRefreshToken(sessionId);

                _sessionCommandService.Create(newSession);
            }
            else
            {
                if (existingSession.IsRevoked)
                {

                    existingSession.RefreshToken = _refreshTokenProvider.GenerateRefreshToken(existingSession.Id);
                    existingSession.IsRevoked = false;

                    _sessionCommandService.Update(existingSession);
                }
            }

            return _mapper.Map<Session, SessionProjection>(_sessionQueryService.GetById(sessionId));
        }
    }
}