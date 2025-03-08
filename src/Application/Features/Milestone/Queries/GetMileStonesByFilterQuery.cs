using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;
using System.Linq.Expressions;

namespace SubmissionService.Application.Features.Milestone.Queries
{
    public class GetMileStonesByFilterQuery : IRequest<List<MileStoneDto>>
    {
        public Expression<Func<SubmissionService.Domain.MileStone, bool>> Filter { get; }

        public GetMileStonesByFilterQuery(Expression<Func<SubmissionService.Domain.MileStone, bool>> filter)
        {
            Filter = filter;
        }
    }

    public class GetMileStonesByFilterQueryHandler : IRequestHandler<GetMileStonesByFilterQuery, List<MileStoneDto>>
    {
        private readonly IRepository<MileStone> _repository;
        private readonly IMapper _mapper;

        public GetMileStonesByFilterQueryHandler(IRepository<MileStone> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<MileStoneDto>> Handle(GetMileStonesByFilterQuery request, CancellationToken cancellationToken)
        {
            var mileStones = await _repository.GetAllAsync(request.Filter);
            return _mapper.Map<List<MileStoneDto>>(mileStones);
        }
    }
}
