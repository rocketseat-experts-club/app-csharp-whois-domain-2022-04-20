using WhoisDomain.Model;

namespace WhoisDomain.Service;

public interface ITldWhoisServerDatabase
{
    public TldWhoisServerModel? GetByTld(string tld);
}