using MediatR;
using SharedKernel;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Milestone.Commands
{
    public class DeleteMileStoneCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteMileStoneCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteMileStoneCommandHandler : IRequestHandler<DeleteMileStoneCommand>
    {
        private readonly IRepository<MileStone> _repository;

        public DeleteMileStoneCommandHandler(IRepository<MileStone> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteMileStoneCommand request, CancellationToken cancellationToken)
        {
            await _repository.RemoveAsync(request.Id);
        }
    }
}
