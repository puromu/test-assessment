using Assessment.Application.DTOs;
using Assessment.Application.Interfaces;
using Assessment.Domain.Entities;

namespace Assessment.Application.Services
{
    public class AssessmentService
    {
        private readonly IAssessmentRepository _repository;

        public AssessmentService(IAssessmentRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Question>> GetQuestionsAsync()
        {
            return _repository.GetQuestionsAsync();
        }

        public async Task<AssessmentResult> SaveResultAsync(SubmitResultRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FullName))
                throw new ArgumentException("Full name is required.");

            if (request.Score < 0)
                throw new ArgumentException("Score cannot be negative.");

            if (request.Total <= 0)
                throw new ArgumentException("Total must be greater than zero.");

            if (request.Score > request.Total)
                throw new ArgumentException("Score cannot be greater than total.");

            var result = new AssessmentResult
            {
                FullName = request.FullName.Trim(),
                Score = request.Score,
                Total = request.Total,
                SubmittedAt = DateTime.UtcNow
            };

            return await _repository.SaveResultAsync(result);
        }

        public Task<List<AssessmentResult>> GetResultsAsync()
        {
            return _repository.GetResultsAsync();
        }
    }
}
