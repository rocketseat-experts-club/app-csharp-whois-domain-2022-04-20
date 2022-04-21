using WhoisDomain.Application;
using WhoisDomain.Service;

ITldWhoisServerDatabase tldWhoisServerDatabase = new TldWhoisServerDatabaseHardCoded();
IWhoisQuery whoisQuery = new WhoisQueryTcp(tldWhoisServerDatabase);
IUserInput userInput = new UserInputFromCommandLineArgument();
IApplication application = new Whois(whoisQuery, userInput);

application.Run();
