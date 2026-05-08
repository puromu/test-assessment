using Assessment.Application.DTOs;
using Assessment.Application.Interfaces;
using Assessment.Application.Services;
using Assessment.Domain.Entities;
using Moq;

namespace Assessment.Tests;

public class AssessmentServiceTests
{
    [Fact]
    public async Task SaveResultAsync_Should_Save_Result_When_Request_Is_Valid()
    {
        var mockRepo = new Mock<IAssessmentRepository>();

        mockRepo
            .Setup(x => x.SaveResultAsync(It.IsAny<AssessmentResult>()))
            .ReturnsAsync((AssessmentResult result) =>
            {
                result.Id = 1;
                return result;
            });

        var service = new AssessmentService(mockRepo.Object);

        var request = new SubmitResultRequest
        {
            FullName = "สมชาย ใจดี",
            Score = 2,
            Total = 2
        };

        var result = await service.SaveResultAsync(request);

        Assert.Equal(1, result.Id);
        Assert.Equal("สมชาย ใจดี", result.FullName);
        Assert.Equal(2, result.Score);
        Assert.Equal(2, result.Total);
        Assert.Equal(DateTimeKind.Utc, result.SubmittedAt.Kind);

        mockRepo.Verify(
            x => x.SaveResultAsync(It.IsAny<AssessmentResult>()),
            Times.Once
        );
    }

    [Fact]
    public async Task SaveResultAsync_Should_Trim_FullName()
    {
        var mockRepo = new Mock<IAssessmentRepository>();

        mockRepo
            .Setup(x => x.SaveResultAsync(It.IsAny<AssessmentResult>()))
            .ReturnsAsync((AssessmentResult result) => result);

        var service = new AssessmentService(mockRepo.Object);

        var request = new SubmitResultRequest
        {
            FullName = "  สมชาย ใจดี  ",
            Score = 1,
            Total = 2
        };

        var result = await service.SaveResultAsync(request);

        Assert.Equal("สมชาย ใจดี", result.FullName);
    }

    [Fact]
    public async Task SaveResultAsync_Should_Throw_When_FullName_Is_Empty()
    {
        var mockRepo = new Mock<IAssessmentRepository>();
        var service = new AssessmentService(mockRepo.Object);

        var request = new SubmitResultRequest
        {
            FullName = "",
            Score = 1,
            Total = 2
        };

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.SaveResultAsync(request)
        );

        mockRepo.Verify(
            x => x.SaveResultAsync(It.IsAny<AssessmentResult>()),
            Times.Never
        );
    }

    [Fact]
    public async Task SaveResultAsync_Should_Throw_When_FullName_Is_WhiteSpace()
    {
        var mockRepo = new Mock<IAssessmentRepository>();
        var service = new AssessmentService(mockRepo.Object);

        var request = new SubmitResultRequest
        {
            FullName = "   ",
            Score = 1,
            Total = 2
        };

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.SaveResultAsync(request)
        );

        mockRepo.Verify(
            x => x.SaveResultAsync(It.IsAny<AssessmentResult>()),
            Times.Never
        );
    }

    [Fact]
    public async Task SaveResultAsync_Should_Throw_When_Score_Is_Negative()
    {
        var mockRepo = new Mock<IAssessmentRepository>();
        var service = new AssessmentService(mockRepo.Object);

        var request = new SubmitResultRequest
        {
            FullName = "สมชาย",
            Score = -1,
            Total = 2
        };

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.SaveResultAsync(request)
        );

        mockRepo.Verify(
            x => x.SaveResultAsync(It.IsAny<AssessmentResult>()),
            Times.Never
        );
    }

    [Fact]
    public async Task SaveResultAsync_Should_Throw_When_Total_Is_Zero()
    {
        var mockRepo = new Mock<IAssessmentRepository>();
        var service = new AssessmentService(mockRepo.Object);

        var request = new SubmitResultRequest
        {
            FullName = "สมชาย",
            Score = 1,
            Total = 0
        };

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.SaveResultAsync(request)
        );

        mockRepo.Verify(
            x => x.SaveResultAsync(It.IsAny<AssessmentResult>()),
            Times.Never
        );
    }

    [Fact]
    public async Task SaveResultAsync_Should_Throw_When_Total_Is_Negative()
    {
        var mockRepo = new Mock<IAssessmentRepository>();
        var service = new AssessmentService(mockRepo.Object);

        var request = new SubmitResultRequest
        {
            FullName = "สมชาย",
            Score = 1,
            Total = -1
        };

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.SaveResultAsync(request)
        );

        mockRepo.Verify(
            x => x.SaveResultAsync(It.IsAny<AssessmentResult>()),
            Times.Never
        );
    }

    [Fact]
    public async Task SaveResultAsync_Should_Throw_When_Score_Greater_Than_Total()
    {
        var mockRepo = new Mock<IAssessmentRepository>();
        var service = new AssessmentService(mockRepo.Object);

        var request = new SubmitResultRequest
        {
            FullName = "สมชาย",
            Score = 3,
            Total = 2
        };

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.SaveResultAsync(request)
        );

        mockRepo.Verify(
            x => x.SaveResultAsync(It.IsAny<AssessmentResult>()),
            Times.Never
        );
    }

    [Fact]
    public async Task GetQuestionsAsync_Should_Return_Questions()
    {
        var mockRepo = new Mock<IAssessmentRepository>();

        mockRepo
            .Setup(x => x.GetQuestionsAsync())
            .ReturnsAsync(new List<Question>
            {
                new Question
                {
                    Id = 1,
                    Text = "ผลลัพธ์ของ 3 + 6 เท่ากับข้อใด",
                    CorrectChoiceId = 3,
                    Choices = new List<Choice>
                    {
                        new Choice { Id = 1, Text = "3" },
                        new Choice { Id = 2, Text = "5" },
                        new Choice { Id = 3, Text = "9" },
                        new Choice { Id = 4, Text = "11" }
                    }
                }
            });

        var service = new AssessmentService(mockRepo.Object);

        var result = await service.GetQuestionsAsync();

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("ผลลัพธ์ของ 3 + 6 เท่ากับข้อใด", result[0].Text);
        Assert.Equal(3, result[0].CorrectChoiceId);
        Assert.Equal(4, result[0].Choices.Count);

        mockRepo.Verify(
            x => x.GetQuestionsAsync(),
            Times.Once
        );
    }

    [Fact]
    public async Task GetResultsAsync_Should_Return_Results()
    {
        var mockRepo = new Mock<IAssessmentRepository>();

        mockRepo
            .Setup(x => x.GetResultsAsync())
            .ReturnsAsync(new List<AssessmentResult>
            {
                new AssessmentResult
                {
                    Id = 1,
                    FullName = "สมชาย ใจดี",
                    Score = 2,
                    Total = 2,
                    SubmittedAt = DateTime.UtcNow
                }
            });

        var service = new AssessmentService(mockRepo.Object);

        var result = await service.GetResultsAsync();

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("สมชาย ใจดี", result[0].FullName);
        Assert.Equal(2, result[0].Score);

        mockRepo.Verify(
            x => x.GetResultsAsync(),
            Times.Once
        );
    }
}