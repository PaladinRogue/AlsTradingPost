using System;
using Authentication.Domain.ApplicationServices.Interfaces;
using Authentication.Domain.Models;
using Authentication.Domain.Persistence;

namespace Authentication.Domain.ApplicationServices
{
	public class ApplicationQueryService : IApplicationQueryService
	{
		private readonly IApplicationRepository _applicationRepository;

		public ApplicationQueryService(IApplicationRepository applicationRepository)
		{
			_applicationRepository = applicationRepository;
		}

		public Application GetById(Guid id)
		{
			return _applicationRepository.GetById(id);
		}

	    public Application GetByName(string applicationName)
	    {
	        return _applicationRepository.GetSingle(a => a.Name == applicationName);
        }
	}
}
