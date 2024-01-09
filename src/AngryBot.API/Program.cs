using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AngryBot.Application;
using AngryBot.Infrastructure;
using AngryBot.Persistence;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(x =>
    {
        x.AddJsonFile("appsettings.json", false);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton(
            new DiscordSocketClient(
                new DiscordSocketConfig()
                {
                    GatewayIntents = GatewayIntents.All
                }
            )
        );        
        services.AddSingleton<InteractionService>();
        services.AddSingleton<CommandService>();
        services.AddApplication(context.Configuration);
        services.AddInfrastructure();
        services.AddDatabase(context.Configuration);        
    })
    .Build();

await host.RunAsync();