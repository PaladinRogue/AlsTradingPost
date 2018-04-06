using System;
using Authentication.Domain.ApplicationServices.Interfaces;
using Authentication.Domain.ApplicationServices.Models;
using Authentication.Domain.Models;
using Authentication.Domain.Persistence;
using AutoMapper;

namespace Authentication.Domain.ApplicationServices
{
	public class ApplicationQueryService : IApplicationQueryService
	{
		private readonly IMapper _mapper;
		private readonly IApplicationRepository _applicationRepository;

		public ApplicationQueryService(IMapper mapper, IApplicationRepository applicationRepository)
		{
			_applicationRepository = applicationRepository;
			_mapper = mapper;
		}

		public ApplicationProjection Get(Guid id)
		{
			return _mapper.Map<Application, ApplicationProjection>(_applicationRepository.GetById(id));
		}

	    public ApplicationProjection GetByName(string applicationName)
	    {
	        return _mapper.Map<Application, ApplicationProjection>(_applicationRepository.GetSingle(a => a.Name == applicationName));
        }
	}
}
