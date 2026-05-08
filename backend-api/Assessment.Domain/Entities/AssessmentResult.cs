namespace Assessment.Domain.Entities
{
    public class AssessmentResult
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int Score { get; set; }
        public int Total { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
