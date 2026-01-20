namespace CjsApi.Dto.RequestDto
{
    public class SaveMcqAttemptDto
    {
        public int McqQuestionId { get; set; }
        public string SelectedOption { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}