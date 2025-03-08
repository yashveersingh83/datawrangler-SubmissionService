//using MongoDB.Bson.Serialization.Attributes;
//using System.Buffers;

//namespace SubmissionService.Domain;

//[BsonIgnoreExtraElements]
//public class DataActionDocument
//{
//    public Guid ReferenceId { get; set; }

//    public int SirYear { get; set; }

//    public SirDataActionType DataActionType { get; set; }

//    public DataSourceName DataSource { get; set; }

//    public List<DataEntityBo> Entities { get; set; }

//    public OperationStatus Status { get; set; }

//    public string UserPersonnelNumber { get; set; }

//    public DateTime TimeStamp { get; set; }

//    public string InformationMessage { get; set; }
//}
//public class DataEntityBo
//{
//    public DataEntityBo()
//    {
//        Conflicts = new List<DataRecordBo>();
//        Discrepancies = new List<DataRecordBo>();
//    }

//    public string Name { get; set; }

//    public string TypeName { get; set; }

//    public int TotalRecordsProcessed { get; set; }

//    public List<DataRecordBo> Conflicts { get; set; }

//    public List<DataRecordBo> Discrepancies { get; set; }
//}

//public class DataRecordBo
//{
//    public DataRecordBo()
//    {
//        Fields = new List<DataFieldBo>();
//        Changes = new List<DataChangeBo>();
//    }

//    [PrimaryKey]
//    public int ReferenceId { get; set; }

//    public SirDataRecordState State { get; set; }

//    public SirDataResolveStrategy ResolveStrategyForEntireRow { get; set; }

//    public string Comments { get; set; }

//    public string LastUpdatedBy { get; set; }

//    public string ChangeLog { get; set; }

//    public List<DataFieldBo> Fields { get; set; }

//    public List<DataChangeBo> Changes { get; set; }
//}

//public class DataChangeBo
//{
//    public string FieldName
//    {
//        get; set;
//    }

//    public string DisplaySourceValue
//    {
//        get; set;
//    }

//    public string DisplayLocalValue
//    {
//        get; set;
//    }

//    public string DisplayLastImportedValue
//    {
//        get; set;
//    }

//    public object SourceValue
//    {
//        get; set;
//    }

//    public object LocalValue
//    {
//        get; set;
//    }

//    public object LastImportedValue
//    {
//        get; set;
//    }

//    public object Value
//    {
//        get; set;
//    }

//    public SirDataResolveStrategy ResolveStrategy
//    {
//        get; set;
//    }
//}


//public class DataFieldBo
//{
//    public string Name { get; set; }
//    public object Value { get; set; }
//    public string DisplayValue { get; set; }
//}


