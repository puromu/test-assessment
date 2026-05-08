namespace Assessment.Application.DTOs
{
    public class SubmitResultRequest
    {
        public string FullName { get; set; } = string.Empty;
        public int Score { get; set; }
        public int Total { get; set; }
    }
}
