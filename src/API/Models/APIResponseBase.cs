namespace SubmissionService.API.Models
{
    public class ApiResponse<T>
    {
        public List<T> Data { get; set; } = new List<T>(); // The actual records
        public int TotalCount { get; set; } // Total number of records
    }

}
