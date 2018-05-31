using Authentication.Domain.ApplicationServices.Interfaces;
using Authentication.Domain.ApplicationServices.Models;
using Authentication.Domain.Models;
using AutoMapper;
using Common.Domain.Models;
using Common.Domain.Services.Command;
using Common.Domain.Services.Query;

namespace Authentication.Domain.ApplicationServices
{
    public class ApplicationDomainService : IApplicationDomainService
	{
        private readonly IMapper _mapper;
        private readonly ICommandService<Application> _applicationCommandService;
        private readonly IQueryService<Application> _applicationQueryService;

        public ApplicationDomainService(IMapper mapper,
            IQueryService<Application> applicationQueryService,
            ICommandService<Application> applicationCommandService)
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
	        return _mapper.Map<Application, ApplicationProjection>(_applicationQueryService.GetSingle(a => a.Name == name));
        }
	}
}
