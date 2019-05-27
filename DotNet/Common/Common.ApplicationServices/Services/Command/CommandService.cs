using System;
using Common.Domain.Exceptions;
using Common.Domain.Models.Interfaces;
using Common.Domain.Persistence;
using Microsoft.Extensions.Logging;

namespace Common.ApplicationServices.Services.Command
{
    public class CommandService<T> : ICommandService<T> where T : IAggregateRoot
    {
        private readonly ILogger<CommandService<T>> _logger;
        private readonly ICommandRepository<T> _commandRepository;

        public CommandService(ILogger<CommandService<T>> logger, 
            ICommandRepository<T> commandRepository)
        {
            _logger = logger;
            _commandRepository = commandRepository;
        }

        public Guid Create(T entity)
        {
            try
            {
                _commandRepository.Add(entity);

                return entity.Id;
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, $"Unable to create { typeof(T).Name }");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Unable to create { typeof(T).Name }");
                throw new CreateDomainException(entity, e);
            }
        }

        public void Update(T entity)
        {
            try
            {
                _commandRepository.Update(entity);
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, $"Unable to update { typeof(T).Name }");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Unable to update { typeof(T).Name }");
                throw new UpdateDomainException(entity, e);
            }
        }
    }
}
