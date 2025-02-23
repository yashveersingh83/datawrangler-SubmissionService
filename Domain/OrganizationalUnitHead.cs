using SharedKernel;

namespace SubmissionService.Domain;

public class OrganizationalUnitHead : IEntity
{


    public Guid Id { get; set; }

    public string DDSUCode
    {
        get; set;
    }

    public string Division
    {
        get; set;
    }

    public string DivisionCode
    {
        get; set;
    }

    public string SectionCode
    {
        get; set;
    }




    // return the full name based on the condition with DelegateFullName
    public string FullName
    {
        get;
        set;
    }

    public string FunctionalTitle
    {
        get; set;
    }


    // Represents if the person has approval role(SIR_APPROVERS)
    public bool ApproverRole
    {
        get; set;
    }

    // Represents if the person has contributors role(SIR_CONTRIBUTORS)
    public bool ContributorRole
    {
        get; set;
    }

    // comma separated values of the person who has access to more than one role assinged(SIR_CONTRIBUTORS) to home or other divisions
    public string CoordinatorRoles
    {
        get; set;
    }

    // checks if role has been delegated to someone then delegated personnel number should be used
    public string PersonnelNumber
    {
        get; set;
    }
    public string ManagerComment
    {
        get; set;
    }

   
}