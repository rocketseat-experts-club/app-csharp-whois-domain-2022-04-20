namespace WhoisDomain.Model;

public class TldWhoisServerModel
{
    private const ushort DefaultPort = 43;
    
    public string? Tld { get; init; }
    public string? Server { get; init; }
    public ushort Port { get; init; } = DefaultPort;

    public override string ToString()
    {
        return $"TLD: {Tld}{Environment.NewLine}Whois server: {Server}:{Port}";
    }
}