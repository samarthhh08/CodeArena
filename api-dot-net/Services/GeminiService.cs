using System.Net.Http.Json;
using System.Text.Json;

public sealed class GeminiService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    public GeminiService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
    }

    public async Task<string> GenerateAsync(string prompt)
    {
        var apiKey = _config["Gemini:ApiKey"];
        var model = _config["Gemini:Model"];

       var url =
 $"https://generativelanguage.googleapis.com/v1beta/{model}:generateContent?key={apiKey}";



        var body = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
        };

        var response = await _http.PostAsJsonAsync(url, body);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        return json
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString()!;
    }
}
