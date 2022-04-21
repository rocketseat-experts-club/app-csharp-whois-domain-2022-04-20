using WhoisDomain.Service;

Console.WriteLine("Whois Internet Domain");

var whoisQuery = new WhoisQueryTcp();
var userInput = new UserInputFromCommandLineArgument();

if (userInput.Domains.Length > 0)
{
    foreach (var internetDomain in userInput.Domains)
    {
        Console.WriteLine($"###[ {internetDomain} ]###".PadRight(80, '#'));
        Console.WriteLine(whoisQuery.Request(internetDomain) ?? $"TLD indisponível para consulta: {internetDomain.Tld}");
    }

    Console.WriteLine(string.Empty.PadRight(80, '#'));
}
else
{
    Console.WriteLine("Informe um ou mais domínios de internet como argumento.");
}
