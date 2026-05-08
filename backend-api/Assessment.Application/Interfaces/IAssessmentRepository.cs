using Assessment.Domain.Entities;

namespace Assessment.Application.Interfaces
{
    public interface IAssessmentRepository
    {
        Task<List<Question>> GetQuestionsAsync();
        Task<AssessmentResult> SaveResultAsync(AssessmentResult result);
        Task<List<AssessmentResult>> GetResultsAsync();
    }
}
