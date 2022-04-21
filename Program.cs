using System.Net.Sockets;
using WhoisDomain.Model;
using WhoisDomain.Service;

Console.WriteLine("Whois Internet Domain");

var tldWhoisServerDatabase = new TldWhoisServerDatabaseHardCoded();

var internetDomains = Environment
    .GetCommandLineArgs()
    .Skip(1)
    .Select(argument => new InternetDomainModel(argument))
    .ToArray();

if (internetDomains.Length > 0)
{
    foreach (var internetDomain in internetDomains)
    {
        Console.WriteLine($"###[ {internetDomain} ]###".PadRight(80, '#'));

        var tldWhois = tldWhoisServerDatabase.GetByTld(internetDomain.Tld);
        if (tldWhois != null &&
            !string.IsNullOrWhiteSpace(tldWhois.Server) &&
            tldWhois.Port > 0)
        {
            using var stream = new TcpClient(tldWhois.Server,tldWhois.Port).GetStream();
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
            Console.WriteLine($"TLD indisponível para consulta: {internetDomain.Tld}");
        }
    }

    Console.WriteLine(string.Empty.PadRight(80, '#'));
}
else
{
    Console.WriteLine("Informe um ou mais domínios de internet como argumento.");
}
