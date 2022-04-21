using System.Net;
using WhoisDomain.Model;

namespace WhoisDomain.Service;

public class WhoisQueryIpAddress : IWhoisQuery
{
    public string? Request(InternetDomainModel internetDomain)
    {
        try
        {
            var url = $"https://{internetDomain}";
            var myUri = new Uri(url);
            var ip = Dns.GetHostAddresses(myUri.Host)[0];
            return ip.ToString();
        }
        catch (Exception exception)
        {
            return exception.ToString();
        }
    }
}