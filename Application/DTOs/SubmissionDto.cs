namespace SubmissionService.Application.DTOs;
[Serializable]
public class SubmissionDto
{
    public Guid Id { get; set; }
    public int InformationRequestID
    {
        get; set;
    }
    public int SIRYear
    {
        get; set;
    }
    public DateTime Timestamp
    {
        get; set;
    }
    public string OrganizationalUnit
    {
        get; set;
    }
    public string InformationRequestNumber
    {
        get; set;
    }
    public string WorksheetType
    {
        get; set;
    }
    public string WorksheetTabRequested
    {
        get; set;
    }
    public string SubmissionStatus
    {
        get; set;
    }
    public string ContributorName
    {
        get; set;
    }
    public bool CanApprove
    {
        get; set;
    }
    public string Actions
    {
        get; set;
    }
    public string ViewLink
    {
        get; set;
    }
    public Dictionary<string, string> InformationRequests
    {
        get; set;
    }
    public List<KeyValuePair<string, Tuple<string, string>>> Documents
    {
        get; set;
    }

    public string SubmissionType
    {
        get; set;
    }
    public string SubmissionComment
    {
        get; set;
    }
    public List<KeyValuePair<string, string>> QCErrors
    {
        get; set;
    }
}
