using System.Text.Json.Serialization;
using CjsApi.Models;

namespace CjsApi.Dto
{
    public class ProblemMetaDataDto
    {
        public string Title { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Difficulty Difficulty { get; set; } = Difficulty.EASY;
    }

    public class GetAllProblemDto
    {
        public List<ProblemMetaDataDto> Problems { get; set; }
            = new List<ProblemMetaDataDto>();

        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
