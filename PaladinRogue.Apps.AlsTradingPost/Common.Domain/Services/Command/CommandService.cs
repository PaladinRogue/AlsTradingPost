using System;
using Common.Domain.Exceptions;
using Common.Domain.Models.Interfaces;
using Common.Domain.Persistence;
using Microsoft.Extensions.Logging;

namespace Common.Domain.Services.Command
{
    public class CommandService<T> : ICommandService<T> where T : IVersionedEntity
    {
        private readonly ILogger<CommandService<T>> _logger;
        private readonly IRepository<T> _repository;

        public CommandService(ILogger<CommandService<T>> logger,
            IRepository<T> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public Guid Create(T entity)
        {
            try
            {
                _repository.Add(entity);

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
                _repository.Update(entity);
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
