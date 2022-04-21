using System.Net.Sockets;
using System.Text;
using WhoisDomain.Model;

namespace WhoisDomain.Service;

public class WhoisQueryTcp : IWhoisQuery
{
    private readonly ITldWhoisServerDatabase _tldWhoisServerDatabase;

    public WhoisQueryTcp()
    {
        _tldWhoisServerDatabase = new TldWhoisServerDatabaseHardCoded();
    }

    public string? Request(InternetDomainModel internetDomain)
    {
        var tldWhoisServer = _tldWhoisServerDatabase.GetByTld(internetDomain.Tld);

        if (tldWhoisServer == null || 
            string.IsNullOrWhiteSpace(tldWhoisServer.Server) || 
            tldWhoisServer.Port <= 0)
        {
            return null;
        }

        using var stream = new TcpClient(tldWhoisServer.Server, tldWhoisServer.Port).GetStream();
        using var buffered = new BufferedStream(stream);
        
        var writer = new StreamWriter(buffered);
        writer.WriteLine(internetDomain);
        writer.Flush();

        var result = new StringBuilder();

        var reader = new StreamReader(buffered);
        string? response;
        while ((response = reader.ReadLine()) != null)
        {
            result.AppendLine(response);
        }

        return result.ToString();
    }
}