using WhoisDomain.Model;

namespace WhoisDomain.Service;

public interface ITldWhoisServerList
{
    public IReadOnlyList<TldWhoisServerModel> List { get; }
}