using MassTransit;
using SubmissionService.Application.DTOs;

namespace SubmissionService.API.Consumers
{
   


public class MileStoneDtoConsumer : IConsumer<MileStoneDto>
    {
        private readonly ILogger<MileStoneDtoConsumer> _logger;

        public MileStoneDtoConsumer(ILogger<MileStoneDtoConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<MileStoneDto> context)
        {
            _logger.LogInformation("Received message: {Text} ",
                context.Message.Comments);
            return Task.CompletedTask;
        }
    }

}
