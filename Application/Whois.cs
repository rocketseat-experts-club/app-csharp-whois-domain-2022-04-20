using WhoisDomain.Service;

namespace WhoisDomain.Application;

public class Whois : IApplication
{
    private readonly IEnumerable<IWhoisQuery> _whoisQueryList;
    private readonly IUserInput _userInput;

    public Whois(IEnumerable<IWhoisQuery> whoisQueryList, IUserInput userInput)
    {
        _whoisQueryList = whoisQueryList;
        _userInput = userInput;
    }
    
    public void Run()
    {
        Console.WriteLine("Whois Internet Domain");

        if (_userInput.Domains.Length > 0)
        {
            foreach (var internetDomain in _userInput.Domains)
            {
                Console.WriteLine($"###[ {internetDomain} ]###".PadRight(80, '#'));
                foreach (var whoisQuery in _whoisQueryList)
                {
                    Console.WriteLine($"===[ {whoisQuery.GetType().Name} ]===".PadRight(80, '='));
                    Console.WriteLine(whoisQuery.Request(internetDomain) ?? $"TLD indisponível para consulta: {internetDomain.Tld}");
                }
            }

            Console.WriteLine(string.Empty.PadRight(80, '#'));
        }
        else
        {
            Console.WriteLine("Informe um ou mais domínios de internet como argumento.");
        }
    }
}