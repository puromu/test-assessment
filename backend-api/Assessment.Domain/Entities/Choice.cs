namespace Assessment.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int CorrectChoiceId { get; set; }
        public List<Choice> Choices { get; set; } = [];
    }
}
