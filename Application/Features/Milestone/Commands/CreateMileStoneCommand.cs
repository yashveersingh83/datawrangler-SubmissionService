using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Milestone.Commands
{
    public class CreateMileStoneCommand : IRequest<MileStoneDto>
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? Comments { get; set; }
        public DateTime Targetdate { get; set; }
        public int IntId { get; set; }
        public int SIRYear { get; set; }
    }


    public class CreateMileStoneCommandHandler : IRequestHandler<CreateMileStoneCommand, MileStoneDto>
    {
        private readonly IRepository<MileStone> _repository;
        private readonly IMapper _mapper;

        public CreateMileStoneCommandHandler(IRepository<MileStone> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MileStoneDto> Handle(CreateMileStoneCommand request, CancellationToken cancellationToken)
        {
            var mileStone = new    MileStone();
            mileStone.Comments = request.Comments;
            mileStone.Targetdate = request.Targetdate;
            mileStone.IntId = request.IntId;
            mileStone.SIRYear = request.SIRYear;
            mileStone.Description = request.Description;
            await _repository.CreateAsync(mileStone);           

            return _mapper.Map<MileStoneDto>(mileStone);
        }

        /*public  async Task InsertSampleData()
        {
        //    List<CreateMileStoneCommand> milestones = new List<CreateMileStoneCommand>()
        //{
        //    new CreateMileStoneCommand { Id = Guid.NewGuid(), IntId = 1, SIRYear = 2020, Description = "HR - Implement New Hiring Process", Comments = "Review policies", Targetdate = new DateTime(2025, 3, 15) },
        //    new CreateMileStoneCommand { Id = Guid.NewGuid(), IntId = 2, SIRYear = 2020, Description = "Finance - Budget Planning", Comments = "Prepare Q2 financial forecast", Targetdate = new DateTime(2025, 4, 20) },
        //    new CreateMileStoneCommand { Id = Guid.NewGuid(), IntId = 3, SIRYear = 2021, Description = "IT - Server Upgrade", Comments = "Upgrade cloud infrastructure", Targetdate = new DateTime(2025, 5, 10) },
        //    new CreateMileStoneCommand { Id = Guid.NewGuid(), IntId = 4, SIRYear = 2021, Description = "Marketing - New Campaign", Comments = "Launch summer campaign", Targetdate = new DateTime(2025, 6, 5) },
        //    new CreateMileStoneCommand { Id = Guid.NewGuid(), IntId = 5, SIRYear = 2022, Description = "Sales - New CRM Implementation", Comments = "Train sales team on CRM", Targetdate = new DateTime(2025, 7, 25) },
        //    new CreateMileStoneCommand { Id = Guid.NewGuid(), IntId = 6, SIRYear = 2022, Description = "Legal - Compliance Audit", Comments = "Ensure regulatory compliance", Targetdate = new DateTime(2025, 8, 30) },
        //    new CreateMileStoneCommand { Id = Guid.NewGuid(), IntId = 7, SIRYear = 2023, Description = "Production - Equipment Maintenance", Comments = "Schedule quarterly maintenance", Targetdate = new DateTime(2025, 9, 15) },
        //    new CreateMileStoneCommand { Id = Guid.NewGuid(), IntId = 8, SIRYear = 2023, Description = "Support - Customer Service Enhancement", Comments = "Implement chatbot", Targetdate = new DateTime(2025, 10, 12) },
        //    new CreateMileStoneCommand { Id = Guid.NewGuid(), IntId = 9, SIRYear = 2024, Description = "R&D - Prototype Testing", Comments = "Validate new product design", Targetdate = new DateTime(2025, 11, 20) },
        //    new CreateMileStoneCommand { Id = Guid.NewGuid(), IntId = 10, SIRYear = 2025, Description = "Operations - Supply Chain Optimization", Comments = "Review vendor agreements", Targetdate = new DateTime(2025, 12, 5) }
        //    };

            foreach (MileStone mile in milestones)
            {
               await  _repository.CreateAsync(mile);
            }
        }*/
    }
}
