using MediatR;
using SharedKernel;
using SubmissionService.Application.Features.Milestone.Commands;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Manager.Commands
{
    public class DeleteManagerCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteManagerCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteManagerCommandHandler : IRequestHandler<DeleteManagerCommand>
    {
        private readonly IRepository<OrganizationalUnitHead> _repository;

        public DeleteManagerCommandHandler(IRepository<OrganizationalUnitHead> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteManagerCommand request, CancellationToken cancellationToken)
        {
            await _repository.RemoveAsync(request.Id);
        }
    }
}
