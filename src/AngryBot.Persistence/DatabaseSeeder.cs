using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AngryBot.Domain.Mappers;
using Microsoft.EntityFrameworkCore;

namespace AngryBot.Persistence;

internal sealed class DatabaseSeeder : IHostedService
{
    private readonly DiscordSocketClient _client;
    private readonly IConfiguration _configuration;
    private readonly ILogger<DiscordSocketClient> _logger;
    private readonly AngryBotDbContext _dbContext;

    public DatabaseSeeder(DiscordSocketClient client, IConfiguration configuration, ILogger<DiscordSocketClient> logger, AngryBotDbContext dbContext)
    {
        _client = client;
        _configuration = configuration;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _dbContext.Database.MigrateAsync(cancellationToken);

        if (!_dbContext.Users.Any())
        {
            var users = _client.Guilds.SelectMany(x => x.Users).AsModel();
            await _dbContext.Users.AddRangeAsync(users, cancellationToken);
        }

        if (!_dbContext.Guilds.Any())
        {
            var guilds = _client.Guilds.AsModel();
            await _dbContext.Guilds.AddRangeAsync(guilds, cancellationToken);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Database seeded!");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
