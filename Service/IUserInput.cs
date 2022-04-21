using WhoisDomain.Model;

namespace WhoisDomain.Service;

public interface IUserInput
{
    public InternetDomainModel[] Domains { get; }
}