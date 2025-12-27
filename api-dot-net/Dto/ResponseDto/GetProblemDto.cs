using System.Text.Json.Serialization;
using CjsApi.Models;

namespace CjsApi.Dto
{
    public class SampleTestCaseDto
    {
        public string Input { get; set; } = string.Empty;
        public string Output { get; set; } = string.Empty;
    }

    public class GetProblemDto
    {
        public string Title { get; set; } = string.Empty;

        public int Id {get;set;} = 1;
        public string Description { get; set; } = string.Empty;

        public List<SampleTestCaseDto> SampleTestCases { get; set; }
            = new List<SampleTestCaseDto>();

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Difficulty Difficulty { get; set; } = Difficulty.EASY;

        public List<string> Tags { get; set; } = new();
    }
}
