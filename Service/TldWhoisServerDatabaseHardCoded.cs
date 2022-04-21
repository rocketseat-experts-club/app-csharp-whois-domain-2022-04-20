using WhoisDomain.Model;

namespace WhoisDomain.Service;

public class TldWhoisServerDatabaseHardCoded : ITldWhoisServerDatabase, ITldWhoisServerList
{
    private readonly TldWhoisServerModel[] _database = {
        new () { Tld = ".com", Server = "whois.verisign-grs.com" },
        new () { Tld = ".net", Server = "whois.verisign-grs.com" },
        new () { Tld = ".org", Server = "whois.pir.org" },
        new () { Tld = ".com.br", Server = "whois.registro.br" },
        new () { Tld = ".net.br", Server = "whois.registro.br" },
        new () { Tld = ".org.br", Server = "whois.registro.br" }
    };

    public TldWhoisServerModel? GetByTld(string tld)
    {
        return _database.FirstOrDefault(x => x.Tld == tld);
    }

    public IReadOnlyList<TldWhoisServerModel> List => _database;
}