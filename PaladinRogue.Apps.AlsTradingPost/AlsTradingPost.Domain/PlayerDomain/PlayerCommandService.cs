using System;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.PlayerDomain.Interfaces;
using AlsTradingPost.Domain.PlayerDomain.Models;
using AutoMapper;
using Common.Domain.Exceptions;
using Common.Domain.Models;
using Microsoft.Extensions.Logging;

namespace AlsTradingPost.Domain.PlayerDomain
{
    public class PlayerCommandService : IPlayerCommandService
    {
        private readonly ILogger<PlayerCommandService> _logger;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        
        public PlayerCommandService(
            IPlayerRepository playerRepository,
            IMapper mapper,
            ILogger<PlayerCommandService> logger)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
            _logger = logger;
        }
        
        public PlayerProjection Create(CreatePlayerDdto entity)
        {
            Player player = null;
            try
            {
                player = _mapper.Map(entity, EntityFactory.CreateEntity<Player>(entity.Id));

                _playerRepository.Add(player);

                return _mapper.Map<Player, PlayerProjection>(_playerRepository.GetById(player.Id));
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, "Unable to create player");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create player");
                throw new CreateDomainException(player, e);
            }
        }
    }
}