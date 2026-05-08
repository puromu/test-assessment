using Assessment.Application.Interfaces;
using Assessment.Domain.Entities;
using Assessment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Infrastructure.Repositories
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly AssessmentDbContext _context;

        public AssessmentRepository(AssessmentDbContext context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetQuestionsAsync()
        {
            return await _context.Questions
                .Include(x => x.Choices)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<AssessmentResult> SaveResultAsync(AssessmentResult result)
        {
            _context.AssessmentResults.Add(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<List<AssessmentResult>> GetResultsAsync()
        {
            return await _context.AssessmentResults
                .OrderByDescending(x => x.SubmittedAt)
                .ToListAsync();
        }
    }
}
