using System.Net.Http.Headers;
using System.Text;

public class ElevenLabsService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    public ElevenLabsService(IConfiguration config)
    {
        _config = config;
        _http = new HttpClient();
    }

    public async Task<byte[]> TextToSpeech(string text)
    {
        var voiceId = _config["ElevenLabs:VoiceId"];
        var apiKey = _config["ElevenLabs:ApiKey"];

        var url = $"https://api.elevenlabs.io/v1/text-to-speech/{voiceId}";

        var json = $@"
        {{
          ""text"": ""{text}"",
          ""model_id"": ""eleven_multilingual_v2""
        }}";

        var request = new HttpRequestMessage(HttpMethod.Post, url);

        request.Headers.Add("xi-api-key", apiKey);

        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.SendAsync(request);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"ElevenLabs Error: {content}");
        }

        return await response.Content.ReadAsByteArrayAsync();
    }
}