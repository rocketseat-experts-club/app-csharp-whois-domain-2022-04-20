using System.Net.Sockets;

Console.WriteLine("Whois Internet Domain");

var tldWhoisServerDatabase = new Dictionary<string, string>()
{
    { ".com", "whois.verisign-grs.com" },
    { ".net", "whois.verisign-grs.com" },
    { ".org", "whois.pir.org" },
    { ".com.br", "whois.registro.br" },
    { ".net.br", "whois.registro.br" },
    { ".org.br", "whois.registro.br" }
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

        if (tldWhoisServerDatabase.ContainsKey(tld))
        {
            const ushort whoisPort = 43;
            var tldWhoisServer = tldWhoisServerDatabase[tld];

            using var stream = new TcpClient(tldWhoisServer, whoisPort).GetStream();
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
