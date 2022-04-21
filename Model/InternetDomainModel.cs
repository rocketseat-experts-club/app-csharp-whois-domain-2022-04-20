namespace WhoisDomain.Model;

public class InternetDomainModel
{
    public string Host { get; }
    public string Tld { get; }
    
    public InternetDomainModel(string domain)
    {
        var dot = $"{domain}.".IndexOf(".", StringComparison.Ordinal);
        Host = domain[..dot].ToLower();
        Tld = domain[dot..].ToLower();
    }

    public override string ToString()
    {
        return $"{Host}{Tld}";
    }
}