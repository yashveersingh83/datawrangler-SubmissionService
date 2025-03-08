using System.ComponentModel.DataAnnotations;

namespace SubmissionService.Domain;

public class MileStoneViewModel
{
    public int Id
    {
        set; get;
    }

    [Required]
    public int SIRYear
    {
        get; set;
    }

    [Required]
    public DateTime TargetDate
    {
        get; set;
    }

    public string Comments
    {
        get; set;
    }

    public string Description
    {
        get; set;
    }

    public string Edit
    {
        get; set;
    }

    /// <summary>
    /// This is used to  on grid to give text search on date
    /// </summary>
    public string TargetDateString
    {
        get; set;
    }

}
