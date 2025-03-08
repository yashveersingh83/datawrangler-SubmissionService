namespace SubmissionService.Domain;

// public class MileStones
//{
//    [Key]
//    [PrimaryKey]
//    public int Id
//    {
//        get; set;
//    }


//    public int SIRYear
//    {
//        get; set;
//    }


//    [Column(TypeName = "Date")]
//    public DateTime Date
//    {
//        get; set;
//    }

//    public string Comments
//    {
//        get; set;
//    }

//    public string Description
//    {
//        get; set;
//    }
//}

public class HasLastModified
{
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }

}



