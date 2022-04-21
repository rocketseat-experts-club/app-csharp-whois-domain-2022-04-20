using WhoisDomain.Model;

namespace WhoisDomain.Service;

public class WhoisQueryFetchHtml : IWhoisQuery
{
    public string? Request(InternetDomainModel internetDomain)
    {
        try
        {
            var url = $"https://{internetDomain}";
            using var webRequest = new HttpClient();
            var result = webRequest.GetStringAsync(url).Result;
            return result;
        }
        catch (Exception exception)
        {
            return exception.ToString();
        }
    }
}