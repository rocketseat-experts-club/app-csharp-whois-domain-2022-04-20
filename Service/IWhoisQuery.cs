using WhoisDomain.Model;

namespace WhoisDomain.Service;

public interface IWhoisQuery
{
    public string? Request(InternetDomainModel internetDomain);
}