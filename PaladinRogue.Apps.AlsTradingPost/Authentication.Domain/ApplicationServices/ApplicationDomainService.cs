using Authentication.Domain.ApplicationServices.Interfaces;
using Authentication.Domain.ApplicationServices.Models;
using Authentication.Domain.Models;
using AutoMapper;
using Common.Domain.Models;

namespace Authentication.Domain.ApplicationServices
{
    public class ApplicationDomainService : IApplicationDomainService
	{
        private readonly IMapper _mapper;
        private readonly IApplicationCommandService _applicationCommandService;
        private readonly IApplicationQueryService _applicationQueryService;

        public ApplicationDomainService(IMapper mapper,
            IApplicationQueryService applicationQueryService,
            IApplicationCommandService applicationCommandService)
        {
            _mapper = mapper;
            _applicationQueryService = applicationQueryService;
            _applicationCommandService = applicationCommandService;
        }

        public ApplicationProjection Create(CreateApplicationDdto entity)
        {
            Application newApplication = _mapper.Map(entity, AggregateFactory.CreateRoot<Application>());

            _applicationCommandService.Create(newApplication);

            return _mapper.Map<Application, ApplicationProjection>(_applicationQueryService.GetById(newApplication.Id));
        }

        public ApplicationProjection Update(UpdateApplicationDdto entity)
        {
            _applicationCommandService.Update(_mapper.Map<UpdateApplicationDdto, Application>(entity));

            return _mapper.Map<Application, ApplicationProjection>(_applicationQueryService.GetById(entity.Id));
        }

	    public ApplicationProjection GetByName(string name)
	    {
	        return _mapper.Map<Application, ApplicationProjection>(_applicationQueryService.GetByName(name));
        }
	}
}
