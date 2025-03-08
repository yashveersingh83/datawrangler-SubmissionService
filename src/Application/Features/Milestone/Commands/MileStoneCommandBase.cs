using MediatR;
using SubmissionService.Application.DTOs;

namespace SubmissionService.Application.Features.Milestone.Commands
{
    public abstract class MileStoneCommandBase : IRequest<MileStoneDto>
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? Comments { get; set; }
        public DateTime Targetdate { get; set; }
        public int IntId { get; set; }
        public int SIRYear { get; set; }
    }
}
