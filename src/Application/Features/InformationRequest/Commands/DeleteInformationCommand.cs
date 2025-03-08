using MediatR;
using SharedKernel;

namespace SubmissionService.Application.Features.InformationRequest.Commands
{

    public class DeleteInformationRequestCommand : IRequest
    {

        public Guid Id { get; set; }
        public DeleteInformationRequestCommand(Guid id)
        {
            Id = id;
        }

    }
    public class DeleteInformationRequestCommandHandler : IRequestHandler<DeleteInformationRequestCommand>
    {
        private readonly IRepository<Domain.InformationRequest> _repository;
        

        public DeleteInformationRequestCommandHandler(IRepository<Domain.InformationRequest> repository)
        {
            _repository = repository;
            
        }
        public async Task Handle(DeleteInformationRequestCommand request, CancellationToken cancellationToken)
        {
            await _repository.RemoveAsync(request.Id);
        }
    }
}
