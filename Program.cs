using System.Net.Sockets;
using WhoisDomain.Model;

Console.WriteLine("Whois Internet Domain");

var tldWhoisServerDatabase = new TldWhoisServerModel[]
{
    new () { Tld = ".com", Server = "whois.verisign-grs.com" },
    new () { Tld = ".net", Server = "whois.verisign-grs.com" },
    new () { Tld = ".org", Server = "whois.pir.org" },
    new () { Tld = ".com.br", Server = "whois.registro.br" },
    new () { Tld = ".net.br", Server = "whois.registro.br" },
    new () { Tld = ".org.br", Server = "whois.registro.br" },
    new () { Tld = ".dev", Server = "whois.nic.google" }
};

var internetDomains = Environment
    .GetCommandLineArgs()
    .Skip(1)
    .ToArray();

if (internetDomains.Length > 0)
{
    foreach (var internetDomain in internetDomains)
    {
        var dot = $"{internetDomain}.".IndexOf(".", StringComparison.Ordinal);
        var host = internetDomain[..dot].ToLower();
        var tld = internetDomain[dot..].ToLower();

        Console.WriteLine($"###[ {host}{tld} ]###".PadRight(80, '#'));

        var tldWhois = tldWhoisServerDatabase.FirstOrDefault(x => x.Tld == tld);
        if (tldWhois != null)
        {
            using var stream = new TcpClient(tldWhois.Server!,tldWhois.Port).GetStream();
            using var buffered = new BufferedStream(stream);

            var writer = new StreamWriter(buffered);
            writer.WriteLine(internetDomain);
            writer.Flush();

            var reader = new StreamReader(buffered);
            string? response;
            while ((response = reader.ReadLine()) != null)
            {
                Console.WriteLine(response);
            }
        }
        else
        {
            Console.WriteLine($"TLD indisponível para consulta: {tld}");
        }
    }

    Console.WriteLine(string.Empty.PadRight(80, '#'));
}
else
{
    Console.WriteLine("Informe um ou mais domínios de internet como argumento.");
}
