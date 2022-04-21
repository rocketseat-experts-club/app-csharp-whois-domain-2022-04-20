using System.Net;
using WhoisDomain.Model;

namespace WhoisDomain.Service;

public class WhoisQueryGeolocation : IWhoisQuery
{
    public string? Request(InternetDomainModel internetDomain)
    {
        try
        {
            var url = $"https://{internetDomain}";
            var myUri = new Uri(url);
            var ip = Dns.GetHostAddresses(myUri.Host)[0];
            var urlForGeolocation = $"http://demo.ip-api.com/json/{ip}?fields=66842623&lang=pt";
            using var webRequest = new HttpClient();
            var result = webRequest.GetStringAsync(urlForGeolocation).Result;
            return result;
        }
        catch (Exception exception)
        {
            return exception.ToString();
        }
    }
}