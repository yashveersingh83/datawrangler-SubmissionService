namespace SubmissionService.Application.DTOs;

public class MileStoneDto
{
    public string? Description { get; set; }
    public string? Comments { get; set; }
    public DateTime Targetdate { get; set; }
    public int IntId { get; set; }
    public int SIRYear { get; set; }
    public Guid Id { get; set; }

}

public class SubmissionTypeDto
{
    public string Id { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
}

