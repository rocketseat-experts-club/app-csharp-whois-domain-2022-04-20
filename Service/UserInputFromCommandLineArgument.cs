using WhoisDomain.Model;

namespace WhoisDomain.Service;

public class UserInputFromCommandLineArgument
{
    public InternetDomainModel[] Domains { get; }

    public UserInputFromCommandLineArgument()
    {
        Domains = Environment
            .GetCommandLineArgs()
            .Skip(1)
            .Select(argument => new InternetDomainModel(argument))
            .ToArray();
    }
}