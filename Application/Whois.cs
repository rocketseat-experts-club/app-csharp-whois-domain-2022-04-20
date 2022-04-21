using WhoisDomain.Service;

namespace WhoisDomain.Application;

public class Whois : IApplication
{
    private readonly IWhoisQuery _whoisQuery;
    private readonly IUserInput _userInput;

    public Whois()
    {
        _whoisQuery = new WhoisQueryTcp();
        _userInput = new UserInputFromCommandLineArgument();
    }
    
    public void Run()
    {
        Console.WriteLine("Whois Internet Domain");

        if (_userInput.Domains.Length > 0)
        {
            foreach (var internetDomain in _userInput.Domains)
            {
                Console.WriteLine($"###[ {internetDomain} ]###".PadRight(80, '#'));
                Console.WriteLine(_whoisQuery.Request(internetDomain) ?? $"TLD indisponível para consulta: {internetDomain.Tld}");
            }

            Console.WriteLine(string.Empty.PadRight(80, '#'));
        }
        else
        {
            Console.WriteLine("Informe um ou mais domínios de internet como argumento.");
        }
    }
}