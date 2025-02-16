﻿using SharedKernel;

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
