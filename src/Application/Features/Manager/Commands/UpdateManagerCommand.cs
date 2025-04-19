using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Manager.Commands
{
    public class UpdateManagerCommand : CreateManagerCommandBase {
        public Guid Id { get; set; }
    }

    public class UpdateManagerCommandHandler : IRequestHandler<UpdateManagerCommand, OrganizationalUnitHeadDto>

    {
        private readonly IRepository<OrganizationalUnitHead> _repository;
        private readonly IMapper _mapper;
        private readonly IRedisCacheService redisCacheService;

        public UpdateManagerCommandHandler(IRepository<OrganizationalUnitHead> repository, IMapper mapper,IRedisCacheService redisCacheService)
        {
            _repository = repository;
            _mapper = mapper;
            this.redisCacheService = redisCacheService;
        }
        public async Task<OrganizationalUnitHeadDto> Handle(UpdateManagerCommand request, CancellationToken cancellationToken)
        {

            var existingorganizationalUnitHead = await _repository.GetAsync(request.Id);
            if (existingorganizationalUnitHead == null)
            {
                throw new KeyNotFoundException($"organizationalUnitHead with ID {request.Id} not found.");
            }



            existingorganizationalUnitHead.DDSUCode = request.DDSUCode;
            existingorganizationalUnitHead.Division = request.Division;
            existingorganizationalUnitHead.DivisionCode = request.DivisionCode;
            existingorganizationalUnitHead.SectionCode = request.SectionCode;
            existingorganizationalUnitHead.FullName = request.FullName;
            existingorganizationalUnitHead.FunctionalTitle = request.FunctionalTitle;
            existingorganizationalUnitHead.ApproverRole = request.ApproverRole;
            existingorganizationalUnitHead.ContributorRole = request.ContributorRole;
            //existingorganizationalUnitHead.CoordinatorRoles = request.CoordinatorRoles;
            existingorganizationalUnitHead.PersonnelNumber = request.PersonnelNumber;
           // existingorganizationalUnitHead.ManagerComment = request.ManagerComment;
           

            await _repository.UpdateAsync(existingorganizationalUnitHead);
            await this.redisCacheService.RemoveCacheAsync(CacheKeyConstant.OrgUnitKey);

            return _mapper.Map<OrganizationalUnitHeadDto>(existingorganizationalUnitHead);
        }
    }
}
