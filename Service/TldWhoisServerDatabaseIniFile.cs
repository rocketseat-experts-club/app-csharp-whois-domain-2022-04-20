using System.Text.RegularExpressions;
using WhoisDomain.Model;

namespace WhoisDomain.Service;

public class TldWhoisServerDatabaseIniFile : ITldWhoisServerDatabase
{
    private const string IniFileName = "tld-whois-servers.ini";
    private readonly string _iniFilePath = Path.Combine(Environment.CurrentDirectory, IniFileName);
    private readonly IReadOnlyList<TldWhoisServerModel> _database;

    public TldWhoisServerDatabaseIniFile(ITldWhoisServerList tldWhoisServerList)
    {
        _database = LoadFromFile(_iniFilePath).ToArray();
        if (_database.Count != 0) return;
        
        _database = tldWhoisServerList.List;
        WriteToFile(_iniFilePath, _database);
    }

    public TldWhoisServerModel? GetByTld(string tld)
    {
        return _database.FirstOrDefault(x => x.Tld == tld);
    }

    private static IEnumerable<TldWhoisServerModel> LoadFromFile(string iniFilePath)
    {
        if (!File.Exists(iniFilePath)) yield break;
        
        using var stream = File.OpenText(iniFilePath);
        string? line;
        while ((line = stream.ReadLine()) != null)
        {
            var regexFields = new Regex(@"^([^=]+)=([^:]+)(?::(\d+)|())");
            var match = regexFields.Match(line);
            if (match.Success &&
                ushort.TryParse(
                    !string.IsNullOrWhiteSpace(match.Groups[3].Value)
                        ? match.Groups[3].Value
                        : TldWhoisServerModel.DefaultPort.ToString(),
                    out var port)
               )
            {
                yield return new TldWhoisServerModel
                {
                    Tld = match.Groups[1].Value,
                    Server = match.Groups[2].Value,
                    Port = port,
                };
            }
        }
    }

    private static void WriteToFile(string iniFilePath, IEnumerable<TldWhoisServerModel> database)
    {
        File.WriteAllLines(iniFilePath, database.Select(entry => 
            $"{entry.Tld}={entry.Server}:{entry.Port}"));
    }
}