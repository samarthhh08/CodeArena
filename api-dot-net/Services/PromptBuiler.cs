namespace CjsApi.Services
{


    public class PromptBuilder
    {


        public static string CodeReviewPrompt(
            string problem,
            string code,
            string language
            )
        {
            return $"""
You are an interview coding assistant.

Rules:
- DO NOT provide full solutions
- DO NOT rewrite entire code
- Only give hints and analysis

Problem:
{problem}

Language:
{language}

User Code:
{code}



Provide:
1. Correctness feedback
2. Time & space complexity
3. Optimization hints
""";
        }

    }
}