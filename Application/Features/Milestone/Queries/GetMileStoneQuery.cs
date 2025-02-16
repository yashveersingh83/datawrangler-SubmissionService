using AutoMapper;
using MassTransit.Mediator;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionService.Application.Features.Milestone.Queries
{
    public class GetAllMileStonesQuery : IRequest<List<MileStoneDto>> { }

    public class GetAllMileStonesQueryHandler : IRequestHandler<GetAllMileStonesQuery, List<MileStoneDto>>
    {
        private readonly IRepository<MileStone> _repository;
        private readonly IMapper _mapper;

        public GetAllMileStonesQueryHandler(IRepository<MileStone> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<MileStoneDto>> Handle(GetAllMileStonesQuery request, CancellationToken cancellationToken)
        {
            var mileStones = await _repository.GetAllAsync();
            return _mapper.Map<List<MileStoneDto>>(mileStones);
        }
    }
}
