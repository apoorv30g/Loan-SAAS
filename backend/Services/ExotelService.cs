using System.Net.Http.Headers;

namespace LoanSaas.Backend.Services;

public class ExotelService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _http;

    public ExotelService(IConfiguration config)
    {
        _config = config;
        _http = new HttpClient();
    }

 public async Task MakeCall(string phone, int leadId)
{
    var apiKey = _config["Exotel:ApiKey"];
    var apiToken = _config["Exotel:ApiToken"];
    var subdomain = _config["Exotel:Subdomain"];
    var callerId = _config["Exotel:CallerId"];
    var baseUrl = _config["BaseUrl"];

    var url = $"https://{apiKey}:{apiToken}@api.exotel.com/v1/Accounts/{subdomain}/Calls/connect.json";

    var form = new FormUrlEncodedContent(new[]
    {
        new KeyValuePair<string,string>("From", callerId),
        new KeyValuePair<string,string>("To", phone),
        new KeyValuePair<string,string>("CallerId", callerId),
        new KeyValuePair<string,string>("CallType", "trans"),
        new KeyValuePair<string,string>("Url", $"{baseUrl}/voice/{leadId}")
    });

    await _http.PostAsync(url, form);
}
}
