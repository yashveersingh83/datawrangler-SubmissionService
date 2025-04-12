namespace SubmissionService.Application.DTOs;

public class OrganizationalUnitDto
{
   
    public Guid Id { get; set; }
    public string Division { get; set; }
    public string DDSUCode { get; set; }
    public string SectionCode { get; set; }
    public string Description { get; set; } // Description added
}
