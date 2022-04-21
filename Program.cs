using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WhoisDomain.Application;
using WhoisDomain.Service;

using var host = Host
    .CreateDefaultBuilder()
    .ConfigureServices((_, services) => services
        .AddTransient<IApplication, Whois>()
        .AddTransient<ITldWhoisServerDatabase, TldWhoisServerDatabaseHardCoded>()
        .AddTransient<IUserInput, UserInputFromCommandLineArgument>()
        .AddTransient<IWhoisQuery, WhoisQueryTcp>()
    ).Build();

var application = host.Services.GetRequiredService<IApplication>();
application.Run();
